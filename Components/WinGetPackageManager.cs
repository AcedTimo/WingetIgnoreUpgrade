//--------------------------------------------------//
// Created by basicx-StrgV                          //
// https://github.com/basicx-StrgV/                 //
//--------------------------------------------------//
using System;
using System.ComponentModel;
using System.Collections.Generic;
using WGetNET.HelperClasses;

namespace WGetNET
{
    /// <summary>
    /// The <see cref="WGetNET.WinGetPackageManager"/> class offers methods to manage packages with winget.
    /// </summary>
    public class WinGetPackageManager : WinGetInfo
    {
        private const string _listCmd = "list";
        private const string _searchCmd = "search {0} --accept-source-agreements";
        private const string _installCmd = "install {0}";
        private const string _upgradeCmd = "upgrade {0}";
        private const string _getUpgradeableCmd = "upgrade";
        private const string _uninstallCmd = "uninstall {0}";
        private const string _exportCmd = "export -o {0}";
        private const string _importCmd = "import -i {0} --ignore-unavailable";

        private readonly ProcessManager _processManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="WGetNET.WinGetPackageManager"/> class.
        /// </summary>
        public WinGetPackageManager()
        {
            _processManager = new ProcessManager();
        }

        //---Search------------------------------------------------------------------------------------
        /// <summary>
        /// Uses the winget search function to search for a package that maches the given name.
        /// </summary>
        /// <param name="packageName">The name of the package for the search.</param>
        /// <returns>
        /// A <see cref="System.Collections.Generic.List{T}"/> of <see cref="WGetNET.WinGetPackage"/> instances.
        /// </returns>
        /// <exception cref="WGetNET.WinGetNotInstalledException">
        /// WinGet is not installed or not found on the system.
        /// </exception>
        /// <exception cref="WGetNET.WinGetActionFailedException">
        /// The current action failed for an unexpected reason.
        /// Please see inner exception.
        /// </exception>
        public List<WinGetPackage> SearchPackage(string packageName)
        {
            try
            {
                ProcessResult result =
                    _processManager.ExecuteWingetProcess(
                        string.Format(_searchCmd, packageName));

                return ProcessOutputReader.ToPackageList(result.Output);
            }
            catch (Win32Exception)
            {
                throw new WinGetNotInstalledException();
            }
            catch (Exception e)
            {
                throw new WinGetActionFailedException("The package search failed.", e);
            }
        }
        //---------------------------------------------------------------------------------------------

        //---Install-----------------------------------------------------------------------------------
        /// <summary>
        /// Gets a list of all installed packages.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Collections.Generic.List{T}"/> of <see cref="WGetNET.WinGetPackage"/> instances.
        /// </returns>
        /// <exception cref="WGetNET.WinGetNotInstalledException">
        /// WinGet is not installed or not found on the system.
        /// </exception>
        /// <exception cref="WGetNET.WinGetActionFailedException">
        /// The current action failed for an unexpected reason.
        /// Please see inner exception.
        /// </exception>
        public List<WinGetPackage> GetInstalledPackages()
        {
            try
            {
                ProcessResult result =
                    _processManager.ExecuteWingetProcess(_listCmd);

                return ProcessOutputReader.ToPackageList(result.Output);
            }
            catch (Win32Exception)
            {
                throw new WinGetNotInstalledException();
            }
            catch (Exception e)
            {
                throw new WinGetActionFailedException("The search of installed packages failed.", e);
            }
        }

        /// <summary>
        /// Install a package using winget.
        /// </summary>
        /// <param name="packageId">The id or name of the package for the installation.</param>
        /// <returns>
        /// <see langword="true"/> if the installation was successfull or <see langword="false"/> if it failed.
        /// </returns>
        /// <exception cref="WGetNET.WinGetNotInstalledException">
        /// WinGet is not installed or not found on the system.
        /// </exception>
        /// <exception cref="WGetNET.WinGetActionFailedException">
        /// The current action failed for an unexpected reason.
        /// Please see inner exception.
        /// </exception>
        public bool InstallPackage(string packageId)
        {
            try
            {
                ProcessResult result =
                    _processManager.ExecuteWingetProcess(
                        string.Format(_installCmd, packageId));

                return result.Success;
            }
            catch (Win32Exception)
            {
                throw new WinGetNotInstalledException();
            }
            catch (Exception e)
            {
                throw new WinGetActionFailedException("The package installtion failed.", e);
            }
        }

