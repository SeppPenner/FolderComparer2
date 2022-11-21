// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainView.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The main form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FolderComparer2.Forms;

/// <summary>
/// The main form.
/// </summary>
public partial class MainView : Form
{
    /// <summary>
    /// The language manager.
    /// </summary>
    private readonly ILanguageManager languageManager = new LanguageManager();

    /// <summary>
    /// The timer.
    /// </summary>
    private readonly Timer timer = new();

    /// <summary>
    /// The first background worker.
    /// </summary>
    private readonly BackgroundWorker worker1 = new();

    /// <summary>
    /// The second background worker.
    /// </summary>
    private readonly BackgroundWorker worker2 = new();

    /// <summary>
    /// The first compare object.
    /// </summary>
    private CompareObject compareObject1 = new(1);

    /// <summary>
    /// The second compare object.
    /// </summary>
    private CompareObject compareObject2 = new(2);

    /// <summary>
    /// An array holding all the finished tasks information.
    /// </summary>
    private bool[] finished = new bool[2];

    /// <summary>
    /// Initializes a new instance of the <see cref="MainView"/> class.
    /// </summary>
    public MainView()
    {
        this.Initialize();
        this.InitializeLanguageManager();
        this.LoadLanguagesToCombo();
    }

    /// <summary>
    /// Raises the file count.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <param name="compare">The compare object.</param>
    private static void RaiseFileCount(string directory, CompareObject compare)
    {
        compare.FileCount += Directory.EnumerateFiles(directory).Count();
    }

    /// <summary>
    /// Raises the sub folder count.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <param name="compare">The compare object.</param>
    private static void RaiseSubFolderCount(string directory, CompareObject compare)
    {
        compare.SubFolderCount += Directory.EnumerateDirectories(directory).Count();
    }

    /// <summary>
    /// Raises the byte count.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <param name="compare">The compare object.</param>
    private static void RaiseBytes(string file, CompareObject compare)
    {
        var fileInfo = new FileInfo(file);
        compare.Size += fileInfo.Length;
        compare.ByteSize += fileInfo.Length;
    }

    /// <summary>
    /// Formats the <see cref="double"/> value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The formatted value as <see cref="string"/>.</returns>
    private static string FormatDouble(double value)
    {
        return $"{value:0.00}";
    }

    /// <summary>
    /// Initializes the data.
    /// </summary>
    private void Initialize()
    {
        this.InitializeComponent();
        this.InitializeCaption();
        this.InitializeBackgroundWorkers();
        this.InitializeTimer();
    }

    /// <summary>
    /// Initializes the caption.
    /// </summary>
    private void InitializeCaption()
    {
        this.Text = Application.ProductName + @" " + Application.ProductVersion;
    }

    /// <summary>
    /// Initializes the background workers.
    /// </summary>
    private void InitializeBackgroundWorkers()
    {
        this.worker1.DoWork += this.SearchDirectoryBackground!;
        this.worker2.DoWork += this.SearchDirectoryBackground!;
        this.worker1.RunWorkerCompleted += this.EvaluateResult!;
        this.worker2.RunWorkerCompleted += this.EvaluateResult!;
    }

    /// <summary>
    /// Initializes the timer.
    /// </summary>
    private void InitializeTimer()
    {
        this.timer.Interval = 500;
        this.timer.Elapsed += this.EvaluateColoringTimer!;
        this.timer.Start();
    }

    /// <summary>
    /// Evaluates the coloring in the timer.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void EvaluateColoringTimer(object sender, ElapsedEventArgs e)
    {
        if (!this.finished[0] || !this.finished[1])
        {
            return;
        }

        this.EvaluateColoring();
        this.UiThreadInvoke(() => { this.LockGui(false); });
    }

    /// <summary>
    /// Evaluates the coloring.
    /// </summary>
    private void EvaluateColoring()
    {
        this.EvaluateByteSizeColoring();
        this.EvaluateFileColoring();
        this.EvaluateSubFolderColoring();
    }

    /// <summary>
    /// Evaluates the byte size coloring.
    /// </summary>
    private void EvaluateByteSizeColoring()
    {
        if (Math.Abs(this.compareObject1.ByteSize - this.compareObject2.ByteSize) < 0.00001)
        {
            this.labelFolder1Size.BackColor = Color.Green;
            this.labelFolder2Size.BackColor = Color.Green;
        }
        else
        {
            this.labelFolder1Size.BackColor = Color.Red;
            this.labelFolder2Size.BackColor = Color.Red;
        }
    }

    /// <summary>
    /// Evaluates the file coloring.
    /// </summary>
    private void EvaluateFileColoring()
    {
        if (Math.Abs(this.compareObject1.FileCount - this.compareObject2.FileCount) < 0.00001)
        {
            this.labelFolder1Files.BackColor = Color.Green;
            this.labelFolder2Files.BackColor = Color.Green;
        }
        else
        {
            this.labelFolder1Files.BackColor = Color.Red;
            this.labelFolder2Files.BackColor = Color.Red;
        }
    }

