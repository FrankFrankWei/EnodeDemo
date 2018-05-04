using ENode.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Domains.Departments
{
    public class DepartmentCreated : DomainEvent<string>
    {
        public string Name { get; set; }
        public string ParentId { get; set; }

        public int SortIndex { get; set; }

        private DepartmentCreated() { }

        public DepartmentCreated(string name,string parentId,int sortIndex)
        {
            Name = name;
            ParentId = parentId;
            SortIndex = sortIndex;
        }
    }
}
