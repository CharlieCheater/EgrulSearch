using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalPayments.Egrul
{
    public class EgrulInformation
    {
        [JsonProperty("i")]
        public string Inn { get; set; }
        [JsonProperty("p")]
        public string Kpp { get; set; }
        [JsonProperty("o")]
        public string Ogrn { get; set; }
        [JsonProperty("c")]
        public string ShortName { get; set; }
        [JsonProperty("n")]
        public string FullName { get; set; }
        [JsonProperty("g")]
        public string GenDir { get; set; }
    }
}
