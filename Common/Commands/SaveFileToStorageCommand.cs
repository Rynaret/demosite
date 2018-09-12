using Common.Conventions.Commands;
using Common.Models.Contexts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Common.Commands
{
    public class SaveFileToStorageCommand : ICommand<SaveFileToStorageContext>
    {
        public async Task ExecuteAsync(SaveFileToStorageContext commandContext)
        {
            var directory = commandContext.Directory;

            if (Directory.Exists(directory) == false)
            {
                Directory.CreateDirectory(directory);
            }

            var guid = Guid.NewGuid().ToString();

            var separator = Path.DirectorySeparatorChar;
            var pathToDirectory = Path.Combine(Directory.GetCurrentDirectory(), directory);

            var fileName = $"{guid}{commandContext.Extension}";
            var filePath = $@"{pathToDirectory}{separator}{fileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await new MemoryStream(commandContext.Data).CopyToAsync(stream);
            }
            commandContext.FilePathAfterSave = $@"{directory}{separator}{fileName}";
        }
    }
}
