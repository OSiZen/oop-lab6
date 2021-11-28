using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace lab6
{
    //Task 1 - 4
    public interface ISignal
    {
        int year { get; set; }
        string name { get; set; }
        int diameter { get; set; }
        int frequency { get; set; }
    }

    class Signal : ISignal, IComparable<Signal>
    {
        public int year { get; set; }
        public string name { get; set; }
        public int diameter { get; set; }
        public int frequency { get; set; }
        int ISignal.year { get; set; }
        string ISignal.name { get; set; }
        int ISignal.diameter { get; set; }
        int ISignal.frequency { get; set; }

        public Signal(int year, string name, int diameter, int frequency)
        {
            this.year = year;
            this.name = name;
            this.diameter = diameter;
            this.frequency = frequency;
        }

        public string Info
        {
            get { return $"{year} {name}"; }
        }

        public int CompareTo(Signal other)
        {
            return string.Compare(other.Info, Info, StringComparison.InvariantCultureIgnoreCase);
        }

        public override string ToString()
        {
            return String.Format("{0,20}|{1,20}|{2,20}|{3,20}", year, name, diameter, frequency);
        }
    }

    class CollectionType<T> : IEnumerable<T> where T : Signal
    {
        List<T> list = new List<T>();

        public CollectionType()
        {
            list = new List<T>();
        }

        public int Count
        {
            get { return list.Count; }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return list[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                list[index] = value;
            }
        }

        public void Add(T signal)
        {
            list.Add(signal);
        }

        public T Remove(T signal)
        {
            var element = list.FirstOrDefault(h => h == signal);
            if (element != null)
            {
                list.Remove(element);
                return element;
            }
            throw new NullReferenceException();
        }

        public void Sort()
        {
            list.Sort();
        }

        public T GetByName(string name)
        {
            return
            list.FirstOrDefault(
            h => string.Compare(h.Info, name, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    //Task 5
    class CollectionType
    {
        LinkedList<CollectionType> link = new LinkedList<CollectionType>();
        int year { get; set; }
        string name { get; set; }
        int diameter { get; set; }
        int frequency { get; set; }

        public CollectionType() {}

        public CollectionType(int year, string name, int diameter, int frequency)
        {
            this.year = year;
            this.name = name;
            this.diameter = diameter;
            this.frequency = frequency;
        }

        public void Add(CollectionType[] coll)
        {
            for (int i = 0; i < coll.Length; i++)
            {
                link.AddLast(coll[i]);
            }
        }

        public void Output()
        {
            Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", "Рiк ", "Науковий керiвник ", "Дiаметр антени (м) ", "Робоча частота (Мгц)"));
            foreach (CollectionType s in link)
            {
                Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", s.year, s.name, s.diameter, s.frequency));
            }
        }

        public void Select()
        {
            Console.WriteLine("\n                Запит 1:");
            var where = link.Where(h => (h.diameter >= 100 && h.frequency > 1450));
            Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", "Рiк ", "Науковий керiвник ", "Дiаметр антени (м) ", "Робоча частота (Мгц)"));
            foreach (var c in where)
            {
                Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", c.year, c.name, c.diameter, c.frequency));
            }
            Console.WriteLine("\n                Запит 2:");
            var min = link.Min(h => h.frequency);
            Console.WriteLine($"                {min}");
            Console.WriteLine("\n                Запит 3:");
            var max = link.Max(h => h.frequency);
            Console.WriteLine($"                {max}");
            Console.WriteLine("\n                Запит 4:");
            var count = link.Count();
            Console.WriteLine($"                {count}");
            Console.WriteLine("\n                Запит 5:");
            var order = link.OrderBy(h => h.diameter).ThenByDescending(h => h.year);
            Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", "Рiк ", "Науковий керiвник ", "Дiаметр антени (м) ", "Робоча частота (Мгц)"));
            foreach (var c in order)
            {
                Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", c.year, c.name, c.diameter, c.frequency));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Signal sgn1 = new Signal(1960, "Дрейк", 26, 1420);
            Signal sgn2 = new Signal(1970, "Троїцкий", 14, 1875);
            Signal sgn3 = new Signal(1978, "Хоровiц", 300, 1665);
            Signal sgn4 = new Signal(1981, "Барановський", 180, 1774);

            CollectionType<Signal> collection = new CollectionType<Signal>();
            collection.Add(sgn1);
            collection.Add(sgn2);
            collection.Add(sgn3);
            collection.Add(sgn4);
            collection.Remove(sgn4);

            Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", "Рiк ", "Науковий керiвник ", "Дiаметр антени (м) ", "Робоча частота (Мгц)"));
            foreach (Signal s in collection)
            {
                Console.WriteLine(s.ToString());
            }

            Signal sgn5 = new Signal(1980, "Саржков", 45, 1256);
            Signal sgn6 = new Signal(1983, "Лузкович", 155, 1733);
            Signal sgn7 = new Signal(1988, "Прокопенко", 288, 1888);

            CollectionType<Signal> collection2 = new CollectionType<Signal>();
            collection.Add(sgn5);
            collection.Add(sgn6);
            collection.Add(sgn7);

            var list = new List<CollectionType<Signal>>();
            list.Add(collection);
            list.Add(collection2);

            Console.WriteLine("\n                OrderBy:");
            var order = collection.OrderBy(h => h.diameter).ThenBy(h => h.year);
            foreach (var signal in order)
            {
                Console.WriteLine(signal);
            }
            Console.WriteLine("\n                where:");
            var where = collection.Where(h => (h.diameter >= 100 && h.frequency > 1450) || h.Info.StartsWith("L"));
            foreach (var signal in where)
            {
                Console.WriteLine(signal.ToString());
            }
            Console.WriteLine("\n                Select:");
            var select = collection.Select((h, i) => new { Index = i + 1, h.Info });
            foreach (var s in select)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("\n                Skip:");
            var skip = collection.Skip(3);
            foreach (var signal in skip)
            {
                Console.WriteLine(signal);
            }
            Console.WriteLine("\n                Take:");
            var take = collection.Take(3);
            foreach (var signal in take)
            {
                Console.WriteLine(signal);
            }
            Console.WriteLine("\n                Concat:");
            var concat = collection.Concat(collection2);
            foreach (var signal in concat)
            {
                Console.WriteLine(signal);
            }
            Console.WriteLine("\n                First:");
            var first = collection.First(h => h.Info.Length > 5);
            Console.WriteLine(first);
            Console.Write("\n                Min: ");
            var min = collection.Min(h => h.frequency);
            Console.WriteLine(min);
            Console.Write("\n                Max: ");
            var max = collection.Max(h => h.frequency);
            Console.WriteLine(max);
            Console.WriteLine("\nAll and Any:");
            var allAny = list.First(c => c.All(h => h.diameter >= 14) && c.Any(h => h is Signal)).Select(h => h.Info).OrderByDescending(s => s);
            foreach (var str in allAny)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine("\nContains:");
            var contains = list.Where(c => c.Contains(sgn1)).SelectMany(c => c.SelectMany(h => h.Info.Split(' '))).Distinct().OrderBy(s => s).ToList();
            foreach (var str in contains)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine();

            CollectionType ct = new CollectionType();

            CollectionType[] collections = {
                new CollectionType(1960, "Дрейк", 26, 1420),
                new CollectionType(1970, "Троїцкий", 14, 1875),
                new CollectionType(1978, "Хоровiц", 300, 1665)
            };

            ct.Add(collections);
            ct.Output();
            ct.Select();

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
