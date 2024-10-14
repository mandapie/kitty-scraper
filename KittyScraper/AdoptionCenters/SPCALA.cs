using AngleSharp;
using KittyScraper.Models;

namespace KittyScraper.AdoptionCenters
{
    //Society for the Prevention of Cruelty to Animals Los Angeles
    public class SPCALA(IBrowsingContext context)
    {
        public IBrowsingContext _context = context;

        public async Task<List<Cat>> SearchCat(Cat? query)
        {
            var result = new List<Cat>();
            var url = $"https://spcala.com/adoptable/?type=Cat";
            if (query != null) url += $"&pname={query.Name}&ss={query.Id}&breed={query.Breed}&gender={query.Sex}";
            var document = await _context.OpenAsync(url);
            var cellSelector = "#adoptList ul li";
            var cells = document.QuerySelectorAll(cellSelector);

            foreach (var c in cells)
            {
                var cat = new Cat
                {
                    UrlLink = $"https://spcala.com/adoptable/{c.Children[0].GetAttribute("href")}",
                    Name = c.Children[0].TextContent,
                    ImgUrl = c.Children[0].Children[0].GetAttribute("src")?.ToString(),
                    Breed = c.Children[1].InnerHtml.Split("<br>")[0],
                    Age = c.Children[1].InnerHtml.Split("<br>")[1].Split(' ')[0],
                    Sex = c.Children[1].InnerHtml.Split("<br>")[1].Split(' ')[1],
                    Location = "spcaLA"
                };

                result.Add(cat);
            }

            return result;
        }
    }
}