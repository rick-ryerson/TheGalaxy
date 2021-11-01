using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Data.Implementations.EntityFramework
{
    public interface IDataContextFactory
    {
        DataContext GetNewDataContext();
    }

    public class DataContextFactory : IDataContextFactory
    {
        private readonly DbContextOptions<DataContext> options;

        public DataContextFactory(DbContextOptions<DataContext> options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public DataContext GetNewDataContext()
        {
            return new DataContext(options);
        }
    }
}
