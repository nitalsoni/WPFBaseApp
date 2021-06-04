using DataModel;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using D = System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace RFIDApp.Helpers
{
    public class ExcelOps
    {
        public static Application ExportToExcel<T>(ObservableCollection<T> records, string path)
        {
            if (records.Count > 0)
            {
                D.DataTable dataTable = ConvertToDataTable<T>(records);
                return GenerateExcel(dataTable, path);
            }

            return null;
        }

        private static D.DataTable ConvertToDataTable<T>(ObservableCollection<T> models)
        {
            D.DataTable dataTable = new D.DataTable(typeof(T).Name);

            //Get all the properties of that model  
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Loop through all the properties and add Column name to our datatable
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }

            // Adding Row and its value to our dataTable  
            foreach (T item in models)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        private static Application GenerateExcel(D.DataTable dataTable, string path)
        {
            D.DataSet dataSet = new D.DataSet();
            dataSet.Tables.Add(dataTable);

            // create a excel app along side with workbook and worksheet and give a name to it  
            Application excelApp = new Application();
            Workbook excelWorkBook = excelApp.Workbooks.Add();
            _Worksheet xlWorksheet = (_Worksheet)excelWorkBook.Sheets[1];
            Range xlRange = xlWorksheet.UsedRange;
            foreach (D.DataTable table in dataSet.Tables)
            {
                //Add a new worksheet to workbook with the Datatable name  
                Worksheet excelWorkSheet = (Worksheet)excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;

                // add all the columns  
                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                // add all the rows  
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }
            xlWorksheet.Columns.AutoFit();
            xlWorksheet.Rows.AutoFit();
            return excelApp;
        }
    }
}
