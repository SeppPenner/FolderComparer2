using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using FolderComparer2.Implementation;
using FolderComparer2.Interfaces;
using FolderComparer2.Models;
using FolderComparer2.UiThreadInvoke;
using Languages.Implementation;
using Languages.Interfaces;
using Timer = System.Timers.Timer;

namespace FolderComparer2.Forms
{
    public partial class MainView : Form
    {
        private readonly ILanguageManager _lm = new LanguageManager();
        private readonly Timer _timer1 = new Timer();
        private readonly BackgroundWorker _worker1 = new BackgroundWorker();
        private readonly BackgroundWorker _worker2 = new BackgroundWorker();
        private CompareObject _compareObject1 = new CompareObject(1);
        private CompareObject _compareObject2 = new CompareObject(2);
        private bool[] _finished = new bool[2];

        public MainView()
        {
            Initialize();
            InitializeLanguageManager();
            LoadLanguagesToCombo();
        }

        private void Initialize()
        {
            InitializeComponent();
            InitializeCaption();
            InitializeBackgroundWorkers();
            InitializeTimer();
        }

        private void InitializeCaption()
        {
            Text = Application.ProductName + @" " + Application.ProductVersion;
        }

        private void InitializeBackgroundWorkers()
        {
            _worker1.DoWork += SearchDirectoryBackground;
            _worker2.DoWork += SearchDirectoryBackground;
            _worker1.RunWorkerCompleted += EvaluateResult;
            _worker2.RunWorkerCompleted += EvaluateResult;
        }

        private void InitializeTimer()
        {
            _timer1.Interval = 500;
            _timer1.Elapsed += EvaluateColoringTimer;
            _timer1.Start();
        }

        private void EvaluateColoringTimer(object sender, ElapsedEventArgs e)
        {
            if (!_finished[0] || !_finished[1]) return;
            EvaluateColoring();
            this.UiThreadInvoke(() => { LockGui(false); });
        }

        private void EvaluateColoring()
        {
            EvaluateByteSizeColoring();
            EvaluateFileColoring();
            EvaluateSubfolderColoring();
        }

        private void EvaluateByteSizeColoring()
        {
            if (Math.Abs(_compareObject1.ByteSize - _compareObject2.ByteSize) < 0.00001)
            {
                labelFolder1Size.BackColor = Color.Green;
                labelFolder2Size.BackColor = Color.Green;
            }
            else
            {
                labelFolder1Size.BackColor = Color.Red;
                labelFolder2Size.BackColor = Color.Red;
            }
        }

        private void EvaluateFileColoring()
        {
            if (Math.Abs(_compareObject1.FileCount - _compareObject2.FileCount) < 0.00001)
            {
                labelFolder1Files.BackColor = Color.Green;
                labelFolder2Files.BackColor = Color.Green;
            }
            else
            {
                labelFolder1Files.BackColor = Color.Red;
                labelFolder2Files.BackColor = Color.Red;
            }
        }

        private void EvaluateSubfolderColoring()
        {
            if (Math.Abs(_compareObject1.SubFolderCount - _compareObject2.SubFolderCount) < 0.00001)
            {
                labelFolder1Subfolders.BackColor = Color.Green;
                labelFolder2Subfolders.BackColor = Color.Green;
            }
            else
            {
                labelFolder1Subfolders.BackColor = Color.Red;
                labelFolder2Subfolders.BackColor = Color.Red;
            }
        }

        private void SearchDirectoryBackground(object sender, DoWorkEventArgs e)
        {
            var select = (int) e.Argument;
            switch (select)
            {
                case 1:
                    var compareObject1 = new CompareObject(1);
                    var directory1 = "";
                    this.UiThreadInvoke(() =>
                    {
                        compareObject1 = _compareObject1;
                        directory1 = richTextBoxFolder1.Text;
                    });
                    SearchDirectory(directory1, compareObject1);
                    e.Result = compareObject1;
                    this.UiThreadInvoke(() => { _finished[0] = true; });
                    break;
                case 2:
                    var compareObject2 = new CompareObject(2);
                    var directory2 = "";
                    this.UiThreadInvoke(() =>
                    {
                        compareObject2 = _compareObject2;
                        directory2 = richTextBoxFolder2.Text;
                    });
                    SearchDirectory(directory2, compareObject2);
                    e.Result = compareObject2;
                    this.UiThreadInvoke(() => { _finished[1] = true; });
                    break;
            }
        }

        private void buttonChooseFolder1_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
                richTextBoxFolder1.Text = dialog.SelectedPath;
        }