        /// <summary>
        /// Install a package using winget.
        /// </summary>
        /// <param name="package">The <see cref="WGetNET.WinGetPackage"/> for the installation.</param>
        /// <returns>
        /// <see langword="true"/> if the installation was successfull or <see langword="false"/> if it failed.
        /// </returns>
        /// <exception cref="WGetNET.WinGetNotInstalledException">
        /// WinGet is not installed or not found on the system.
        /// </exception>
        /// <exception cref="WGetNET.WinGetActionFailedException">
        /// The current action failed for an unexpected reason.
        /// Please see inner exception.
        /// </exception>
        public bool InstallPackage(WinGetPackage package)
        {
            try
            {
                ProcessResult result =
                    _processManager.ExecuteWingetProcess(
                        string.Format(_installCmd, package.PackageId));

                return result.Success;
            }
            catch (Win32Exception)
            {
                throw new WinGetNotInstalledException();
            }
            catch (Exception e)
            {
                throw new WinGetActionFailedException("The package installtion failed.", e);
            }
        }
        //---------------------------------------------------------------------------------------------

        //---Uninstall---------------------------------------------------------------------------------
        /// <summary>
        /// Uninsatll a package using winget.
        /// </summary>
        /// <param name="packageId">The id or name of the package for uninstallation.</param>
        /// <returns>
        /// <see langword="true"/> if the uninstallation was successfull or <see langword="false"/> if it failed.
        /// </returns>
        /// <exception cref="WGetNET.WinGetNotInstalledException">
        /// WinGet is not installed or not found on the system.
        /// </exception>
        /// <exception cref="WGetNET.WinGetActionFailedException">
        /// The current action failed for an unexpected reason.
        /// Please see inner exception.
        /// </exception>
        public bool UninstallPackage(string packageId)
        {
            try
            {
                ProcessResult result =
                    _processManager.ExecuteWingetProcess(
                        string.Format(_uninstallCmd, packageId));

                return result.Success;
            }
            catch (Win32Exception)
            {
                throw new WinGetNotInstalledException();
            }
            catch (Exception e)
            {
                throw new WinGetActionFailedException("The package uninstalltion failed.", e);
            }
        }

        /// <summary>
        /// Uninsatll a package using winget.
        /// </summary>
        /// <param name="package">The <see cref="WGetNET.WinGetPackage"/> for the uninstallation.</param>
        /// <returns>
        /// <see langword="true"/> if the uninstallation was successfull or <see langword="false"/> if it failed.
        /// </returns>
        /// <exception cref="WGetNET.WinGetNotInstalledException">
        /// WinGet is not installed or not found on the system.
        /// </exception>
        /// <exception cref="WGetNET.WinGetActionFailedException">
        /// The current action failed for an unexpected reason.
        /// Please see inner exception.
        /// </exception>
        public bool UninstallPackage(WinGetPackage package)
        {
            try
            {
                ProcessResult result =
                    _processManager.ExecuteWingetProcess(
                        string.Format(_uninstallCmd, package.PackageId));

                return result.Success;
            }
            catch (Win32Exception)
            {
                throw new WinGetNotInstalledException();
            }
            catch (Exception e)
            {
                throw new WinGetActionFailedException("The package uninstalltion failed.", e);
            }
        }
        //---------------------------------------------------------------------------------------------

        //---Upgrade-----------------------------------------------------------------------------------
        /// <summary>
        /// Get all upgradeable packages.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Collections.Generic.List{T}"/> of <see cref="WGetNET.WinGetPackage"/> instances.
        /// </returns>
        /// <exception cref="WGetNET.WinGetNotInstalledException">
        /// WinGet is not installed or not found on the system.
        /// </exception>
        /// <exception cref="WGetNET.WinGetActionFailedException">
        /// The current action failed for an unexpected reason.
        /// Please see inner exception.
        /// </exception>
        public List<WinGetPackage> GetUpgradeablePackages()
        {
            ProcessResult result =
                    _processManager.ExecuteWingetProcess(_getUpgradeableCmd);

            return ProcessOutputReader.ToPackageList(result.Output);

            /*
            try
            {
                ProcessResult result =
                    _processManager.ExecuteWingetProcess(_getUpgradeableCmd);

                return ProcessOutputReader.ToPackageList(result.Output);
            }
            catch (Win32Exception)
            {
                throw new WinGetNotInstalledException();
            }
            catch (Exception e)
            {
                throw new WinGetActionFailedException("Getting updateable packages failed.", e);
            }*/
        }

