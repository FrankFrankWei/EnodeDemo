using ENode.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Domains.Departments
{
    public class DepartmentRemoved : DomainEvent<string>
    {
        public string ParentId { get; private set; }

        private DepartmentRemoved() { }

        public DepartmentRemoved(string parentId)
        {
            ParentId = parentId;
        }
    }
}
