using Newtonsoft.Json;
using System.Net;
using System;

namespace InternationalPayments.Egrul
{
    public class EgrulService
    {
        private HttpClient _httpClient;
        private string _urlApi = "https://egrul.nalog.ru/";
        private string _urlApiSearch => _urlApi + "search-result/";
        private string _urlApiIndex => _urlApi + "index.html";
        private string _cookies = "";
        public EgrulService()
        {
            _httpClient = new HttpClient();
            var response = _httpClient.GetAsync(_urlApiIndex).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            var allCookies = response.TrailingHeaders.ToList();
            if(response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
            {
                _cookies = values.ToList()[0].Split(";")[0];
                Console.WriteLine("Egrul set cookie");
            }
            else
            {
                Console.WriteLine("Egrul not find Cookies");
                throw new Exception("Egrul not find Cookie");
            }
        }
        public async Task<List<EgrulInformation>> GetEgrulInformation(string search)
        {
            var body = new Dictionary<string, string>
            {
                { "vyp3CaptchaToken", "" },
                { "page", "" },
                { "query", search },
                { "nameEq", "" },
                { "region", "" },
                { "PreventChromeAutocomplete", "" }
            };

            // Выполняем POST-запрос
            var content = new FormUrlEncodedContent(body);
            var postResponse = await _httpClient.PostAsync(_urlApi, content);
            postResponse.EnsureSuccessStatusCode();

            // Получаем часть URL из ответа
            var postResponseContent = await postResponse.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(postResponseContent);
            string urlPart = jsonResponse.t;
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Cookie", _cookies);

            // Выполняем GET-запрос
            var time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var getUrl = $"{_urlApiSearch}{urlPart}?r={time}&_={time}";

            // Убираем payload из заголовков
            _httpClient.DefaultRequestHeaders.Remove("Cookie");

            var getResponse = await _httpClient.GetAsync(getUrl);
            getResponse.EnsureSuccessStatusCode();

            // Возвращаем результат
            var getResponseContent = await getResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EgrulData>(getResponseContent).EgrulInformations;
        }
    }
}
