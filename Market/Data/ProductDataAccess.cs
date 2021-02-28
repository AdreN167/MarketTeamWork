using Market.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Data
{
    public class ProductDataAccess : DbDataAccess<Product>
    {
        public override void Delete(Product entity) { }
        public override void Insert(Product entity) { }
        public override void Update(Product entity) { }

        public override ICollection<Product> Select()
        {
            var selectFlatsSqlScript = $"select * from Flats order by Id offset {_offset} rows fetch next {_fetch} rows only";
            var selectHousesSqlScript = $"select * from Houses order by Id offset {_offset} rows fetch next {_fetch} rows only";

            var products = new List<Product>();

            var command = factory.CreateCommand();
            command.CommandText = selectFlatsSqlScript;
            command.Connection = connection;

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    products.Add(new Flat
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        Address = dataReader["Address"].ToString(),
                        Area = double.Parse(dataReader["Area"].ToString()),
                        Floor = int.Parse(dataReader["Floor"].ToString()),
                        RoomsCount = int.Parse(dataReader["RoomsCount"].ToString()),
                        IsRepairedCurrentYear = bool.Parse(dataReader["IsRepairedCurrentYear"].ToString()),
                        IsSold = bool.Parse(dataReader["IsSold"].ToString()),
                        IsBuiltInFurniture = bool.Parse(dataReader["IsBuiltInFurniture"].ToString()),
                        Mark = double.Parse(dataReader["Mark"].ToString()),
                        Price = int.Parse(dataReader["Price"].ToString())
                    });
                }
            }

            command.CommandText = selectHousesSqlScript;

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    products.Add(new House
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        Address = dataReader["Address"].ToString(),
                        Area = double.Parse(dataReader["Area"].ToString()),
                        DistanceToTheСity = int.Parse(dataReader["DistanceToTheСity"].ToString()),
                        FloorsCount = int.Parse(dataReader["FloorsCount"].ToString()),
                        IsGarage = bool.Parse(dataReader["IsGarage"].ToString()),
                        IsInfrastructure = bool.Parse(dataReader["IsInfrastructure"].ToString()),
                        IsWarehouse = bool.Parse(dataReader["IsWarehouse"].ToString()),
                        Mark = double.Parse(dataReader["Mark"].ToString()),
                        Price = int.Parse(dataReader["Price"].ToString()),
                        WallsMaterial = dataReader["WallsMaterial"].ToString()
                    });
                }
            }

            command.Dispose();

            if (products.Count == _step)
            {
                _offset += _step;
                _fetch += _step;
            }

            return products;
        }
    }
}
