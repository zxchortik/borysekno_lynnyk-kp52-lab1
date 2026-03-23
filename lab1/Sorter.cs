using System.Diagnostics;

namespace lab1;

public class Sorter
{
    private List<Record> collection;
    private SortStatistics stats;
    private List<string> intermediateSteps;

    public Sorter()
    {
        InitCollection();
        stats = new SortStatistics();
    }

    public void InitCollection()
    {
        collection = new List<Record>();
        intermediateSteps = new List<string>();
    }

    public void AddRecord(Record record)
    {
        collection.Add(record);
    }

    public void RemoveRecord(string sku)
    {
        for (int i = 0; i < collection.Count; i++)
        {
            if (collection[i].Sku.Equals(sku, StringComparison.OrdinalIgnoreCase))
            {
                collection.RemoveAt(i);
                Console.WriteLine($"Товар з SKU '{sku}' видалено.");
                return;
            }
        }

        Console.WriteLine("Товар з таким SKU не знайдено.");
    }

    public void PrintCollection()
    {
        if (collection.Count == 0)
        {
            Console.WriteLine("Колекція порожня.");
            return;
        }

        for (int i = 0; i < collection.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {collection[i]}");
        }
    }

    public void GenerateControlData()
    {
        InitCollection();

        AddRecord(new Record("A001", "Сік яблучний", "Напої", 45.50m));
        AddRecord(new Record("B002", "Хліб білий", "Випічка", 25.00m));
        AddRecord(new Record("C003", "Молоко 2.5%", "Молочне", 38.00m));
        AddRecord(new Record("D004", "Шоколад", "Солодощі", 45.50m));
        AddRecord(new Record("E005", "Масло вершкове", "Молочне", 75.20m));
        AddRecord(new Record("F006", "Банани", "Фрукти", 60.00m));
        AddRecord(new Record("G007", "Яблука", "Фрукти", 25.00m));
        AddRecord(new Record("H008", "Кава розчинна", "Напої", 120.00m));
        AddRecord(new Record("I009", "Чай чорний", "Напої", 55.00m));
        AddRecord(new Record("J010", "Печиво", "Солодощі", 32.40m));
        AddRecord(new Record("K011", "Свинина", "М'ясо", 180.00m));
        AddRecord(new Record("L012", "Кефір", "Молочне", 40.00m));

        Console.WriteLine("Контрольні дані успішно згенеровано (12 елементів)");
    }

    public void SortCollection()
    {
        if (collection.Count <= 1)
        {
            return;
        }

        stats.Reset();
        intermediateSteps.Clear();

        Stopwatch sw = Stopwatch.StartNew();
        QuickSort(0, collection.Count - 1);
        sw.Stop();

        stats.ExecutionTime = sw.Elapsed;
        Console.WriteLine("Сортування завершено");
    }

    private void Swap(int i, int j)
    {
        if (i == j)
        {
            return;
        }

        stats.Swaps++;
        Record temp = collection[i];
        collection[i] = collection[j];
        collection[j] = temp;
    }

    private int Partition(int low, int high)
    {
        Record pivot = collection[high];
        intermediateSteps.Add($"Діапазон [{low}..{high}]. Обрано pivot: {pivot.ProductName} (Ціна: {pivot.Price})");

        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            stats.Comparisons++;

            if (collection[j].Price < pivot.Price || (collection[j].Price == pivot.Price && string.Compare(collection[j].ProductName, pivot.ProductName, StringComparison.OrdinalIgnoreCase) < 0))
            {
                i++;
                Swap(i, j);
            }
        }

        Swap(i + 1, high);
        return i + 1;
    }

    private void QuickSort(int low, int high)
    {
        stats.RecursiveCalls++;
        if (low < high)
        {
            int pi = Partition(low, high);
            QuickSort(low, pi - 1);
            QuickSort(pi + 1, high);
        }
    }

    public void PrintIntermediateSteps()
    {
        if (intermediateSteps.Count == 0)
        {
            Console.WriteLine("Кроки не записані. Спочатку виконайте сортування.");
            return;
        }

        for (int i = 0; i < intermediateSteps.Count; i++)
        {
            Console.WriteLine($"Крок {i + 1}: {intermediateSteps[i]}");
        }
    }

    public void PrintStatistics()
    {
        Console.WriteLine($"Кількість порівнянь: {stats.Comparisons}");
        Console.WriteLine($"Кількість перестановок: {stats.Swaps}");
        Console.WriteLine($"Кількість рекурсивних викликів: {stats.RecursiveCalls}");
        Console.WriteLine($"Час виконання: {stats.ExecutionTime.TotalMilliseconds} мс");
    }

    public void PrintTop10Cheapest()
    {
        Console.WriteLine("10 найдешевших товарів");
        int limit = collection.Count < 10 ? collection.Count : 10;

        for (int i = 0; i < limit; i++)
        {
            Console.WriteLine($"[{i + 1}] {collection[i]}");
        }
    }

    public void PrintCheaperThan(decimal maxPrice)
    {
        Console.WriteLine($" Товари, дешевші за {maxPrice}:");
        bool found = false;

        for (int i = 0; i < collection.Count; i++)
        {
            if (collection[i].Price < maxPrice)
            {
                Console.WriteLine(collection[i]);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Таких товарів не знайдено.");
        }
    }
}