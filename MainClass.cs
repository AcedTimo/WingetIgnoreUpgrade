using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using WGetNET;

// This project uses slightly modified source code from: https://github.com/basicx-StrgV/WGet.NET
// A huge thanks to them, otherwise it would have been a pain for me to handle the output data from winget. :D

namespace WingetUpgrade
{
    internal class MainClass
    {
        static bool skipUpdate()
        {
            try
            {
                Process[] wingetProcesses = Process.GetProcessesByName("winget");
                foreach (Process process in wingetProcesses)
                {
                    process.Kill();
                }

                return true;
            } catch { return false; }
        }

        static void Main(string[] args)
        {
            Console.CancelKeyPress += delegate(object? sender, ConsoleCancelEventArgs e) {
                if (skipUpdate()) { Console.WriteLine("[i] User is skipping package.."); }
                e.Cancel = true;
            };

            WinGetPackageManager packageManager = new WinGetPackageManager();
            
            if (!packageManager.WinGetInstalled)
            {
                Console.WriteLine("[!] Winget is not installed, quitting");
                Environment.Exit(1);
            }

            Console.Clear();
            Console.WriteLine("[i] Winget Version: " + packageManager.WinGetVersion + "\n");

            List <WinGetPackage> upgradablePackages = packageManager.GetUpgradeablePackages();

            foreach (WinGetPackage package in upgradablePackages)
            {
                string packageName = Regex.Replace(package.PackageName, @"[^\u0000-\u007F]+", string.Empty).Trim();
                string packageID = Regex.Replace(package.PackageId, @"[^\u0000-\u007F]+", string.Empty).Trim();
                string packageVersion = Regex.Replace(package.PackageVersion, @"[^\u0000-\u007F]+", string.Empty).Trim();

                Console.WriteLine("Name   : " + packageName);
                Console.WriteLine("ID     : " + packageID);
                Console.WriteLine("Version: " + packageVersion + "\n");

                bool ignorePackage = false;
                foreach (string _packageID in args)
                {
                    if (package.PackageId.Contains(_packageID)) 
                    {
                        Console.WriteLine("[i] Ignoring package..");
                        ignorePackage = true;
                        break;
                    }
                }

                if (!ignorePackage)
                {
                    Console.WriteLine("[i] Attempting Upgrade..");
                    ProcessResult upgradeResult = packageManager.UpgradePackage(Regex.Replace(package.PackageId, @"[^\u0000-\u007F]+", string.Empty).Trim());
                    if (upgradeResult.Success) { Console.WriteLine("[i] Upgrade Successful"); }
                    else
                    {
                        if (upgradeResult.ExitCode != -1) // Possibly skip on: -2147467260
                        {
                            Console.WriteLine("[!] Upgrade Unsuccessful");
                            Console.WriteLine("    Exit Code: " + upgradeResult.ExitCode);
                            Console.WriteLine("    Output: ");

                            foreach (string line in upgradeResult.Output)
                            {
                                Console.WriteLine(line);
                            }
                        }
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("\n[i] Process Complete");
        }
    }
}
