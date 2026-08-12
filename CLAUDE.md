# Project rules for Claude

## What this is

FolderComparer2 is a Windows Forms application that compares two folders and shows, for each of
them, the total size, the number of files and the number of subfolders. The three result pairs are
colored green when the two folders match and red when they do not. The application is localized in
German and English, the language is switched at runtime with a combo box.

This is an **application**, not a library. There is no NuGet package, no `GeneratePackageOnBuild`
and no push script. The `README.md` calls it "a software/library" and shows the folder walking code
as a "Basic usage" sample, but nothing here is published for consumers, the sample is documentation
only.

One solution `src/FolderComparer2.sln` with exactly one project:

- `src/FolderComparer2/FolderComparer2.csproj`, `OutputType` `WinExe`, `UseWindowsForms`, the whole
  application.

Layout inside `src/FolderComparer2`:

- `Program.cs`: `Main` with `[STAThread]`, three lines, runs `MainView`.
- `Forms/MainView.cs` plus `MainView.Designer.cs` and `MainView.resx`: the only form and all of the
  logic. It owns the language manager, the two background workers, the timer and the two compare
  objects. Keep new logic in the shape that is there, one small private method per step
  (`Initialize`, `InitializeCaption`, `ResetCaptions`, `SetByteSize` and so on).
- `Implementation/UnitConverter.cs` plus `Interfaces/IUnitConverter.cs`: converts a raw byte count
  into the largest fitting unit.
- `Enumerations/Unit.cs`: the units from `B` to `Eb`.
- `Models/CompareObject.cs`: the payload, one instance per folder, `Number` tells them apart.
- `UiThreadInvoke/UiThreadInvokeClass.cs`: the `UiThreadInvoke` extension method on `Control`, the
  only way background code is allowed to touch the form.
- `GlobalUsings.cs`: all usings of the project, including the alias `Timer`.
- `languages/de-DE.xml` and `languages/en-US.xml`: 13 keys each, copied to the output directory with
  `CopyToOutputDirectory=Always`.
- `License.txt` and `FolderComparer2.ico`: shipped next to the executable, the icon is also the
  `ApplicationIcon`.

Repository root: `README.md` (the only user documentation), `Changelog.md`, `License.txt` (MIT),
`.gitattributes`, `.gitignore`, `Screenshot_1.PNG` and `Screenshot_2.PNG` (English and German
window, linked from the Readme) and the `Setup` folder. There is no `Updating.md`, no `HowToUse.md`
and no `.github` folder.

`Setup` holds `build-setup-files.bat` (cleans, publishes, deletes the `*.pdb`),
`FolderComparer2-Setup.iss` (Inno Setup 6) and the built `FolderComparer2-Setup.exe`, which is
tracked.

## Build

```powershell
dotnet build src/FolderComparer2.sln -c Release
```

```powershell
dotnet test src/FolderComparer2.sln
```

- Single target framework `net10.0-windows` in both projects, no multi-targeting.
  `RuntimeIdentifiers` is `win-x64`.
- All build properties live directly in the `.csproj` files. There is **no**
  `Directory.Build.props` in this repository.
- `TreatWarningsAsErrors` is enabled, so every warning breaks the build, NuGet warnings (`NU****`)
  from restore included. A clean build reports zero warnings, keep it that way.
- `NU1803` (HTTP source usage during restore) is the one warning suppressed via `NoWarn`. Fix
  warnings instead of extending that list. `NuGetAudit` and `NuGetAuditMode=all` are on, so a
  vulnerable transitive package fails the build too.
- Versions come from GitVersion.MsBuild out of the git tags, for example `2.0.8-1` for the first
  commit after tag `2.0.7`. Never edit a version property or an assembly version by hand.
- Restore needs nuget.org. If a private feed is configured globally on the machine and answers 404
  for public packages, restore fails with `NU1301`. Then build with an explicit source:
  `dotnet build src/FolderComparer2.sln --source https://api.nuget.org/v3/index.json`.
