using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopExam
{
    public interface IProduct
    {
         string Barcode { get;  }
         string BarcodeShop { get; }
         string PromotionalCode { get;  }
         string Name { get; }
         string Info { get; }
         decimal Price { get; }
         int Count { get; set; }
         DateTime DateOfProduction { get; }
        string ToShort();
    }
}
