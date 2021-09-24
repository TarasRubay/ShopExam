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
    public class UrgentProduct : Product
    {
        public UrgentProduct(DateTime dateProduction, string name, string info, decimal price) : base(dateProduction, name, info, price)
        {
            Count = 0;
            Barcode = new Barcode(dateProduction.ToString() + name.ToString() + info.ToString()).Code;
        }
        public override string ToString()
        {
            return $"Barcode:{Barcode}              Counts ({Count})\nDate production: {DateOfProduction.ToShortDateString()}\nName: {Name}\nInfo: {Info}\nPrice: {Price}UAH";
        }
        public override string ToShort()
        {
            return $"{Name}  {Price} UAH    Counts ({Count})";
        }
    }
}
