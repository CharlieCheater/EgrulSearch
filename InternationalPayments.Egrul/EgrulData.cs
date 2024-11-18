using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalPayments.Egrul
{
    internal class EgrulData
    {
        [JsonProperty("rows")]
        public List<EgrulInformation> EgrulInformations { get; set; }
    }
}
