using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ShopExam
{
    [Serializable]
    public class ProductCollection : IEnumerable<IProduct> // колекція продуктів
    {
        private List<IProduct> Products { get; set; } = new List<IProduct>();

        public IEnumerator<IProduct> GetEnumerator()
        {
            return Products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void Add(IProduct product)
        {
            Products.Add(product);
        }
        public void Remove(IProduct product)
        {
            Products.Remove(product);
        }
    }
}
