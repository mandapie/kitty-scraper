namespace KittyScraper.Models
{
    public class Cat
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Breed { get; set; }
        public string? Color { get; set; }
        public string? Age { get; set; }
        public string? Sex { get; set; }
        public string? Altered { get; set; }
        public string? Weight { get; set; }
        public string? ImgUrl { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public List<string>? Attributes { get; set; } //Good with children, dogs, vaccine up to date, etc
    }
}
