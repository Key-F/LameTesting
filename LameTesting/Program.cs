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

            

            //int exitCode = process.ExitCode;

            //Process.Start(@"D:\lametest\3.99.5\lame.exe", "-b 128 test.wav cmd");
           //ConvertTimeMeasure(exepath, wavpath, input, param);
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
            process.StartInfo.RedirectStandardOutput = true; // Вот это чтобы не было System.InvalidOperationException: "Поток StandardOut не был перенаправлен или процесс еще не был запущен
            process.StartInfo.Arguments = string.Format(
                "{0} {1} {2}",
                lameArgs,
                wavFile,
                mp3File);

            Stopwatch watch = new Stopwatch();            
            process.Start();
            watch.Start();
            //process.BeginOutputReadLine();
            // Thread.Sleep(6660);
            StreamReader reader = process.StandardOutput;
            string output = reader.ReadToEnd();
            StreamReader outputReader = process.StandardOutput;
            process.WaitForExit();
            watch.Stop();
            long timepassed = watch.ElapsedMilliseconds;
            //string output = process.StandardOutput.ReadToEnd();
            //StreamReader reader = process.StandardOutput;
            //string output = reader.ReadToEnd();
            Console.ReadKey();
            return timepassed;
        }

        private static void oldConvert(string exepath, string wavpath, string input, string param)
        {
            Process iStartProcess = new Process(); // новый процесс
            //iStartProcess.StartInfo = new ProcessStartInfo();
            iStartProcess.StartInfo.FileName = @"D:\lametest\3.99.5\lame.exe"; // путь к запускаемому файлу
            //iStartProcess.StartInfo.Arguments = "-b 128 test.wav"; // эта строка указывается, если программа запускается с параметрами (здесь указан пример, для наглядности)
            iStartProcess.StartInfo.Arguments = "-preset medium test.wav";
            //iStartProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized; // эту строку указываем, если хотим запустить программу в скрытом виде
            // iStartProcess.StartInfo.CreateNoWindow = false;

            iStartProcess.Start(); // запускаем программу

            /*while (!iStartProcess.StandardOutput.EndOfStream)
            {
                string line = iStartProcess.StandardOutput.ReadLine();
                // do something with line
            }
            if (!iStartProcess.HasExited)
            {
                Console.WriteLine("process is running");
            }
            else
            {
                Console.WriteLine("process is stopped");
            }
            /*string output = iStartProcess.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            Thread.Sleep(6660);
            StreamReader reader = iStartProcess.StandardOutput;
           // string output = reader.ReadToEnd();

            Console.WriteLine(output);
            */
            iStartProcess.WaitForExit();
            iStartProcess.Close();

           // Console.ReadKey();
           // iStartProcess.WaitForExit(120000); // эту строку указываем, если нам надо будет ждать завершения программы определённое время, пример: 2 мин. (указано в миллисекундах - 2 мин. * 60 сек. * 1000 м.сек.)
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

            // Determine if the same file was referenced two times.
           /* if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            } */

            // Open the two files.
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            // Check the file sizes. If they are not the same, the files 
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);
        }

        private string ComputeMD5Checksum(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                byte[] checkSum = md5.ComputeHash(fileData);
                string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
                return result;
            }
        }
    }
}
