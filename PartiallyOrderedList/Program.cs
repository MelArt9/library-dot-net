using PartiallyOrderedList;

// Мельников Артем, 10 группа, лабораторная работа №2
// Разработать библиотеку обобщенных классов для работы с частично упорядоченными списками данных

namespace PartiallyOrderedListDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Демонстрация работы с частично упорядоченными списками");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Выберите тип списка:");
                Console.WriteLine("1. Массивный список (ArrayList)");
                Console.WriteLine("2. Ссылочный список (LinkedList)");
                Console.WriteLine("3. Выход");

                Console.Write("Ваш выбор: ");
                string choice = ReadStringFromConsole();

                Console.Clear();

                if (choice == "1")
                {
                    RunArrayListDemo();
                }
                else if (choice == "2")
                {
                    RunLinkedListDemo();
                }
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите 1, 2 или 3.");
                }
            }
        }

        static void RunArrayListDemo()
        {
            PartiallyOrderedList.IList<int> listArray = new ArrayList<int>();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Массивный список (ArrayList):");
                Console.WriteLine("1.  Добавить элемент");
                Console.WriteLine("2.  Очистить список");
                Console.WriteLine("3.  Проверить наличие элемента в списке");
                Console.WriteLine("4.  Получить индекс первого вхождения элемента");
                Console.WriteLine("5.  Вставить элемент по индексу");
                Console.WriteLine("6.  Удалить заданный элемент во всем списке");
                Console.WriteLine("7.  Удалить элемент по индексу");
                Console.WriteLine("8.  Вывести список");
                Console.WriteLine("9.  Получить подсписок");
                Console.WriteLine("10. Проверить существование заданного элемента");
                Console.WriteLine("11. Найти индекс первого заданного элемента");
                Console.WriteLine("12. Найти индекс последнего заданного элемента");
                Console.WriteLine("13. Вывести список, содержащий все элементы, больше заданного числа");
                Console.WriteLine("14. Преобразовать каждый элемент списка в новый тип");
                Console.WriteLine("15. Выполнить увеличение на указанное число для каждого элемента списка");
                Console.WriteLine("16. Отсортировать список в соответствии с заданным компаратором");
                Console.WriteLine("17. Сравнить элементы списка");
                Console.WriteLine("18. Найти первый элемент, удовлетворяющий условию (кратность 2)");
                Console.WriteLine("19. Найти последний элемент, удовлетворяющий условию (кратность 2)");
                Console.WriteLine("20. Проверить, что все числа положительные");
                Console.WriteLine("21. Сделать список неизменяемым / изменяемым");
                Console.WriteLine("22. Выход");

                Console.Write("Ваш выбор: ");
                string choice = ReadStringFromConsole();

                Console.Clear();

                try
                {
                    if (choice == "1")
                    {
                        Console.Write("Введите значение элемента: ");
                        int value = ReadIntFromConsole();
                        int index = listArray.Add(value);
                        //Console.WriteLine($"Элемент {value} добавлен в список с индексом {index}.");
                    }
                    else if (choice == "2")
                    {
                        listArray.Clear();
                        //Console.WriteLine("Список очищен.");
                    }
                    else if (choice == "3")
                    {
                        Console.Write("Введите значение для проверки: ");
                        int value = ReadIntFromConsole();
                        bool contains = listArray.Contains(value);
                        Console.WriteLine($"Список {(contains ? "содержит" : "не содержит")} элемент {value}.");
                    }
                    else if (choice == "4")
                    {
                        Console.Write("Введите значение элемента для поиска индекса: ");
                        int value = ReadIntFromConsole();
                        int index = ListUtils<int>.FindIndex(listArray, x => x == value);

                        if (index != -1)
                        {
                            Console.WriteLine($"Индекс первого вхождения элемента {value}: {index}");
                        }
                        else
                        {
                            Console.WriteLine($"Элемент {value} не найден в списке.");
                        }
                    }
                    else if (choice == "5")
                    {
                        Console.Write("Введите индекс, на который нужно вставить элемент: ");
                        int index = ReadIntFromConsole();
                        Console.Write("Введите значение элемента для вставки: ");
                        int value = ReadIntFromConsole();
                        listArray.Insert(index, value);
                    }
                    else if (choice == "6")
                    {
                        Console.Write("Введите значение элемента для удаления: ");
                        int value = ReadIntFromConsole();

                        if (listArray is UnmutableList<int>)
                        {
                            Console.WriteLine("Ошибка: Список не поддерживает операции изменения.");
                        }
                        else
                        {
                            if (listArray.Contains(value))
                            {
                                ListUtils<int>.Remove(listArray, x => x == value);
                                Console.WriteLine($"Все вхождения элемента {value} удалено из списка.");
                            }
                            else
                            {
                                Console.WriteLine($"Элемент {value} не найден в списке.");
                            }
                        }
                    }
                    else if (choice == "7")
                    {
                        Console.Write("Введите индекс элемента для удаления: ");
                        int index = ReadIntFromConsole();
                        listArray.RemoveAt(index);
                    }
                    else if (choice == "8")
                    {
                        if (listArray.Count == 0)
                        {
                            Console.WriteLine("Список пуст.");
                        }
                        else
                        {
                            Console.WriteLine("Список:");
                            foreach (var item in listArray)
                            {
                                Console.Write($"{item} ");
                            }
                            Console.WriteLine();
                        }
                    }
                    else if (choice == "9")
                    {
                        Console.Write("Введите начальный индекс для подсписка: ");
                        int fromIndex = ReadIntFromConsole();
                        Console.Write("Введите конечный индекс для подсписка: ");
                        int toIndex = ReadIntFromConsole();
                        var subList = listArray.SubList(fromIndex, toIndex);
                    }
                    else if (choice == "10")
                    {
                        Console.Write("Введите значение для проверки: ");
                        int value = ReadIntFromConsole();
                        bool exists = ListUtils<int>.Exists(listArray, x => x == value);
                        Console.WriteLine($"Существует ли элемент {value}: {exists}");
                    }
                    else if (choice == "11")
                    {
                        Console.Write("Введите значение для поиска: ");
                        int value = ReadIntFromConsole();
                        int index = ListUtils<int>.FindIndex(listArray, x => x == value);
                        if (index != -1)
                        {
                            Console.WriteLine($"Индекс первого вхождения элемента {value}: {index}");
                        }
                        else
                        {
                            Console.WriteLine($"Элемент со значением {value} не найден в списке.");
                        }
                    }
                    else if (choice == "12")
                    {
                        Console.Write("Введите значение для поиска: ");
                        int value = ReadIntFromConsole();
                        int index = ListUtils<int>.FindLastIndex(listArray, x => x == value);
                        if (index != -1)
                        {
                            Console.WriteLine($"Индекс последнего вхождения элемента {value}: {index}");
                        }
                        else
                        {
                            Console.WriteLine($"Элемент со значением {value} не найден в списке.");
                        }
                    }
                    else if (choice == "13")
                    {
                        Console.Write("Введите минимальное значение: ");
                        int minValue = ReadIntFromConsole();

                        var result = ListUtils<int>.FindAll(listArray, x => x > minValue, ListUtils<int>.ArrayListConstructor);

                        if (result.Count > 0)
                        {
                            Console.WriteLine($"Элементы, больше {minValue}:");
                            foreach (var item in result)
                            {
                                Console.Write($"{item} ");
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine($"Нет элементов, больше {minValue} в списке.");
                        }
                    }
                    else if (choice == "14")
                    {
                        Console.WriteLine("Выберите действие:");
                        Console.WriteLine("1. Преобразовать список в строки");
                        Console.WriteLine("2. Преобразовать список в числа типа long");
                        Console.WriteLine("3. Выход");
                        Console.Write("Ваш выбор: ");
                        string choices = ReadStringFromConsole();

                        if (choices == "1")
                        {
                            Console.WriteLine("Преобразование списка целых чисел в список строк:");
                            PartiallyOrderedList.IList<string> stringList = ListUtils<int>.ConvertAll(listArray, x => x.ToString(), ListUtils<string>.ArrayListConstructor);
                            Console.WriteLine();
                        }
                        else if (choices == "2")
                        {
                            Console.WriteLine("Преобразование списка целых чисел в список чисел типа long:");
                            PartiallyOrderedList.IList<long> longList = ListUtils<int>.ConvertAll(listArray, x => (long)x, ListUtils<long>.ArrayListConstructor);
                            Console.WriteLine();
                        }
                        else if (choices == "3")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный выбор. Пожалуйста, выберите 1, 2 или 3.");
                        }
                    }
                    else if (choice == "15")
                    {
                        Console.Write("Введите значение, на которое нужно увеличить каждый элемент: ");
                        int incrementValue = ReadIntFromConsole();
                        ListUtils<int>.ForEach(listArray, item => item += incrementValue);
                        Console.WriteLine();
                    }
                    else if (choice == "16")
                    {
                        Console.WriteLine("Выберите метод сортировки:");
                        Console.WriteLine("1. Сортировка по возрастанию");
                        Console.WriteLine("2. Сортировка по убыванию");
                        Console.Write("Ваш выбор: ");
                        string sortChoice = ReadStringFromConsole();

                        Comparison<int> comparer;

                        if (sortChoice == "1")
                        {
                            comparer = (x, y) => x.CompareTo(y);
                        }
                        else if (sortChoice == "2")
                        {
                            comparer = (x, y) => y.CompareTo(x);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный выбор метода сортировки.");
                            continue;
                        }

                        ListUtils<int>.Sort(listArray, comparer);
                    }
                    else if (choice == "17")
                    {
                        Console.Write("Введите индекс первого элемента: ");
                        if (int.TryParse(Console.ReadLine(), out int index1) && index1 >= 0 && index1 < listArray.Count)
                        {
                            Console.Write("Введите индекс второго элемента: ");
                            if (int.TryParse(Console.ReadLine(), out int index2) && index2 >= 0 && index2 < listArray.Count)
                            {
                                int element1 = listArray[index1];
                                int element2 = listArray[index2];

                                if (listArray.IsComparable(element1) && listArray.IsComparable(element2))
                                {
                                    int comparisonResult = Comparer<int>.Default.Compare(element1, element2);

                                    if (comparisonResult == 0)
                                    {
                                        Console.WriteLine("Первый и второй элементы равны.");
                                    }
                                    else if (comparisonResult < 0)
                                    {
                                        Console.WriteLine("Первый элемент меньше второго.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Первый элемент больше второго.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ошибка: Один или оба элемента не сравнимы.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: Неверный индекс второго элемента.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: Неверный индекс первого элемента.");
                        }
                    }
                    else if (choice == "18")
                    {
                        // Логика для поиска элемента, удовлетворяющего условию (кратность 2)
                        var foundItem = ListUtils<int>.Find(listArray, item => (int)item % 2 == 0);

                        if (!EqualityComparer<int>.Default.Equals(foundItem, default(int)))
                        {
                            Console.WriteLine($"Найден первый элемент, удовлетворяющий условию (кратность 2): {foundItem}");
                        }
                        else
                        {
                            Console.WriteLine("Элемент, удовлетворяющий условию (кратность 2), не найден.");
                        }
                    }
                    else if (choice == "19")
                    {
                        // Логика для поиска последнего элемента, удовлетворяющего условию (кратность 2)
                        var foundItem = ListUtils<int>.FindLast(listArray, item => (int)item % 2 == 0);

                        if (!EqualityComparer<int>.Default.Equals(foundItem, default(int)))
                        {
                            Console.WriteLine($"Найден последний элемент, удовлетворяющий условию (кратность 2): {foundItem}");
                        }
                        else
                        {
                            Console.WriteLine("Элемент, удовлетворяющий условию (кратность 2), не найден.");
                        }
                    }
                    else if (choice == "20")
                    {
                        // Логика для проверки, что все элементы в списке положительные
                        bool allItemsArePositive = ListUtils<int>.CheckForAll(listArray, (item, index) => (int)item >= 0);

                        if (allItemsArePositive)
                        {
                            Console.WriteLine("Все элементы в списке положительные.");
                        }
                        else
                        {
                            Console.WriteLine("Не все элементы в списке положительные.");
                        }
                    }
                    else if (choice == "21")
                    {
                        if (listArray is UnmutableList<int>)
                        {
                            listArray = new ArrayList<int>(listArray);
                            Console.WriteLine("Список стал изменяемым.");
                        }
                        else
                        {
                            listArray = new UnmutableList<int>(listArray);
                            Console.WriteLine("Список стал неизменяемым.");
                        }
                    }
                    else if (choice == "22")
                    {
                        break;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        static void RunLinkedListDemo()
        {
            PartiallyOrderedList.IList<int> listLinked = new PartiallyOrderedList.LinkedList<int>();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Связанный список (LinkedList):");
                Console.WriteLine("1.  Добавить элемент");
                Console.WriteLine("2.  Очистить список");
                Console.WriteLine("3.  Проверить наличие элемента в списке");
                Console.WriteLine("4.  Получить индекс первого вхождения элемента");
                Console.WriteLine("5.  Вставить элемент по индексу");
                Console.WriteLine("6.  Удалить заданный элемент во всем списке");
                Console.WriteLine("7.  Удалить элемент по индексу");
                Console.WriteLine("8.  Вывести список");
                Console.WriteLine("9.  Получить подсписок");
                Console.WriteLine("10. Проверить существование заданного элемента");
                Console.WriteLine("11. Найти индекс первого заданного элемента");
                Console.WriteLine("12. Найти индекс последнего заданного элемента");
                Console.WriteLine("13. Вывести список, содержащий все элементы, больше заданного числа");
                Console.WriteLine("14. Преобразовать каждый элемент списка в новый тип");
                Console.WriteLine("15. Выполнить увеличение на указанное число для каждого элемента списка");
                Console.WriteLine("16. Отсортировать список в соответствии с заданным компаратором");
                Console.WriteLine("17. Сравнить элементы списка");
                Console.WriteLine("18. Найти первый элемент, удовлетворяющий условию (кратность 2)");
                Console.WriteLine("19. Найти последний элемент, удовлетворяющий условию (кратность 2)");
                Console.WriteLine("20. Проверить, что все числа положительные");
                Console.WriteLine("21. Сделать список неизменяемым / изменяемым");
                Console.WriteLine("22. Выход");

                Console.Write("Ваш выбор: ");
                string choice = ReadStringFromConsole();

                Console.Clear();

                try
                {
                    if (choice == "1")
                    {
                        Console.Write("Введите значение элемента: ");
                        int value = ReadIntFromConsole();
                        int index = listLinked.Add(value);
                        //Console.WriteLine($"Элемент {value} добавлен в список с индексом {index}.");
                    }
                    else if (choice == "2")
                    {
                        listLinked.Clear();
                        //Console.WriteLine("Список очищен.");
                    }
                    else if (choice == "3")
                    {
                        Console.Write("Введите значение для проверки: ");
                        int value = ReadIntFromConsole();
                        bool contains = listLinked.Contains(value);
                        Console.WriteLine($"Список {(contains ? "содержит" : "не содержит")} элемент {value}.");
                    }
                    else if (choice == "4")
                    {
                        Console.Write("Введите значение элемента для поиска индекса: ");
                        int value = ReadIntFromConsole();
                        int index = ListUtils<int>.FindIndex(listLinked, x => x == value);

                        if (index != -1)
                        {
                            Console.WriteLine($"Индекс первого вхождения элемента {value}: {index}");
                        }
                        else
                        {
                            Console.WriteLine($"Элемент {value} не найден в списке.");
                        }
                    }
                    else if (choice == "5")
                    {
                        Console.Write("Введите индекс, на который нужно вставить элемент: ");
                        int index = ReadIntFromConsole();
                        Console.Write("Введите значение элемента для вставки: ");
                        int value = ReadIntFromConsole();
                        listLinked.Insert(index, value);
                    }
                    else if (choice == "6")
                    {
                        Console.Write("Введите значение элемента для удаления: ");
                        int value = ReadIntFromConsole();

                        if (listLinked is UnmutableList<int>)
                        {
                            Console.WriteLine("Ошибка: Список не поддерживает операции изменения.");
                        }
                        else
                        {
                            if (listLinked.Contains(value))
                            {
                                ListUtils<int>.Remove(listLinked, x => x == value);
                                Console.WriteLine($"Все вхождения элемента {value} удалено из списка.");
                            }
                            else
                            {
                                Console.WriteLine($"Элемент {value} не найден в списке.");
                            }
                        }
                    }
                    else if (choice == "7")
                    {
                        Console.Write("Введите индекс элемента для удаления: ");
                        int index = ReadIntFromConsole();
                        listLinked.RemoveAt(index);
                    }
                    else if (choice == "8")
                    {
                        if (listLinked.Count == 0)
                        {
                            Console.WriteLine("Список пуст.");
                        }
                        else
                        {
                            Console.WriteLine("Список:");
                            foreach (var item in listLinked)
                            {
                                Console.Write($"{item} ");
                            }
                            Console.WriteLine();
                        }
                    }
                    else if (choice == "9")
                    {
                        Console.Write("Введите начальный индекс для подсписка: ");
                        int fromIndex = ReadIntFromConsole();
                        Console.Write("Введите конечный индекс для подсписка: ");
                        int toIndex = ReadIntFromConsole();
                        var subList = listLinked.SubList(fromIndex, toIndex);
                    }
                    else if (choice == "10")
                    {
                        Console.Write("Введите значение для проверки: ");
                        int value = ReadIntFromConsole();
                        bool exists = ListUtils<int>.Exists(listLinked, x => x == value);
                        Console.WriteLine($"Существует ли элемент {value}: {exists}");
                    }
                    else if (choice == "11")
                    {
                        Console.Write("Введите значение для поиска: ");
                        int value = ReadIntFromConsole();
                        int index = ListUtils<int>.FindIndex(listLinked, x => x == value);
                        if (index != -1)
                        {
                            Console.WriteLine($"Индекс первого вхождения элемента {value}: {index}");
                        }
                        else
                        {
                            Console.WriteLine($"Элемент со значением {value} не найден в списке.");
                        }
                    }
                    else if (choice == "12")
                    {
                        Console.Write("Введите значение для поиска: ");
                        int value = ReadIntFromConsole();
                        int index = ListUtils<int>.FindLastIndex(listLinked, x => x == value);
                        if (index != -1)
                        {
                            Console.WriteLine($"Индекс последнего вхождения элемента {value}: {index}");
                        }
                        else
                        {
                            Console.WriteLine($"Элемент со значением {value} не найден в списке.");
                        }
                    }
                    else if (choice == "13")
                    {
                        Console.Write("Введите минимальное значение: ");
                        int minValue = ReadIntFromConsole();

                        var result = ListUtils<int>.FindAll(listLinked, x => x > minValue, ListUtils<int>.LinkedListConstructor);

                        if (result.Count > 0)
                        {
                            Console.WriteLine($"Элементы, больше {minValue}:");
                            foreach (var item in result)
                            {
                                Console.Write($"{item} ");
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine($"Нет элементов, больше {minValue} в списке.");
                        }
                    }
                    else if (choice == "14")
                    {
                        Console.WriteLine("Выберите тип для преобразования:");
                        Console.WriteLine("1. Преобразовать список в строки");
                        Console.WriteLine("2. Преобразовать список в числа типа long");
                        Console.WriteLine("3. Выход");
                        Console.Write("Ваш выбор: ");
                        string convertChoice = ReadStringFromConsole();

                        if (convertChoice == "1")
                        {
                            Console.WriteLine("Преобразование ссылочного списка (LinkedList) в список строк:");
                            PartiallyOrderedList.IList<string> stringList = ListUtils<int>.ConvertAll(listLinked, x => x.ToString(), ListUtils<string>.LinkedListConstructor);
                            Console.WriteLine();
                        }
                        else if (convertChoice == "2")
                        {
                            Console.WriteLine("Преобразование ссылочного списка (LinkedList) в список чисел типа long:");
                            PartiallyOrderedList.IList<long> longList = ListUtils<int>.ConvertAll(listLinked, x => (long)x, ListUtils<long>.LinkedListConstructor);
                            Console.WriteLine();
                        }
                        else if (convertChoice == "3")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный выбор. Пожалуйста, выберите 1, 2 или 3.");
                        }
                    }
                    else if (choice == "15")
                    {
                        Console.Write("Введите значение, на которое нужно увеличить каждый элемент: ");
                        int incrementValue = ReadIntFromConsole();
                        ListUtils<int>.ForEach(listLinked, item => item += incrementValue);
                        Console.WriteLine();
                    }
                    else if (choice == "16")
                    {
                        Console.WriteLine("Выберите метод сортировки:");
                        Console.WriteLine("1. Сортировка по возрастанию");
                        Console.WriteLine("2. Сортировка по убыванию");
                        Console.Write("Ваш выбор: ");
                        string sortChoice = ReadStringFromConsole();

                        Comparison<int> comparer;

                        if (sortChoice == "1")
                        {
                            comparer = (x, y) => x.CompareTo(y);
                        }
                        else if (sortChoice == "2")
                        {
                            comparer = (x, y) => y.CompareTo(x);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный выбор метода сортировки.");
                            continue;
                        }

                        ListUtils<int>.Sort(listLinked, comparer);

                        Console.WriteLine("Список отсортирован.");
                    }
                    else if (choice == "17")
                    {
                        Console.Write("Введите индекс первого элемента: ");
                        if (int.TryParse(Console.ReadLine(), out int index1) && index1 >= 0 && index1 < listLinked.Count)
                        {
                            Console.Write("Введите индекс второго элемента: ");
                            if (int.TryParse(Console.ReadLine(), out int index2) && index2 >= 0 && index2 < listLinked.Count)
                            {
                                int element1 = listLinked.ElementAt(index1);
                                int element2 = listLinked.ElementAt(index2);

                                if (listLinked.IsComparable(element1) && listLinked.IsComparable(element2))
                                {
                                    int comparisonResult = Comparer<int>.Default.Compare(element1, element2);

                                    if (comparisonResult == 0)
                                    {
                                        Console.WriteLine("Первый и второй элементы равны.");
                                    }
                                    else if (comparisonResult < 0)
                                    {
                                        Console.WriteLine("Первый элемент меньше второго.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Первый элемент больше второго.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ошибка: Один или оба элемента не сравнимы.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: Неверный индекс второго элемента.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: Неверный индекс первого элемента.");
                        }
                    }
                    else if (choice == "18")
                    {
                        // Логика для поиска первого элемента, удовлетворяющего условию (кратность 2) в LinkedList
                        var foundItem = ListUtils<int>.Find(listLinked, item => (int)item % 2 == 0);

                        if (!EqualityComparer<int>.Default.Equals(foundItem, default(int)))
                        {
                            Console.WriteLine($"Найден первый элемент, удовлетворяющий условию (кратность 2): {foundItem}");
                        }
                        else
                        {
                            Console.WriteLine("Элемент, удовлетворяющий условию (кратность 2), не найден.");
                        }
                    }
                    else if (choice == "19")
                    {
                        // Логика для поиска последнего элемента, удовлетворяющего условию (кратность 2) в LinkedList
                        var foundItem = ListUtils<int>.FindLast(listLinked, item => (int)item % 2 == 0);

                        if (!EqualityComparer<int>.Default.Equals(foundItem, default(int)))
                        {
                            Console.WriteLine($"Найден последний элемент, удовлетворяющий условию (кратность 2): {foundItem}");
                        }
                        else
                        {
                            Console.WriteLine("Элемент, удовлетворяющий условию (кратность 2), не найден.");
                        }
                    }
                    else if (choice == "20")
                    {
                        // Логика для проверки, что все элементы в LinkedList положительные
                        bool allItemsArePositive = ListUtils<int>.CheckForAll(listLinked, (item, index) => (int)item >= 0);

                        if (allItemsArePositive)
                        {
                            Console.WriteLine("Все элементы в LinkedList положительные.");
                        }
                        else
                        {
                            Console.WriteLine("Не все элементы в LinkedList положительные.");
                        }
                    }
                    else if (choice == "21")
                    {
                        if (listLinked is UnmutableList<int>)
                        {
                            listLinked = new PartiallyOrderedList.LinkedList<int>(listLinked);
                            Console.WriteLine("Список стал изменяемым.");
                        }
                        else
                        {
                            listLinked = new UnmutableList<int>(listLinked);
                            Console.WriteLine("Список стал неизменяемым.");
                        }
                    }
                    else if (choice == "22")
                    {
                        break;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        public static int ReadIntFromConsole()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (input != null && int.TryParse(input, out int value))
                {
                    return value;
                }

                Console.WriteLine("Ошибка при вводе числа. Попробуйте еще раз.");
            }
        }

        public static string ReadStringFromConsole()
        {
            string input = Console.ReadLine();

            while (input == null)
            {
                Console.WriteLine("Ошибка при вводе. Пожалуйста, введите строку заново:");
                input = Console.ReadLine();
            }

            return input;
        }
    }
}