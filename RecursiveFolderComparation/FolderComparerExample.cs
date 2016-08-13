using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FolderComparer2
{
    public class FolderComparerExample
    {
        private readonly CompareObject _compareObject1 = new CompareObject(1);

        private void Example()
        {
            SearchDirectory("C:\\Users", _compareObject1);
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
            FileInfo fileInfo = new FileInfo(file);
            compare.Size += fileInfo.Length;
            compare.ByteSize += fileInfo.Length;
        }

        private void SearchDirectory(string directory, CompareObject compare)
        {
            try
            {
                RaiseFileCount(directory, compare);
                RaiseSubFolderCount(directory, compare);
                foreach (string file in Directory.EnumerateFiles(directory))
                {
                    RaiseBytes(file, compare);
                }

                foreach (string dir in Directory.EnumerateDirectories(directory))
                {
                    SearchDirectory(dir, compare);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}