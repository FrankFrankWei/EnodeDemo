using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizationBC.Domains.Employees;
using ENode.Infrastructure;
using ECommon.IO;
using ECommon.Dapper;

namespace OrganizationBC.EventHandlers
{
    public class EmployeeEventHanlder : AbstractDenormalizer,
        IMessageHandler<EmployeeCreated>,
        IMessageHandler<EmployeePasswordChanged>,
        IMessageHandler<EmployeeStatusChanged>,
        IMessageHandler<EmployeeRemoved>,
        IMessageHandler<EmployeeBaseInfoChanged>


    {
        public Task<AsyncTaskResult> HandleAsync(EmployeeCreated evnt)
        {
            return TryInsertRecordAsync(connection => {
                return connection.InsertAsync(
                    new
                    {
                        Id = evnt.AggregateRootId,
                        UserName = evnt.UserName,
                        Password = evnt.Password,
                        RealName = evnt.RealName,
                        Status = evnt.Status,
                        Sex = evnt.Sex,
                        DepartmentId = evnt.DepartmentId,
                        CreatedOn= evnt.Timestamp,
                        UpdatedOn= evnt.Timestamp,
                        Version = evnt.Version
                    }
                    , "Employee");
            });
        }

        public Task<AsyncTaskResult> HandleAsync(EmployeePasswordChanged evnt)
        {
            return TryUpdateRecordAsync(connection => {
                return connection.UpdateAsync(
                    new
                    {
                        Password = evnt.Password,
                        Version = evnt.Version,
                        UpdatedOn = evnt.Timestamp
                    },
                    new
                    {
                        Id = evnt.AggregateRootId,
                        Version = evnt.Version -1
                    }, "Employee");
            });
        }

        public Task<AsyncTaskResult> HandleAsync(EmployeeStatusChanged evnt)
        {
            return TryUpdateRecordAsync(connection => {
                return connection.UpdateAsync(
                    new
                    {
                        Status = evnt.Status,
                        Version = evnt.Version,
                        UpdatedOn = evnt.Timestamp
                    },
                    new
                    {
                        Id = evnt.AggregateRootId,
                        Version = evnt.Version - 1
                    }, "Employee");
            });
        }

        public Task<AsyncTaskResult> HandleAsync(EmployeeRemoved evnt)
        {
            return TryUpdateRecordAsync(connection => {
                return connection.DeleteAsync(
                    new
                    {
                        Id = evnt.AggregateRootId,
                        Version = evnt.Version - 1
                    }, "Employee");
            });
        }

        public Task<AsyncTaskResult> HandleAsync(EmployeeBaseInfoChanged evnt)
        {
            return TryUpdateRecordAsync(connection => {
                return connection.UpdateAsync(
                    new
                    {
                        DepartmentId= evnt.NewDepartmentId,
                        RealName = evnt.RealName,
                        Sex = evnt.Sex,
                        Version = evnt.Version,
                        UpdatedOn = evnt.Timestamp
                    },
                    new
                    {
                        Id = evnt.AggregateRootId,
                        Version = evnt.Version - 1
                    }, "Employee");
            });
        }
    }
}
