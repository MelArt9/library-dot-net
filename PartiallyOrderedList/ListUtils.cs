namespace PartiallyOrderedList
{
    // Делегат для проверки элементов списка
    public delegate bool CheckDelegate<T>(T item);

    // Делегат для преобразования элементов списка
    public delegate TO ConvertDelegate<TI, TO>(TI item);

    // Делегат для создания нового списка
    public delegate IList<T> ListConstructorDelegate<T>();

    // Делегат, представляющий функцию для проверки элемента списка с учетом его индекса
    public delegate bool CheckDelegateWithIndex<T>(T item, int index);

    public class ListUtils<T>
    {
        // Статические делегаты для создания списков ArrayList и LinkedList
        public static readonly ListConstructorDelegate<T> ArrayListConstructor = () => new ArrayList<T>();
        public static readonly ListConstructorDelegate<T> LinkedListConstructor = () => new LinkedList<T>();

        // Получение делегата для создания списка на основе типа элементов исходного списка
        public static ListConstructorDelegate<TO>? GetConstructorDelegate<TI, TO>(IList<TI> list)
        {
            switch (list)
            {
                case LinkedList<TI> _:
                    return ListUtils<TO>.LinkedListConstructor;
                case ArrayList<TI> _:
                    return ListUtils<TO>.ArrayListConstructor;
                default:
                    return null;
            }
        }

        // Проверка наличия элемента, удовлетворяющего условию
        public static bool Exists(IList<T> list, CheckDelegate<T> checkDelegate)
        {
            foreach (var item in list)
            {
                if (checkDelegate(item))
                {
                    return true;
                }
            }
            return false;
        }

        // Поиск элемента, удовлетворяющего условию
        public static T Find(IList<T> list, CheckDelegate<T> checkDelegate)
        {
            foreach (var item in list)
            {
                if (checkDelegate(item))
                {
                    return item;
                }
            }
            return default;
        }

        // Поиск последнего элемента, удовлетворяющего условию
        public static T FindLast(IList<T> list, CheckDelegate<T> checkDelegate)
        {
            T resItem = default;
            foreach (var item in list)
            {
                if (checkDelegate(item))
                {
                    resItem = item;
                }
            }
            return resItem;
        }

        // Поиск индекса первого элемента, удовлетворяющего условию
        public static int FindIndex(IList<T> list, CheckDelegate<T> checkDelegate)
        {
            int index = 0;
            foreach (var item in list)
            {
                if (checkDelegate(item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        // Поиск индекса последнего элемента, удовлетворяющего условию
        public static int FindLastIndex(IList<T> list, CheckDelegate<T> checkDelegate)
        {
            int index = list.Count - 1;
            for (int i = index; i >= 0; i--)
            {
                if (checkDelegate(list[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        // Создание списка, содержащего элементы, удовлетворяющие условию
        public static IList<T> FindAll(IList<T> list, CheckDelegate<T> checkDelegate, ListConstructorDelegate<T> constructorDelegate)
        {
            IList<T> resList = constructorDelegate();

            foreach (var item in list)
            {
                if (checkDelegate(item))
                {
                    resList.Add(item);
                }
            }

            return resList;
        }

        // Преобразование всех элементов списка и создание нового списка с преобразованными элементами
        public static IList<TO> ConvertAll<TO>(IList<T> list, ConvertDelegate<T, TO> convertDelegate, ListConstructorDelegate<TO> constructorDelegate)
        {
            try
            {
                if (list is UnmutableList<T>)
                {
                    throw new UnmutableListException("Ошибка: Список не поддерживает операции изменения.");
                }

                IList<TO> resList = constructorDelegate();

                foreach (var item in list)
                {
                    TO addedItem = convertDelegate(item);
                    resList.Add(addedItem);
                }

                Console.WriteLine("Преобразованный список:");
                foreach (var item in resList)
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine();

                return resList;
            }
            catch (UnmutableListException ex)
            {
                Console.WriteLine(ex.Message);
                return null; // Возвращаем null или другое значение по умолчанию в случае ошибки
            }
        }

        // Изменение всех элементов списка с использованием заданной функции
        public static void ForEach(IList<T> list, Func<T, T> func)
        {
            try
            {
                if (list == null)
                {
                    throw new ArgumentNullException(nameof(list));
                }

                if (list is UnmutableList<T>)
                {
                    throw new UnmutableListException("Ошибка: Список не поддерживает операции изменения элементов.");
                }

                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = func(list[i]);
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (UnmutableListException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Измененный список:");
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }

        // Сортировка списка с использованием сравнения
        public static void Sort<T>(IList<T> list, Comparison<T> comparison)
        {
            if (list is List<T> concreteList)
            {
                concreteList.Sort(comparison);
            }
            else
            {
                List<T> tempList = new List<T>(list);
                tempList.Sort(comparison);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = tempList[i];
                }
            }
        }

        // Проверка, что все элементы списка удовлетворяют условию
        public static bool CheckForAll<T>(IList<T> list, CheckDelegateWithIndex<T> checkDelegateWithIndex)
        {
            for (int index = 0; index < list.Count; index++)
            {
                if (!checkDelegateWithIndex(list[index], index))
                {
                    return false;
                }
            }
            return true;
        }

        // Удаление элементов списка, удовлетворяющих условию.
        public static void Remove<T>(IList<T> list, Predicate<T> predicate)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (list is UnmutableList<T>)
            {
                throw new UnmutableListException("Ошибка: Список не поддерживает операции изменения.");
            }

            int index = list.Count - 1;
            while (index >= 0)
            {
                if (predicate(list[index]))
                {
                    list.RemoveAt(index);
                }
                index--;
            }
        }
    }
}