using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace LameTesting
{
    class Program
    {
        static void Main(string[] args)
        {
           
            string wavpath = args[0]; // Путь к входному wav файлу

            string exepath4first = args[1]; // Путь к первой тестируемой версии
            string param4first = args[2]; // Аргументы к первой тестируемой версии
            string mp3first = args[3]; // Где будет храниться выходной mp3 первой версии

            string exepath4second = args[4]; // Путь к второй тестируемой версии
            string param4second = args[5]; // Аргументы к второй тестируемой версии
            string mp3second = args[6]; // Где будет храниться выходной mp3 второй тестируемой версии
           
            long firsttime = ConvertTimeMeasure(exepath4first, wavpath, param4first, mp3first);
           
            long secondtime = ConvertTimeMeasure(exepath4second, wavpath, param4second, mp3second);

            bool equal = FileCompare(mp3first, mp3second);

            string results =
            ("У первой версии lame на конвертацию ушло " + firsttime + " миллисекунд \r\n") +
            ("У второй версии lame на конвертацию ушло " + secondtime + " миллисекунд \r\n");         
            if (equal)
                results += ("mp3 файлы совпадают \r\n");
            else results += ("mp3 файлы не совпадают \r\n");

            Setlogs(wavpath, exepath4first, param4first, exepath4second, param4second, results);

        }

        private static long ConvertTimeMeasure(string exepath, string wavpath, string param, string mp3path)
        {
            
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            process.StartInfo.FileName = exepath;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true; 
            process.StartInfo.Arguments = string.Format(
                "{0} {1} {2}",
                param,
                wavpath,
                mp3path);

            Stopwatch watch = new Stopwatch();
            try
            {
                process.Start();
            }
            catch (Exception e)
            {
                AddToLog("Exception при запуске Lame: " + e);
                return 0;
            }
            watch.Start();            
            process.WaitForExit();
            watch.Stop();
            long timepassed = watch.ElapsedMilliseconds;
                       
            return timepassed;
        }

       

        private static void Setlogs(string wavpath, string exe4first, string param4first, string exe4second, string param4second, string results)
        {
            string pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            if (!Directory.Exists(pathToLog))
                Directory.CreateDirectory(pathToLog); // Создаем директорию, если нужно
            string filename = Path.Combine(pathToLog, string.Format("{0}_{1:dd.MM.yyy}.log",
            AppDomain.CurrentDomain.FriendlyName, DateTime.Now));
            string dateText = string.Format("\r\n     [{0:dd.MM.yyy HH:mm:ss.fff}] \r\n", DateTime.Now);
            string testInfo = string.Format(" Wav: {0}\r\n Первый exe: {1} аргументы:({2})\r\n Второй exe: {3} аргументы:({4})\r\n", 
                wavpath, exe4first, param4first, exe4second, param4second);
           
            File.AppendAllText(filename, dateText, Encoding.GetEncoding("Windows-1251"));
            File.AppendAllText(filename, testInfo, Encoding.GetEncoding("Windows-1251"));
            File.AppendAllText(filename, results, Encoding.GetEncoding("Windows-1251"));
        }

        private static void AddToLog(string info)
        {
            string pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            if (!Directory.Exists(pathToLog))
                Directory.CreateDirectory(pathToLog); // Создаем директорию, если нужно
            string filename = Path.Combine(pathToLog, string.Format("{0}_{1:dd.MM.yyy}.log",
            AppDomain.CurrentDomain.FriendlyName, DateTime.Now));
            string Text = string.Format("[{0:dd.MM.yyy HH:mm:ss.fff}]  {1} \r\n", DateTime.Now, info);
           
            File.AppendAllText(filename, Text, Encoding.GetEncoding("Windows-1251"));           
        }

        private static bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1 = null;
            FileStream fs2 = null;

            try
            {
                fs1 = new FileStream(file1, FileMode.Open);
            }
            catch (Exception e)
            {
                AddToLog("Во время открытия первого mp3 файла возникло исключение: " + e);
                return false;
            }

            try
            {
                fs2 = new FileStream(file2, FileMode.Open);
            }
            catch (Exception e)
            {
                AddToLog("Во время открытия второго mp3 файла возникло исключение: " + e);
                return false;
            }

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
