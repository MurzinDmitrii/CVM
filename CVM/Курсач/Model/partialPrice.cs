using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Model
{
    partial class Price
    {
        public string PriceNormal
        {
            get
            {
                return Math.Round(Convert.ToDouble(PriceSize), 2).ToString();
            }
            set
            {
                PriceNormal = value;
            }
        }
    }
}
