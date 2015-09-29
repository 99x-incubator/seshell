using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace SeShell.Test.Core
{
    public sealed class ExcelReader
    {
        public static List<IXLCells> ReadFromExcelFile(string fileName, string workSheetName, string leadCell)
        {
            var rowCellList = new List<IXLCells>();
            var wb = new XLWorkbook(fileName);
            var ws = wb.Worksheet(workSheetName);
            // Look for the first row used
            var firstRowUsed = ws.FirstRowUsed();
            int rowCount = 0;
            // Narrow down the row so that it only includes the used part
            var categoryRow = firstRowUsed.RowUsed();

            while (!categoryRow.Cell(1).IsEmpty())
            {
                string categoryName = categoryRow.Cell(1).GetString();
                Console.WriteLine(categoryName + "\n" + categoryRow.Cells().Count());
                rowCellList.Add(categoryRow.Cells());
                categoryRow = categoryRow.RowBelow();
                rowCount++;
            }


            return rowCellList;
        }
    }
}
