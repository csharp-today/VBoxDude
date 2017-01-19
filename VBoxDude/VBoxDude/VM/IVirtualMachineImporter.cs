namespace VBoxDude.VM
{
    public interface IVirtualMachineImporter
    {
        VirtualMachine Import(string filePath, string newMachineName);
    }
}