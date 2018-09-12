using Common.Constants;
using Common.Conventions.Commands;
using Common.Extensions;
using Common.Models.Contexts;
using NPOI.XSSF.UserModel;
using Reports.Models.Contexts;
using System.IO;
using System.Threading.Tasks;

namespace Reports.Commands
{
    public class GenerateExcelReportCommand : ICommand<GenerateExcelReportContext>
    {
        private readonly ICommandBuilder commandBuilder;

        public GenerateExcelReportCommand(ICommandBuilder commandBuilder)
        {
            this.commandBuilder = commandBuilder;
        }

        public async Task ExecuteAsync(GenerateExcelReportContext commandContext)
        {
            var workBook = new XSSFWorkbook();
            var sheet = workBook.CreateSheet("Report");

            var headerStyle = workBook.CreateCellStyle();
            var headerFont = workBook.CreateFont();
            headerFont.IsBold = true;

            var headIndex = 0;
            foreach (var columnInfo in commandContext.Metadata)
            {
                sheet.SetCellValue(0, headIndex, columnInfo.Name);
                var cell = sheet.GetRow(0).GetCell(headIndex);
                cell.CellStyle = headerStyle;
                cell.CellStyle.SetFont(headerFont);
                headIndex++;
            }

            var cellStyle = workBook.CreateCellStyle();
            var row = 1;
            foreach (var item in commandContext.Data)
            {
                var column = 0;
                foreach (var property in item.GetType().GetProperties())
                {
                    var value = property.GetValue(item);
                    var dataFormatCustom = workBook.CreateDataFormat();
                    switch (value)
                    {
                        case double doubleValue:
                            sheet.SetCellValue(row, column, doubleValue);
                            break;
                        default:
                            sheet.SetCellValue(row, column, value?.ToString());
                            break;
                    }
                    column++;
                }
                column = 0;
                row++;
            }

            sheet.GetRow(0).Cells.ForEach(x => sheet.AutoSizeColumn(x.ColumnIndex));

            byte[] data;
            using (var stream = new MemoryStream())
            {
                workBook.Write(stream);
                data = stream.ToArray();
            }

            var saveFileContext = new SaveFileToStorageContext(data, FolderConstants.ReportsStorage, ".xls");
            await commandBuilder.ExecuteAsync(saveFileContext);

            commandContext.GeneratedFilePath = saveFileContext.FilePathAfterSave;
        }
    }
}
