using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizationBC.Domains.Employees;
using ENode.Infrastructure;
using ECommon.IO;
using ENode.Commanding;
using OrganizationBC.Commands.Departments;

namespace OrganizationBC.ProcessManagers
{
    [ECommon.Components.Component]
    public class EmployeeProcessMananger :
        IMessageHandler<EmployeeCreated>,
        IMessageHandler<EmployeeRemoved>,
        IMessageHandler<EmployeeBaseInfoChanged>
    {
        private ICommandService _commandService;

        public EmployeeProcessMananger(ICommandService commandService)
        {
            _commandService = commandService;
        }
        public Task<AsyncTaskResult> HandleAsync(EmployeeCreated evnt)
        {
            if (!string.IsNullOrEmpty(evnt.DepartmentId))
                return _commandService.SendAsync(new AddDepartmentPeopleCommand(evnt.DepartmentId, evnt.AggregateRootId));
            return Task.FromResult(AsyncTaskResult.Success);
        }

        public Task<AsyncTaskResult> HandleAsync(EmployeeRemoved evnt)
        {
            if (!string.IsNullOrEmpty(evnt.DepartmentId))
                return _commandService.SendAsync(new RemoveDepartmentPeopleCommand(evnt.DepartmentId, evnt.AggregateRootId));
            return Task.FromResult(AsyncTaskResult.Success);
        }

        public async Task<AsyncTaskResult> HandleAsync(EmployeeBaseInfoChanged evnt)
        {
            if (evnt.OldDepartmentId != evnt.NewDepartmentId)
            {
                List<Task<AsyncTaskResult>> tasks = new List<Task<AsyncTaskResult>>();
                tasks.Add(_commandService.SendAsync(new AddDepartmentPeopleCommand(evnt.NewDepartmentId, evnt.AggregateRootId)));
                tasks.Add(_commandService.SendAsync(new RemoveDepartmentPeopleCommand(evnt.OldDepartmentId, evnt.AggregateRootId)));
                if (tasks.Any())
                {
                    var totalResult = await Task.WhenAll(tasks).ConfigureAwait(false);

                    var failedResults = totalResult.Where(x => x.Status == AsyncTaskStatus.Failed);
                    if (failedResults.Count() > 0)
                    {
                        return new AsyncTaskResult(AsyncTaskStatus.Failed, string.Join("|", failedResults.Select(x => x.ErrorMessage)));
                    }

                    var ioExceptionResults = totalResult.Where(x => x.Status == AsyncTaskStatus.IOException);
                    if (ioExceptionResults.Count() > 0)
                    {
                        return new AsyncTaskResult(AsyncTaskStatus.IOException, string.Join("|", ioExceptionResults.Select(x => x.ErrorMessage)));
                    }
                }
            }
            return AsyncTaskResult.Success;
        }
    }
}
