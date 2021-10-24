using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System;
using SymlinkSwapper.Logic;

namespace SymlinkSwapper.Core
{
    public class SymLinkWorker
    {
        private static readonly int PRIVILEGE_NOT_HELD = 1314;
        private CancellationTokenSource tokenSource;
        private string sourcePath, outputFilePath;
        private int delay;
        private Task task;

        public bool ToggleWorker(string sourcePath, string outputFilePath, int delay)
        {
            if (IsTaskRunning)
            {
                StopWorker();
                return false;
            }

            this.sourcePath = sourcePath;
            this.outputFilePath = outputFilePath;
            this.delay = delay;

            try
            {
                StartWorker();
            }
            catch
            {
                return false;
            }

            return true;
        }

        private bool IsTaskRunning => task is not null && task.Status != TaskStatus.RanToCompletion;

        private void StartWorker()
        {
            if (task == null || task.Status == TaskStatus.RanToCompletion)
            {
                tokenSource = new();
                task = Task.Factory.StartNew(RunWorker, tokenSource.Token)
                                  .ContinueWith(_ => task = null);
            }
        }

        private void StopWorker()
        {
            if (task != null)
            {
                tokenSource.Cancel();
            }
        }

        private void RunWorker()
        {
            try
            {
                int index = 0;
                bool symlinkCreated = false;
                string[] sourceFiles = Directory.GetFiles(sourcePath)
                                                .Where(filePath => filePath != outputFilePath)
                                                .ToArray();
                
                if (sourceFiles.Length < 2)
                {
                    throw new NoFilesException();
                }

                while (!tokenSource.IsCancellationRequested)
                {
                    File.Delete(outputFilePath);

                    symlinkCreated = CreateSymbolicLink(outputFilePath, sourceFiles[index], SymbolicLink.File);

                    if (!symlinkCreated)
                    {
                        int lastError = Marshal.GetLastWin32Error();

                        if (lastError == PRIVILEGE_NOT_HELD)
                        {
                            throw new PriviligeNotHeldException();
                        }

                        throw new FailedToCreateSymlinkException();
                    }

                    index = sourceFiles.Length == index + 1 ? 0 : ++index;
                    Thread.Sleep(delay);
                }
            }
            catch (Exception ex)
            {
                ErrorEventArgs args = new(ex);
                OnWorkerIssue(args);
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool CreateSymbolicLink(
        string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);

        private enum SymbolicLink
        {
            File = 0,
            Directory = 1
        }

        public event ErrorEventHandler WorkerIssue;

        protected virtual void OnWorkerIssue(ErrorEventArgs e)
        {
            ErrorEventHandler handler = WorkerIssue;
            handler?.Invoke(this, e);
        }

    }
}