- Tests are MSTest in `src/FolderComparer2.Tests`, added in version 2.0.8.0, with the same package
  set as the sibling repositories: `Microsoft.NET.Test.Sdk`, `MSTest.TestAdapter`,
  `MSTest.TestFramework`, `coverlet.collector` and `GitVersion.MsBuild`. They cover `UnitConverter`
  and the two language files, they need no network and no fixture outside the repository. The form
  itself is not covered, it is Windows Forms. Never claim a test run happened without running it.
- Beyond the tests, a behaviour change is verified by starting the published executable, the window
  title carries the GitVersion informational version.

## Code conventions

Follow the surrounding code, it is consistent throughout every file:

- File header comment block with `<copyright file="..." company="Hämmer Electronics">` and a
  `<summary>`, then the file-scoped namespace.
- XML doc comments on every type and every member, private members included, no exceptions.
  Implementations of an interface member additionally carry `<inheritdoc cref="..."/>` and
  `<seealso cref="..."/>` pointing at that interface.
- `Nullable`, `ImplicitUsings` and `LangVersion latest` are enabled.
- New `using` directives go into the `GlobalUsings.cs` of the respective project, inside the
  existing `#pragma warning disable IDE0065` block, never at the top of a file. The editorconfig
  requires usings inside the namespace (`csharp_using_directive_placement=inside_namespace:warning`),
  which global usings cannot satisfy, that is what the pragma is for. Do not add other pragmas. The
  comment text in that block is German because Visual Studio generated it, leave it alone.
- Fields, properties, methods and events are always accessed with `this.` qualification
  (`dotnet_style_qualification_for_*` at severity `warning`).
- `src/.editorconfig` also enforces braces everywhere, no multiple blank lines, four spaces, CRLF,
  UTF-8, file scoped namespaces, `System` usings sorted first and `IDE0005` as warning. Analyzer
  warnings are fixed, not silenced.
- `Forms/MainView.Designer.cs` is generated by the Windows Forms designer. It does not follow the
  conventions above and is not supposed to, do not reformat it by hand.

## Known quirks

Do not silently "clean up" these, they are existing behaviour:

- **`Size` shrinks, `ByteSize` does not.** `RaiseBytes` adds every file length to both properties.
  `UnitConverter.EvaluateByteSize` then divides `Size` down through the unit chain until it is
  below 1024 and leaves `ByteSize` at the raw byte count. That is on purpose: the labels show the
  converted `Size` with its `Unit`, while the coloring compares `ByteSize`, so the green light means
  the two folders match to the byte and not just after rounding.
- **`Unit.KB` is spelled differently from the rest.** All other members are `Mb`, `Gb`, `Tb`, `Pb`
  and `Eb`, only the kilobyte member is `KB`. The member name is printed next to the size, so
  renaming it changes what the user sees.
- **The unit chain stops at the first unit that fits.** Every `Evaluate*` method returns `false`
  when the value is below 1024, and `EvaluateByteSize` returns on the first `false`. The methods are
  six copies of the same four lines, that repetition is the shape of the file.
- **Doubles compared with `Math.Abs(a - b) < 0.00001`.** File counts, subfolder counts and byte
  sizes are all `double` in `CompareObject`, so the three coloring methods use an epsilon comparison
  instead of `==`.
- **The timer never stops.** `InitializeTimer` starts a 500 ms `System.Timers.Timer` in the
  constructor and nothing ever stops it. Once both background workers have set their `finished`
  flag, every single tick re-applies the coloring and unlocks the GUI again, until the program ends.
- **`Timer` is an alias.** `GlobalUsings.cs` has `global using Timer = System.Timers.Timer;` because
  `System.Windows.Forms.Timer` would otherwise win through `ImplicitUsings`. `Elapsed` therefore
  runs on a thread pool thread, which is why `EvaluateColoringTimer` does all of its work inside
  `UiThreadInvoke`.
