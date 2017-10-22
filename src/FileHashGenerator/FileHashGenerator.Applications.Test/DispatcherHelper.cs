using System;
using System.Diagnostics;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Threading;

namespace Test.FileHashGenerator.Applications
{
    internal static class DispatcherHelper
    {
        internal static void WaitUntil(Func<bool> conditionToProceed, TimeSpan timeout)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (!conditionToProceed()) 
            {
                if (stopwatch.Elapsed > timeout)
                {
                    throw new TimeoutException();
                }

                Thread.Sleep(200);
                DoEvents();
            }
        }
        
        /// <summary>
        /// Execute the event queue of the dispatcher.
        /// </summary>
        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        internal static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }

        private static object ExitFrame(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }
    }
}
