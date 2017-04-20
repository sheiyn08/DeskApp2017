

using DeskApp.Data;
using DeskApp.DataLayer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

//using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections;
using System.IO;
using System.Linq;


using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace DeskApp.Controllers
{

    public partial class ExcelFileResult : Controller
    {

        private readonly ApplicationDbContext db;


        public ExcelFileResult(ApplicationDbContext context)
        {
            db = context;

        }


        //[HttpPost]
        //public int perception_survey(AngularFilterModel item)
        //{
        //    var controller = DependencyResolver.Current.GetService<PerceptionSurveyController>();

        //    var model = controller.GetData(item);

        //    var result = from data in model
        //                 select new
        //                 {
                       

        //                     Office_Level = data.current_position_id != null ? data.current_position.lib_position.lib_office_level.name : "",
                             




        //                 };

        //    DataTable dto = result.ToList().ToDataTable();

        //    string FileName = "General Staffing - " + Guid.NewGuid().ToString() + ".xls";

        //    ExportToExcel(dto, FileName);

        //    int excel_id = accessrepository.LogDownload(FileName, "xls");

        //    return excel_id;
        //}



        //private object _model;
        //public ExcelFileResult(object model) :
        //    base("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        //{
        //    _model = model;
        //}




        //protected override Task WriteFileAsync(HttpResponse response)
        //{

        //    //   CancellationToken cancellation;


        //    var enumerable = _model as System.Collections.IEnumerable;
        //    if (enumerable == null)
        //    {
        //        throw new ArgumentException("IEnumerable type required");
        //    }

        //    byte[] FileContents = null;
        //    using (MemoryStream mem = new MemoryStream())
        //    {
        //        using (var workbook = SpreadsheetDocument.Create(mem, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
        //        {
        //            var workbookPart = workbook.AddWorkbookPart();
        //            workbook.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();
        //            workbook.WorkbookPart.Workbook.Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets();
        //            var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
        //            var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
        //            sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData);

        //            DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>();
        //            string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

        //            uint sheetId = 1;
        //            if (sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Count() > 0)
        //            {
        //                sheetId = sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Select(s => s.SheetId.Value).Max() + 1;
        //            }

        //            DocumentFormat.OpenXml.Spreadsheet.Sheet sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet() { Id = relationshipId, SheetId = sheetId, Name = "Sheet1" };
        //            sheets.Append(sheet);

        //            DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

        //            List<String> columns = new List<string>();

        //            var properties = GetBaseTypeOfEnumerable(enumerable).GetProperties();

        //            //   var properties = typeof(enumurable).GetProperties();

        //            foreach (var property in properties)
        //            {
        //                columns.Add(property.Name);

        //                DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
        //                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
        //                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(property.Name);
        //                headerRow.AppendChild(cell);
        //            }

        //            sheetData.AppendChild(headerRow);

        //            foreach (var item in enumerable)
        //            {
        //                DocumentFormat.OpenXml.Spreadsheet.Row newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

        //                foreach (var header in properties)
        //                {
        //                    DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
        //                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;

        //                    var value = header.GetValue(item);
        //                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(value?.ToString()); //
        //                    newRow.AppendChild(cell);
        //                }
        //                sheetData.AppendChild(newRow);
        //            }

        //            sheetPart.Worksheet.Save();
        //            workbook.WorkbookPart.Workbook.Save();
        //            workbook.Close();
        //            FileContents = mem.ToArray();
        //            return response.Body.WriteAsync(FileContents, 0, FileContents.Length);
        //        }
        //    }

        //}


        //public Type GetBaseTypeOfEnumerable(IEnumerable enumerable)
        //{
        //    if (enumerable == null)
        //    {
        //        //you'll have to decide what to do in this case
        //    }

        //    var genericEnumerableInterface = enumerable
        //        .GetType()
        //        .GetInterfaces()
        //        .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

        //    if (genericEnumerableInterface == null)
        //    {
        //        //If we're in this block, the type implements IEnumerable, but not IEnumerable<T>;
        //        //you'll have to decide what to do here.

        //        //Justin Harvey's (now deleted) answer suggested enumerating the 
        //        //enumerable and examining the type of its elements; this 
        //        //is a good idea, but keep in mind that you might have a
        //        //mixed collection.
        //    }

        //    var elementType = genericEnumerableInterface.GetGenericArguments()[0];
        //    return elementType.IsGenericType && elementType.GetGenericTypeDefinition() == typeof(Nullable<>)
        //        ? elementType.GetGenericArguments()[0]
        //        : elementType;
        //}
    }
}

