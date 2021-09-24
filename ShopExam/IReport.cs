using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopExam
{
    public interface IReport
    {
     string nameClient { get; set; }
     decimal money { get; set; }
     List<IProduct> products { get; set; }
     DateTime timeSales { get; set; }
     string ToString();
    }
}
