using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.DTOs
{
    public class DepartmentDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public int SortIndex { get; set; }
        public int PeopleAmount { get; set; }
        public int ChildAmount { get; set; }
        public int Version { get; set; }
    }
}
