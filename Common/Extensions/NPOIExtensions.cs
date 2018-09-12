using NPOI.SS.UserModel;

namespace Common.Extensions
{
    public static class NPOIExtensions
    {
        public static ICell CreateCell(this ISheet sheet, int row, int column)
        {
            return sheet.GetRow(row).CreateCell(column);
        }

        public static void SetCellValue(this ISheet sheet, int rowIndex, int column, string value)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(column) ?? row.CreateCell(column);
            cell.SetCellValue(value);
        }

        public static void SetCellValue(this ISheet sheet, int rowIndex, int column, double value)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(column) ?? row.CreateCell(column);
            cell.SetCellValue(value);
        }
    }
}
