using Abp;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using H2Service.Dto;
using H2Service.Salaries;
using H2Service.Salaries.Dto;
using H2Service.Web.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    [AbpMvcAuthorize]
    public class SalaryController : H2ServiceControllerBase
    {
        private readonly ISalaryAppService _salaryAppService;    
        public SalaryController(ISalaryAppService salaryAppService) {
         
            _salaryAppService = salaryAppService;

        }

        // GET: Salary
        
        public async Task<PartialViewResult> Index()
        {
            var userIdentifier = new UserIdentifier(null, 1);
           
            return PartialView("Index");
        }
        
        public PartialViewResult SalaryTypes()
        {
            var allTypes = _salaryAppService.GetAllTypes();
            return PartialView("_SalaryType",allTypes);
        }
        /// <summary>
        /// 上传工资
        /// </summary>
        /// <param name="period"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public JsonResult SalaryUpload(string period,int typeId)
        {
            List<SalaryDetailDto> salaryList = new List<SalaryDetailDto>();
          
            if (Request.Files != null) {
                var salaryFile = Request.Files[0];
                 salaryList = this.SalaryDetailListFromFile(salaryFile);
            }

            var periodInput = new CreateSalaryPeriodInput {  Period=period, SalaryTypeID=typeId};//, CreatorUserId=AbpSession.UserId
            periodInput.SalaryDetailList = salaryList;
            _salaryAppService.UploadSalary(periodInput);
            return  Json(new RepViewModel {  Msg="保存成功"});
        }

        /// <summary>
        /// 工资上传历史
        /// </summary>
        /// <returns></returns>
        public PartialViewResult SalaryPeriodList() {
            
            return PartialView("_PeriodList");
        }
        [DontWrapResult]
        public JsonResult GetSalaryPeriodList(PagedInputDto request)
        {
            
           var result= _salaryAppService.GetPagedSalaryPeriods(request);
            return  Json(new {  total=result.TotalCount, rows=result.Items},JsonRequestBehavior.AllowGet);
        }
        public ActionResult PersonalPeriodIndex()
        {
            return View();
        }
        /// <summary>
        /// 个人历史工资
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [DontWrapResult]
        public JsonResult PersonalPeriodList(PagedInputDto request)
        {
           
            var result = _salaryAppService.GetPagedPersonalSalaryPeriods(request);
            return Json(new { total = result.TotalCount, rows = result.Items }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult PersonalDetail(int Id=0)
        {
            if(Id==0)
                return PartialView("PersonalDetail","");
            var result = new PersonalSalaryOutput();
            if(Id!=0)
             result = _salaryAppService.GetPersonSalary(Id);
            return PartialView("PersonalDetail", result.Detail??"");
        }
        private List<SalaryDetailDto> SalaryDetailListFromFile(HttpPostedFileBase salaryFile) {
            
            var extension = salaryFile.FileName.Substring(salaryFile.FileName.LastIndexOf("."));
            var filePath = Server.MapPath(@"~/tmpFiles/" + DateTime.Now.ToFileTime().ToString() + extension);
            salaryFile.SaveAs(filePath);
            var workBook = new XSSFWorkbook(filePath);
            var workSheet= workBook.GetSheetAt(0);
            if(workSheet==null)
                throw new UserFriendlyException("工资表中没有可用sheet.");
            //根据表头整理有效的表头，去掉空表头
            IRow firstRow = workSheet.GetRow(0);            
            var colsNum = firstRow.LastCellNum;
            List<string> colList = new List<string>();
            for (int i = 0; i < colsNum; i++) {
                var cell = firstRow.GetCell(i);
                if(cell!=null)
                switch (cell.CellType) {
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
            //if(colList.Count<=15)
               // throw new UserFriendlyException("工资表格式不对(多余15列视为).");
            List<SalaryDetailDto> salaryDetailList = new List<SalaryDetailDto>();
         
            var  rowCount = workSheet.LastRowNum;//表的最大行数
            //Logger.Error("最大行数" + rowCount);
            for (int i = 1; i <= rowCount; i++) {//从非标题行开始
                IRow row = workSheet.GetRow(i);                
                //取标题行和数据行单元格最小数,防止标题行和数据行单元格不一样多时报索引错误
                int minCount = row.Count() < colList.Count ? row.Count() : colList.Count;
                string rowDetail = "";
                
                for (int colIndex = 1; colIndex < minCount; colIndex++) {
                    rowDetail+=colList[colIndex]+":"+row.Cells[colIndex]+"^";
                }
                SalaryDetailDto salaryDetail = new SalaryDetailDto {
                    Detail = rowDetail,
                    UserNumber = row.Cells[0]+""
                };
            
                salaryDetailList.Add(salaryDetail);
            }
            return salaryDetailList;
            
        }


    }
}