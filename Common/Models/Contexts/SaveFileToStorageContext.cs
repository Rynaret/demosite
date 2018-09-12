using Common.Conventions.Commands;

namespace Common.Models.Contexts
{
    public class SaveFileToStorageContext : ICommandContext
    {
        public SaveFileToStorageContext(byte[] data, string directory, string extension)
        {
            Data = data;
            Directory = directory;
            Extension = extension;
        }

        public byte[] Data { get; }
        public string Directory { get; }
        public string Extension { get; }

        public string FilePathAfterSave { get; set; }
    }
}
