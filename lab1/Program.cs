namespace lab1;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Sorter sorter = new Sorter();

        while (true)
        {
            Console.WriteLine("1. Створити порожню колекцію");
            Console.WriteLine("2. Згенерувати контрольні дані (12 елементів)");
            Console.WriteLine("3. Додати товар з клавіатури");
            Console.WriteLine("4. Видалити товар за SKU");
            Console.WriteLine("5. Вивести вміст колекції");
            Console.WriteLine("6. Відсортувати колекцію");
            Console.WriteLine("7. Вивести проміжні етапи роботи алгоритму");
            Console.WriteLine("8. Вивести статистику виконання алгоритму");
            Console.WriteLine("9. Вивести 10 найдешевших товарів");
            Console.WriteLine("10. Знайти товари, дешевші за вказану суму");
            Console.WriteLine("0. Вийти з програми");
            Console.Write("Оберіть дію: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                sorter.InitCollection();
                Console.WriteLine("Створено нову порожню колекцію.");
            }
            else if (choice == "2")
            {
                sorter.GenerateControlData();
            }
            else if (choice == "3")
            {
                Record newRecord = ReadRecordFromConsole();
                sorter.AddRecord(newRecord);
                Console.WriteLine("Товар успішно додано!");
            }
            else if (choice == "4")
            {
                Console.Write("Введіть SKU товару для видалення: ");
                string sku = Console.ReadLine();
                sorter.RemoveRecord(sku);
            }
            else if (choice == "5")
            {
                sorter.PrintCollection();
            }
            else if (choice == "6")
            {
                sorter.SortCollection();
            }
            else if (choice == "7")
            {
                sorter.PrintIntermediateSteps();
            }
            else if (choice == "8")
            {
                sorter.PrintStatistics();
            }
            else if (choice == "9")
            {
                sorter.PrintTop10Cheapest();
            }
            else if (choice == "10")
            {
                Console.Write("Введіть максимальну ціну: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal maxPrice) && maxPrice >= 0)
                {
                    sorter.PrintCheaperThan(maxPrice);
                }
                else
                {
                    Console.WriteLine("Помилка: введено некоректну суму. Введіть додатне число.");
                }
            }
            else if (choice == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("Невідома команда. Будь ласка, введіть число від 0 до 10.");
            }
        }
    }

    static Record ReadRecordFromConsole()
    {
        Console.Write("Введіть SKU (наприклад, A001): ");
        string sku = Console.ReadLine();

        Console.Write("Введіть назву товару: ");
        string name = Console.ReadLine();

        Console.Write("Введіть категорію: ");
        string category = Console.ReadLine();

        decimal price = 0;
        bool validPrice = false;

        while (!validPrice)
        {
            Console.Write("Введіть ціну: ");
            if (decimal.TryParse(Console.ReadLine(), out price) && price >= 0)
            {
                validPrice = true;
            }
            else
            {
                Console.WriteLine("Помилка: ціна має бути числом більше або дорівнювати нулю. Спробуйте ще раз.");
            }
        }

        return new Record(sku, name, category, price);
    }
}