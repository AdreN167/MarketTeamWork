using System;
using System.Data.Common;
using System.Collections.Generic;
using Market.Services;

namespace Market.Data
{
    public abstract class DbDataAccess<T> : IDisposable
    {
        protected int _offset;
        protected int _fetch;

        protected readonly int _step;
        protected readonly DbProviderFactory factory;
        protected readonly DbConnection connection;

        public DbDataAccess()
        {
            _step = 10;
            _offset = 0;
            _fetch = 10;

            factory = DbProviderFactories.GetFactory("MarketProvider");

            connection = factory.CreateConnection();
            connection.ConnectionString = ConfigurationService.Configuration["ConnectionString"];
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        } 

        public void ExecuteTransaction(params DbCommand[] commands)
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (var command in commands)
                    {
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (DbException)
                {
                    transaction.Rollback();
                }
            }
        }

        public abstract void Insert(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(T entity);
        public abstract ICollection<T> Select();
    }
}