using KittyScraper.Models;
using System.Reflection;

namespace KittyScraper.Utilities
{
    public static class Search
    {

        public static Cat? SearchCriteria(Cat query, Cat result)
        {
            Type t = query.GetType();
            PropertyInfo[] pi = t.GetProperties();
            foreach (PropertyInfo p in pi)
            {
                var queryValue = p.GetValue(query)?.ToString();
                if (queryValue != null)
                {
                    var value = result.GetType().GetProperties().Where(c => c.Name == p.Name).Select(c => c.GetValue(result)).Single()?.ToString();
                    if (value.Contains(queryValue, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return result;
                    }
                }
            }

            return null;
        }
    }
}
