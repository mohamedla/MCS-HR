using MCSHR.Models;
using System;
using System.Data;
using System.IO;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MCSHR.Context;

namespace MCSHR.BussinessLayer
{
    public static class FilesHandler
    {
        private static string mainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
        public static void UploadFile(ref UploadFileModal fileModla)
        {
            if(!Directory.Exists(mainPath))
                Directory.CreateDirectory(mainPath);

            FileInfo fileInfo = new FileInfo(fileModla.File.FileName);

            string fileName = fileModla.File.FileName;

            string fileNameWithPath = Path.Combine(mainPath, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                fileModla.File.CopyTo(stream);
            }

            fileModla.IsSuccess = true;
            fileModla.Message = "File Upload Successfully";
        }

        private static DateTime ConvertStringToDateTime(string dateString)
        {
            DateTime date;
            DateTime.TryParseExact(dateString, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            return date;
        }

        public static void UploadEmployeeFromFile(string fileName, bool hasHeader, RepositoryContext repository)
        {
            DataTable datatable = FilesHandler.ReadTabSeparatedFile(fileName, hasHeader);

            IEnumerable<Employee> Employees = (from DataRow dr in datatable.Rows
                                               select new Employee()
                                               {
                                                   Name = dr["Column1"].ToString(),
                                                   Address = dr["Column2"].ToString(),
                                                   Birthday = FilesHandler.ConvertStringToDateTime(dr["Column3"].ToString()),
                                                   Graduation = dr["Column4"].ToString(),
                                                   Emp_Type = dr["Column5"].ToString() == "Free lancer" ? EmployeeTypes.FreeLancer : dr["Column5"].ToString() == "Monthly payroll" ? EmployeeTypes.Monthly : dr["Column5"].ToString() == "Hourly Payroll" ? EmployeeTypes.Hourly : EmployeeTypes.FreeLancer,
                                                   Assurance = dr["Column6"].ToString().Contains("Assurance") ? true : false
                                               }).ToList();
            repository.employees.AddRangeAsync(Employees);
            repository.SaveChanges();
        }

        public static DataTable ReadTabSeparatedFile(string fileName, bool hasHeader)
        {
            DataTable datatable = new DataTable();
            using (StreamReader streamreader = new StreamReader(Path.Combine(mainPath, fileName)))
            {
                char[] delimiter = new char[] { '\t' };
                string[] firstDataLine = streamreader.ReadLine().Split(delimiter);
                

                if (hasHeader)
                {
                    foreach (string columnheader in firstDataLine)
                    {
                        datatable.Columns.Add(columnheader);
                    }
                }
                else
                {
                    for (int i = 0; i < firstDataLine.Length; i++)
                    {
                        datatable.Columns.Add("Column" + (i + 1));
                    }
                }

                if (!hasHeader)
                {
                    DataRow firstDataRow = datatable.NewRow();
                    firstDataRow.ItemArray = firstDataLine;
                    datatable.Rows.Add(firstDataRow);
                }

                while (streamreader.Peek() > 0)
                {
                    DataRow datarow = datatable.NewRow();
                    datarow.ItemArray = streamreader.ReadLine().Split(delimiter);
                    datatable.Rows.Add(datarow);
                }
            }

            return datatable;
        }
    }
}
