using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxRecruitment.Core.Model
{
    public sealed class ExchangeRateDTO
    {
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRateValue { get; set; }
    }
}
