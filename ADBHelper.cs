using System.Diagnostics;

namespace AdAutomation
{
    public static class ADBHelper
    {
        public static void TakeScreenshot(string outputPath)
        {
            string fullPath = "D:/Projects/Auto_v0/temp/screen.png";

            // Creating screenshot on a smartphone
            Process adbProcess1 = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = "shell screencap -p /sdcard/screen.png",
                    RedirectStandardOutput = false,
                    UseShellExecute = true,
                    CreateNoWindow = true
                }
            };
            adbProcess1.Start();
            adbProcess1.WaitForExit();

            // Transfering screenshot to directory
            Process adbProcess2 = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = $"pull /sdcard/screen.png {fullPath}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            adbProcess2.Start();
            adbProcess2.WaitForExit();

            Console.WriteLine($"Screenshot saved in: {fullPath}");
        }

        public static void ClickButton(int x, int y)
        {
            Process adbProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = $"shell input tap {x} {y}",
                    RedirectStandardOutput = false,
                    UseShellExecute = true,
                    CreateNoWindow = true
                }
            };
            adbProcess.Start();
            adbProcess.WaitForExit();
        }
    }
}