    /// <summary>
    /// Evaluates the sub folder coloring.
    /// </summary>
    private void EvaluateSubFolderColoring()
    {
        if (Math.Abs(this.compareObject1.SubFolderCount - this.compareObject2.SubFolderCount) < 0.00001)
        {
            this.labelFolder1Subfolders.BackColor = Color.Green;
            this.labelFolder2Subfolders.BackColor = Color.Green;
        }
        else
        {
            this.labelFolder1Subfolders.BackColor = Color.Red;
            this.labelFolder2Subfolders.BackColor = Color.Red;
        }
    }

    /// <summary>
    /// Searches the directory in the background.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void SearchDirectoryBackground(object sender, DoWorkEventArgs e)
    {
        if (e?.Argument == null)
        {
            return;
        }

        var select = (int)e.Argument;

        switch (select)
        {
            case 1:
                var compareObject1Local = new CompareObject(1);
                var directory1 = string.Empty;

                this.UiThreadInvoke(() =>
                {
                    compareObject1Local = this.compareObject1;
                    directory1 = this.richTextBoxFolder1.Text;
                });

                this.SearchDirectory(directory1, compareObject1Local);
                e.Result = compareObject1Local;
                this.UiThreadInvoke(() => { this.finished[0] = true; });
                break;
            case 2:
                var compareObject2Local = new CompareObject(2);
                var directory2 = string.Empty;

                this.UiThreadInvoke(() =>
                {
                    compareObject2Local = this.compareObject2;
                    directory2 = this.richTextBoxFolder2.Text;
                });

                this.SearchDirectory(directory2, compareObject2Local);
                e.Result = compareObject2Local;
                this.UiThreadInvoke(() => { this.finished[1] = true; });
                break;
        }
    }

