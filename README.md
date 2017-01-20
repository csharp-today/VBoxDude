# VBoxDude

Version 0.0.0.3 - Import virtual machine from file

```c#
new VirtualBoxDude().VMFactory.ImportFromFile(@"C:\Some-OS-image.ova", "NewVirtualMachineName");
```

Version 0.0.0.2 - Functionality to start a VM

```c#
new VirtualBoxDude().VMFactory.CreateFromName("VirtualMachineName").Start();
```
