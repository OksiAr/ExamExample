using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleExam.Components
{
    public partial class Intake
    {
        public decimal FullPrice
        {
            get
            {
                return ProductIntake.Sum(x=> x.AllPrice).Value;
            }
        }
    }
}
 