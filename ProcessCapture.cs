using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;
using Capture.Interface;
using Capture.Hook;
using Capture;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BejeweledBot
{
    public delegate void BitmapCallback(Bitmap gameWindow);

    class ProcessCapture
    {
        public bool Initalized { get; private set; }

        public bool Capturing { get; private set; }

        AutoResetEvent _waitHandle = new AutoResetEvent(false);
        Thread _thread = null;

        Rectangle boundingBox = new Rectangle(0, 0, 0, 0);
        TimeSpan timeSpan = new TimeSpan(0, 0, 1);
        ImageFormat imageFormat = (ImageFormat)Enum.Parse(typeof(ImageFormat), "Bitmap");

        int _processId = 0;
        Process _process;
        CaptureProcess _captureProcess;


        List<BitmapCallback> callbacks = new List<BitmapCallback>();

        private POINT _coordinates = new POINT();
        public POINT Location { 
            get 
            {
                GetRealLocation();
                return _coordinates; 
            } 
        } 

        public void Initialize()
        {
            string executablePath = "Bejeweled3.exe";

            if (_captureProcess == null)
            {
                string executableName = Path.GetFileNameWithoutExtension(executablePath);

                Process[] processes = Process.GetProcessesByName(executableName);
                foreach (Process process in processes)
                {
                    if (process.MainWindowHandle == IntPtr.Zero)
                    {
                        continue;
                    }

                    if (HookManager.IsHooked(process.Id))
                    {
                        continue;
                    }

                    CaptureConfig cc = new CaptureConfig()
                    {
                        Direct3DVersion = Direct3DVersion.Direct3D9,
                        ShowOverlay = false
                    };

                    var captureInterface = new CaptureInterface();
                    _process = process;
                    _processId = process.Id;
                    _captureProcess = new CaptureProcess(_process, cc, captureInterface);
                    Initalized = true;



                    return;
                }
            }
            else
            {
                HookManager.RemoveHookedProcess(_captureProcess.Process.Id);
                _captureProcess.CaptureInterface.Disconnect();
                _captureProcess = null;
            }

            Initalized = false;
        }

        public void StartCapture()
        {
            _waitHandle.Reset();
            Capturing = true;
            _captureProcess.BringProcessWindowToFront();
            Thread.Sleep(10); // TODO: Cleanup
            _thread = new Thread(new ThreadStart(CaptureLoop));
            _thread.Start();
        }

        public void StopCapture()
        {
            Capturing = false;
            _waitHandle.Set();
        }

        void CaptureLoop()
        {
            while (Capturing)
            {
                // TODO: Validate process is alive before attempting to capture a screenshot?
                _captureProcess.CaptureInterface.BeginGetScreenshot(boundingBox, timeSpan, Callback, null, imageFormat);
                _waitHandle.WaitOne();
            }
        }

        // TODO: Add the ability to remove registered callbacks
        public void RegisterBitmapCallback(BitmapCallback callback)
        {
            callbacks.Add(callback);
        }

        void Callback(IAsyncResult result)
        {
            using (Screenshot screenshot = _captureProcess.CaptureInterface.EndGetScreenshot(result))
            {
                try
                {
                    if (screenshot != null && screenshot.Data != null)
                    {
                        foreach (BitmapCallback bitmapCallback in callbacks)
                        {
                            bitmapCallback(screenshot.ToBitmap());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            _waitHandle.Set();
        }

        private void GetRealLocation()
        {
            if (_process == null) return;
            _coordinates = new POINT();
            ClientToScreen(_process.MainWindowHandle, ref _coordinates);
        }

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);


        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        public struct POINT
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public struct RECT
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }


    }
}
