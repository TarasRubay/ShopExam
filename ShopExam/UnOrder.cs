using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopExam
{
        [Serializable]
    public class UnOrder 
    {
        [Serializable]
        private class report : IReport
        {
            public string nameClient { get; set; }
            public decimal money { get; set; }
            public List<IProduct> products { get; set; }
            public DateTime timeSales { get; set; }
            public override string ToString()
            {
                string listIPproduct = "";
                products.ForEach(delegate (IProduct it) { listIPproduct += "\n" + it.ToString(); });
                return $"\n\nTime Sales {timeSales}\nName Client {nameClient}\nCost Sales {money}\n{listIPproduct}";
            }
        }

        private static UnOrder uniqueInstans { get; set; }
        private UnOrder() { }
        public static UnOrder Instanse()
        {
            if (uniqueInstans == null)
            {
                uniqueInstans = new UnOrder();
                return uniqueInstans;
            }
            return uniqueInstans;
        }
        List<IReport> UnOrderList { get; set; } = new List<IReport>();
        public void AddReport(string nameClient, decimal money, in List<IProduct> products)
        {

            report reporT = new report();
            reporT.timeSales = DateTime.Now;
            reporT.nameClient = nameClient;
            reporT.money = money;
            reporT.products = new List<IProduct>(products);
            UnOrderList.Add(reporT);
        }
       
        public string ReportAllSales()
        {
            string all = "";
            UnOrderList.ForEach(delegate (IReport it) { all += it.ToString(); });
            return all;
        } // Виводить дані про замовлення від клієнтів
        public List<IReport> FulfillOrders() {
            List<IReport> TmpUnOrderList = new List<IReport>(UnOrderList);
            UnOrderList.Clear();
            return TmpUnOrderList;
        }  // відправляє дані про виконння замовлення
        
    }
}
