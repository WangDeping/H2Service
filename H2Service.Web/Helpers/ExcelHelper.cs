using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace H2Service.Web.Helpers
{
    public class ExcelHelper
    {
        public static void ExportEasy<T>(List<T> dtSource, string strFileName,string[] headerArrary)
        {
            var dataTable = ConvertHelper.ListToTable(dtSource);
            var workbook = new XSSFWorkbook();
           var sheet = workbook.CreateSheet();
            
            //填充表头
           var  dataRow = sheet.CreateRow(0);
            for(int i=0;i<headerArrary.Length;i++)
                dataRow.CreateCell(i).SetCellValue(headerArrary[i]);

            //填充内容
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dataRow = sheet.CreateRow(i + 1);
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    dataRow.CreateCell(j).SetCellValue(dataTable.Rows[i][j].ToString());
                }
            }

            //保存
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                  
                }
            
            }
          
        }
    }
}