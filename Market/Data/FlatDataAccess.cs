using Market.Models;
using System;
using System.Collections.Generic;

namespace Market.Data
{
    public class FlatDataAccess : DbDataAccess<Flat>
    {
        public override void Delete(Flat entity) { }
        public override void Insert(Flat entity) { }
        public override void Update(Flat entity) { }

        public override ICollection<Flat> Select()
        {
            var selectSqlScript = $"select * from Flats order by Id offset {_offset} rows fetch next {_fetch} rows only";

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

            command.Dispose();

            if (flats.Count == _step)
            {
                _offset += _step;
                _fetch += _step;
            }

            return flats;
        }
    }
}
