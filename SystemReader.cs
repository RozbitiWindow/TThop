class SystemReader
{
    public int _avaibleRam;
    public int _totalRam;
    string hwmonPath = "/sys/class/hwmon/";
    public int getRam()
    {
        foreach (var line in File.ReadLines("/proc/meminfo"))
        {
            if (line.StartsWith("MemTotal"))
                _totalRam = int.Parse(line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);

            if (line.StartsWith("MemAvailable"))
                _avaibleRam = int.Parse(line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
        }
        return 0;
    }



    public double _cpuLoad;
    public int cpuCores = Environment.ProcessorCount;

    public double getCpuLoad()
    {
        if (File.Exists("/proc/loadavg"))
        {
            string loadStr = File.ReadAllText("/proc/loadavg");
            // první číslo = load za poslední 1 minutu
            _cpuLoad = double.Parse(loadStr.Split(' ')[0], System.Globalization.CultureInfo.InvariantCulture);
        }
        else
        {
            _cpuLoad = -1;
        }
        return _cpuLoad;
    }


    public int _cpuTemp;

    public int getCpuTemp()
    {
        if (File.Exists("/sys/class/thermal/thermal_zone0/temp"))
        {
            string tempStr = File.ReadAllText("/sys/class/thermal/thermal_zone0/temp");
            _cpuTemp = int.Parse(tempStr) / 1000; // převod na °C
        }
        else
        {
            _cpuTemp = -1; // soubor neexistuje
        }
        return _cpuTemp;
    }

    public double getUptime()
    {
        string uptime = File.ReadAllText("/proc/uptime").Split(' ')[0];
        return double.Parse(uptime, System.Globalization.CultureInfo.InvariantCulture);
    }

    public Dictionary<string, double> GetCpuAndGpuTemperatures()
    {
        var temps = new Dictionary<string, double>();
        string hwmonPath = "/sys/class/hwmon/";

        foreach (var hwmonDir in Directory.GetDirectories(hwmonPath, "hwmon*"))
        {
            foreach (var tempFile in Directory.GetFiles(hwmonDir, "temp*_input"))
            {
                try
                {
                    string content = File.ReadAllText(tempFile).Trim();
                    if (!int.TryParse(content, out int tempMilli)) continue;
                    double tempC = tempMilli / 1000.0;

                    // Pokus najít label
                    string labelFile = tempFile.Replace("_input", "_label");
                    string label = File.Exists(labelFile)
                        ? File.ReadAllText(labelFile).Trim()
                        : Path.GetFileName(tempFile);

                    // Filtrovat jen Tctl (CPU) a Sensor 1–3 (GPU / další)
                    if (label.Equals("Tctl", StringComparison.OrdinalIgnoreCase) ||
                        label.StartsWith("Sensor", StringComparison.OrdinalIgnoreCase))
                    {
                        temps[label] = tempC;
                    }
                }
                catch
                {
                    continue;
                }
            }
        }

        return temps;
    }

}class SystemReader
{
    public int _avaibleRam;
    public int _totalRam;
    public int getRam()
    {
        foreach (var line in File.ReadLines("/proc/meminfo"))
        {
            if (line.StartsWith("MemTotal"))
                _totalRam = int.Parse(line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);

            if (line.StartsWith("MemAvailable"))
                _avaibleRam = int.Parse(line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
        }
        return 0;
    }



    public double _cpuLoad;
    public int cpuCores = Environment.ProcessorCount;

    public double getCpuLoad()
    {
        if (File.Exists("/proc/loadavg"))
        {
            string loadStr = File.ReadAllText("/proc/loadavg");
            // první číslo = load za poslední 1 minutu
            _cpuLoad = double.Parse(loadStr.Split(' ')[0], System.Globalization.CultureInfo.InvariantCulture);
        }
        else
        {
            _cpuLoad = -1;
        }
        return _cpuLoad;
    }


    public int _cpuTemp;

    public int getCpuTemp()
    {
        if (File.Exists("/sys/class/thermal/thermal_zone0/temp"))
        {
            string tempStr = File.ReadAllText("/sys/class/thermal/thermal_zone0/temp");
            _cpuTemp = int.Parse(tempStr) / 1000; // převod na °C
        }
        else
        {
            _cpuTemp = -1; // soubor neexistuje
        }
        return _cpuTemp;
    }

    public double getUptime()
{
    string uptime = File.ReadAllText("/proc/uptime").Split(' ')[0];
    return double.Parse(uptime, System.Globalization.CultureInfo.InvariantCulture);
}

}

