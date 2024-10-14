using AngleSharp;
using KittyScraper.Models;
using KittyScraper.Utilities;

namespace KittyScraper.AdoptionCenters
{
    //Seal Beach Animal Care Center
    public class SBACC(IBrowsingContext context)
    {
        public IBrowsingContext _context = context;

        public async Task<List<Cat>> SearchCat(Cat? query)
        {
            var result = new List<Cat>();
            int page = 1;
            int lastPage = 1;

            var url = $"https://www.sbacc.org/adverts-catskittens/";
            var document = await _context.OpenAsync(url);
            var cellSelector = ".adverts-list .adoptable-item";

            var pagination = document.QuerySelectorAll(".adverts-pagination a");
            if (pagination.Any())
            {
                lastPage = Int32.Parse(pagination.Last().TextContent);
            }

            while (page <= lastPage)
            {
                url = $"https://www.sbacc.org/adverts-catskittens/?pg={page}";
                document = await _context.OpenAsync(url);
                var cells = document.QuerySelectorAll(cellSelector);

                foreach (var c in cells)
                {
                    var cat = new Cat
                    {
                        UrlLink = c.Children[0].Children[0].Children[1].Children[0].Children[0].GetAttribute("href"),
                        Name = c.Children[0].Children[0].Children[1].Children[0].Children[0].TextContent,
                        ImgUrl = c.Children[0].Children[0].Children[0].Children[0].Children[0].GetAttribute("src")?.ToString(),
                        Sex = c.Children[0].Children[0].Children[1].Children[1].TextContent.Split(": ")[1].Replace("\n", "").Trim(),
                        Breed = c.Children[0].Children[0].Children[1].Children[2].TextContent.Split(": ")[1].Trim(),
                        Location = "Seal Beach Animal Care Center"
                    };

                    if (query == null || Search.SearchCriteria(query, cat) != null)
                    {
                        result.Add(cat);
                    }
                }

                page++;
            }

            return result;
        }
    }
}
