FolderComparer2
===============

FolderComparer2 is a software/library to compare folders according to their size, files and subfolders.

[![Build status](https://ci.appveyor.com/api/projects/status/6qqhjk6pia7nvn67?svg=true)](https://ci.appveyor.com/project/SeppPenner/foldercomparer2)
[![GitHub issues](https://img.shields.io/github/issues/SeppPenner/FolderComparer2.svg)](https://github.com/SeppPenner/FolderComparer2/issues)
[![GitHub forks](https://img.shields.io/github/forks/SeppPenner/FolderComparer2.svg)](https://github.com/SeppPenner/FolderComparer2/network)
[![GitHub stars](https://img.shields.io/github/stars/SeppPenner/FolderComparer2.svg)](https://github.com/SeppPenner/FolderComparer2/stargazers)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://raw.githubusercontent.com/SeppPenner/FolderComparer2/master/License.txt)
[![Known Vulnerabilities](https://snyk.io/test/github/SeppPenner/FolderComparer2/badge.svg)](https://snyk.io/test/github/SeppPenner/FolderComparer2)
[![Blogger](https://img.shields.io/badge/Follow_me_on-blogger-orange)](https://franzhuber23.blogspot.de/)
[![Patreon](https://img.shields.io/badge/Patreon-F96854?logo=patreon&logoColor=white)](https://patreon.com/SeppPennerOpenSourceDevelopment)
[![PayPal](https://img.shields.io/badge/PayPal-00457C?logo=paypal&logoColor=white)](https://paypal.me/th070795)

## Basic usage
```csharp
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
                MessageBox.Show(ex.Message + ex.StackTrace, ex.Message,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
```

## Screenshot from the executable German
![Screenshot from the executable German](https://github.com/SeppPenner/FolderComparer2/blob/master/Screenshot_2.PNG "Screenshot from the executable German")

## Screenshot from the executable English
![Screenshot from the executable English](https://github.com/SeppPenner/FolderComparer2/blob/master/Screenshot_1.PNG "Screenshot from the executable English")

Change history
--------------

See the [Changelog](https://github.com/SeppPenner/FolderComparer2/blob/master/Changelog.md).