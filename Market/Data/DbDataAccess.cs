using System;
using System.Data.Common;
using System.Collections.Generic;
using Market.Services;

namespace Market.Data
{
    public abstract class DbDataAccess<T> : IDisposable
    {
        protected int _rows;

        protected readonly int _step;
        protected readonly DbProviderFactory factory;
        protected readonly DbConnection connection;

        public DbDataAccess()
        {
            _rows = 10;

            factory = DbProviderFactories.GetFactory("MarketProvider");

            connection = factory.CreateConnection();
            connection.ConnectionString = ConfigurationService.Configuration["ConnectionString"];
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public abstract void Update(T entity);
        public abstract void Delete(T entity);
        public abstract ICollection<T> SelectBy(double area, int price, int roomsCount, int page);
    }
}