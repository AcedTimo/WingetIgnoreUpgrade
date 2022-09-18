# WingetIgnoreUpgrade
A small project that enables you to upgrade all available packages on winget while being able to ignore single ones.</br>
Since winget doesnt have a flag to ignore packages from upgrades until current day.</br>
Ive found out myself that for example PyCharm or Discord seem to have some issues when being upgraded via winget.</br>
Sometimes bringing the entire upgrade process to a halt until i kill the setup process manually.</br></br>
As always, if you happen to find any bugs or have suggestions, feel free to open an issue and ill take a look.

# How to use
Execute it however youd like and pass along the IDs of the packages to ignore as arguments.</br>
Example: `WingetUpgrade.exe Discord.Discord Microsoft.WindowsSDK VSCodium.VSCodium`</br>
If you happen to not know the ID of an installed program, simply run `winget list`

# Credits
This project uses slightly modified source code from: https://github.com/basicx-StrgV/WGet.NET</br>
A huge thanks to them, otherwise it would have been a pain for me to handle the output data from winget. :D