- **Two workers, one pair of handlers.** `worker1` and `worker2` share `SearchDirectoryBackground`
  and `EvaluateResult`. They are told apart by the `int` argument passed to `RunWorkerAsync` and by
  `CompareObject.Number`, the `switch` on that number is how the results reach the right labels.
- **The error dialog comes from the worker thread.** `SearchDirectory` runs inside the background
  worker and shows its `MessageBox` directly from there, so the dialog has no owner window. It works
  and it keeps the recursion simple, it is left that way on purpose.
- **`GetWord` returns `null` for an unknown key** and does not fall back to the other language, so
  every new key has to be added to `de-DE.xml` **and** `en-US.xml`. The language files are UTF-8
  without BOM, use tabs and CRLF, keep that when editing them.
- **The installer is tracked although `.gitignore` excludes `*.exe`.**
  `Setup/FolderComparer2-Setup.exe` needs `git add -f`. Since 2.0.8.0 the publish is self contained,
  so the installer is around 35 MB and every release adds that to the history for good.
- **Two copies of the icon and the license.** `src/FolderComparer2.ico` and
  `src/FolderComparer2/FolderComparer2.ico` are byte identical, as are the root `License.txt` and
  `src/FolderComparer2/License.txt`. The `.csproj` and the `.iss` both use the copies inside the
  project folder.
- **`Setup/FolderComparer2-Setup.iss` is UTF-8 with BOM.** Inno Setup 6 only reads a script as UTF-8
  when the BOM is there, otherwise it falls back to the system code page and turns
  `Hämmer Electronics` into `HÃ¤mmer Electronics` in the installer. Keep the BOM and keep CRLF.
- **AppVeyor badge without CI in the repository.** `README.md` links an AppVeyor build that is
  configured outside of this repository. There is no pipeline file here.
- **`src/FolderComparer2.sln.DotSettings`** is tracked and holds nothing but a ReSharper user
  dictionary (`exabyte`, `petabyte`, `terrabyte`, `H_00E4mmer`). Leave it alone.

## Releasing

1. Make the change.
2. Add an entry at the top of `Changelog.md` in the existing format:
   `* **Version 2.0.8.0 (2026-08-12)** : Short description.`
3. Set `MyAppVersion` in `Setup/FolderComparer2-Setup.iss` to the same four part version. The file
   is UTF-8 with BOM and CRLF, keep both.
4. Commit that.
5. Tag the commit with the plain version number, no `v` prefix (`2.0.7`, `2.0.6`, ...). The existing
   tags are lightweight tags, create new ones the same way.
6. **Then** build the installer, in this order: `Setup/build-setup-files.bat`, afterwards
   `ISCC.exe Setup/FolderComparer2-Setup.iss`. The tag has to exist first, otherwise GitVersion
   burns a prerelease version such as `2.0.8-1+Branch.master.Sha...` into the shipped executable.
7. `git add -f Setup/FolderComparer2-Setup.exe` and commit it as `Updated setup.`.
8. Push the commits and the tag.

The version in the `Changelog.md` has four parts (`2.0.8.0`), the tag has three (`2.0.8`).
GitVersion turns the tag into the assembly version. There is no package to push, so the release ends
with the push.

## Git

- **Never amend a commit.** No `git commit --amend`, not for a typo in the message, not to add a
  forgotten file, not even when the commit is still local. Write a follow-up commit instead. The
  release versions come from tags on exact commits, an amended commit leaves its tag pointing at a
  commit that no longer exists in the branch.

## Writing style

- Commit messages are written **in English only**: short, precise subject line, explanatory body
  when needed.
- Code comments and comments in project files such as `.csproj` are **always English**, regardless
  of the language used in the conversation.
- **No em dashes or en dashes** (`—`, `–`), neither in prose, commit messages, code comments nor
  documentation. Use a regular hyphen, comma, colon, parentheses or a separate sentence.
- German texts (documentation, chat replies) always use real umlauts and ß, never ASCII
  transliterations such as `ae`, `oe`, `ue` or `ss`. Identifiers, file names and configuration keys
  stay unchanged where umlauts are technically undesirable.
</content>
</invoke>