        private void buttonChooseFolder2_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
                richTextBoxFolder2.Text = dialog.SelectedPath;
        }

        private void buttonCompare_Click(object sender, EventArgs e)
        {
            if (!CheckFoldersSelected()) return;
            LockGui(true);
            Reset();
            _worker1.RunWorkerAsync(1);
            _worker2.RunWorkerAsync(2);
        }

        private bool CheckFoldersSelected()
        {
            if (string.IsNullOrWhiteSpace(richTextBoxFolder1.Text))
            {
                MessageBox.Show(_lm.GetCurrentLanguage().GetWord("Folder1NotSelectedText"),
                    _lm.GetCurrentLanguage().GetWord("Folder1NotSelectedCaption"), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(richTextBoxFolder2.Text)) return true;
            MessageBox.Show(_lm.GetCurrentLanguage().GetWord("Folder2NotSelectedText"),
                _lm.GetCurrentLanguage().GetWord("Folder2NotSelectedCaption"), MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return false;
        }

        private void Reset()
        {
            ResetCaptions();
            ResetCompareObjects();
            ResetFinished();
        }

        private void LockGui(bool locked)
        {
            if (locked)
                LockButtons();
            else
                FreeButtons();
        }

        private void LockButtons()
        {
            buttonChooseFolder1.Enabled = false;
            buttonChooseFolder2.Enabled = false;
            buttonCompare.Enabled = false;
        }

        private void FreeButtons()
        {
            buttonChooseFolder1.Enabled = true;
            buttonChooseFolder2.Enabled = true;
            buttonCompare.Enabled = true;
        }

        private void ResetCaptions()
        {
            labelFolder1Size.Text = "";
            labelFolder2Size.Text = "";
            labelFolder1Files.Text = "";
            labelFolder2Files.Text = "";
            labelFolder1Subfolders.Text = "";
            labelFolder2Subfolders.Text = "";
        }

        private void ResetCompareObjects()
        {
            _compareObject1 = new CompareObject(1);
            _compareObject2 = new CompareObject(2);
        }

        private void ResetFinished()
        {
            _finished = new bool[2];
        }

        private void RaiseFileCount(string directory, CompareObject compare)
        {
            compare.FileCount += Directory.EnumerateFiles(directory).Count();
        }

        private void RaiseSubFolderCount(string directory, CompareObject compare)
        {
            compare.SubFolderCount += Directory.EnumerateDirectories(directory).Count();
        }

        private void RaiseBytes(string file, CompareObject compare)
        {
            var fileInfo = new FileInfo(file);
            compare.Size += fileInfo.Length;
            compare.ByteSize += fileInfo.Length;
        }

        private void SearchDirectory(string directory, CompareObject compare)
        {
            try
            {
                RaiseFileCount(directory, compare);
                RaiseSubFolderCount(directory, compare);
                foreach (var file in Directory.EnumerateFiles(directory))
                    RaiseBytes(file, compare);

                foreach (var dir in Directory.EnumerateDirectories(directory))
                    SearchDirectory(dir, compare);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EvaluateResult(object sender, RunWorkerCompletedEventArgs e)
        {
            Evaluate((CompareObject) e.Result);
        }

        private void Evaluate(CompareObject compare)
        {
            IUnitConverter converter = new UnitConverter();
            converter.EvaluateByteSize(compare);
            SetGuiValues(compare);
        }

        private void SetGuiValues(CompareObject compare)
        {
            SetByteSize(compare);
            SetFileCount(compare);
            SetSubfolderCount(compare);
        }

        private string FormatDouble(double value)
        {
            return $"{value:0.00}";
        }

        private void SetByteSize(CompareObject compare)
        {
            switch (compare.Number)
            {
                case 1:
                    labelFolder1Size.Text = FormatDouble(compare.Size) + @" " + compare.Unit;
                    break;
                default:
                    labelFolder2Size.Text = FormatDouble(compare.Size) + @" " + compare.Unit;
                    break;
            }
        }

        private void SetFileCount(CompareObject compare)
        {
            switch (compare.Number)
            {
                case 1:
                    labelFolder1Files.Text = compare.FileCount.ToString(CultureInfo.CurrentCulture);
                    break;
                default:
                    labelFolder2Files.Text = compare.FileCount.ToString(CultureInfo.CurrentCulture);
                    break;
            }
        }

        private void SetSubfolderCount(CompareObject compare)
        {
            switch (compare.Number)
            {
                case 1:
                    labelFolder1Subfolders.Text = compare.SubFolderCount.ToString(CultureInfo.CurrentCulture);
                    break;
                default:
                    labelFolder2Subfolders.Text = compare.SubFolderCount.ToString(CultureInfo.CurrentCulture);
                    break;
            }
        }

        private void InitializeLanguageManager()
        {
            _lm.SetCurrentLanguage("de-DE");
            _lm.OnLanguageChanged += OnLanguageChanged;
        }

        private void LoadLanguagesToCombo()
        {
            foreach (var lang in _lm.GetLanguages())
                comboBoxLanguage.Items.Add(lang.Name);
            comboBoxLanguage.SelectedIndex = 0;
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lm.SetCurrentLanguageFromName(comboBoxLanguage.SelectedItem.ToString());
        }

        private void OnLanguageChanged(object sender, EventArgs eventArgs)
        {
            buttonChooseFolder1.Text = _lm.GetCurrentLanguage().GetWord("SelectFolder1");
            buttonChooseFolder2.Text = _lm.GetCurrentLanguage().GetWord("SelectFolder2");
            buttonCompare.Text = _lm.GetCurrentLanguage().GetWord("CommpareFolders");
            labelFolder1Caption.Text = _lm.GetCurrentLanguage().GetWord("Folder1");
            labelFolder2Caption.Text = _lm.GetCurrentLanguage().GetWord("Folder2");
            labelSize.Text = _lm.GetCurrentLanguage().GetWord("FileSize");
            labelFiles.Text = _lm.GetCurrentLanguage().GetWord("FileCount");
            labelSubfolders.Text = _lm.GetCurrentLanguage().GetWord("SubFolderCount");
        }
    }
}