using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ENode.Commanding;
using System.Web.Mvc;
using System.Threading.Tasks;
using OrganizationBC.Commands.Departments;
using OrganizationBC.QueryServices;
using Google.Web.ViewModels;

namespace Google.Web.Controllers
{
    public class DepartmentController : BaseController
    {
        private IDepartmentQueryService _departmentQueryService;
        public DepartmentController(ICommandService commandService,IDepartmentQueryService departmentQueryService) : base(commandService)
        {
            _departmentQueryService = departmentQueryService;
        }

        public async Task<ActionResult> CreateDepartment(string name,string parentId,int sortIndex)
        {
            var departmentId = ECommon.Utilities.ObjectId.GenerateNewStringId();
            var cmd = new CreateDepartmentCommand(departmentId,name,parentId==null?"":parentId,sortIndex);
            return await ExecuteCommandAndReturnOperationResult(cmd, CommandReturnType.EventHandled);
        }

        public async Task<ActionResult> RemoveDepartment(string departmentId)
        {
            var cmd = new RemoveDepartmentCommand(departmentId);
            return await ExecuteCommandAndReturnOperationResult(cmd, CommandReturnType.EventHandled);
        }


        public ActionResult GetDeparmentList()
        {
            var data = _departmentQueryService.GetAllDepartmentList();
            var model = new List<DepartmentDataModel>();
            if (data != null && data.Any())
            {
                data.ForEach(o => model.Add(new DepartmentDataModel() {
                    Id = o.Id,
                    Name = o.Name
                }));
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }
}