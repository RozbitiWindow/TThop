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
            DriveInfo drive = new DriveInfo("/");
            Console.Clear();
            Console.WriteLine("TThop system stats by RozbitiOkno");
            int processCount = Directory.GetDirectories("/proc")
              .Count(dir => int.TryParse(Path.GetFileName(dir), out _));
            Console.WriteLine("\nProcesses: " + processCount);
            systemReader.getRam();
            Console.WriteLine("\n--- RAM   usage ---         --- Disk space ---");
            Console.WriteLine("Total RAM: " + systemReader._totalRam / 1048576 + " GB             Total: " + drive.TotalSize / 1073741824 + " GB");
            Console.WriteLine("Availible RAM: " + systemReader._avaibleRam / 1048576 + " GB         Free:  " + drive.AvailableFreeSpace / 1073741824 + " GB");
            systemReader.getCpuLoad();
            Console.Write("\n--- CPU   load ---");
            Console.WriteLine("        --- System uptime ---");
            systemReader._cpuLoad = Math.Round((systemReader._cpuLoad / systemReader.cpuCores) * 100);
            if (systemReader._cpuLoad >= 1) { Console.ForegroundColor = ConsoleColor.Green; }
            if (systemReader._cpuLoad >= 30) { Console.ForegroundColor = ConsoleColor.DarkGreen; }
            if (systemReader._cpuLoad >= 50) { Console.ForegroundColor = ConsoleColor.Yellow; }
            if (systemReader._cpuLoad >= 80) { Console.ForegroundColor = ConsoleColor.Red; }
            if (systemReader._cpuLoad >= 90) { Console.ForegroundColor = ConsoleColor.DarkRed; }
            Console.Write("CPU load: " + systemReader._cpuLoad + "%");
            Console.ResetColor();
            double uptimeSeconds = Math.Round(systemReader.getUptime());
            TimeSpan uptime = TimeSpan.FromSeconds(uptimeSeconds);
            Console.ResetColor();
            Console.Write("                    " + uptime);
            systemReader.getCpuTemp();
            Console.WriteLine("\n\n--- CPU - temp ---");
            Console.Write("CPU temp: " + systemReader._cpuTemp + " °C");
            if (systemReader._cpuTemp == -1) { Console.WriteLine(" - cant get system data (not suported)"); }
            else { Console.WriteLine(""); }
            Thread.Sleep(1000);
           /* while (i < 3)
            {
                Console.Write(".");
                Thread.Sleep(1000);
                i++;
            }*/

            continue;
        }


    }

}


//double uptime = systemReader.getUptime();