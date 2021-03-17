namespace Market.Models
{
    public class Feedback : Entity
    {
        public string Text { get; set; }
        public string UserName { get; set; }
        public string PublishDate { get; set; }

        public override string ToString()
        {
            return $"Дата: {PublishDate}\nОт пользователя: {UserName}\n {Text}";
        }
    }
}
