using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopExam
{
    [Serializable]
    public class Promotional : IEnumerable<IProduct>, IProduct // колекція акційних товарів
    {
        private List<IProduct> Products { get; set; } = new List<IProduct>();

        public string Barcode { get { return PromotionalCode; } }

        public string BarcodeShop => throw new NotImplementedException();

        public string PromotionalCode { get; private set;}

        public string Name => throw new NotImplementedException();

        public string Info => throw new NotImplementedException();

        public decimal Price { get; set; }

        public int Count { get; set; } = 0;

        public DateTime DateOfProduction  {get; set; }

    public IEnumerator<IProduct> GetEnumerator()
        {
            return Products.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void Add(IProduct product)
        {
            Products.Add(product);
            string NewBarcode = "";
            Products.ForEach(delegate (IProduct product1) { NewBarcode += product1.Barcode; });
            PromotionalCode = new Barcode(NewBarcode).Code;
        }
        public void Remove(IProduct product)
        {
            Products.Remove(product);
            string NewBarcode = "";
            Products.ForEach(delegate (IProduct product1) { NewBarcode += product1.Barcode; });
            PromotionalCode = new Barcode(NewBarcode).Code;
        }

        public string ToShort()
        {
            string all = $"Promotional Barcode {Barcode} counts product {Count}";
            Products.ForEach(delegate (IProduct it) { all += "\n" + it.ToShort(); });
            all += $"\n---------------------------- sum to pay = {Price}UAH";
            return all;
        }
        public override string ToString()
        {
            string all = $"Promotional Barcode {Barcode} counts product {Count}";
            Products.ForEach(delegate (IProduct it) { all += "\n" + it.ToString(); });
            all += $"\n---------------------------- sum to pay = {Price}UAH";
            return all;
        }
    }
}
