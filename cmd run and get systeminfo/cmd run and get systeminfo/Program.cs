using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmd_run_and_get_systeminfo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (Process p = new Process())
                {
                    // set start info
                    p.StartInfo = new ProcessStartInfo("cmd.exe")
                    {
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                        WorkingDirectory = @"d:\"
                    };
                    // event handlers for output & error
                    p.OutputDataReceived += p_OutputDataReceived;
                    p.ErrorDataReceived += p_ErrorDataReceived;

                    // start process
                    p.Start();
                    // send command to its input
                    p.StandardInput.Write("systeminfo" + p.StandardInput.NewLine);
                    //wait
                    p.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            Console.WriteLine(e.Data);
        }

        static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            Console.WriteLine(e.Data);
        }
    }
    }

