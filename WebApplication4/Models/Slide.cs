namespace WebApplication4.Models
{
    public class Slide
    {
        public int Id { get; set; }
        public string Discount { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ?PhotoUrl { get; set; }
    }
}
