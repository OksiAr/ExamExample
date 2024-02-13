using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleExam.Components
{
    public partial class ProductIntake
    {
        public decimal? AllPrice
        {
            get
            {
                return Product.Cost * Count;
            }
        }
    }
}
