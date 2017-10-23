using System;
using System.IO;
using System.Threading;

namespace Waf.FileHashGenerator.Applications
{
    public sealed class ProgressStream : Stream
    {
        private const int numberOfCallbacks = 1000;

        private readonly Stream stream;
        private readonly CancellationToken cancellationToken;
        private readonly IProgress<double> progressCallback;
        private int nextCallback;
        private int nextCallbackReset;


        public ProgressStream(Stream stream, CancellationToken cancellationToken, IProgress<double> progressCallback)
        {
            this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
            this.cancellationToken = cancellationToken;
            this.progressCallback = progressCallback ?? throw new ArgumentNullException(nameof(progressCallback));
            nextCallbackReset = -1;
        }


        public override bool CanRead => stream.CanRead;

        public override bool CanSeek => stream.CanSeek;

        public override bool CanWrite => stream.CanWrite;

        public override long Length => stream.Length;

        public override long Position
        {
            get => stream.Position;
            set => stream.Position = value;
        }


        public override int Read(byte[] buffer, int offset, int count)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (nextCallbackReset < 0)
            {
                nextCallbackReset = (int)(Length / ((long)count * numberOfCallbacks));
            }

            int result = stream.Read(buffer, offset, count);

            if (--nextCallback <= 0)
            {
                progressCallback.Report((double)Position / Length);
                nextCallback = nextCallbackReset;
            }

            return result;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            stream.Write(buffer, offset, count);
        }

        public override void Flush()
        {
            stream.Flush();
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing) { stream.Dispose(); }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
    }
}
