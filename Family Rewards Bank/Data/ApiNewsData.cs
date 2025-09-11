using Family_Rewards_Bank.Models;
using System.Xml.Linq;

namespace Family_Rewards_Bank.Data
{
    public class ApiNewsData
    {
        private readonly HttpClient _httpClient;

        public ApiNewsData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<NewsModel>> GetNews(string url = "https://news.yam.md/ro/rss")
        {
            var newsList = new List<NewsModel>();
            var localPath = Path.Combine(FileSystem.AppDataDirectory, "rss.fed.xml");
            string response = string.Empty;

            try
            {
                // Попытка загрузить RSS из интернета
                response = await _httpClient.GetStringAsync(url);

                // Сохраняем локально
                await File.WriteAllTextAsync(localPath, response);
            }
            catch
            {
                // Если интернет недоступен, пробуем прочитать локальный файл
                if (File.Exists(localPath))
                {
                    response = await File.ReadAllTextAsync(localPath);
                }
                else
                {
                    Console.WriteLine("RSS недоступен и локальный файл не найден.");
                    return newsList; // вернём пустой список
                }
            }

            try
            {
                var doc = XDocument.Parse(response);
                XNamespace atom = "http://www.w3.org/2005/Atom";

                newsList = doc.Descendants(atom + "entry")
                    .Select(entry => new NewsModel
                    {
                        Id = (string?)entry.Element(atom + "id"),
                        Title = (string?)entry.Element(atom + "title"),
                        Description = (string?)entry.Element(atom + "summary"),
                        Name = (string?)entry.Element(atom + "author")?.Element(atom + "name"),
                        Updated = DateTime.TryParse((string?)entry.Element(atom + "updated"), out var date)
                                  ? date
                                  : DateTime.MinValue,
                        Url = (string?)entry.Element(atom + "link")?.Attribute("href")
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при парсинге RSS: {ex.Message}");
            }

            return newsList;
        }
    }
}
