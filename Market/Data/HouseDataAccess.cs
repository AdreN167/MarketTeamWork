using Market.Models;
using System;
using System.Collections.Generic;

namespace Market.Data
{
    public class HouseDataAccess : DbDataAccess<House>
    {
        public override void Delete(House entity) { }
        public override void Insert(House entity) { }
        public override void Update(House entity) { }

        public override ICollection<House> Select()
        {
            var selectSqlScript = $"select * from Houses order by Id offset {_offset} rows fetch next {_fetch} rows only";

            var houses = new List<House>();

            var command = factory.CreateCommand();
            command.CommandText = selectSqlScript;
            command.Connection = connection;

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    houses.Add(new House
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

            if (houses.Count == _step)
            {
                _offset += _step;
                _fetch += _step;
            }

            return houses;
        }
    }
}
