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
    class PerpeualProduct : Product
    {
        public DateTime ValidUntil { get; private set; }
        public PerpeualProduct(DateTime dateProduction,DateTime validUntil, string name, string info, decimal price) : base(dateProduction, name, info, price)
        {
            Count = 0;
            ValidUntil = validUntil;
            Barcode = new Barcode(dateProduction.ToString() + validUntil.ToString() + name.ToString() + info.ToString()).Code;
        }
        public override string ToString()
        {
            return $"Barcode:{Barcode}              Counts ({Count})\nDate production: {DateOfProduction.ToShortDateString()}\nValid until: {ValidUntil.ToShortDateString()}\nName: {Name}\nInfo: {Info}\nPrice: {Price:F2}UAH";
        }

        public override string ToShort()
        {
            return $"{Name}  {Price} UAH    Counts ({Count})";
        }
    }
}
