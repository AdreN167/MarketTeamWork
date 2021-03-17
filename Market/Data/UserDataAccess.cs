using Market.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Data
{
    class UserDataAccess : DbDataAccess<User>
    {
        public override void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public override void Insert(User entity)
        {
            var selectSqlScript = $"INSERT INTO Users (Id, TelephoneNumber) values('{entity.Id}','{entity.TelephoneNumber}');";

            var command = factory.CreateCommand();
            command.CommandText = selectSqlScript;
            command.Connection = connection;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public ICollection<User> SelectBy(string number)
        {
            List<User> users = new List<User>();

            var selectSqlScript = $"select * from User where TelephoneNumber = '{number}';";

            var command = factory.CreateCommand();
            command.CommandText = selectSqlScript;
            command.Connection = connection;

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    users.Add(new User
                    {
                        Id = Guid.Parse(dataReader["Id"].ToString()),
                        TelephoneNumber = dataReader["TelephoneNumber"].ToString(),
                    });
                }
            }

            command.Dispose();

            return users;
        }


        public override void Update(User entity, string updateColumn, string value)
        {
            throw new NotImplementedException();
        }
    }
}
