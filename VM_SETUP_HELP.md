This file is a catch-all for (non-proprietary) notes about setting up a Windows system/VM to build the .NET client library.

### General Notes

- If you are starting a VM from scratch, it's recommended you "de-bloat" Windows 10/11. [Chris Titus's utility](https://christitus.com/windows-tool/) is quite effective and well-tested.
- If you are starting a VM from scratch, follow this process:
  - Start up the VM
  - Debloat Windows (optional)
  - Open PowerShell as Administrator and [install Chocolatey](https://docs.chocolatey.org/en-us/choco/setup#install-with-powershell.exe)
  - Install `git` and `make` via Chocolatey: `choco install git make`
  - Open Command Prompt and clone the .NET client library repo: `git clone https://github.com/EasyPost/easypost-csharp.git`
    - Command Prompt should open to your user directory by default, so you do not need to navigate to a different directory first
  - Enter the repo directory: `cd easypost-csharp`
  - Set up your system: `make setup`
- All Batch scripts in the `scripts` folder can be run via the Makefile (e.g. `make setup`, `make prep-release`). You should never need to run any Batch script directly; some are designed with the expectation that you are running them from the root of the repository.
- If you can, don't shut down your VM. Instead, suspend it via "Save Machine State". This will improve startup time and preserve any open applications.
- You can [pass through folders](https://pureinfotech.com/create-shared-folder-virtual-machine-virtualbox/) into your VM to transfer files between your host and guest OS.

### Troubleshooting

#### Windows VM can't find my network drive
- Make sure the folder exists on your host OS *prior* to starting the VM. If you need to create the folder, shut down the VM first and then start it again after creating the folder.
  - If you are passing through folders to files stored in 1Password cache, those files are temporary and may need to be re-cached on your host machine. Navigate to the file in 1Password, click the dropdown arrow next to "Quick Look" and select "Show in Finder" (MacOS). This will re-cache the file and make the folder available to the VM.

#### Text won't paste into the VM

- Try opening Notepad on the Windows VM and pasting into that first. If it works, try pasting into the application you want to use.
- In Command Prompt, you "paste" with a right-click on the mouse. Do not

#### Restoring NuGet packages fails

- **"No packages exist with this id in source(s): Microsoft Visual Studio Offline Packages"**
  - You need to add nuget.org as a package source.
    - Via Visual Studio:
      - Open Visual Studio -> Debug -> Options -> NuGet Package Manager -> General. Click "Clear All NuGet Cache(s)".
      - Click "Package Sources" and add `https://api.nuget.org/v3/index.json` as a package source.
    - Via the command line:
      - Open a command prompt and run `nuget sources add -Name nuget.org -Source https://api.nuget.org/v3/index.json`
  - The scripts in this repository are configured to use the `nuget.config` file in the root of the repository, which should avoid this issue.
    - If you would like to use this config file, include `-configFile nuget.config` in your `nuget` command.
