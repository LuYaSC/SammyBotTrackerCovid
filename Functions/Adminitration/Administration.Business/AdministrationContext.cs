﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Data;
using TC.Core.Data.Attributes;
using TC.Core.Data.Context;

namespace TC.Functions.Administration.Business
{
    public class AdministrationContext : SBTCContext
    {
        public AdministrationContext(string nameOrConnectionString = "SBTCContext") : base(nameOrConnectionString)
        {
        }

       

        public DbSet<Parameter> Parameters { get; set; }

        public DbSet<SendNotification> SendNotifications { get; set; }
    }
}
