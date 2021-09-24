using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace ShopExam
{
    [Serializable]
    public abstract class Product : IProduct
    {
        public  string Barcode { get; protected set; }
        public  string BarcodeShop { get; private set; }
        public  string PromotionalCode { get; private set; }
        public  string Name { get; private set; } = "";
        public  string Info { get; private set; } = "";
        public  decimal Price { get; private set; }
        public  DateTime DateOfProduction { get; private set; }

        public int Count { get; set; }

        public Product(DateTime dateProduction,string name, string info,decimal price)
        {
           
            DateOfProduction = dateProduction;
            Name = name;
            Info = info;
            Price = price;
        }

        public abstract string ToShort();
    }
}
