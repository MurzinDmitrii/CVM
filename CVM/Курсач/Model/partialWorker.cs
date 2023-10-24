using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Model
{
    partial class Worker
    {
        public string FullName
        {
            get
            {
                return WorkerLN + " " + WorkerFN + " " + WorkerPatronimic;
            }
        }
    }
}
