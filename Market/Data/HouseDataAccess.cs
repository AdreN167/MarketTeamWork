using Market.Models;
using System;
using System.Collections.Generic;

namespace Market.Data
{
    public class HouseDataAccess : DbDataAccess<House>
    {
        public override void Delete(House entity) { }
        public override void Update(House entity) { }

        public override ICollection<House> SelectBy(double area, int price, int roomsCount, int page)
        {
            var selectSqlScript = $"select * from Houses where Area >= {area} and RoomsCount >= {roomsCount} and Price <= {price} order by Price offset {page * _rows} rows fetch next {_rows} rows only";

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
                        Id = Guid.Parse(dataReader["Id"].ToString()),
                        Address = dataReader["Address"].ToString(),
                        Area = double.Parse(dataReader["Area"].ToString()),
                        //DistanceToTheСity = int.Parse(dataReader["DistanceToTheCity"].ToString()),
                        FloorsCount = int.Parse(dataReader["FloorsCount"].ToString()),
                        IsGarage = bool.Parse(dataReader["IsGarage"].ToString()),
                        IsInfrastructure = bool.Parse(dataReader["IsInfrastructure"].ToString()),
                        IsWarehouse = bool.Parse(dataReader["IsWarehouse"].ToString()),
                        Mark = double.Parse(dataReader["Mark"].ToString()),
                        Price = int.Parse(dataReader["Price"].ToString()),
                        WallsMaterial = dataReader["WallsMaterial"].ToString(),
                        IsSold = bool.Parse(dataReader["IsSold"].ToString()),
                        CityName = dataReader["CityName"].ToString(),
                        GardensArea = double.Parse(dataReader["GardensArea"].ToString()),
                        IsBathhouse = bool.Parse(dataReader["IsBathhouse"].ToString()),
                        IsCellar = bool.Parse(dataReader["IsCellar"].ToString()),
                        IsContractPrice = bool.Parse(dataReader["IsContractPrice"].ToString()),
                        RoomsCount = int.Parse(dataReader["RoomsCount"].ToString())
                    });
                }
            }

            command.Dispose();

            return houses;
        }
    }
}
