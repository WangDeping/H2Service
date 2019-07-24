using Abp.UI;
using Abp.Web.Models;
using H2Service.Dto;
using H2Service.Extensions;
using H2Service.MeritPays;
using H2Service.MeritPays.Dto;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class MeritPayController : H2ServiceControllerBase
    {
        private readonly IMeritPayAppService _meritPayAppService;

        public MeritPayController(IMeritPayAppService meritPayAppService)
        {
            _meritPayAppService = meritPayAppService;
        }
        // GET: meritPay
        public ActionResult Index()
        {
            return View();
        }


        [DontWrapResult]
        public JsonResult GetMeritPayPeriodsGrid(PagedInputDto request)
        {
            var result = _meritPayAppService.GetPagedSalaryPeriods(request);
            return Json(new { total = result.TotalCount, rows = result.Items }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MeritPayUpload(string period)
        {
           var detailsList = new List<MeritPayDetailDto>();

            if (Request.Files != null)
            {
                var meritPayFile = Request.Files[0];
                detailsList = this.MeritPayDetailListFromFile(meritPayFile);            }
            if (detailsList.Count == 0)
                throw new UserFriendlyException("表中没有数据.");
            var periodInput = new CreateMeritPayPeriodInput { Period = period};
            periodInput.DetailCollection= detailsList;
            _meritPayAppService.UploadMeritPeriod(periodInput);
            return Json(new ErrorInfo(0, "保存成功"));
        }

        public JsonResult Remove(int Id)
        {
            _meritPayAppService.RemovePeriod(Id);
            return Json(new ErrorInfo(0, "删除成功"));
        }

        public ActionResult PersonalIndex()
        {
            return View();
        }
        public PartialViewResult PersonalDetail(int Id = 0)
        {
            var detailsList = new List<MeritPayDetailDto>();
            if (Id == 0)
                return PartialView("_PersonalDetail", detailsList);
            detailsList = _meritPayAppService.PersonalDetails(Id);
            return PartialView("_PersonalDetail", detailsList);
        }
        public ActionResult HeaderIndex()
        {
            return View();
        }

        public PartialViewResult HeaderDetail(int Id = 0)
        {
            var userNumber = AbpSession.GetUserNumber();
            var detailsList = new List<MeritPayDetailDto>();
            if (Id == 0)
                return PartialView("_HeaderDetail", detailsList);
            detailsList = _meritPayAppService.HeaderDetails(Id,userNumber);
            return PartialView("_HeaderDetail", detailsList);
        }

        private List<MeritPayDetailDto> MeritPayDetailListFromFile(HttpPostedFileBase meritPayFile)
        {

            var extension = meritPayFile.FileName.Substring(meritPayFile.FileName.LastIndexOf("."));
            var filePath = Server.MapPath(@"~/tmpFiles/" + DateTime.Now.ToFileTime().ToString() + extension);
            meritPayFile.SaveAs(filePath);
            var workBook = new XSSFWorkbook(filePath);
            var workSheet = workBook.GetSheetAt(0);
            if (workSheet == null)
                throw new UserFriendlyException("表格中没有可用sheet.");
            //根据表头整理有效的表头，去掉空表头
            IRow firstRow = workSheet.GetRow(0);
            var colsNum = firstRow.LastCellNum;
            List<string> colList = new List<string>();
            for (int i = 0; i < colsNum; i++)
            {
                var cell = firstRow.GetCell(i);
                if (cell != null)
                    switch (cell.CellType)
                    {
                        case CellType.Blank:
                            colList.Add("");
                            break;
                        case CellType.String:
                            colList.Add(cell.StringCellValue);
                            break;
                        case CellType.Numeric:
                            colList.Add(cell.NumericCellValue.ToString());
                            break;
                        default:
                            throw new UserFriendlyException("格式错误");

                    }
            }
            //Logger.Error("标题列数:" + colList.Count);
           // if (colList.Count <= 15)
              //  throw new UserFriendlyException("表格式不正确");
            List<MeritPayDetailDto> meritPayDetailList = new List<MeritPayDetailDto>();

            var rowCount = workSheet.LastRowNum;//表的最大行数
            for (int i = 1; i <= rowCount; i++)
            {//从非标题行开始
                IRow row = workSheet.GetRow(i);
               // Logger.Error("第"+i+"行的单元格数:"+row.Count());
                //取标题行和数据行单元格最小数,防止标题行和数据行单元格不一样多时报索引错误
                int minCount = row.Count() < colList.Count ? row.Count() : colList.Count;
                string rowDetail = "";
                //第一列主任工号，第二列自己工号
                for (int colIndex = 2; colIndex < minCount; colIndex++)
                {
                    rowDetail += colList[colIndex] + ":" + row.Cells[colIndex] + "^";
                }
                MeritPayDetailDto dto = new MeritPayDetailDto
                {
                    Detail = rowDetail,
                    UserNumber = (row.Cells[1]+"").PadLeft(4,'0'),
                    HeaderNumber = (row.Cells[0]+"").PadLeft(4,'0')
                };
               
                meritPayDetailList.Add(dto);
            }          
            return meritPayDetailList;

        }
    }
}