using ENode.Domain;
using Google.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Domains.Departments
{
    public class Department : AggregateRoot<string>
    {
        private string _name;
        private int _sortIndex;
        private string _parentId;
        private IList<string> _employeeSet;
        private IList<string> _children;
        private bool _isRemoved;

        private Department(string dId,string name,int sortIndex,string parentId,IList<string> employeeIds,IList<string> childIds,int version):base(dId,version)
        {
            _name = name;
            _sortIndex = sortIndex;
            _parentId = parentId;
            _employeeSet = employeeIds;
            _children = childIds;
        }

        public static Department CreateInstance(string dId, string name, int sortIndex, string parentId, IList<string> employeeIds, IList<string> childIds, int version)
        {
            return new Department(dId, name, sortIndex, parentId, employeeIds, childIds, version);
        }

        public Department(string departmentId,string name,string parentId,int sortIndex) : base(departmentId)
        {
            Assert.IsNotNullOrEmpty("name", name);
            ApplyEvent(new DepartmentCreated(name, parentId, sortIndex));

        }

        public void Remove()
        {
            if (_children.Count > 0 || _employeeSet.Count > 0)
            {
                throw new Exception("部门下还有子部门或者员工，不能删除");
            }
            if (!_isRemoved)
            {
                ApplyEvent(new DepartmentRemoved(_parentId));
            }
        }

        public void AddPeople(string employeeId)
        {
            if (!_employeeSet.Contains(employeeId))
            {
                ApplyEvent(new DepartmentPeopleAdded(employeeId,_employeeSet.Count+1));
            }
        }

        public void RemovePeople(string employeeId)
        {
            if (_employeeSet.Contains(employeeId))
            {
                ApplyEvent(new DepartmentPeopleRemoved(employeeId,_employeeSet.Count - 1));
            }
        }

        public void AddChild(string departmentId)
        {
            if (!_children.Contains(departmentId))
            {
                ApplyEvent(new DepartmentChildAdded(departmentId,_children.Count + 1));
            }
        }

        public void RemoveChild(string departmentId)
        {
            if (_children.Contains(departmentId))
            {
                ApplyEvent(new DepartmentChildRemoved(departmentId,_children.Count - 1));
            }
        }

        private void Handle(DepartmentCreated evnt)
        {
            _name = evnt.Name;
            _parentId = evnt.ParentId;
            _sortIndex = evnt.SortIndex;
            _isRemoved = false;
            _employeeSet = new List<string>();
            _children = new List<string>();
        }

        private void Handle(DepartmentRemoved evnt)
        {
            _isRemoved = true;
        }

        private void Handle(DepartmentPeopleAdded evnt)
        {
            _employeeSet.Add(evnt.EmployeeId);
        }

        private void Handle(DepartmentPeopleRemoved evnt)
        {
            _employeeSet.Remove(evnt.EmployeeId);
        }

        private void Handle(DepartmentChildAdded evnt)
        {
            _children.Add(evnt.DepartmentId);
        }

        private void Handle(DepartmentChildRemoved evnt)
        {
            _children.Remove(evnt.DepartmentId);
        }
    }
}
