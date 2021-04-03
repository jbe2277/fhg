using System;
using System.Waf.Foundation;

namespace Waf.FileHashGenerator.Domain
{
    public class FileHashItem : Model
    {
        private byte[] hashBytes = Array.Empty<byte>();
        private string? hash;
        private string? expectedHash;
        private bool isCaseSensitive;
        private double progress;
        private bool? isHashValid;


        public FileHashItem(string fileName)
        {
            FileName = fileName;
        }


        public string FileName { get; }

        public byte[] HashBytes
        {
            get => hashBytes;
            set => SetProperty(ref hashBytes, value);
        }

        public string? Hash
        {
            get => hash;
            set
            {
                if (SetProperty(ref hash, value))
                {
                    UpdateIsHashValid();
                }
            }
        }

        public string? ExpectedHash
        {
            get => expectedHash;
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
            get => isCaseSensitive;
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
            get => progress;
            set => SetProperty(ref progress, value);
        }

        public bool? IsHashValid
        {
            get => isHashValid;
            private set => SetProperty(ref isHashValid, value);
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
