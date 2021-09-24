using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ShopExam
{
    class DataManagerBin // запис та читання даних
    {
        public string Path { get; private set; } = "";
        public string PathBankAccount { get; private set; } = "";
        public string PathUnOrder { get; private set; } = "";
        public DataManagerBin(string path, string pathBank, string unOrder)
        {
            Path = path;
            PathBankAccount = pathBank;
            PathUnOrder = unOrder;
        }

        public void SaveDataBankAccount(CashRegister cashRegister)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(PathBankAccount, FileMode.Create, FileAccess.Write))
                    formatter.Serialize(stream, cashRegister);
                Console.WriteLine("CashRegister save");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public CashRegister LoadDataBankAccount()
        {
            CashRegister cashRegister = CashRegister.Instanse();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(PathBankAccount, FileMode.Open, FileAccess.Read))
                    cashRegister = formatter.Deserialize(stream) as CashRegister;
                Console.WriteLine($"CashRegister load from {PathBankAccount}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return cashRegister;
        }
        public void SaveData(ProductCollection products)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(Path, FileMode.Create, FileAccess.Write))
                    formatter.Serialize(stream, products);
                Console.WriteLine("Products save");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public ProductCollection LoadData()
        {
            ProductCollection products = new ProductCollection();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                    products = formatter.Deserialize(stream) as ProductCollection;
                Console.WriteLine($"ProductList load from {Path}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return products;
        }
        public void SaveDataUnOrder(UnOrder unOrder)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(PathUnOrder, FileMode.Create, FileAccess.Write))
                    formatter.Serialize(stream, unOrder);
                Console.WriteLine("unOrder save");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public UnOrder LoadDataUnOrder()
        {
            UnOrder unOrder = UnOrder.Instanse();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(PathUnOrder, FileMode.Open, FileAccess.Read))
                    unOrder = formatter.Deserialize(stream) as UnOrder;
                Console.WriteLine($"unOrder load from {PathUnOrder}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return unOrder;
        }
    }
}
