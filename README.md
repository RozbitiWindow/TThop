<div align="center">

# 🖥️ TThop

**A lightweight, terminal-based system monitor for Linux — built with C# and .NET 10.**

[![Platform](https://img.shields.io/badge/platform-Linux-orange.svg)](https://github.com/RozbitiWindow/TThop)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/language-C%23-brightgreen.svg)](https://github.com/RozbitiWindow/TThop)

</div>

---

TThop is a simple, real-time system stats viewer that runs in your terminal. It reads directly from Linux kernel interfaces (`/proc`, `/sys`) — no dependencies, no fluff. Think of it as a minimal `htop` alternative written in pure C#.

## 📊 What it shows

| Metric | Details |
|---|---|
| **Process count** | Number of currently running processes |
| **RAM usage** | Total and available memory (GB) |
| **Disk space** | Total and free space on `/` (GB) |
| **CPU load** | 1-minute load average, color-coded by severity |
| **System uptime** | Days, hours, minutes, seconds since last boot |
| **CPU temperature** | In °C, read from thermal zone 0 |

CPU load is color-coded in real time:

| Load | Color |
|---|---|
| ≥ 1% | 🟢 Green |
| ≥ 30% | 🟩 Dark Green |
| ≥ 50% | 🟡 Yellow |
| ≥ 80% | 🔴 Red |
| ≥ 90% | 🟥 Dark Red |

The display refreshes every **1 second**.

---

## ⚙️ Requirements

- Linux (reads from `/proc` and `/sys` — **will not work on Windows/macOS**)
- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

---

## 🚀 Installation & Usage

### Clone and run

```bash
git clone https://github.com/RozbitiWindow/TThop.git
cd TThop
dotnet run
```

### Build a standalone binary

```bash
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true
```

The binary will be output to:
```
bin/Release/net10.0/linux-x64/publish/tthop
```

---

## ⚡ Alias Setup (run it from anywhere)

Once you've built the binary, you can set up a shell alias so you can launch TThop by just typing `tthop` in any terminal.

### Step 1 — Build the binary

```bash
cd TThop
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true
```

### Step 2 — Move it to a permanent location

```bash
mkdir -p ~/.local/bin
cp bin/Release/net10.0/linux-x64/publish/tthop ~/.local/bin/tthop
chmod +x ~/.local/bin/tthop
```

### Step 3 — Add the alias to your shell config

**Bash** (`~/.bashrc`):
```bash
echo 'alias tthop="~/.local/bin/tthop"' >> ~/.bashrc
source ~/.bashrc
```

**Zsh** (`~/.zshrc`):
```bash
echo 'alias tthop="~/.local/bin/tthop"' >> ~/.zshrc
source ~/.zshrc
```

**Fish** (`~/.config/fish/config.fish`):
```fish
echo 'alias tthop="~/.local/bin/tthop"' >> ~/.config/fish/config.fish
source ~/.config/fish/config.fish
```

### Step 4 — Launch it

```bash
tthop
```

> **Tip:** If `~/.local/bin` is already in your `$PATH`, you can skip the alias entirely and just run the binary directly after Step 2.

---

## 📁 Project Structure

```
TThop/
├── Program.cs         # Entry point, display loop & rendering logic
├── SystemReader.cs    # Linux system stat readers (/proc, /sys)
├── tthop.csproj       # .NET project file
└── tthop.sln          # Solution file
```

---

## 🔍 How it works

TThop reads system metrics directly from the Linux kernel's virtual filesystems:

| Source | Data |
|---|---|
| `/proc/meminfo` | Total and available RAM |
| `/proc/loadavg` | 1-minute CPU load average |
| `/proc/uptime` | System uptime in seconds |
| `/proc/` (dirs) | Running process count |
| `/sys/class/thermal/thermal_zone0/temp` | CPU temperature |
| `DriveInfo("/")` | Disk usage for root partition |

---

## 📝 Notes

- **CPU temperature** may show `cant get system data (not supported)` on systems without a thermal zone at `/sys/class/thermal/thermal_zone0/temp` (e.g. some VMs or older hardware).
- Press `Ctrl+C` to exit.

---

## 👤 Author

Made by **RozbitiOkno** — [github.com/RozbitiWindow](https://github.com/RozbitiWindow)
