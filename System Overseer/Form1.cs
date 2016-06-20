using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Management;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace System_Overseer
{
    public partial class SystemOverseer : Form
    {
        int coreCount;
        Timer updateTimer = new Timer();
        PerformanceCounter cpuCounter = new PerformanceCounter();
        PerformanceCounter cpuCore1 = new PerformanceCounter();
        PerformanceCounter cpuCore2 = new PerformanceCounter();
        PerformanceCounter cpuCore3 = new PerformanceCounter();
        PerformanceCounter cpuCore4 = new PerformanceCounter();
        PerformanceCounter cpuCore5 = new PerformanceCounter();
        PerformanceCounter cpuCore6 = new PerformanceCounter();
        PerformanceCounter cpuCore7 = new PerformanceCounter();
        PerformanceCounter cpuCore8 = new PerformanceCounter();
        PerformanceCounter ramCounter = new PerformanceCounter();

        public SystemOverseer()
        {
            InitializeComponent();
        }

        private void SystemOverseer_Load(object sender, EventArgs e)
        {
            InitTimer();

            coreCount = getCoreCount();

            ramUsageBar.Maximum = getRAMSize();

            string sysUser = Environment.UserName;
            string sysOS = getOS();
            string sysBit = "";
            string sysMachine = Environment.MachineName;

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432")))
            {
                sysBit = "x86 (32bit)";
            }
            else
            {
                sysBit = "x64 (64bit)";
            }

            systemInfoLabel.Text = ("Logged in as '" + sysUser + "' on machine '" + sysMachine + "' using " + sysOS + " " + sysBit + ".");

            if (getPhysProcessors() != 1)
            {
                cpuPhysLabel.Text = getPhysProcessors() + " Physical Processors";
            } 
            else
            {
                cpuPhysLabel.Text = getPhysProcessors() + " Physical Processor";
            }

            if (getLogProcessors() != 1)
            {
                cpuLogLabel.Text = getLogProcessors() + " Logical Processors";
            }
            else
            {
                cpuLogLabel.Text = getLogProcessors() + " Logical Processor";
            }

            if (getCoreCount() != 1)
            {
                cpuCoreLabel.Text = getCoreCount() + " Cores";
            }
            else
            {
                cpuCoreLabel.Text = getCoreCount() + " Core";
            }

            string cpuName = getCPUName();
            cpuName = cpuName
            .Replace("(R)", "®")
            .Replace("(C)", "©")
            .Replace("(TM)", "™")
            .Replace("(r)", "®")
            .Replace("(c)", "©")
            .Replace("(tm)", "™");
            cpuNameLabel.Text = cpuName.Trim();

            string ramSize = getRAMSize().ToString();
            string ramRate = getRAMRate().ToString();
            string ramManufacturer = getRAMManufacturer().ToString();

            if ((ramManufacturer == "Undefined" || ramManufacturer == "") & ramRate == "0")
            {
                ramInfoLabel.Text = ramSize.Trim() + "MB of Unknown RAM @ Unknown MHz";
            }
            else
            if (ramManufacturer == "Undefined")
            {
                ramInfoLabel.Text = ramSize.Trim() + "MB of Unknown RAM @ " + ramRate + "MHz";
            }
            else
            if (ramManufacturer == "")
            {
                ramInfoLabel.Text = ramSize.Trim() + "MB of Unknown RAM @ " + ramRate + "MHz";
            }
            else
            if (ramRate == "0")
            {
                ramInfoLabel.Text = ramSize.Trim() + "MB of " + ramManufacturer.Trim() + " RAM @ Unknown MHz";
            }
            else
            {
                ramInfoLabel.Text = ramSize.Trim() + "MB of " + ramManufacturer.Trim() + " RAM @ " + ramRate + "MHz";
            }

        }

        public double getCPUUsage()
        {
            cpuCounter.CategoryName = "Processor Information";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            return cpuCounter.NextValue();
        }
        
        public double getCPUCore1Usage()
        {
            cpuCore1.CategoryName = "Processor";
            cpuCore1.CounterName = "% Processor Time";
            cpuCore1.InstanceName = "0";

            return cpuCore1.NextValue();
        }

        public double getCPUCore2Usage()
        {
            cpuCore2.CategoryName = "Processor";
            cpuCore2.CounterName = "% Processor Time";
            cpuCore2.InstanceName = "1";

            return cpuCore2.NextValue();
        }

        public double getCPUCore3Usage()
        {
            cpuCore3.CategoryName = "Processor";
            cpuCore3.CounterName = "% Processor Time";
            cpuCore3.InstanceName = "2";

            return cpuCore3.NextValue();
        }

        public double getCPUCore4Usage()
        {
            cpuCore4.CategoryName = "Processor";
            cpuCore4.CounterName = "% Processor Time";
            cpuCore4.InstanceName = "3";

            return cpuCore4.NextValue();
        }

        public double getCPUCore5Usage()
        {
            cpuCore5.CategoryName = "Processor";
            cpuCore5.CounterName = "% Processor Time";
            cpuCore5.InstanceName = "4";

            return cpuCore5.NextValue();
        }

        public double getCPUCore6Usage()
        {
            cpuCore6.CategoryName = "Processor";
            cpuCore6.CounterName = "% Processor Time";
            cpuCore6.InstanceName = "5";

            return cpuCore6.NextValue();
        }

        public double getCPUCore7Usage()
        {
            cpuCore7.CategoryName = "Processor";
            cpuCore7.CounterName = "% Processor Time";
            cpuCore7.InstanceName = "6";

            return cpuCore7.NextValue();
        }

        public double getCPUCore8Usage()
        {
            cpuCore8.CategoryName = "Processor";
            cpuCore8.CounterName = "% Processor Time";
            cpuCore8.InstanceName = "7";

            return cpuCore8.NextValue();
        }

        public double getRAMUsage()
        {
            ramCounter.CategoryName = "Memory";
            ramCounter.CounterName = "Available MBytes";
            ramCounter.InstanceName = null;

            return ramCounter.NextValue();
        }

        public void InitTimer()
        {
            Timer updateTimer = new Timer();
            updateTimer.Tick += new EventHandler(updateTimer_Tick);
            updateTimer.Interval = 1000;
            updateTimer.Start();
        }

        public void updateTimer_Tick(object sender, EventArgs e)
        {
            double CPUUsage = getCPUUsage();

            cpuPercentLabel.Text = "Overall" + (CPUUsage.ToString("N1")).PadLeft(5) + "%";
            cpuUsageBar.Value = Convert.ToInt32(CPUUsage) * 10;

            double RAMUsage = getRAMUsage();

            double RAMTotal = getRAMSize();

            double currentRAMUsage = 100 - ((RAMUsage / RAMTotal) * 100);

            ramPercentLabel.Text = "Overall " + currentRAMUsage.ToString("N1").PadLeft(5) + "%";
            ramUsageBar.Value = Convert.ToInt32(getRAMSize()) - Convert.ToInt32(RAMUsage);

            if (coreCount == 1)
            {
                double CPUCore1 = getCPUCore1Usage();
                cpuCore1Label.Text = "Core 1: " + (CPUCore1.ToString("N1")).PadLeft(5) + "%";
                cpuCore1Bar.Value = Convert.ToInt32(CPUCore1) * 10;

                cpuCore2Label.Text = "Inactive";
                cpuCore3Label.Text = "Inactive";
                cpuCore4Label.Text = "Inactive";
                cpuCore5Label.Text = "Inactive";
                cpuCore6Label.Text = "Inactive";
                cpuCore7Label.Text = "Inactive";
                cpuCore8Label.Text = "Inactive";
                cpuCore2Bar.Value = 0;
                cpuCore3Bar.Value = 0;
                cpuCore4Bar.Value = 0;
                cpuCore5Bar.Value = 0;
                cpuCore6Bar.Value = 0;
                cpuCore7Bar.Value = 0;
                cpuCore8Bar.Value = 0;
            }
            
            if (coreCount == 2)
            {
                double CPUCore1 = getCPUCore1Usage();
                cpuCore1Label.Text = "Core 1: " + (CPUCore1.ToString("N1")).PadLeft(5) + "%";
                cpuCore1Bar.Value = Convert.ToInt32(CPUCore1) * 10;

                double CPUCore2 = getCPUCore2Usage();
                cpuCore2Label.Text = "Core 2: " + (CPUCore2.ToString("N1")).PadLeft(5) + "%";
                cpuCore2Bar.Value = Convert.ToInt32(CPUCore2) * 10;

                cpuCore3Label.Text = "Inactive";
                cpuCore4Label.Text = "Inactive";
                cpuCore5Label.Text = "Inactive";
                cpuCore6Label.Text = "Inactive";
                cpuCore7Label.Text = "Inactive";
                cpuCore8Label.Text = "Inactive";
                cpuCore3Bar.Value = 0;
                cpuCore4Bar.Value = 0;
                cpuCore5Bar.Value = 0;
                cpuCore6Bar.Value = 0;
                cpuCore7Bar.Value = 0;
                cpuCore8Bar.Value = 0;
            }

            if (coreCount == 3)
            {
                double CPUCore1 = getCPUCore1Usage();
                cpuCore1Label.Text = "Core 1: " + (CPUCore1.ToString("N1")).PadLeft(5) + "%";
                cpuCore1Bar.Value = Convert.ToInt32(CPUCore1) * 10;

                double CPUCore2 = getCPUCore2Usage();
                cpuCore2Label.Text = "Core 2: " + (CPUCore2.ToString("N1")).PadLeft(5) + "%";
                cpuCore2Bar.Value = Convert.ToInt32(CPUCore2) * 10;

                double CPUCore3 = getCPUCore3Usage();
                cpuCore3Label.Text = "Core 3: " + (CPUCore3.ToString("N1")).PadLeft(5) + "%";
                cpuCore3Bar.Value = Convert.ToInt32(CPUCore3) * 10;

                cpuCore4Label.Text = "Inactive";
                cpuCore5Label.Text = "Inactive";
                cpuCore6Label.Text = "Inactive";
                cpuCore7Label.Text = "Inactive";
                cpuCore8Label.Text = "Inactive";
                cpuCore4Bar.Value = 0;
                cpuCore5Bar.Value = 0;
                cpuCore6Bar.Value = 0;
                cpuCore7Bar.Value = 0;
                cpuCore8Bar.Value = 0;
            }

            if (coreCount == 4)
            {
                double CPUCore1 = getCPUCore1Usage();
                cpuCore1Label.Text = "Core 1: " + (CPUCore1.ToString("N1")).PadLeft(5) + "%";
                cpuCore1Bar.Value = Convert.ToInt32(CPUCore1) * 10;

                double CPUCore2 = getCPUCore2Usage();
                cpuCore2Label.Text = "Core 2: " + (CPUCore2.ToString("N1")).PadLeft(5) + "%";
                cpuCore2Bar.Value = Convert.ToInt32(CPUCore2) * 10;

                double CPUCore3 = getCPUCore3Usage();
                cpuCore3Label.Text = "Core 3: " + (CPUCore3.ToString("N1")).PadLeft(5) + "%";
                cpuCore3Bar.Value = Convert.ToInt32(CPUCore3) * 10;

                double CPUCore4 = getCPUCore4Usage();
                cpuCore4Label.Text = "Core 4: " + (CPUCore4.ToString("N1")).PadLeft(5) + "%";
                cpuCore4Bar.Value = Convert.ToInt32(CPUCore4) * 10;

                cpuCore5Label.Text = "Inactive";
                cpuCore6Label.Text = "Inactive";
                cpuCore7Label.Text = "Inactive";
                cpuCore8Label.Text = "Inactive";
                cpuCore5Bar.Value = 0;
                cpuCore6Bar.Value = 0;
                cpuCore7Bar.Value = 0;
                cpuCore8Bar.Value = 0;
            }

            if (coreCount == 5)
            {
                double CPUCore1 = getCPUCore1Usage();
                cpuCore1Label.Text = "Core 1: " + (CPUCore1.ToString("N1")).PadLeft(5) + "%";
                cpuCore1Bar.Value = Convert.ToInt32(CPUCore1) * 10;

                double CPUCore2 = getCPUCore2Usage();
                cpuCore2Label.Text = "Core 2: " + (CPUCore2.ToString("N1")).PadLeft(5) + "%";
                cpuCore2Bar.Value = Convert.ToInt32(CPUCore2) * 10;

                double CPUCore3 = getCPUCore3Usage();
                cpuCore3Label.Text = "Core 3: " + (CPUCore3.ToString("N1")).PadLeft(5) + "%";
                cpuCore3Bar.Value = Convert.ToInt32(CPUCore3) * 10;

                double CPUCore4 = getCPUCore4Usage();
                cpuCore4Label.Text = "Core 4: " + (CPUCore4.ToString("N1")).PadLeft(5) + "%";
                cpuCore4Bar.Value = Convert.ToInt32(CPUCore4) * 10;

                double CPUCore5 = getCPUCore5Usage();
                cpuCore5Label.Text = "Core 5: " + (CPUCore5.ToString("N1")).PadLeft(5) + "%";
                cpuCore5Bar.Value = Convert.ToInt32(CPUCore5) * 10;

                cpuCore6Label.Text = "Inactive";
                cpuCore7Label.Text = "Inactive";
                cpuCore8Label.Text = "Inactive";
                cpuCore6Bar.Value = 0;
                cpuCore7Bar.Value = 0;
                cpuCore8Bar.Value = 0;
            }

            if (coreCount == 6)
            {
                double CPUCore1 = getCPUCore1Usage();
                cpuCore1Label.Text = "Core 1: " + (CPUCore1.ToString("N1")).PadLeft(5) + "%";
                cpuCore1Bar.Value = Convert.ToInt32(CPUCore1) * 10;

                double CPUCore2 = getCPUCore2Usage();
                cpuCore2Label.Text = "Core 2: " + (CPUCore2.ToString("N1")).PadLeft(5) + "%";
                cpuCore2Bar.Value = Convert.ToInt32(CPUCore2) * 10;

                double CPUCore3 = getCPUCore3Usage();
                cpuCore3Label.Text = "Core 3: " + (CPUCore3.ToString("N1")).PadLeft(5) + "%";
                cpuCore3Bar.Value = Convert.ToInt32(CPUCore3) * 10;

                double CPUCore4 = getCPUCore4Usage();
                cpuCore4Label.Text = "Core 4: " + (CPUCore4.ToString("N1")).PadLeft(5) + "%";
                cpuCore4Bar.Value = Convert.ToInt32(CPUCore4) * 10;

                double CPUCore5 = getCPUCore5Usage();
                cpuCore5Label.Text = "Core 5: " + (CPUCore5.ToString("N1")).PadLeft(5) + "%";
                cpuCore5Bar.Value = Convert.ToInt32(CPUCore5) * 10;

                double CPUCore6 = getCPUCore6Usage();
                cpuCore6Label.Text = "Core 6: " + (CPUCore6.ToString("N1")).PadLeft(5) + "%";
                cpuCore6Bar.Value = Convert.ToInt32(CPUCore6) * 10;

                cpuCore7Label.Text = "Inactive";
                cpuCore8Label.Text = "Inactive";
                cpuCore7Bar.Value = 0;
                cpuCore8Bar.Value = 0;
            }

            if (coreCount == 7)
            {
                double CPUCore1 = getCPUCore1Usage();
                cpuCore1Label.Text = "Core 1: " + (CPUCore1.ToString("N1")).PadLeft(5) + "%";
                cpuCore1Bar.Value = Convert.ToInt32(CPUCore1) * 10;

                double CPUCore2 = getCPUCore2Usage();
                cpuCore2Label.Text = "Core 2: " + (CPUCore2.ToString("N1")).PadLeft(5) + "%";
                cpuCore2Bar.Value = Convert.ToInt32(CPUCore2) * 10;

                double CPUCore3 = getCPUCore3Usage();
                cpuCore3Label.Text = "Core 3: " + (CPUCore3.ToString("N1")).PadLeft(5) + "%";
                cpuCore3Bar.Value = Convert.ToInt32(CPUCore3) * 10;

                double CPUCore4 = getCPUCore4Usage();
                cpuCore4Label.Text = "Core 4: " + (CPUCore4.ToString("N1")).PadLeft(5) + "%";
                cpuCore4Bar.Value = Convert.ToInt32(CPUCore4) * 10;

                double CPUCore5 = getCPUCore5Usage();
                cpuCore5Label.Text = "Core 5: " + (CPUCore5.ToString("N1")).PadLeft(5) + "%";
                cpuCore5Bar.Value = Convert.ToInt32(CPUCore5) * 10;

                double CPUCore6 = getCPUCore6Usage();
                cpuCore6Label.Text = "Core 6: " + (CPUCore6.ToString("N1")).PadLeft(5) + "%";
                cpuCore6Bar.Value = Convert.ToInt32(CPUCore6) * 10;

                double CPUCore7 = getCPUCore7Usage();
                cpuCore7Label.Text = "Core 7: " + (CPUCore7.ToString("N1")).PadLeft(5) + "%";
                cpuCore7Bar.Value = Convert.ToInt32(CPUCore7) * 10;

                cpuCore8Label.Text = "Inactive";
                cpuCore8Bar.Value = 0;
            }

            if (coreCount >= 8)
            {
                double CPUCore1 = getCPUCore1Usage();
                cpuCore1Label.Text = "Core 1: " + (CPUCore1.ToString("N1")).PadLeft(5) + "%";
                cpuCore1Bar.Value = Convert.ToInt32(CPUCore1) * 10;

                double CPUCore2 = getCPUCore2Usage();
                cpuCore2Label.Text = "Core 2: " + (CPUCore2.ToString("N1")).PadLeft(5) + "%";
                cpuCore2Bar.Value = Convert.ToInt32(CPUCore2) * 10;

                double CPUCore3 = getCPUCore3Usage();
                cpuCore3Label.Text = "Core 3: " + (CPUCore3.ToString("N1")).PadLeft(5) + "%";
                cpuCore3Bar.Value = Convert.ToInt32(CPUCore3) * 10;

                double CPUCore4 = getCPUCore4Usage();
                cpuCore4Label.Text = "Core 4: " + (CPUCore4.ToString("N1")).PadLeft(5) + "%";
                cpuCore4Bar.Value = Convert.ToInt32(CPUCore4) * 10;

                double CPUCore5 = getCPUCore5Usage();
                cpuCore5Label.Text = "Core 5: " + (CPUCore5.ToString("N1")).PadLeft(5) + "%";
                cpuCore5Bar.Value = Convert.ToInt32(CPUCore5) * 10;

                double CPUCore6 = getCPUCore6Usage();
                cpuCore6Label.Text = "Core 6: " + (CPUCore6.ToString("N1")).PadLeft(5) + "%";
                cpuCore6Bar.Value = Convert.ToInt32(CPUCore6) * 10;

                double CPUCore7 = getCPUCore7Usage();
                cpuCore7Label.Text = "Core 7: " + (CPUCore7.ToString("N1")).PadLeft(5) + "%";
                cpuCore7Bar.Value = Convert.ToInt32(CPUCore7) * 10;

                double CPUCore8 = getCPUCore8Usage();
                cpuCore8Label.Text = "Core 8: " + (CPUCore8.ToString("N1")).PadLeft(5) + "%";
                cpuCore8Bar.Value = Convert.ToInt32(CPUCore8) * 10;
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public int getPhysProcessors()
        {
            int physCount = 0;

            ManagementObjectSearcher physQuery = new ManagementObjectSearcher("Select * FROM Win32_ComputerSystem");

            foreach (ManagementObject mObj in physQuery.Get())
            {
                physCount = Convert.ToInt32(mObj["NumberOfProcessors"]);
            }

            return physCount;
        }

        public int getLogProcessors()
        {
            int logCount = Environment.ProcessorCount;
            return logCount;
        }

        public int getCoreCount()
        {
            int coreCount = 0;

            ManagementObjectSearcher coreQuery = new ManagementObjectSearcher("Select * FROM Win32_Processor");

            foreach (ManagementObject mObj in coreQuery.Get())
            {
                coreCount = Convert.ToInt32(mObj["NumberOfCores"]);
            }

            return coreCount;
        }

        public string getCPUName()
        {
            string CPUName = "";


            ManagementObjectSearcher nameQuery = new ManagementObjectSearcher("Select * FROM Win32_Processor");

            foreach (ManagementObject mObj in nameQuery.Get())
            {
                CPUName = (mObj["Name"]).ToString();
            }

            return CPUName;
        }

        public int getRAMSize()
        {
            ulong ramSizePhys = 0;

            ManagementObjectSearcher ramQuery = new ManagementObjectSearcher("Select * FROM Win32_ComputerSystem");

            foreach (ManagementObject mObj in ramQuery.Get())
            {
                ramSizePhys = Convert.ToUInt64(mObj["TotalPhysicalMemory"]);
            }

            int ramSizeGB = Convert.ToInt32(((ramSizePhys) / 1024) / 1024);

            return ramSizeGB;
        }

        public int getRAMRate()
        {
            int ramRate = 0;
            try {
                ManagementObjectSearcher rateQuery = new ManagementObjectSearcher("Select * FROM Win32_PhysicalMemory");

                foreach (ManagementObject mObj in rateQuery.Get())
                {
                    ramRate = Convert.ToInt32(mObj["ConfiguredClockSpeed"]);
                }

                return ramRate;
            }
            catch
            {
                ramRate = 0;
                return ramRate;
            }
        }

        public string getRAMManufacturer()
        {
            string ramManufacturer = "";

            ManagementObjectSearcher manuQuery = new ManagementObjectSearcher("Select * FROM Win32_PhysicalMemory");

            foreach (ManagementObject mObj in manuQuery.Get())
            {
                ramManufacturer = mObj["Manufacturer"].ToString();
            }

            return ramManufacturer;
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Credits creditsForm = new Credits();
            creditsForm.Show();
        }

        public string getOS()
        {
            string OS = "";

            ManagementObjectSearcher osQuery = new ManagementObjectSearcher("Select * FROM Win32_OperatingSystem");

            foreach (ManagementObject mObj in osQuery.Get())
            {
                OS = mObj["Caption"].ToString();
            }

            return OS != null ? OS : "Unknown Operating System";
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Help helpForm = new Help();
            helpForm.Show();
        }
    }
}
