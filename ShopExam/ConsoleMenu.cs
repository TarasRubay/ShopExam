using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace ShopExam
{
    public class ConsoleMenu
    {
        [Serializable]
        struct data {
            public string name, info;
            public decimal price;
        }
        public ConsoleMenu(string Path, string PathBanc , string unOrder)
        {
            menu = 100;
            max_menu_client = 5;
            max_menu_admin = 11;
            switch_on = menu;
            path = Path;
            pathBancAccount = PathBanc;
            pathUnOrder = unOrder;
        }
        int menu { get; }
        int max_menu_admin { get; }
        int max_menu_client { get; }
        int switch_on { get; set; }

        string path { get; } = "";
        string pathBancAccount { get; } = "";
        string pathUnOrder { get; } = "";



        public void Start()
        {
            DataManagerBin manager = new DataManagerBin(path, pathBancAccount, pathUnOrder);
            Shop shop = Shop.Instanse();
            shop.Products = manager.LoadData();
            shop.CashRegister = manager.LoadDataBankAccount();
            shop.unOrder = manager.LoadDataUnOrder();

            do
            {
                switch (switch_on)
                {
                    case 100:
                        do
                        {
                            try
                            {
                                Console.WriteLine("1 - Admin");
                                Console.WriteLine("2 - Client");
                                Console.WriteLine("0 - exit");
                                switch_on = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception msg)
                            {
                                Console.WriteLine(msg.Message);
                                switch_on = menu;
                            }
                            Console.Clear();
                        } while (switch_on < 0 || switch_on > 2);

                        break;
                    case 1:
                        Admin(shop,manager);
                        switch_on = menu;
                        break;
                    case 2:
                        Client(shop);
                        switch_on = menu;
                        break;
                    
                    default:
                        break;
                }

            } while (switch_on != 0);
            manager.SaveData(shop.Products);
            manager.SaveDataUnOrder(shop.unOrder);
        } // старт програми
        void PrintAdmin()
        {
            Console.WriteLine();
            Console.WriteLine($"1 - add product");
            Console.WriteLine("2 - view UrgentProduct");
            Console.WriteLine("3 - view PerpeualProduct");
            Console.WriteLine("4 - view all");
            Console.WriteLine("5 - view promotional");
            Console.WriteLine("6 - remove product");
            Console.WriteLine("7 - report Bank Account");
            Console.WriteLine("8 - report of all sales");
            Console.WriteLine("9 - Fulfill Orders ");
            Console.WriteLine("10 - report Un Orders ");
            Console.WriteLine("11 - Create Promotional ");
            Console.WriteLine("0 - exit");
        } // виводить меню для адміністратора
        void PrintClient()
        {
            Console.WriteLine();
            Console.WriteLine($"1 - View All");
            Console.WriteLine("2 - View Clothers");
            Console.WriteLine("3 - View Food");
            Console.WriteLine("4 - view basket");
            Console.WriteLine("5 - view Promotional");
            Console.WriteLine("0 - exit");
        } // виводить меню для клієнта
        void AddProduct(Shop shop) {
            
            DateTime productDate; // тимчасова змінна для вводу дати воробництва
            DateTime validUntil; // тимчасова змінна для вводу терміну придатності
            data data; //тимчасова змінна структура для вводу даних Імя, Інформації про продукт, ціни 

            do
            {
                switch (switch_on)
                {
                    case 100:
                        do
                        {
                            try
                            {
                              
                                Console.WriteLine($"1 - Add Clothing Product");
                                Console.WriteLine("2 - Add Food product");
                               
                                Console.WriteLine("0 - exit");
                                switch_on = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception msg)
                            {
                                Console.WriteLine(msg.Message);
                                switch_on = menu;
                            }
                            Console.Clear();
                        } while (switch_on < 0 || switch_on > 2);

                        break;
                    case 1:
                        Console.WriteLine("Clothing product");
                        data = enterData();
                        Console.WriteLine("date production");
                        productDate = enterDate();
                        Product product = new UrgentProduct(productDate, data.name, data.info, data.price);
                        bool flag = true;
                        if(shop.Products.Count() != 0) 
                        { 
                           
                           
                                foreach (var item in shop.Products)
                                    if (item.Barcode == product.Barcode)
                                    {
                                        item.Count++;
                                        flag = false;
                                        break;
                                    }
                           if(flag) shop.Products.Add(product);
                        }
                        else shop.Products.Add(product);
                        switch_on = menu;
                        break;
                    case 2:
                        Console.WriteLine($"Food Product");
                        data = enterData();
                        do
                        {
                        Console.WriteLine("date production");
                        productDate = enterDate();
                        Console.WriteLine("enter valid product date");
                        validUntil = enterDate();
                            if (productDate > validUntil) Console.WriteLine("not valid data");
                        } while (productDate > validUntil);
                        
                        Product product2 = new PerpeualProduct(productDate, validUntil, data.name, data.info, data.price);
                        bool flag2 = true;
                        if (shop.Products.Count() != 0)
                        {


                            foreach (var item in shop.Products)
                                if (item.Barcode == product2.Barcode)
                                {
                                    item.Count++;
                                    flag2 = false;
                                    break;
                                }
                            if (flag2) shop.Products.Add(product2);
                        }
                        else shop.Products.Add(product2);
                        switch_on = menu;
                        break;

                    default:
                        break;
                }

            } while (switch_on != 0);

        }//додавання продуктів адміністратором
        private data enterData()
        {

            data data;//тимчасова змінна структура для вводу даних Імя, Інформації про продукт, ціни
            data.price = 0.0M;
            Console.WriteLine("Enter Name Product");
            data.name = Console.ReadLine();
            Console.WriteLine("Enter Info Product");
            data.info = Console.ReadLine();
            do
            {
                try
                {
                    Console.WriteLine("Enter Price");
                    data.price = Convert.ToDecimal(Console.ReadLine());
                    switch_on = 1;
                }
                catch (Exception msg)
                {
                    Console.Clear();
                    Console.WriteLine(msg.Message);
                    switch_on = 0;
                }
            } while (switch_on != 1);
            return new data {name = data.name,info = data.info,price = data.price};
        }//введення даних продукту
        private DateTime enterDate() {
            int year = -1;
            int mount = -1;
            int day = -1;
            do
            {
                do
            {
                try
                {
                    Console.WriteLine("Enter year");
                    year = Convert.ToInt32(Console.ReadLine());
                    switch_on = 1;
                }
                catch (Exception msg)
                {
                    Console.Clear();
                    Console.WriteLine(msg.Message);
                    switch_on = 0;
                }
            } while (switch_on != 1);
            do
            {
                try
                {
                    Console.WriteLine("Enter mount");
                    mount = Convert.ToInt32(Console.ReadLine());
                    switch_on = 1;
                }
                catch (Exception msg)
                {
                    Console.Clear();
                    Console.WriteLine(msg.Message);
                    switch_on = 0;
                }
            } while (switch_on != 1);
            do
            {
                try
                {
                    Console.WriteLine("Enter day");
                    day = Convert.ToInt32(Console.ReadLine());
                    switch_on = 1;
                }
                catch (Exception msg)
                {
                    Console.Clear();
                    Console.WriteLine(msg.Message);
                    switch_on = 0;
                }
            } while (switch_on != 1);
                try
                {
                    switch_on = 1;
                    return new DateTime(year,mount,day);
                }
                catch (Exception msg)
                {
                    Console.Clear();
                    Console.WriteLine(msg.Message);
                    switch_on = 0;
                }
            } while (switch_on != 1);
            
                    return new DateTime(year,mount,day);// тут явний exception, але сюди б не мало дойти
        }// введення дати виробництва та придатності
        private void Admin(Shop shop, DataManagerBin manager) {
            switch_on = menu;
            do
            {
                switch (switch_on)
                {
                    case 100:
                        do
                        {
                            try
                            {
                                PrintAdmin();
                                switch_on = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception msg)
                            {
                                Console.WriteLine(msg.Message);
                                switch_on = menu;
                            }
                            Console.Clear();
                        } while (switch_on < 0 || switch_on > max_menu_admin);

                        break;
                    case 1:
                        Console.WriteLine("Add product");
                        switch_on = menu;
                        AddProduct(shop);
                        switch_on = menu;
                        break;
                    case 2:
                        Console.WriteLine("UrgentProduct");
                        if (shop.Products.Count() != 0)
                            foreach (var item in shop.Products)
                            {
                                if (item is UrgentProduct)
                                {
                                    Console.WriteLine(item);
                                    Console.WriteLine();
                                }
                            }

                        switch_on = menu;
                        break;
                    case 3:
                        Console.WriteLine("PerpeualProduct");
                        if (shop.Products.Count() != 0)
                            foreach (var item in shop.Products)
                            {
                                if (item is PerpeualProduct)
                                {
                                    Console.WriteLine(item);
                                    Console.WriteLine();
                                }
                            }

                        switch_on = menu;
                        break;
                    case 4:
                        Console.WriteLine("All");
                        if (shop.Products.Count() != 0)
                            foreach (var item in shop.Products)
                            {

                                Console.WriteLine(item.ToShort());
                                Console.WriteLine();

                            }

                        switch_on = menu;
                        break;
                    case 5:
                        Console.WriteLine("view promotional");

                        if (shop.Products.Count() != 0)
                            foreach (var item in shop.Products)
                            {
                                if (item is Promotional)
                                {
                                    Console.WriteLine(item);
                                    Console.WriteLine();
                                }
                            }
                        switch_on = menu;
                        break;
                    case 6:
                        Console.WriteLine("Remove product");
                      

                        switch_on = menu;
                        break;
                    case 7:
                        Console.WriteLine("report Bank Account");
                        Console.WriteLine($"all sum in account {shop.CashRegister.BankAccount()} UAH");
                        switch_on = menu;
                        break;
                    case 8:
                        Console.WriteLine("report of all sales");
                        Console.WriteLine(shop.CashRegister.ReportAllSales());
                        switch_on = menu;
                        break;
                    case 9:
                        Console.WriteLine("Fulfill Orders");
                        shop.FulfillOrdersToCashRegister();
                        manager.SaveDataBankAccount(shop.CashRegister);
                        manager.SaveDataUnOrder(shop.unOrder);
                        switch_on = menu;
                        break;
                    case 10:
                        Console.WriteLine("report Un Orders");
                        Console.WriteLine(shop.unOrder.ReportAllSales());
                        switch_on = menu;
                        break;
                    case 11:
                        Console.WriteLine("Create Promotional");
                        CreatePromotional(shop);
                        switch_on = menu;
                        break;

                    default:
                        break;
                }

            } while (switch_on != 0);
        }//меню адміністратора
        private void Client(Shop shop)

        {
            string NameClient = "";
                                Console.WriteLine("Enter you name");
                                NameClient = Console.ReadLine();
            switch_on = menu;
            do
            {
                switch (switch_on)
                {
                    case 100:
                        do
                        {
                            try
                            {
                                PrintClient();
                                switch_on = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception msg)
                            {
                                Console.WriteLine(msg.Message);
                                switch_on = menu;
                            }
                            Console.Clear();
                        } while (switch_on < 0 || switch_on > max_menu_client);

                        break;
                    case 1:
                        Console.WriteLine("Wiew product");
                        
                        if (shop.Products.Count() != 0)
                            foreach (var item in shop.Products)
                                Console.WriteLine(item.ToShort());
                        
                        switch_on = menu;
                        break;
                    case 2:
                        Console.WriteLine("Clother");
                        try
                        {
                            IEnumerable<IProduct> seq = shop.Products.Where(it => it is UrgentProduct);
                            var prod = ChoiceMenuForClient(seq);
                            if (prod != null) ViewOrBuy(prod, shop,NameClient);
                        }
                        catch {
                            Console.WriteLine("Non Product");
                        }
                        switch_on = menu;
                        break;
                    case 3:
                        Console.WriteLine("Food");
                        try
                        {
                            IEnumerable<IProduct> seq1 = shop.Products.Where(it => it is PerpeualProduct);
                            var prod1 = ChoiceMenuForClient(seq1);
                            if(prod1 != null)ViewOrBuy(prod1, shop, NameClient);
                        }
                        catch
                        {
                            Console.WriteLine("Non Product");
                        }
                        switch_on = menu;
                        break;
                    case 4:
                        Console.WriteLine("Basket");
                        BuyAllBasketOrRemoveOneElement(shop,NameClient);
                        switch_on = menu;
                        break;
                    case 5:
                        Console.WriteLine("view Promotional");
                        IEnumerable<IProduct> seq2 = shop.Products.Where(it => it is Promotional);
                        var prod2 = ChoiceMenuForClient(seq2);
                        if (prod2 != null) ViewOrBuy(prod2, shop, NameClient);
                        switch_on = menu;
                        break;
                    case 6:
                        Console.WriteLine("save");
                       
                        switch_on = menu;
                        break;

                    default:
                        break;
                }

            } while (switch_on != 0);

        } //меню клієнта
        private IProduct ChoiceMenuForClient(IEnumerable<IProduct> products)
        {
              do
              {
             
                  try
                  {
                    switch_on = 1;
                    foreach (var item in products)
                    {
                        Console.WriteLine($"{switch_on++} - {item.ToShort()}");
                    }
                    Console.WriteLine("0 - exit");
                    Console.WriteLine("Choice Number Product");
                      switch_on = Convert.ToInt32(Console.ReadLine());
                    if (switch_on != 0) {
                        return products.ElementAt(switch_on - 1); 
                    }
                  }
                     catch (Exception msg)
                  {
                    Console.WriteLine(msg.Message);
                    switch_on = menu;
                  }
                  Console.Clear();
              } while (switch_on < 0 || switch_on > products.Count());
            return null;
        }// вибір одного продукту з колекції
        private void ViewOrBuy(in IProduct product,in Shop shop, string nameClient) {
            do
            {

                try
                {
                    
                    Console.WriteLine( product.ToString()); 
                    Console.WriteLine("0 - exit");
                    Console.WriteLine("1 - Buy");
                    switch_on = Convert.ToInt32(Console.ReadLine());
                    //if (switch_on == 1) shop.Basket.Add(product);

                    if (switch_on == 1) {

                        if (shop.BasketClient.ContainsKey(nameClient))
                        {
                            foreach (var item in shop.BasketClient)
                            {
                                if (item.Key == nameClient)
                                {

                                    bool flag = true;
                                    foreach (var it in item.Value)
                                        if (it.Barcode == product.Barcode)
                                        {
                                            it.Count++;
                                            flag = false;
                                            break;
                                        }
                                    if (flag) item.Value.Add(product);

                                }
                            }
                        }
                        else {
                            shop.BasketClient.Add(nameClient,new List<IProduct>());
                            foreach (var item in shop.BasketClient)
                            {
                                if (item.Key == nameClient)
                                {
                                    item.Value.Add(product);
                                }
                            }
                        }

                    }
                }
                catch (Exception msg)
                {
                    Console.WriteLine(msg.Message);
                    switch_on = menu;
                }
                Console.Clear();
            } while (switch_on < 0 || switch_on > 1);

        }// перегляд клієнтом деталів про продукт
        private void BuyAllBasketOrRemoveOneElement(in Shop shop, string nameClient)
        {
            if (shop.BasketClient.ContainsKey(nameClient))
            {
                decimal sumToPay = 0.0M;
                        switch_on = 1;
                foreach (var item in shop.BasketClient)
                {
                    if (item.Key == nameClient)
                    {
                        item.Value.ForEach(delegate (IProduct it) {
                            sumToPay += it.Price;
                            Console.WriteLine($"{switch_on++} - {it}");
                        });
                Console.WriteLine($"\nSum to pay {sumToPay}");
                int tmp = switch_on;
                do
                {
                    try
                    {
                        Console.WriteLine("0 - exit");
                                if (item.Value.Count != 0)
                                {
                                    Console.WriteLine("Choice Number Product to Remove");
                                    Console.WriteLine($"{tmp} - Buy All");
                                    switch_on = Convert.ToInt32(Console.ReadLine());
                                    if (switch_on == tmp)
                                    {
                                        Console.WriteLine("Enter Money");
                                        try
                                        {
                                            shop.orderFormation(Convert.ToDecimal(Console.ReadLine()), nameClient, item.Value);
                                        }
                                        catch (Exception msg)
                                        {
                                            Console.WriteLine(msg.Message);
                                            switch_on = menu;
                                        }
                                    }
                                    else if (switch_on != 0) item.Value.Remove(item.Value.ElementAt(switch_on - 1));
                                }
                                else
                                {
                                    Console.WriteLine("You basket empty");
                                    switch_on = Convert.ToInt32(Console.ReadLine());
                                }
                            }
                    catch (Exception msg)
                    {
                        Console.WriteLine(msg.Message);
                        switch_on = menu;
                    }
                    //Console.Clear();
                } while (switch_on < 0 || switch_on > tmp);

                    }
                }
            }
            else
            {
                Console.WriteLine("You Basket is empty");
            }

        }// перегляд кошика клієнта
        private void CreatePromotional(in Shop shop)
        {
            Promotional PromoProducts = new Promotional();
            do
            {
                try
                {
                    PromoProducts.Add(ChoiceMenuForClient(shop.Products));
                    Console.WriteLine("0 - exit and create promotional");
                    Console.WriteLine("1 - Add more");
                    switch_on = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception msg)
                {
                    Console.WriteLine(msg.Message);
                }
                Console.Clear();
            } while (switch_on < 0 || switch_on > 0);

            do
            {
                try
                {
                    Console.WriteLine("Enter discount for promotional group (in %)");
                    switch_on = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception msg)
                {
                    Console.WriteLine(msg.Message);
                }
                Console.Clear();
            } while (switch_on < 0);
            decimal sum = 0.0M;
            foreach (var item in PromoProducts)
            {
                sum = item.Price;
            }
            PromoProducts.Price = sum - ((sum / 100) * switch_on);
            shop.Products.Add(PromoProducts);
        }// створення акційних товарів адміністратором
    }
}