        /// <summary>
        /// Upgrades a package using winget.
        /// </summary>
        /// <param name="packageId">The id or name of the package for upgrade.</param>
        /// <returns>
        /// <see langword="true"/> if the upgrade was successfull or <see langword="false"/> if it failed.
        /// </returns>
        /// <exception cref="WGetNET.WinGetNotInstalledException">
        /// WinGet is not installed or not found on the system.
        /// </exception>
        /// <exception cref="WGetNET.WinGetActionFailedException">
        /// The current action failed for an unexpected reason.
        /// Please see inner exception.
        /// </exception>
        public ProcessResult UpgradePackage(string packageId)
        {
            try
            {
                ProcessResult result =
                    _processManager.ExecuteWingetProcess(
                        string.Format(_upgradeCmd, packageId));

                return result;
            }
            catch (Win32Exception)
            {
                throw new WinGetNotInstalledException();
            }
            catch (Exception e)
            {
                throw new WinGetActionFailedException("Upgrading the package failed.", e);
            }
        }

        /// <summary>
        /// Upgrades a package using winget.
        /// </summary>
        /// <param name="package">The <see cref="WGetNET.WinGetPackage"/> that for the upgrade</param>
        /// <returns>
        /// <see langword="true"/> if the upgrade was successfull or <see langword="false"/> if it failed.
        /// </returns>
        /// <exception cref="WGetNET.WinGetNotInstalledException">
        /// WinGet is not installed or not found on the system.
        /// </exception>
        /// <exception cref="WGetNET.WinGetActionFailedException">
        /// The current action failed for an unexpected reason.
        /// Please see inner exception.
        /// </exception>
        public bool UpgradePackage(WinGetPackage package)
        {
            try
            {
                ProcessResult result =
                    _processManager.ExecuteWingetProcess(
                        string.Format(_upgradeCmd, package.PackageId));

                return result.Success;
            }
            catch (Win32Exception)
            {
                throw new WinGetNotInstalledException();
            }
            catch (Exception e)
            {
                throw new WinGetActionFailedException("Upgrading the package failed.", e);
            }
        }
        //---------------------------------------------------------------------------------------------

        //---Other------------------------------------------------------------------------------------
        /// <summary>
        /// Exports a list of all installed winget packages as json to the given file.
        /// </summary>
        /// <param name="file">The file for the export.</param>
        /// <returns>
        /// <see langword="true"/> if the export was successfull or <see langword="false"/> if it failed.
        /// </returns>
        /// <exception cref="WGetNET.WinGetNotInstalledException">
        /// WinGet is not installed or not found on the system.
        /// </exception>
        /// <exception cref="WGetNET.WinGetActionFailedException">
        /// The current action failed for an unexpected reason.
        /// Please see inner exception.
        /// </exception>
        public bool ExportPackagesToFile(string file)
        {
            try
            {
                ProcessResult result =
                    _processManager.ExecuteWingetProcess(
                        string.Format(_exportCmd, file));

                return result.Success;
            }
            catch (Win32Exception)
            {
                throw new WinGetNotInstalledException();
            }
            catch (Exception e)
            {
                throw new WinGetActionFailedException("Exporting packages failed.", e);
            }
        }

        /// <summary>
        /// Imports packages and trys to installes/upgrade all pakages in the list, if possible.
        /// </summary>
        /// <remarks>
        /// This may take some time and winget may not install/upgrade all packages.
        /// </remarks>
        /// <param name="file">The file with the package data for the import.</param>
        /// <returns>
        /// <see langword="true"/> if the import was compleatly successfull or 
        /// <see langword="false"/> if some or all packages failed to install.
        /// </returns>
        /// <exception cref="WGetNET.WinGetNotInstalledException">
        /// WinGet is not installed or not found on the system.
        /// </exception>
        /// <exception cref="WGetNET.WinGetActionFailedException">
        /// The current action failed for an unexpected reason.
        /// Please see inner exception.
        /// </exception>
        public bool ImportPackagesFromFile(string file)
        {
            try
            {
                ProcessResult result =
                    _processManager.ExecuteWingetProcess(
                        string.Format(_importCmd, file));

                return result.Success;
            }
            catch (Win32Exception)
            {
                throw new WinGetNotInstalledException();
            }
            catch (Exception e)
            {
                throw new WinGetActionFailedException("Importing packages failed.", e);
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
