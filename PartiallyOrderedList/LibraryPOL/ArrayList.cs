using System.Collections;

namespace PartiallyOrderedList.LibraryPOL
{
    public class ArrayList<T> : IList<T>
    {
        private T[] items; // Объявляем массив items для хранения элементов списка
        private int count; // Объявляем переменную count для отслеживания количества элементов в списке
        private const int DefaultCapacity = 4; // Объявляем константу DefaultCapacity с начальной емкостью массива по умолчанию

        // Конструктор класса ArrayList
        public ArrayList()
        {
            items = new T[DefaultCapacity]; // Создаем массив элементов с начальной емкостью DefaultCapacity
            count = 0; // Изначально список пустой, поэтому устанавливаем count в 0
        }

        public ArrayList(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            // Вычислить начальную емкость на основе размера коллекции
            int initialCapacity = collection.Count();
            if (initialCapacity < DefaultCapacity)
            {
                initialCapacity = DefaultCapacity;
            }

            // Создаем массив с начальной емкостью
            items = new T[initialCapacity];
            count = 0;

            // Итерируемся по элементам переданной коллекции
            foreach (T item in collection)
            {
                // Если текущая емкость достигла предела, увеличиваем массив
                if (count == items.Length)
                {
                    ResizeArray();
                }

                // Добавляем элемент в массив и увеличиваем счетчик
                items[count] = item;
                count++;
            }
        }

        public int Count => count; // Свойство Count возвращает текущее количество элементов в списке

        // Индексатор для доступа к элементам списка по индексу
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count) // Проверяем, находится ли индекс в допустимом диапазоне
                    throw new IndexOutOfRangeException("Индекс находится за пределами диапазона.");
                return items[index]; // Возвращаем элемент списка по указанному индексу
            }
            set
            {
                if (index < 0 || index >= count) // Проверяем, находится ли индекс в допустимом диапазоне
                    throw new IndexOutOfRangeException("Индекс находится за пределами диапазона.");
                items[index] = value; // Устанавливаем новое значение элемента списка по указанному индексу
            }
        }

        // Метод для добавления элемента в список
        public int Add(T value)
        {
            if (count == items.Length) // Проверяем, заполнен ли массив, и при необходимости увеличиваем его размер
                ResizeArray();

            items[count] = value; // Добавляем новый элемент в конец массива
            count++; // Увеличиваем счетчик элементов

            return count - 1; // Возвращаем индекс, по которому был добавлен элемент
        }

        // Метод для очистки списка
        public void Clear()
        {
            Array.Clear(items, 0, count); // Очищаем массив, начиная с индекса 0 и до текущего количества элементов
            count = 0; // Устанавливаем счетчик элементов в 0, таким образом, список становится пустым
        }

        // Метод для проверки наличия элемента в списке
        public bool Contains(T value)
        {
            for (int i = 0; i < count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(items[i], value))
                    return true; // Если элемент найден, возвращаем true
            }
            return false; // Если элемент не найден, возвращаем false
        }

        // Метод для поиска индекса элемента в списке
        public int IndexOf(T value)
        {
            for (int i = 0; i < count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(items[i], value))
                    return i; // Возвращаем индекс первого вхождения элемента, если найден
            }
            return -1; // Возвращаем -1, если элемент не найден
        }

        // Метод для вставки элемента по указанному индексу
        public void Insert(int index, T value)
        {
            if (index < 0 || index > count)
            {
                Console.WriteLine("Ошибка: Индекс находится за пределами диапазона.");
                return; // Если индекс недопустим, выводим сообщение об ошибке и выходим из метода
            }

            if (count == items.Length) // Проверяем, заполнен ли массив, и при необходимости увеличиваем его размер
                ResizeArray();

            for (int i = count; i > index; i--) // Сдвигаем элементы вправо, чтобы освободить место для нового элемента
            {
                items[i] = items[i - 1];
            }

            items[index] = value; // Вставляем новый элемент на указанный индекс
            count++; // Увеличиваем счетчик элементов

            Console.WriteLine($"Элемент {value} успешно вставлен по индексу {index}.");
        }

        // Метод для удаления элемента из списка
        public void Remove(T value)
        {
            int index = IndexOf(value); // Находим индекс элемента

            if (index != -1) // Если элемент найден
            {
                RemoveAt(index); // Удаляем элемент по индексу
            }
        }

        // Метод для удаления элемента по индексу
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                Console.WriteLine("Ошибка: Индекс находится за пределами диапазона.");
                return; // Если индекс недопустим, выводим сообщение об ошибке и выходим из метода
            }

            for (int i = index; i < count - 1; i++) // Сдвигаем элементы влево, чтобы заполнить пустое место
            {
                items[i] = items[i + 1];
            }

            count--; // Уменьшаем счетчик элементов
            items[count] = default; // Очищаем последний элемент

            //Console.WriteLine($"Элемент по индексу {index} удален из списка.");
        }

        // Метод для создания подсписка из списка
        public IList<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex < 0 || fromIndex > toIndex || toIndex >= count)
            {
                Console.WriteLine("Ошибка: Индексы находятся за пределами допустимого диапазона.");
                return null; // Если индексы недопустимы, выводим сообщение об ошибке и возвращаем null
            }

            IList<T> subList = new ArrayList<T>(); // Создаем новый список для подсписка

            for (int i = fromIndex; i <= toIndex; i++) // Копируем элементы из основного списка в подсписок
            {
                subList.Add(items[i]);
            }

            if (subList.Count > 0)
            {
                Console.WriteLine("Подсписок:");
                foreach (var item in subList)
                {
                    Console.Write($"{item} "); // Выводим элементы подсписка
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Подсписок пуст.");
            }

            return subList; // Возвращаем подсписок
        }

        // Метод для изменения размера массива элементов
        private void ResizeArray()
        {
            int newCapacity = items.Length * 2; // Увеличиваем емкость массива в два раза

            if (newCapacity < DefaultCapacity)
                newCapacity = DefaultCapacity; // Проверяем, чтобы новая емкость не была меньше DefaultCapacity

            T[] newItems = new T[newCapacity]; // Создаем новый массив с увеличенной емкостью
            Array.Copy(items, newItems, count); // Копируем элементы из старого массива в новый
            items = newItems; // Заменяем старый массив новым
        }

        // Реализация интерфейса IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                // Возвращаем элементы списка по одному. Позволяет возвращать последовательность значений из метода,
                // не вычисляя их все сразу и не сохраняя в памяти, а предоставляя значения по мере необходимости
                yield return items[i];
            }
        }

        // Реализация интерфейса IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Метод для проверки наличия элемента в списке
        public bool IsComparable(T value)
        {
            foreach (var item in items)
            {
                if (EqualityComparer<T>.Default.Equals(item, value))
                {
                    return true;
                }
            }
            return false;
        }
    }
}