    /// <summary>
    /// Handles the button click to choose the first folder.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void ChooseFolder1Click(object sender, EventArgs e)
    {
        var dialog = new FolderBrowserDialog();
        var result = dialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            this.richTextBoxFolder1.Text = dialog.SelectedPath;
        }
    }

    /// <summary>
    /// Handles the button click to choose the second folder.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void ChooseFolder2Click(object sender, EventArgs e)
    {
        var dialog = new FolderBrowserDialog();
        var result = dialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            this.richTextBoxFolder2.Text = dialog.SelectedPath;
        }
    }

    /// <summary>
    /// Handles the button click to compare the data.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void CompareClick(object sender, EventArgs e)
    {
        if (!this.CheckFoldersSelected())
        {
            return;
        }

        this.LockGui(true);
        this.Reset();
        this.worker1.RunWorkerAsync(1);
        this.worker2.RunWorkerAsync(2);
    }

    /// <summary>
    /// Checks whether both folders are selected.
    /// </summary>
    /// <returns>A value indicating whether both folders are selected or not.</returns>
    private bool CheckFoldersSelected()
    {
        if (string.IsNullOrWhiteSpace(this.richTextBoxFolder1.Text))
        {
            MessageBox.Show(
                this.languageManager.GetCurrentLanguage().GetWord("Folder1NotSelectedText"),
                this.languageManager.GetCurrentLanguage().GetWord("Folder1NotSelectedCaption"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return false;
        }

        if (!string.IsNullOrWhiteSpace(this.richTextBoxFolder2.Text))
        {
            return true;
        }

        MessageBox.Show(
            this.languageManager.GetCurrentLanguage().GetWord("Folder2NotSelectedText"),
            this.languageManager.GetCurrentLanguage().GetWord("Folder2NotSelectedCaption"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        return false;
    }

    /// <summary>
    /// Resets the data.
    /// </summary>
    private void Reset()
    {
        this.ResetCaptions();
        this.ResetCompareObjects();
        this.ResetFinished();
    }

    /// <summary>
    /// Locks or unlocks the GUI.
    /// </summary>
    /// <param name="locked">A value indicating whether the GUI should be locked or not.</param>
    private void LockGui(bool locked)
    {
        if (locked)
        {
            this.LockButtons();
        }
        else
        {
            this.FreeButtons();
        }
    }

    /// <summary>
    /// Locks the buttons.
    /// </summary>
    private void LockButtons()
    {
        this.buttonChooseFolder1.Enabled = false;
        this.buttonChooseFolder2.Enabled = false;
        this.buttonCompare.Enabled = false;
    }

    /// <summary>
    /// Frees the buttons.
    /// </summary>
    private void FreeButtons()
    {
        this.buttonChooseFolder1.Enabled = true;
        this.buttonChooseFolder2.Enabled = true;
        this.buttonCompare.Enabled = true;
    }

    /// <summary>
    /// Resets the captions.
    /// </summary>
    private void ResetCaptions()
    {
        this.labelFolder1Size.Text = string.Empty;
        this.labelFolder2Size.Text = string.Empty;
        this.labelFolder1Files.Text = string.Empty;
        this.labelFolder2Files.Text = string.Empty;
        this.labelFolder1Subfolders.Text = string.Empty;
        this.labelFolder2Subfolders.Text = string.Empty;
    }

    /// <summary>
    /// Resets the compare objects.
    /// </summary>
    private void ResetCompareObjects()
    {
        this.compareObject1 = new CompareObject(1);
        this.compareObject2 = new CompareObject(2);
    }

    /// <summary>
    /// Resets the finished array.
    /// </summary>
    private void ResetFinished()
    {
        this.finished = new bool[2];
    }

    /// <summary>
    /// Searches the directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <param name="compare">The compare object.</param>
    private void SearchDirectory(string directory, CompareObject compare)
    {
        try
        {
            RaiseFileCount(directory, compare);
            RaiseSubFolderCount(directory, compare);

            foreach (var file in Directory.EnumerateFiles(directory))
            {
                RaiseBytes(file, compare);
            }

            foreach (var dir in Directory.EnumerateDirectories(directory))
            {
                this.SearchDirectory(dir, compare);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Evaluates the result.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void EvaluateResult(object sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Result is CompareObject compareObject)
        {
            this.Evaluate(compareObject);
        }
    }

    /// <summary>
    /// Evaluates the compare object.
    /// </summary>
    /// <param name="compare">The compare object.</param>
    private void Evaluate(CompareObject compare)
    {
        IUnitConverter converter = new UnitConverter();
        converter.EvaluateByteSize(compare);
        this.SetGuiValues(compare);
    }

    /// <summary>
    /// Sets the GUI values.
    /// </summary>
    /// <param name="compare">The compare object.</param>
    private void SetGuiValues(CompareObject compare)
    {
        this.SetByteSize(compare);
        this.SetFileCount(compare);
        this.SetSubFolderCount(compare);
    }

    /// <summary>
    /// Sets the byte size.
    /// </summary>
    /// <param name="compare">The compare object.</param>
    private void SetByteSize(CompareObject compare)
    {
        switch (compare.Number)
        {
            case 1:
                this.labelFolder1Size.Text = FormatDouble(compare.Size) + @" " + compare.Unit;
                break;
            default:
                this.labelFolder2Size.Text = FormatDouble(compare.Size) + @" " + compare.Unit;
                break;
        }
    }

    /// <summary>
    /// Sets the file count.
    /// </summary>
    /// <param name="compare">The compare object.</param>
    private void SetFileCount(CompareObject compare)
    {
        switch (compare.Number)
        {
            case 1:
                this.labelFolder1Files.Text = compare.FileCount.ToString(CultureInfo.CurrentCulture);
                break;
            default:
                this.labelFolder2Files.Text = compare.FileCount.ToString(CultureInfo.CurrentCulture);
                break;
        }
    }

    /// <summary>
    /// Sets the sub folder count.
    /// </summary>
    /// <param name="compare">The compare object.</param>
    private void SetSubFolderCount(CompareObject compare)
    {
        switch (compare.Number)
        {
            case 1:
                this.labelFolder1Subfolders.Text = compare.SubFolderCount.ToString(CultureInfo.CurrentCulture);
                break;
            default:
                this.labelFolder2Subfolders.Text = compare.SubFolderCount.ToString(CultureInfo.CurrentCulture);
                break;
        }
    }

    /// <summary>
    /// Initializes the language manager.
    /// </summary>
    private void InitializeLanguageManager()
    {
        this.languageManager.SetCurrentLanguage("de-DE");
        this.languageManager.OnLanguageChanged += this.OnLanguageChanged!;
    }

    /// <summary>
    /// Loads the languages to the combo box.
    /// </summary>
    private void LoadLanguagesToCombo()
    {
        foreach (var lang in this.languageManager.GetLanguages())
        {
            this.comboBoxLanguage.Items.Add(lang.Name);
        }

        this.comboBoxLanguage.SelectedIndex = 0;
    }

    /// <summary>
    /// Handles the selected index changed event for the languages combo box.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void ComboBoxLanguageSelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedItem = this.comboBoxLanguage.SelectedItem.ToString();

        if (string.IsNullOrWhiteSpace(selectedItem))
        {
            return;
        }

        this.languageManager.SetCurrentLanguageFromName(selectedItem);
    }

    /// <summary>
    /// Handles the language changed event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void OnLanguageChanged(object sender, EventArgs e)
    {
        this.buttonChooseFolder1.Text = this.languageManager.GetCurrentLanguage().GetWord("SelectFolder1");
        this.buttonChooseFolder2.Text = this.languageManager.GetCurrentLanguage().GetWord("SelectFolder2");
        this.buttonCompare.Text = this.languageManager.GetCurrentLanguage().GetWord("CompareFolders");
        this.labelFolder1Caption.Text = this.languageManager.GetCurrentLanguage().GetWord("Folder1");
        this.labelFolder2Caption.Text = this.languageManager.GetCurrentLanguage().GetWord("Folder2");
        this.labelSize.Text = this.languageManager.GetCurrentLanguage().GetWord("FileSize");
        this.labelFiles.Text = this.languageManager.GetCurrentLanguage().GetWord("FileCount");
        this.labelSubfolders.Text = this.languageManager.GetCurrentLanguage().GetWord("SubFolderCount");
    }
}
