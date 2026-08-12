// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LanguageFileTests.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A test class to test the language files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FolderComparer2.Tests;

/// <summary>
/// A test class to test the language files. <see cref="ILanguage.GetWord"/> returns <c>null</c> for an unknown key and
/// does not fall back to another language, so a key that is missing in one of the files is an empty label at runtime.
/// </summary>
[TestClass]
public sealed class LanguageFileTests
{
    /// <summary>
    /// The file name of the German language file.
    /// </summary>
    private const string GermanFileName = "de-DE.xml";

    /// <summary>
    /// The file name of the English language file.
    /// </summary>
    private const string EnglishFileName = "en-US.xml";

    /// <summary>
    /// All keys the form asks for.
    /// </summary>
    private static readonly string[] KeysUsedByTheForm =
    [
        "SelectFolder1",
        "SelectFolder2",
        "CompareFolders",
        "FileSize",
        "FileCount",
        "SubFolderCount",
        "Folder1",
        "Folder2",
        "Folder1NotSelectedCaption",
        "Folder1NotSelectedText",
        "Folder2NotSelectedCaption",
        "Folder2NotSelectedText",
        "ErrorTitle"
    ];

    /// <summary>
    /// Gets the languages folder next to the test assembly, the same place the application reads its files from.
    /// </summary>
    private static string LanguageFolder
        => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "languages");

    /// <summary>
    /// Tests that both language files are copied next to the assembly that uses them.
    /// </summary>
    [TestMethod]
    public void BothLanguageFilesAreCopiedToTheOutputDirectory()
    {
        Assert.IsTrue(File.Exists(Path.Combine(LanguageFolder, GermanFileName)));
        Assert.IsTrue(File.Exists(Path.Combine(LanguageFolder, EnglishFileName)));
    }

    /// <summary>
    /// Tests that both language files declare the identifier and the name the application shows in the combo box.
    /// </summary>
    [TestMethod]
    public void BothLanguageFilesDeclareTheirIdentifierAndName()
    {
        var german = LoadLanguage(GermanFileName);
        var english = LoadLanguage(EnglishFileName);
        Assert.AreEqual("de-DE", german.Identifier);
        Assert.AreEqual("Deutsch", german.Name);
        Assert.AreEqual("en-US", english.Identifier);
        Assert.AreEqual("English (US)", english.Name);
    }

    /// <summary>
    /// Tests that both language files contain exactly the same keys.
    /// </summary>
    [TestMethod]
    public void BothLanguageFilesContainTheSameKeys()
    {
        var germanKeys = GetKeys(LoadLanguage(GermanFileName));
        var englishKeys = GetKeys(LoadLanguage(EnglishFileName));
        CollectionAssert.AreEqual(germanKeys, englishKeys);
    }

    /// <summary>
    /// Tests that every key the form asks for exists in both language files.
    /// </summary>
    /// <param name="fileName">The file name of the language file.</param>
    [TestMethod]
    [DataRow(GermanFileName)]
    [DataRow(EnglishFileName)]
    public void EveryKeyUsedByTheFormExists(string fileName)
    {
        var language = LoadLanguage(fileName);

        foreach (var key in KeysUsedByTheForm)
        {
            Assert.IsNotNull(language.GetWord(key), $"The key {key} is missing in {fileName}.");
        }
    }

    /// <summary>
    /// Tests that no language file carries an empty key or an empty value.
    /// </summary>
    /// <param name="fileName">The file name of the language file.</param>
    [TestMethod]
    [DataRow(GermanFileName)]
    [DataRow(EnglishFileName)]
    public void NoWordIsEmpty(string fileName)
    {
        var language = LoadLanguage(fileName);
        Assert.AreEqual(KeysUsedByTheForm.Length, language.Words.Count);

        foreach (var word in language.Words)
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(word.Key), $"An empty key was found in {fileName}.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(word.Value), $"The key {word.Key} has no value in {fileName}.");
        }
    }

    /// <summary>
    /// Tests that the language manager finds both languages under the names the combo box shows.
    /// </summary>
    [TestMethod]
    public void LanguageManagerLoadsBothLanguages()
    {
        ILanguageManager languageManager = new LanguageManager();
        var names = languageManager.GetLanguages().Select(language => language.Name).OrderBy(name => name, StringComparer.Ordinal).ToList();
        CollectionAssert.AreEqual(new List<string> { "Deutsch", "English (US)" }, names);
    }

    /// <summary>
    /// Tests that the title of the error dialog is translated in both languages.
    /// </summary>
    [TestMethod]
    public void ErrorTitleIsTranslatedInBothLanguages()
    {
        ILanguageManager languageManager = new LanguageManager();
        languageManager.SetCurrentLanguage("de-DE");
        Assert.AreEqual("Fehler", languageManager.GetCurrentLanguage().GetWord("ErrorTitle"));
        languageManager.SetCurrentLanguage("en-US");
        Assert.AreEqual("Error", languageManager.GetCurrentLanguage().GetWord("ErrorTitle"));
    }

    /// <summary>
    /// Tests that an unknown key returns <c>null</c> instead of falling back to another language.
    /// </summary>
    [TestMethod]
    public void UnknownKeyReturnsNull()
    {
        ILanguageManager languageManager = new LanguageManager();
        languageManager.SetCurrentLanguage("de-DE");
        Assert.IsNull(languageManager.GetCurrentLanguage().GetWord("ThisKeyDoesNotExist"));
    }

    /// <summary>
    /// Loads a language file from the output directory.
    /// </summary>
    /// <param name="fileName">The file name of the language file.</param>
    /// <returns>The loaded <see cref="Language"/>.</returns>
    private static Language LoadLanguage(string fileName)
    {
        IImportExport importExport = new ImportExport();
        var language = importExport.Load(Path.Combine(LanguageFolder, fileName));
        Assert.IsNotNull(language, $"The language file {fileName} could not be loaded.");
        return language;
    }

    /// <summary>
    /// Gets the sorted keys of a language.
    /// </summary>
    /// <param name="language">The language.</param>
    /// <returns>The sorted keys as <see cref="List{T}"/> of <see cref="string"/>.</returns>
    private static List<string> GetKeys(Language language)
    {
        return language.Words.Select(word => word.Key).OrderBy(key => key, StringComparer.Ordinal).ToList();
    }
}
