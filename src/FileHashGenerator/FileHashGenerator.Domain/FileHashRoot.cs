﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Waf.Foundation;

namespace Waf.FileHashGenerator.Domain
{
    public class FileHashRoot : Model
    {
        private readonly ObservableCollection<FileHashItem> fileHashItems;

        public FileHashRoot()
        {
            fileHashItems = new ObservableCollection<FileHashItem>();
        }

        public IReadOnlyList<FileHashItem> FileHashItems => fileHashItems;

        public FileHashItem AddNewFileHashItem(string fileName)
        {
            var fileHashItem = new FileHashItem(fileName);
            fileHashItems.Add(fileHashItem);
            return fileHashItem;
        }

        public void RemoveFileHashItem(FileHashItem fileHashItem) => fileHashItems.Remove(fileHashItem);
    }
}
