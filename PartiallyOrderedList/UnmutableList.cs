using System.Collections;

namespace PartiallyOrderedList
{
    public class UnmutableList<T> : IList<T>
    {
        private IList<T> innerList;

        // Конструктор класса, принимает внутренний список и делает его неизменяемым
        public UnmutableList(IList<T> list)
        {
            innerList = list ?? throw new ArgumentNullException(nameof(list));
            // Присваиваем внутренний список значению параметра "list", если он не равен null.
            // В противном случае генерируется исключение ArgumentNullException с указанием имени параметра "list".
        }

        // Возвращает количество элементов в неизменяемом списке
        public int Count => innerList.Count;

        // Индексатор для доступа к элементам списка
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= innerList.Count)
                {
                    throw new IndexOutOfRangeException($"Ошибка: Попытка доступа к элементу за пределами списка (индекс {index}).");
                }
                return innerList[index];
            }
            set
            {
                try
                {
                    // Генерирует исключение при попытке изменить элемент в неизменяемом списке
                    throw new UnmutableListException("Ошибка: Список не поддерживает операции изменения.");
                }
                catch (UnmutableListException ex)
                {
                    Console.WriteLine(ex.Message); // Вывод сообщения об ошибке на консоль
                }
            }
        }

        // Попытка добавить элемент в неизменяемый список генерирует исключение и выводит сообщение
        public int Add(T value)
        {
            try
            {
                throw new UnmutableListException("Ошибка: Список не поддерживает операции изменения.");
            }
            catch (UnmutableListException ex)
            {
                Console.WriteLine(ex.Message); // Вывод сообщения об ошибке на консоль
                return default; // Возвращает значение по умолчанию для типа T
            }
        }

        // Попытка очистить неизменяемый список генерирует исключение и выводит сообщение
        public void Clear()
        {
            try
            {
                throw new UnmutableListException("Ошибка: Список не поддерживает операции изменения.");
            }
            catch (UnmutableListException ex)
            {
                Console.WriteLine(ex.Message); // Вывод сообщения об ошибке на консоль
            }
        }

        // Проверяет, содержит ли неизменяемый список указанный элемент
        public bool Contains(T value)
        {
            return innerList.Contains(value);
        }

        // Возвращает перечислитель для неизменяемого списка
        public IEnumerator<T> GetEnumerator()
        {
            return innerList.GetEnumerator();
        }

        // Возвращает индекс указанного элемента в неизменяемом списке
        public int IndexOf(T value)
        {
            return innerList.IndexOf(value);
        }

        // Попытка вставить элемент в неизменяемый список генерирует исключение и выводит сообщение
        public void Insert(int index, T value)
        {
            try
            {
                throw new UnmutableListException("Ошибка: Список не поддерживает операции изменения.");
            }
            catch (UnmutableListException ex)
            {
                Console.WriteLine(ex.Message); // Вывод сообщения об ошибке на консоль
            }
        }

        // Попытка удалить элемент из неизменяемого списка генерирует исключение и выводит сообщение
        public void Remove(T value)
        {
            try
            {
                throw new UnmutableListException("Ошибка: Список не поддерживает операции изменения.");
            }
            catch (UnmutableListException ex)
            {
                Console.WriteLine(ex.Message); // Вывод сообщения об ошибке на консоль
            }
        }

        // Попытка удалить элемент по индексу из неизменяемого списка генерирует исключение и выводит сообщение
        public void RemoveAt(int index)
        {
            try
            {
                throw new UnmutableListException("Ошибка: Список не поддерживает операции изменения.");
            }
            catch (UnmutableListException ex)
            {
                Console.WriteLine(ex.Message); // Вывод сообщения об ошибке на консоль
            }
        }

        // Возвращает подсписок из неизменяемого списка
        public IList<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex >= Count || fromIndex > toIndex)
            {
                throw new UnmutableListException("Ошибка: Некорректные индексы для подсписка.");
            }

            return new UnmutableList<T>(innerList.SubList(fromIndex, toIndex));
        }

        // Возвращает перечислитель для неизменяемого списка (неявная реализация интерфейса IEnumerable)
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Переопределение метода ToString для получения строкового представления типа списка
        public override string ToString()
        {
            return $"UnmutableList<{typeof(T).Name}>";
        }

        // Проверяет, поддерживает ли тип элементов сравнение
        public bool IsComparable(T value)
        {
            if (value is IComparable<T>)
            {
                // Проверяем, является ли объект "value" реализацией интерфейса IComparable<T>.
                // Если да, то объект поддерживает сравнение с объектами того же типа
                return true;
            }
            else
            {
                Type type = typeof(T);
                // Получаем объект Type, представляющий тип объекта "T"

                return type.GetInterface("IComparable`1") != null;
                // Проверяем, реализует ли тип "T" интерфейс "IComparable<T>",
                // используя метод GetInterface, и возвращаем true, если да, и false в противном случае
            }
        }
    }
}