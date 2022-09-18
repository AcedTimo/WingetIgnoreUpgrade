using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WGetNET;

// This project uses slightly modified source code from: https://github.com/basicx-StrgV/WGet.NET
// A huge thanks to them, otherwise it would have been a pain for me to handle the output data from winget. :D

namespace WingetUpgrade
{
    internal class MainClass
    {
        static void Main(string[] args)
        {
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
                        Console.WriteLine("[!] Upgrade Unsuccessful");
                        Console.WriteLine("    Exit Code: " + upgradeResult.ExitCode);
                        Console.WriteLine("    Output: ");

                        foreach (string line in upgradeResult.Output)
                        {
                            Console.WriteLine(line);
                        }
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("\n[i] Process Complete");
        }
    }
}
