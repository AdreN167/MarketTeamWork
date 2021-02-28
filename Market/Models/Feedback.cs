namespace Market.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public string PublishDate { get; set; }
        public int ProductId { get; set; }
        public int Type { get; set; }

        public override string ToString()
        {
            return $"Дата: {PublishDate}\nОт пользователя: {UserName}\n {Text}";
        }
    }
}
