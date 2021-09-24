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
    public class CashRegister // патерн Одинак
    {
        [Serializable]
        private class report : IReport
        {
            public string nameClient { get; set; }
            public decimal money { get; set; } = 0.0M;
            public List<IProduct> products { get; set; }
            public DateTime timeSales { get; set; }
            public override string ToString()
            {
                string listIPproduct = "";
                products.ForEach(delegate (IProduct it) { listIPproduct += "\n" + it.ToString(); });
                return $"\n\nTime Sales {timeSales}\nName Client {nameClient}\nCost Sales {money}\n{listIPproduct}";
            }
        }//вкладений клас для тримання колекції підверджених ордерів
        
        private static CashRegister uniqueInstans { get; set; }
        private CashRegister() { }
        public static CashRegister Instanse()
        {
            if (uniqueInstans == null)
            {
                uniqueInstans = new CashRegister();
                return uniqueInstans;
            }
            return uniqueInstans;
        }
         List<IReport> reportsList { get; set; } = new List<IReport>();
        public void AddReport(List<IReport> reports)
        {
            //reportsList = new List<IReport>(reports);
            foreach (var item in reports)
            {
                reportsList.Add(item);
            }

        } // приймає звіт від UnOrder
        public decimal BankAccount()
        {
            decimal allSum = 0.0M;
            reportsList.ForEach(delegate (IReport it) { allSum += it.money; });
            return allSum;
        }// ретурнить позитивний залишок рахунку
        public string ReportAllSales()
        {
            string all = "";
            reportsList.ForEach(delegate (IReport it) { all += it.ToString(); });
            return all;
        }// виводить звіт
    }
}
