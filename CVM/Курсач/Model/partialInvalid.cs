using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Model
{
    partial class Invalid
    {
        public string First
        {
            get
            {
                if(InvalidFirst == false)
                {
                    return "Повторная";
                }
                else
                {
                    return "Первичная";
                }
            }
        }
    }
}
