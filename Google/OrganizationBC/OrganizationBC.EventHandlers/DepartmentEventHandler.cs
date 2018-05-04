using ENode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizationBC.Domains.Departments;
using ECommon.IO;
using ECommon.Dapper;

namespace OrganizationBC.EventHandlers
{
    public class DepartmentEventHandler : AbstractDenormalizer,
        IMessageHandler<DepartmentCreated>,
        IMessageHandler<DepartmentRemoved>,
        IMessageHandler<DepartmentPeopleAdded>,
        IMessageHandler<DepartmentChildRemoved>,
        IMessageHandler<DepartmentPeopleRemoved>,
        IMessageHandler<DepartmentChildAdded>
    {
        public Task<AsyncTaskResult> HandleAsync(DepartmentCreated evnt)
        {
            return TryInsertRecordAsync(connection => {
                return connection.InsertAsync(
                    new
                    {
                        Id = evnt.AggregateRootId,
                        Name = evnt.Name,
                        ParentId = evnt.ParentId,
                        SortIndex = evnt.SortIndex,
                        PeopleAmount = 0,
                        ChildAmount =0,
                        CreatedOn = evnt.Timestamp,
                        UpdatedOn = evnt.Timestamp,
                        Version = evnt.Version
                    }
                    , "Department");
            });
        }

        public Task<AsyncTaskResult> HandleAsync(DepartmentRemoved evnt)
        {
            return TryUpdateRecordAsync(connection => {
                return connection.DeleteAsync(
                    new
                    {
                        Id = evnt.AggregateRootId,
                        Version = evnt.Version - 1
                    }, "Department");
            });
        }

        public Task<AsyncTaskResult> HandleAsync(DepartmentPeopleAdded evnt)
        {
            return TryUpdateRecordAsync(connection => {
                return connection.UpdateAsync(
                    new
                    {
                        PeopleAmount = evnt.Amount,
                        Version = evnt.Version,
                        UpdatedOn = evnt.Timestamp
                    },
                    new
                    {
                        Id = evnt.AggregateRootId,
                        Version = evnt.Version - 1
                    }, "Department");
            });
        }

        public Task<AsyncTaskResult> HandleAsync(DepartmentChildRemoved evnt)
        {
            return TryUpdateRecordAsync(connection => {
                return connection.UpdateAsync(
                    new
                    {
                        ChildAmount = evnt.Amount,
                        Version = evnt.Version,
                        UpdatedOn = evnt.Timestamp
                    },
                    new
                    {
                        Id = evnt.AggregateRootId,
                        Version = evnt.Version - 1
                    }, "Department");
            });
        }

        public Task<AsyncTaskResult> HandleAsync(DepartmentPeopleRemoved evnt)
        {
            return TryUpdateRecordAsync(connection => {
                return connection.UpdateAsync(
                    new
                    {
                        PeopleAmount = evnt.Amount,
                        Version = evnt.Version,
                        UpdatedOn = evnt.Timestamp
                    },
                    new
                    {
                        Id = evnt.AggregateRootId,
                        Version = evnt.Version - 1
                    }, "Department");
            });
        }

        public Task<AsyncTaskResult> HandleAsync(DepartmentChildAdded evnt)
        {
            return TryUpdateRecordAsync(connection => {
                return connection.UpdateAsync(
                    new
                    {
                        ChildAmount = evnt.Amount,
                        Version = evnt.Version,
                        UpdatedOn = evnt.Timestamp
                    },
                    new
                    {
                        Id = evnt.AggregateRootId,
                        Version = evnt.Version - 1
                    }, "Department");
            });
        }
    }
}
