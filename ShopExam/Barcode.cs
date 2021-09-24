using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronBarCode;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace ShopExam
{
   
    public class Barcode
    {
        public string Code { get; private set; }
        public Barcode(string HashCode) // ретурнить штрихкод з ХашКоду
        {
            
            if(HashCode.GetHashCode() < 0) Code = new string('0',12 - (HashCode.GetHashCode() * -1).ToString().Length)
                + (HashCode.GetHashCode() * -1).ToString();
            else Code = new string('0',12 - HashCode.GetHashCode().ToString().Length)+ HashCode.GetHashCode().ToString();
        }
    }
}
