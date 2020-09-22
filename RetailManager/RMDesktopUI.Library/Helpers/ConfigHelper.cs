using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        public decimal GetTaxRate()
        {

            var rateText = ConfigurationManager.AppSettings["taxRate"];

            var isValidTaxRate = decimal.TryParse(rateText, out decimal output);

            if (!isValidTaxRate)
            {
                throw new ConfigurationErrorsException("The tax rate is not set-up correctly.");
            }

            return output;
        }
    }
}
