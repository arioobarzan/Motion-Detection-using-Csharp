﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;


namespace WindowsFormsApplication2
{
    class WMP_Control
    {
        MethodInvoker simpleDelegate = new MethodInvoker(make_a_beep);

        static private void make_a_beep()
        {
            Console.Beep(659, 200);
        }

        [DllImport("USER32.DLL")]
        private static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.

        [DllImport("USER32.DLL")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public void ControlMediaPlayer(string button)
        {
            IntPtr mediaPlayerHandle =
                   FindWindow("WMPlayerApp", "Windows Media Player");

            simpleDelegate.BeginInvoke(null, null);
            // Verify that WMP is a running process.
            if (mediaPlayerHandle == IntPtr.Zero)
            {
                return;
            }

            switch (button)
            {
                case "PreviousSong":
                    SetForegroundWindow(mediaPlayerHandle);
                    SendKeys.SendWait("^b");
                    break;

                case "NextSong":
                    SetForegroundWindow(mediaPlayerHandle);
                    SendKeys.SendWait("^f");
                    break;

                case "Play/Pause":
                    SetForegroundWindow(mediaPlayerHandle);
                    SendKeys.SendWait("^p");
                    break;
            }


        }

    }
}
