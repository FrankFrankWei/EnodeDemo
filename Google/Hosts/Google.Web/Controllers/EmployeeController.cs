using ENode.Commanding;
using OrganizationBC.Commands.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Infrastructure.Enums;
using System.Threading.Tasks;
using OrganizationBC.QueryServices;
using Google.Web.ViewModels;
using Google.Web.Services;

namespace Google.Web.Controllers
{
    public class EmployeeController :BaseController
    {
        private IEmployeeQueryService _employeeQueryService;
        private IDepartmentQueryService _departmentQueryService;
        
        public EmployeeController(ICommandService commandService,
            IEmployeeQueryService employeeQueryService,
            IDepartmentQueryService departmentQueryService) : base(commandService)
        {
            _employeeQueryService = employeeQueryService;
            _departmentQueryService = departmentQueryService;
            
        }

        public async Task<ActionResult> CreateEmployee(string userName,string password,string realName,int sex,string departmentId)
        {
            var employeeId = ECommon.Utilities.ObjectId.GenerateNewStringId();
            var cmd = new CreateEmployeeCommand(employeeId, userName, password, sex == 1 ? Sex.Male : 0, realName, departmentId);
            //return await ExecuteCommandAndReturnOperationResult(cmd, CommandReturnType.EventHandled);
            var sendResult = await SendCommandAsync(cmd);
            if (!sendResult)
            {
                return Json(new { Result = false, Msg = "添加失败" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var isExist = await WaitCommandResult(() => _employeeQueryService.IsExist(employeeId), o => o.HasValue && o.Value);
                if (isExist.HasValue && isExist.Value)
                {
                    return Json(new { Result = true, Msg = "添加成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = false, Msg = "操作结果未知，请刷新界面看结果" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public async Task<ActionResult> ChangeEmployeeInfo(string employeeId,string realName,Sex sex,string departmentId)
        {
            var cmd = new ChangeEmployeeBaseInfoCommand(employeeId, departmentId, realName, sex);
            return await ExecuteCommandAndReturnOperationResult(cmd, CommandReturnType.EventHandled);
        }

        public async Task<ActionResult> RemoveEmployee(string employeeId)
        {
            var cmd = new RemoveEmployeeCommand(employeeId);
            return await ExecuteCommandAndReturnOperationResult(cmd, CommandReturnType.EventHandled);
        }

        public async Task<ActionResult> DisableEmployee(string employeeId)
        {
            var cmd = new DisableEmployeeCommand(employeeId);
            return await ExecuteCommandAndReturnOperationResult(cmd, CommandReturnType.EventHandled);
        }

        public async Task<ActionResult> EnableEmployee(string employeeId)
        {
            var cmd = new EnableEmplyeeCommand(employeeId);
            return await ExecuteCommandAndReturnOperationResult(cmd);
        }

        [HttpGet]
        public ActionResult GetEmployeeList(int pageIndex, int pageSize = 15)
        {
            var data = _employeeQueryService.GetEmployeeList(pageIndex, pageSize);
            var model = new DataListModel<EmployeeModel>();
            if (data != null && data.TotalCount > 0)
            {
                model.TotalCount = data.TotalCount;
                model.PageIndex = data.PageIndex;
                model.TotalPage = data.TotalPage;
                model.DataList = new List<EmployeeModel>();
                data.ForEach(o => model.DataList.Add(new EmployeeModel()
                {
                    Id = o.Id,
                    RealName = o.RealName,
                    Sex = o.Sex,
                    Status = o.Status,
                    UserName = o.UserName,
                    CreateTime = o.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss")
                }));
            }
            return Json(model,JsonRequestBehavior.AllowGet);

        }
    }
}