using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System.Data;

namespace Diploma.Models
{
    public class ExcelToJsonParser
    {
        public static string Parse(string fileName)
        {
            try
            {
                List<DataTable> list = new List<DataTable>();
                Console.WriteLine(fileName);
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, false))
                {
                    WorkbookPart? workbookPart = document.WorkbookPart;
                    Sheets? sheets = workbookPart?.Workbook.GetFirstChild<Sheets>();
                    if (sheets is not null && workbookPart is not null)
                    {
                        foreach (Sheet sheet in sheets.OfType<Sheet>())
                        {
                            DataTable dataTable = new DataTable();
                            Worksheet? worksheet = ((WorksheetPart)workbookPart.GetPartById(sheet.Id)).Worksheet;
                            SheetData? sheetData = worksheet.GetFirstChild<SheetData>();
                            for (int i = 0; i < sheetData?.ChildElements.Count; i++)
                            {
                                List<string> rowData = new List<string>();
                                for (int j = 0; j < sheetData.ElementAt(i).ChildElements.Count; j++)
                                {
                                    Cell cell = (Cell)sheetData.ElementAt(i).ChildElements.ElementAt(j);
                                    if (cell.DataType is not null)
                                    {
                                        if (cell.DataType == CellValues.SharedString)
                                        {
                                            int id;
                                            if (Int32.TryParse(cell.InnerText, out id))
                                            {
                                                SharedStringItem? item = workbookPart?.SharedStringTablePart?.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
                                                if (item?.Text is not null)
                                                {
                                                    if (i == 0)
                                                    {
                                                        dataTable.Columns.Add(item.Text.Text);
                                                    }
                                                    else
                                                    {
                                                        rowData.Add(item.Text.Text);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (i != 0)
                                    {
                                        rowData.Add(cell.InnerText);
                                    }
                                }
                                if (i != 0)
                                {
                                    dataTable.Rows.Add(rowData.ToArray());
                                }
                            }
                            list.Add(dataTable);
                        }
                    }
                    return JsonConvert.SerializeObject(list);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }
    }
}
