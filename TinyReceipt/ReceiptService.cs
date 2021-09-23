using System;
using System.Collections.Generic;
using System.Text;

namespace TinyReceipt
{
    public class ReceiptService
    {
        /// <summary>
        /// rounding rule for tax
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static decimal RoundingTax(decimal d)
        {
            d = decimal.Round(d, 2, MidpointRounding.AwayFromZero);
            int mod = Convert.ToInt32(d * 100) % 10;
            //rounded up to the nearest 0.05
            if (mod > 0 && mod < 5)
            {
                return (d * 100 - mod + 5) / 100;
            }
            else if (mod > 5 && mod <= 9)
            {
                return (d * 100 - mod + 10) / 100;
            }
            else
                return d;
        }

    }
}
