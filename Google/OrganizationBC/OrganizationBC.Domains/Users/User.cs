/******************************************************************
** auth: wei.huazhong
** date: 5/4/2018 5:46:50 PM
** desc:
******************************************************************/

using ENode.Domain;
using Google.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Domains.Users
{
    public class User : AggregateRoot<string>
    {
        public User(string userId, string name, int version) : base(userId, version)
        {
            Id = userId;
            Name = name;
        }

        public User(string userId, string name) : base(userId)
        {
            Assert.IsNotNullOrEmpty("name", name);
            Assert.IsNotNullOrEmpty("userId", userId);

            //ApplyEvent()
        }

        public string Id { get; private set; }
        public string Name { get; private set; }

    }
}
