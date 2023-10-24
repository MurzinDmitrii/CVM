using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Model
{
    partial class AllAboutClientView
    {
        public string RH
        {
            get
            {
                if(ClientRh_ == true)
                {
                    return "+";
                }
                else
                {
                    return "-";
                }
            }
        }
    }
}
