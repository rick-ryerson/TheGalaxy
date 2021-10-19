﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleAndOrganizations.Data.Interfaces
{
    public interface IUnitOfWork<out TContext> where TContext : DbContext
    {
        TContext Context { get; }

        void Begin();
        void Commit();
        void Rollback();
        void Save();
    }
}