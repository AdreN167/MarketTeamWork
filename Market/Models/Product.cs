using System.Collections.Generic;

namespace Market.Models
{
    public abstract class Product : Entity
    {
        public int Price { get; set; }
        public double Mark { get; set; }
        public bool IsSold { get; set; }
        public bool IsContractPrice { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }

        public override string ToString()
        {
            var isSoldAsText = (IsSold) ? "продано" : "продается";
            var isContractPriceAsText = (IsContractPrice) ? "да" : "нет";
            return $"Цена: {Price}\nОценка: {Mark}\nСтатус: {isSoldAsText}\nДоговорная цена: {isContractPriceAsText}";
        }
    }
}
