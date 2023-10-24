using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Model
{
    partial class Client
    {
        public string ClientGenderWord
        {
            get
            {
                if (ClientGender == true) return "Мужской";
                else return "Женский";
            }
        }
        public string FullName
        {
            get
            {
                return ClientLN + " " + ClientFN + " " + ClientPatronomic;
            }
        }
        public string FIO
        {
            get
            {
                return ClientLN + " " + ClientFN[0] + "." + ClientPatronomic[0] + ".";
            }
        }
        public int GroupBlood
        {
            get
            {
                if (ClientBloodGroup == null)
                {
                    ClientBloodGroup = "1";
                    return 0;
                }
                else
                    return int.Parse(ClientBloodGroup) - 1;
            }
            set
            {

            }
        }
        public string DateOfBorn
        {
            get
            {
                return ClientHB.Value.Day.ToString()+"."+ ClientHB.Value.Month.ToString()+"."+ ClientHB.Value.Year.ToString();
            }
        }
    }
}
