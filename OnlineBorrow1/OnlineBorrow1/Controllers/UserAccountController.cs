using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBorrow1.Models;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Web.SessionState;
using Excel=Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using System.Reflection;
using System.Diagnostics;
using System.Threading.Tasks;





namespace OnlineBorrow1.Controllers
{

    
    public class UserAccountController : Controller
    {
        private UserContext db = new UserContext();
        //
        // GET: /UserAccount/

         [Authorize]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        //
        // GET: /User/Details/5

         [Authorize]
        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /User/Create
        /*
        public ActionResult Create()
        {
            return View();
        }
        */
        //
        // POST: /User/Create
        
        /*
        [HttpPost]
        public ActionResult Create(User user)
        {

            

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }
        */

         [Authorize]
        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

         [Authorize]
        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
    
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


/*
        public System.Data.DataTable GetExcelTableByOleDB(string strExcelPath, string tableName)
        {
            try
            {
                UserContext userContext = new UserContext();

                DataTable dtExcel = new DataTable();

                DataSet ds = new DataSet();

                string strExtension = System.IO.Path.GetExtension(strExcelPath);

                string strFileName = System.IO.Path.GetFileName(strExcelPath);

                OleDbConnection objConn = null;

                switch (strExtension)
                {
                    case ".xls":
                        objConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strExcelPath + ";" + "Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\"");
                        break;
                    case ".xlsx":
                        objConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strExcelPath + ";" + "Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1;\"");
                        break;
                    default:
                        objConn = null;
                        break;
                }

                if (objConn == null)
                {
                    return null;
                }

                objConn.Open();

                System.Data.DataTable schemaTable = objConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);

                string tableNameTT = schemaTable.Rows[0][2].ToString().Trim();

                string strSql = "select * from [" + tableName + "]";

                OleDbCommand objCmd = new OleDbCommand(strSql, objConn);

                OleDbDataAdapter myData = new OleDbDataAdapter(strSql, objConn);

                myData.Fill(ds, tableName);//填充数据

                objConn.Close();

                //dtExcel即为excel文件中指定表中存储的信息

                dtExcel= ds.Tables[tableName];
                 
                 return dtExcel;
            }
            catch (Exception ex)
            {

                return null;

            }
        }
 * */
        #region 批量导入基站
         [Authorize]
        public ActionResult Create()
        {
            return View();
        }
       
        [Authorize]
        [HttpPost]
        public ActionResult Create(User users)
        {
            
            HttpPostedFileBase file = Request.Files["files"];
            string FileName;
            string savePath;
            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "文件不能为空";
                return View();

            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "文件类型不对，只能导入xls和xlsx格式的文件";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "上传文件超过4M，不能上传";
                    return View();
                }
                string path = AppDomain.CurrentDomain.BaseDirectory;
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
            }


            object missing = System.Reflection.Missing.Value;
            Excel.Application excel = new Excel.Application();
            excel.Visible = false;
            excel.UserControl = true;

            string strFileName = savePath;//Directory.GetCurrentDirectory() + FileName;
            Excel.Workbook wb = excel.Application.Workbooks.Open(strFileName, missing, true, missing, missing, missing,
                                missing, missing, missing, true, missing, missing, missing, missing, missing);

            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets.get_Item(1);
            for (int i = 1; i <= 43; i++)  //第一个人的成绩就是翟伟鹏：73 坐标为第4行 第3列
            {
                string excelData = ((Excel.Range)ws.Cells[i, 1]).Text.ToString();
                users.username = excelData;
                users.password = excelData;

                UserContext userContext = new UserContext();

                userContext.Users.Add(users);
                userContext.SaveChanges();
            }
            /*
            //string result = string.Empty;
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + savePath + ";" + "Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [Sheet1$]", strConn);
            DataSet myDataSet = new DataSet();
            try
            {
                myCommand.Fill(myDataSet, "ExcelInfo");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
            DataTable User = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();
            /*
            using (TransactionScope transaction = new TransactionScope())
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //获取地区名称
                    string _areaName = table.Rows[i][0].ToString();
                    //判断地区是否存在
                    if (!_areaRepository.CheckAreaExist(_areaName))
                    {
                        ViewBag.error = "导入的文件中：" + _areaName + "地区不存在，请先添加该地区";
                        return View();
                    }
                    else
                    {
                        Station station = new Station();
                        station.AreaID = _areaRepository.GetIdByAreaName(_areaName).AreaID;
                        station.StationName = table.Rows[i][1].ToString();
                        station.TerminaAddress = table.Rows[i][2].ToString();
                        station.CapacityGrade = table.Rows[i][3].ToString();
                        station.OilEngineCapacity = decimal.Parse(table.Rows[i][4].ToString());
                        _stationRepository.AddStation(station);
                    }
                }
                transaction.Complete();
            }
             * */
            
            ViewBag.error = "导入成功";
            System.Threading.Thread.Sleep(2000);
            return RedirectToAction("Index");
        }
        #endregion
       
    
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
