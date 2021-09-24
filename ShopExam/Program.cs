using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml.Serialization;

namespace ShopExam
{
    class Program
    {
        static void Main(string[] args)
        {
            /* UML class diagram у вкладені
             * Інформаційна система додає строкові та безстрокові продукти, а також формує бокси з доданих продуктів зі знижкою у відсотках (зберігає та завантажує)
             * Тримає в собі колекцію не виконаних замовлень від клієнтів (зберігає та завантажує)
             * Тримає в собі колекцію виконаних замовлень від клієнтів (зберігає та завантажує)
             * 
             * "НЕ Виконано" пошук по штрихкоду, видалення продуктів адміном, перевантажені оператори
             * "НЕкоректно працює" збільшення та шменшення кількості наявності продуктів
             * (Причина час)
             * 
             * Клієнт має можливість додавати у кошик продукти та видаляти їх
             * Клієну надається можливість підтвердження покупки товарів із внесенням коштів
             */
            string pathCatalog = @"Catalog.bin";
            string bankAccount = @"bankAccount.bin";
            string unOrder = @"unOrder.bin";
            ConsoleMenu menu = new ConsoleMenu(pathCatalog, bankAccount, unOrder);
            menu.Start();
        }
    }
}
