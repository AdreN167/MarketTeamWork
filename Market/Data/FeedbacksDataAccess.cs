using Market.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Data
{
    public class FeedbacksDataAccess : DbDataAccess<Feedback>
    {
        public override void Delete(Feedback entity) { }
        public override void Insert(Feedback entity) { }
        public override void Update(Feedback entity) { }

        public override ICollection<Feedback> Select()
        {
            throw new NotImplementedException();
        }

        public ICollection<Feedback> SelectFor(Product product)
        {
            var feedbacksTableName = (product is Flat) ? "FlatFeedbacks" : "HouseFeedbacks";
            var ProductsTableName = (product is Flat) ? "Flats" : "Houses";

            var selectSqlScript = $"select sourse.Id, sourse.ProductId, sourse.PublishDate, sourse.Text, sourse.UserName from {feedbacksTableName} sourse join {ProductsTableName} ref on ref.Id = sourse.ProductId where ref.Id = {product.Id} order by sourse.Id offset {_offset} rows fetch next {_fetch} rows only";

            var feedbacks = new List<Feedback>();

            var command = factory.CreateCommand();
            command.CommandText = selectSqlScript;
            command.Connection = connection;

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    feedbacks.Add(new Feedback
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        ProductId = int.Parse(dataReader["ProductId"].ToString()),
                        PublishDate = dataReader["PublishDate"].ToString(),
                        Text = dataReader["Text"].ToString(),
                        Type = int.Parse(dataReader["Type"].ToString()),
                        UserName = dataReader["UserName"].ToString()
                    });
                }
            }

            command.Dispose();

            if (feedbacks.Count == _step)
            {
                _offset += _step;
                _fetch += _step;
            }

            return feedbacks;
        }
    }
}
