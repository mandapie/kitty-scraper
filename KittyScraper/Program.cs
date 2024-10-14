using AngleSharp;
using KittyScraper.AdoptionCenters;
using KittyScraper.Models;
using KittyScraper.Utilities;
using Newtonsoft.Json;
using PuppeteerSharp;

namespace KittyScraper
{
    public static class Program
    {
        /// <summary>
        /// Enter json into argument to search, else search all cats
        /// Sample argument:
        /// "{
        ///     'Id': 'A1234',
        ///     'Name': 'Abbie',
        ///     'Breed': 'domestic sh',
        ///     'Color': 'orange',
        ///     'Age': 'kitten',
        ///     'Sex': 'female'
        /// }"
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async static Task Main(string[] args)
        {
            try
            {
                var catResults = new List<Cat>();

                //static website config (Anglesharp)
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);

                //dynamic website config (PuppeteerSharp)
                //fetch browser - need this to run Puppeteer. Creates browser folders in /bin/Debug, but need to copy folders to /Release.
                var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync();

                var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
                var page = await browser.NewPageAsync();                

                //search criteria
                Cat? searchCat = null;
                if (args.Length > 0)
                {
                    searchCat = JsonConvert.DeserializeObject<Cat>(args[0]);
                }

                //adoption centers
                var sPCALA = new SPCALA(context);
                var sPCALACats = await sPCALA.SearchCat(searchCat);
                catResults.AddRange(sPCALACats);

                var sBACC = new SBACC(context);
                var sBACCCats = await sBACC.SearchCat(searchCat);
                catResults.AddRange(sBACCCats);

                var oCAC = new OCAC(page);
                var oCACCats = await oCAC.SearchCat(searchCat);
                catResults.AddRange(oCACCats);

                //output result
                foreach (var cat in catResults)
                {
                    //write to console
                    Output.ConsoleWriteLine(cat);
                    Console.WriteLine();

                    //send to discord
                    Output.SendDiscordWebhook(cat);
                }

                Console.WriteLine("Scrape done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Output.SendDiscordWebhook(ex);
            }
        }
    }
}
