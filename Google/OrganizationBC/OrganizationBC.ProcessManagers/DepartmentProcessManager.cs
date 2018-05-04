using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENode.Infrastructure;
using OrganizationBC.Domains.Departments;
using OrganizationBC.Commands.Departments;
using ECommon.IO;
using ENode.Commanding;

namespace OrganizationBC.ProcessManagers
{
    [ECommon.Components.Component]
    public class DepartmentProcessManager :
        IMessageHandler<DepartmentCreated>,
        IMessageHandler<DepartmentRemoved>
    {
        ICommandService _commandService;
        public DepartmentProcessManager(ICommandService commandService)
        {
            _commandService = commandService;
        }
        public Task<AsyncTaskResult> HandleAsync(DepartmentCreated evnt)
        {
            if (!string.IsNullOrEmpty(evnt.ParentId))
                _commandService.Send(new AddDepartmentChildCommand(evnt.ParentId, evnt.AggregateRootId));
            return Task.FromResult(AsyncTaskResult.Success);
        }

        public Task<AsyncTaskResult> HandleAsync(DepartmentRemoved evnt)
        {
            if (!string.IsNullOrEmpty(evnt.ParentId))
                _commandService.Send(new RemoveDepartmentChildCommand(evnt.ParentId, evnt.AggregateRootId));
            return Task.FromResult(AsyncTaskResult.Success);
        }
    }
}
