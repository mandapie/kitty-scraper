using KittyScraper.Models;
using KittyScraper.Utilities;
using PuppeteerSharp;

namespace KittyScraper.AdoptionCenters
{
    //OC Animal Care
    public class OCAC(IPage page)
    {
        public IPage _page = page;

        public async Task<List<Cat>> SearchCat(Cat? query)
        {
            var result = new List<Cat>();

            await _page.GoToAsync("https://petadoption.ocpetinfo.com/Adopt/#/list/CAT");
            var cells = await _page.QuerySelectorAllAsync(".adoptBox");

            foreach (var c in cells)
            {
                var detailsString = await c.QuerySelectorAsync("small").EvaluateFunctionAsync<string>("el => el.innerText");
                var details = detailsString.Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToList();
                var alteredValue = details[1].Split(": ")[1];

                var cat = new Cat()
                {
                    ImgUrl = $"https://petadoption.ocpetinfo.com/Adopt/{await c.QuerySelectorAsync("img").EvaluateFunctionAsync<string>("el => el.getAttribute('src')")}",
                    Name = await c.QuerySelectorAsync("h3").EvaluateFunctionAsync<string>("el => el.innerText"),
                    Id = details[0].Split(": ")[1],
                    Altered = alteredValue == "S" || alteredValue == "N" ? alteredValue : null,
                    Age = details[2].Split(": ")[1],
                    Weight = details[3].Split(": ")[1],
                    Color = details[4].Split(": ")[1],
                    Breed = details[5].Split(": ")[1],
                    Location = "OC Animal Care"
                };

                cat.Sex = cat.Altered == null ? alteredValue : null;

                if (query == null || Search.SearchCriteria(query, cat) != null)
                {
                    result.Add(cat);
                }
            }

            return result;
        }
    }
}