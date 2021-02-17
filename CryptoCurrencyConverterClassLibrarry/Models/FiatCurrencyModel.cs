using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverterClassLibrarry.Models
{
    [Serializable]
    public class FiatCurrencyModel
    {

        public string Base { get; set; }
        public DateTime Date { get; set; }
        public RatesModel Rates { get; set; }

        public override string ToString()
        {
            return $"Base - {Base}\nDate - {Date}";
        }
    }
}
