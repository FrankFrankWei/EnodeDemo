using Google.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Google.Web.ViewModels
{
    public class EmployeeModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public Sex Sex { get; set; }

        public string RealName { get; set; }
        public EmployeeStatus Status { get; set; }

        public string DepartmentId { get; set; }

        public string CreateTime { get; set; }

    }
}