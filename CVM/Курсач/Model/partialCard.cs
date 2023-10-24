using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Model
{
    partial class Card
    {
        public string LookOrNot
        {
            get
            {
                if (ClientLook == true)
                {
                    return "Да";
                }
                else
                {
                    return "Нет";
                }
            }
        }
        public string GroopHealth
        {
            get
            {
                return Convert.ToString(Convert.ToInt32(ClientHealthGroup) + 1);
            }
        }
    }
}
