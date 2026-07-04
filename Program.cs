using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            //int i = 0;
            SystemReader systemReader = new SystemReader();
            long totalDisk = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed).Sum(d => d.TotalSize);
            long freeDisk = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed).Sum(d => d.AvailableFreeSpace);
            Console.Clear();
            Console.WriteLine("TThop - system stats by RozbitiOkno");
            int processCount = Directory.GetDirectories("/proc")
              .Count(dir => int.TryParse(Path.GetFileName(dir), out _));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nProcesses: " + processCount);
            Console.ResetColor();
            systemReader.getRam();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n--- RAM   usage ---         --- Disk space ---");
            Console.ResetColor();
            Console.WriteLine("    Total RAM: " + systemReader._totalRam / 1048576 + " GB         Total: " + totalDisk / 1073741824 + " GB");
            Console.Write("Availible RAM: " + systemReader._avaibleRam / 1048576 + " GB");
            Console.WriteLine("          Free: " + freeDisk / 1073741824 + " GB");

            if (systemReader._avaibleRam <= systemReader._totalRam * 0.3) //RAM text
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (systemReader._avaibleRam <= systemReader._totalRam * 0.5)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            double freePercent = (double)systemReader._avaibleRam / systemReader._totalRam * 100;
            Console.WriteLine($"System has only available {freePercent:F1}%  of all RAM");
            Console.ResetColor();

            double freeDiskPercent = (double)freeDisk / totalDisk * 100; //Disk text

            if (freeDiskPercent <= 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (freeDiskPercent <= 20)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine($"{freeDiskPercent:F1}% free disk space");
            Console.ResetColor();

            systemReader.getCpuLoad(); //CPU load
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n--- CPU load ---");
            Console.ResetColor();

            systemReader._cpuLoad = Math.Round((systemReader._cpuLoad / systemReader.cpuCores) * 100);
            if (systemReader._cpuLoad >= 1) { Console.ForegroundColor = ConsoleColor.Green; }
            if (systemReader._cpuLoad >= 30) { Console.ForegroundColor = ConsoleColor.DarkGreen; }
            if (systemReader._cpuLoad >= 50) { Console.ForegroundColor = ConsoleColor.Yellow; }
            if (systemReader._cpuLoad >= 80) { Console.ForegroundColor = ConsoleColor.Red; }
            if (systemReader._cpuLoad >= 90) { Console.ForegroundColor = ConsoleColor.DarkRed; }
            Console.WriteLine("\n   CPU load: " + systemReader._cpuLoad + "%");
            Console.ResetColor();
            Console.WriteLine("");

            if (systemReader._cpuLoad >= 50 && systemReader._cpuLoad < 74)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("CPU load is more then 50%");
                Console.ResetColor();
            }
            else if (systemReader._cpuLoad >= 75 && systemReader._cpuLoad < 84)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("CPU load is more then 75%");
                Console.ResetColor();
            }
            else if (systemReader._cpuLoad >= 85)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("CPU load warning! CPU load is more then 85% now!");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n--- System uptime ---");
            Console.ResetColor();
            double uptimeSeconds = Math.Round(systemReader.getUptime());
            TimeSpan uptime = TimeSpan.FromSeconds(uptimeSeconds);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("      " + uptime + "\n\n--- CPU temp ---\n\n");
            Console.ResetColor();
            var temps = systemReader.GetCpuAndGpuTemperatures();
            foreach (var kv in temps)
            {
                Console.WriteLine($"{kv.Key}: {kv.Value:F2} °C");
            }
            Console.WriteLine("*sensor → GPU temp\n Tctl → CPU\nDetecting every data possible, 'core 0' for main orientation.\nEvery Linux should be suported.\n\nThanks for using! ('CTRL + c' to exit)\n(https://github.com/RozbitiWindow/TThop)");
            if (systemReader._cpuTemp == -1) { Console.WriteLine(" - cant get system data (not suported)"); }
            else { Console.WriteLine(""); }
            Thread.Sleep(3000);


            continue;
        }


    }

}


//double uptime = systemReader.getUptime();
