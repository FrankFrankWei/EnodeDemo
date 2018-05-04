using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizationBC.Commands.Departments;
using ENode.Commanding;
using OrganizationBC.Domains.Departments;

namespace OrganizationBC.CommandHandlers
{
    public class DepartmentCommandHandler :
        ICommandHandler<CreateDepartmentCommand>,
        ICommandHandler<RemoveDepartmentCommand>,
        ICommandHandler<AddDepartmentChildCommand>,
        ICommandHandler<RemoveDepartmentPeopleCommand>,
        ICommandHandler<AddDepartmentPeopleCommand>,
        ICommandHandler<RemoveDepartmentChildCommand>
    {
        public void Handle(ICommandContext context, CreateDepartmentCommand command)
        {
            context.Add(new Department(command.AggregateRootId, command.Name, command.ParentId, command.SortIndex));
        }

        public void Handle(ICommandContext context, RemoveDepartmentCommand command)
        {
            context.Get<Department>(command.AggregateRootId).Remove();
        }

        public void Handle(ICommandContext context, AddDepartmentChildCommand command)
        {
            context.Get<Department>(command.AggregateRootId).AddChild(command.ChildDepartmentId);
        }

        public void Handle(ICommandContext context, RemoveDepartmentPeopleCommand command)
        {
            context.Get<Department>(command.AggregateRootId).RemovePeople(command.EmployeeId);
        }

        public void Handle(ICommandContext context, AddDepartmentPeopleCommand command)
        {
            context.Get<Department>(command.AggregateRootId).AddPeople(command.EmployeeId);
        }

        public void Handle(ICommandContext context, RemoveDepartmentChildCommand command)
        {
            context.Get<Department>(command.AggregateRootId).RemoveChild(command.ChildDepartmentId);
        }
    }
}
