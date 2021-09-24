using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace ShopExam
{
    
    public class Shop // Singelton
    {
       
        public CashRegister CashRegister { get; set; } 
        public UnOrder unOrder { get; set; } 
        private static Shop uniqueInstans { get; set; }
        private Shop() {
        CashRegister = CashRegister.Instanse();
        }
        public static Shop Instanse()
        {
            if (uniqueInstans == null)
            {
                uniqueInstans = new Shop();
                return uniqueInstans;
            }
            return uniqueInstans;
        }
        public ProductCollection Products { get; set; } = new ProductCollection();

        
        public Dictionary<string,List<IProduct>> BasketClient { get; set; } = new Dictionary<string, List<IProduct>>();
        public void orderFormation(decimal money, string nameClient,in List<IProduct> products)
        {
            
            decimal sumPay = 0.0M;
            products.ForEach(delegate (IProduct pr) { sumPay += pr.Price; });
            if (sumPay != money) throw new Exception("not enough money");
           
            
            foreach (var item in BasketClient)
            {
                if (item.Key == nameClient)
                {
                    foreach (var clientBask in item.Value)
                    {
                        
                         foreach (var it in Products)
                            {
                            if (clientBask.Barcode == it.Barcode && it.Count > 1 && !(it is Promotional)) it.Count--;
                            else if (clientBask.Barcode == it.Barcode && it.Count == 1 && !(it is Promotional))
                            {
                                Products.Remove(it);
                                break;
                            }
                            else if (clientBask.Barcode == it.Barcode && it.Count > 1 && it is Promotional)
                            {
                                Products.Remove(it);
                                break;
                            }
                        }
                        
                    }
                    unOrder.AddReport(nameClient,money, item.Value);
                    item.Value.Clear();
                }
            }
        }//приймає ордери від клієнтів в UoOrder
        public void FulfillOrdersToCashRegister()
        {
            CashRegister.AddReport(unOrder.FulfillOrders());
        }//виконання замовлення з UnOrder in CachRegister 
    }
}
