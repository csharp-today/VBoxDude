namespace VBoxDude.VM
{
    internal interface IVirtualMachineImporter
    {
        VirtualMachine Import(string filePath, string newMachineName);
    }
}