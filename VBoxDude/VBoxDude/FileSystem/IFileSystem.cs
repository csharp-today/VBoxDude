namespace VBoxDude.FileSystem
{
    internal interface IFileSystem
    {
        bool FileExists(string filePath);
    }
}