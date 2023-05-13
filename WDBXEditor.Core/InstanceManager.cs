using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WDBXEditor.Core;
using WDBXEditor.Core.Common;

namespace WDBXEditor
{
    public static class InstanceManager
    {
        public static ConcurrentQueue<string> AutoRun = new ConcurrentQueue<string>();
        public static Action AutoRunAdded;

        private static Mutex _mutex;
        private static NamedPipeManager _pipeServer;

        /// <summary>
        /// Checks a mutex to see if an instance is running and decides how to proceed based on this and args
        /// </summary>
        /// <param name="args"></param>
        public static void InstanceCheck()
        {
            Func<string[], bool> argCheck = a => a != null && a.Length > 0 && File.Exists(a[0]);
            bool isOnlyInstance = false;

            //if (argCheck(args) || args.Length == 0)
            //{
            //    mutex = new Mutex(true, "WDBXEditorMutex", out isOnlyInstance);
            //    if (!isOnlyInstance)
            //    {
            //        Program.PrimaryInstance = false;
            //        SendData(args); //Send args to the primary instance
            //    }
            //    else
            //    {
            //        Program.PrimaryInstance = true;
            //        pipeServer = new NamedPipeManager();
            //        pipeServer.ReceiveString += OpenRequest;
            //        pipeServer.StartServer();
            //    }
            //}

            _mutex = new Mutex(true, "WDBXEditorMutex", out isOnlyInstance);
            if (!isOnlyInstance)
            {
                Program.PrimaryInstance = false;
                //SendData(args); //Send args to the primary instance
            }
            else
            {
                Program.PrimaryInstance = true;
                _pipeServer = new NamedPipeManager();
                _pipeServer.ReceiveString += OpenRequest;
                _pipeServer.StartServer();
            }
        }

    //    public static void LoadDll(string lib)
    //    {
    //        string startupDirectory = Path.GetDirectoryName(Application.ExecutablePath);
    //        string stormlibPath = Path.Combine(startupDirectory, lib);
    //        bool copyDll = true;

    //        if (File.Exists(stormlibPath)) //If the file exists check if it is the right architecture
    //        {
    //            byte[] data = new byte[4096];
    //            using (Stream s = new FileStream(stormlibPath, FileMode.Open, FileAccess.Read))
				//{
    //                s.Read(data, 0, 4096);
    //            }

    //            int PE_HEADER_ADDR = BitConverter.ToInt32(data, 0x3C);
    //            bool x86 = BitConverter.ToUInt16(data, PE_HEADER_ADDR + 0x4) == 0x014c; //32bit check
    //            copyDll = (x86 != !Environment.Is64BitProcess);
    //        }

    //        if (copyDll)
    //        {
    //            string copypath = Path.Combine(startupDirectory, Environment.Is64BitProcess ? "x64" : "x86", lib);
    //            if (File.Exists(copypath))
				//{
    //                File.Copy(copypath, stormlibPath, true);
    //            }
    //        }
    //    }

        /// <summary>
        /// Enqueues recieved file names and launches the AutoRun delegate
        /// </summary>
        /// <param name="filenames"></param>
        public static void OpenRequest(string filenames)
        {
            string[] files = filenames.Split((Char)3);
            Parallel.For(0, files.Length, f =>
            {
                if (Regex.IsMatch(files[f], Constants.FileRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase))
				{
                    AutoRun.Enqueue(files[f]);
                }
                    
            });

            AutoRunAdded?.Invoke();
        }

        public static void Start()
        {
            _pipeServer?.StartServer();
        }

        public static void Stop()
        {
            if (_pipeServer != null)
            {
                _pipeServer.ReceiveString -= OpenRequest;
                _pipeServer.StopServer();
            }
        }

    //    /// <summary>
    //    /// Opens a new version of the application which bypasses the mutex
    //    /// </summary>
    //    /// <param name="files"></param>
    //    /// <returns></returns>
    //    public static bool LoadNewInstance(IEnumerable<string> files)
    //    {
    //        //Stop server.
    //        Stop(); 

    //        using (var process = new Process())
    //        {
    //            process.StartInfo.FileName = Application.ExecutablePath;
    //            process.StartInfo.Arguments = string.Join(" ", files);
    //            bool started = process.Start();

    //            // Wait for the program to fully load
    //            while (started && process.MainWindowHandle == IntPtr.Zero)
				//{
    //                // TODO: Check if this is the version of sleep we should be using.
    //                Thread.Sleep(50);
    //            }

    //            if (Program.PrimaryInstance)
				//{
    //                //Start server.
    //                Start(); 
    //            }
                   

    //            return started;
    //        }
    //    }

        public static IEnumerable<string> GetFilesToOpen()
        {
            var paths = new HashSet<string>();
            while (AutoRun.Count > 0)
            {
                string file;
                if (AutoRun.TryDequeue(out file) && File.Exists(file))
				{
                    paths.Add(file);
                }
            }
            return paths;
        }

        //public static bool IsRunningAsAdmin()
        //{
        //    WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
        //    return principal.IsInRole(WindowsBuiltInRole.Administrator);
        //}


        #region Send Data

        private static void SendData(string args)
        {
            var clientPipe = new NamedPipeManager();
            if (clientPipe.Write(args))
			{
                Environment.Exit(0);
            }
        }

        private static void SendData(string[] args)
        {
            SendData(string.Join(((Char)3).ToString(), args));
        }

        #endregion

        #region Flash Methods

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        internal struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hwnd;
            public uint dwFlags;
            public uint uCount;
            public uint dwTimeout;
        }

        #endregion

    }
}
