using Google.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.DTOs
{
    public class EmployeeDTO
    {
        public string Id  { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Sex Sex { get; set; }

        public string RealName { get; set; }
        public EmployeeStatus Status { get; set; }

        public string DepartmentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Verion { get; set; }
    }
}
