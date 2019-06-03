using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerInquiry.Responses
{
    public class TransactionResponse
    {
        public int id { get; set; }
        public string date { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionStatus status { get; set; }
    }
}
