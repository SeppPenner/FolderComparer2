FolderComparer2
===============

FolderComparer2 is a software/library to compare folders according to their size, files and subfolders.
Both are written and tested in .Net 4.8.

[![Build status](https://ci.appveyor.com/api/projects/status/6qqhjk6pia7nvn67?svg=true)](https://ci.appveyor.com/project/SeppPenner/foldercomparer2)
[![GitHub issues](https://img.shields.io/github/issues/SeppPenner/FolderComparer2.svg)](https://github.com/SeppPenner/FolderComparer2/issues)
[![GitHub forks](https://img.shields.io/github/forks/SeppPenner/FolderComparer2.svg)](https://github.com/SeppPenner/FolderComparer2/network)
[![GitHub stars](https://img.shields.io/github/stars/SeppPenner/FolderComparer2.svg)](https://github.com/SeppPenner/FolderComparer2/stargazers)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://raw.githubusercontent.com/SeppPenner/FolderComparer2/master/License.txt)
[![Known Vulnerabilities](https://snyk.io/test/github/SeppPenner/FolderComparer2/badge.svg)](https://snyk.io/test/github/SeppPenner/FolderComparer2)

## Basic usage:
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

## Screenshot from the executable german:
![Screenshot from the executable german](https://github.com/SeppPenner/FolderComparer2/blob/master/Screenshot_2.PNG "Screenshot from the executable german")

## Screenshot from the executable english:
![Screenshot from the executable english](https://github.com/SeppPenner/FolderComparer2/blob/master/Screenshot_1.PNG "Screenshot from the executable english")

Change history
--------------

* **Version 2.0.1.0 (2019-10-27)** : Updated nuget packages, added GitVersionTask.
* **Version 2.0.0.4 (2019-05-07)** : Updated .Net version to 4.8.
* **Version 2.0.0.3 (2017-03-24)** : Updated Languages.dll to version 1.0.0.4.
* **Version 2.0.0.2 (2017-03-21)** : Updated Languages.dll.
* **Version 2.0.0.1 (2017-03-11)** : Switched to .Net 4.6.2, refactored code.
* **Version 2.0.0.0 (2016-12-03)** : Added basic usage to Readme.
* **Version 2.0.0.0 (2016-08-13)** : 1.0 release.
