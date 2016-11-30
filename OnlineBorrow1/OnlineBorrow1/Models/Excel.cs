using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;


namespace OnlineBorrow1.Models
{
    public class Excel1
    {
        [STAThread]
        static void Main(string[] args)
        {
            Microsoft.Office.Interop.Excel.Application xApp = new Microsoft.Office.Interop.Excel.Application();
            xApp.Visible = true;


            Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks.Add(Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xApp.ActiveSheet;

            Microsoft.Office.Interop.Excel.Range rng1 = xSheet.get_Range("A1", Type.Missing);
            Console.WriteLine(rng1.Value2);

            Microsoft.Office.Interop.Excel.Range rng2 = xSheet.Cells[3,1];
            Console.WriteLine(rng2.Value2);

            Microsoft.Office.Interop.Excel.Range rng3 = xSheet.get_Range("C6", Missing.Value);
            rng3.Value2 = "hello";
            rng3.Interior.ColorIndex = 6;//设置Range的背景色

            xBook.Save();

            xSheet = null;
            xBook = null;
            xApp.Quit(); //这一句是非常重要的，否则Excel对象不能从内存中退出 
            xApp = null; 
        }
    }
}