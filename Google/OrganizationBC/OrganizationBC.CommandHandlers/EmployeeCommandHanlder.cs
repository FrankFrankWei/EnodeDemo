using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENode.Commanding;
using OrganizationBC.Commands.Employees;
using OrganizationBC.Domains.Employees;
namespace OrganizationBC.CommandHandlers
{
    public class EmployeeCommandHanlder :
        ICommandHandler<CreateEmployeeCommand>,
        ICommandHandler<ChangeEmployeePasswordCommand>,
        ICommandHandler<ChangeEmployeeBaseInfoCommand>,
        ICommandHandler<DisableEmployeeCommand>,
        ICommandHandler<EnableEmplyeeCommand>,
        ICommandHandler<RemoveEmployeeCommand>
    {
        public void Handle(ICommandContext context, CreateEmployeeCommand command)
        {
            //调用第三方接口

            context.Add(new Employee(command.AggregateRootId, command.UserName, command.Password, command.RealName, command.Sex, command.DepartmentId));
        }

        public void Handle(ICommandContext context, ChangeEmployeePasswordCommand command)
        {
            context.Get<Employee>(command.AggregateRootId).ChangePassword(command.Password);
        }

        public void Handle(ICommandContext context, ChangeEmployeeBaseInfoCommand command)
        {
            context.Get<Employee>(command.AggregateRootId).ChangeBaseInfo(command.RealName, command.Sex, command.DepartmentId);
        }

        public void Handle(ICommandContext context, DisableEmployeeCommand command)
        {
            context.Get<Employee>(command.AggregateRootId).ChangeStatus(Google.Infrastructure.Enums.EmployeeStatus.Invalid);
        }

        public void Handle(ICommandContext context, EnableEmplyeeCommand command)
        {
            context.Get<Employee>(command.AggregateRootId).ChangeStatus(Google.Infrastructure.Enums.EmployeeStatus.Valid);
        }

        public void Handle(ICommandContext context, RemoveEmployeeCommand command)
        {
            context.Get<Employee>(command.AggregateRootId).Remove();
        }
    }
}
