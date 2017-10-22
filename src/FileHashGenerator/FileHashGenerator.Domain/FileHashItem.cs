using System;
using System.Waf.Foundation;

namespace Waf.FileHashGenerator.Domain
{
    public class FileHashItem : Model
    {
        private readonly string fileName;
        private byte[] hashBytes = new byte[0];
        private string hash;
        private string expectedHash;
        private bool isCaseSensitive;
        private double progress;
        private bool? isHashValid;


        public FileHashItem(string fileName)
        {
            this.fileName = fileName;
        }


        public string FileName { get { return fileName; } }

        public byte[] HashBytes
        {
            get { return hashBytes; }
            set { SetProperty(ref hashBytes, value); }
        }

        public string Hash
        {
            get { return hash; }
            set
            {
                if (SetProperty(ref hash, value))
                {
                    UpdateIsHashValid();
                }
            }
        }

        public string ExpectedHash
        {
            get { return expectedHash; }
            set
            {
                if (SetProperty(ref expectedHash, value))
                {
                    UpdateIsHashValid();
                }
            }
        }

        public bool IsCaseSensitive 
        { 
            get { return isCaseSensitive; }
            set 
            {
                if (SetProperty(ref isCaseSensitive, value))
                {
                    UpdateIsHashValid();
                }
            }
        }

        public double Progress
        {
            get { return progress; }
            set { SetProperty(ref progress, value); }
        }

        public bool? IsHashValid
        {
            get { return isHashValid; }
            private set { SetProperty(ref isHashValid, value); }
        }

        private void UpdateIsHashValid()
        {
            if (string.IsNullOrEmpty(Hash) || string.IsNullOrEmpty(ExpectedHash))
            {
                IsHashValid = null;
            }
            else
            {
                var comparisonType = IsCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
                IsHashValid = string.Equals(Hash, ExpectedHash, comparisonType);
            }
        }
    }
}
