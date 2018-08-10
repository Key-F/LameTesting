using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Security.Cryptography;

namespace LameTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            string exepath = args[0];
            string wavpath = args[1];
            string input = args[2];
            string param = args[3];

            

            

            
            ConvertTimeMeasure(exepath, wavpath, input, param);
            bool tt = FileCompare(@"D:\lametest\3.99.5\test.mp3", @"D:\lametest\3.100\test.mp3");

        }

        private static long ConvertTimeMeasure(string exepath, string wavpath, string input, string param)
        {
            string lameEXE = exepath;
            string lameArgs = "-V2";

            string wavFile = wavpath;
            string mp3File = @"D:\lametest\3.99.5\test.mp3";

            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = lameEXE;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true; 
            process.StartInfo.Arguments = string.Format(
                "{0} {1} {2}",
                lameArgs,
                wavFile,
                mp3File);

            Stopwatch watch = new Stopwatch();            
            process.Start();
            watch.Start();            
            process.WaitForExit();
            watch.Stop();
            long timepassed = watch.ElapsedMilliseconds;
           
            return timepassed;
        }

       

        private static void Setlogs(string logpath)
        {
            // string writer

        }

        private static bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;
                       
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);
            
            if (fs1.Length != fs2.Length)
            {               
                fs1.Close();
                fs2.Close();                
                return false;
            }           
            do
            {
                
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));
           
            fs1.Close();
            fs2.Close();
          
            return ((file1byte - file2byte) == 0);
        }
       
    }
}
