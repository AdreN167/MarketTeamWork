using Market.Models;
using System;
using System.Collections.Generic;

namespace Market.Data
{
    public class FlatDataAccess : DbDataAccess<Flat>
    {
        public override void Delete(Flat entity) { }
        public override void Update(Flat entity) { }

        public override ICollection<Flat> SelectBy(double area, int price, int roomsCount, int page)
        {
            var selectSqlScript = $"select * from Flats where Area >= {area} and RoomsCount >= {roomsCount} and Price <= {price} order by Price offset {page * _rows} rows fetch next {_rows} rows only";

            var flats = new List<Flat>();

            var command = factory.CreateCommand();
            command.CommandText = selectSqlScript;
            command.Connection = connection;

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    flats.Add(new Flat
                    {
                        Id = Guid.Parse(dataReader["Id"].ToString()),
                        Address = dataReader["Address"].ToString(),
                        Area = double.Parse(dataReader["Area"].ToString()),
                        Floor = int.Parse(dataReader["Floor"].ToString()),
                        RoomsCount = int.Parse(dataReader["RoomsCount"].ToString()),
                        CityName = dataReader["CityName"].ToString(),
                        IsBalcony = bool.Parse(dataReader["IsBalcony"].ToString()),
                        IsBuiltInFurniture = bool.Parse(dataReader["IsBuiltInFurniture"].ToString()),
                        SanitaryUnitsCount = int.Parse(dataReader["SanitaryUnitsCount"].ToString()),
                        IsConnectedCentralHeating = bool.Parse(dataReader["IsConnectedCentralHeating"].ToString()),
                        IsContractPrice = bool.Parse(dataReader["IsContractPrice"].ToString()),
                        IsGarage = bool.Parse(dataReader["IsContractPrice"].ToString()),
                        IsSold = bool.Parse(dataReader["IsGarage"].ToString()),
                        IsParkingPlace = bool.Parse(dataReader["IsParkingPlace"].ToString()),
                        IsWarehouse = bool.Parse(dataReader["IsWarehouse"].ToString()),
                        YearOfRepair = int.Parse(dataReader["YearOfRepair"].ToString()),
                        Mark = double.Parse(dataReader["Mark"].ToString()),
                        Price = int.Parse(dataReader["Price"].ToString())
                    });
                }
            }

            command.Dispose();

            return flats;
        }
    }
}
