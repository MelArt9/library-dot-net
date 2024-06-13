using System.Collections;

namespace PartiallyOrderedList
{
    public class LinkedList<T> : IList<T>
    {
        private PartiallyOrderedNode<T> head; // Ссылка на начальный узел списка
        private PartiallyOrderedNode<T> tail; // Ссылка на конечный узел списка
        private int count; // Количество элементов в списке

        // Конструктор по умолчанию
        public LinkedList()
        {
            head = null; // Инициализация головы как null
            tail = null; // Инициализация хвоста как null
            count = 0; // Инициализация счетчика как 0
        }

        // Конструктор, принимающий коллекцию для инициализации списка
        public LinkedList(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            // Итерируемся по коллекции и добавляем элементы в список
            foreach (T item in collection)
            {
                Add(item);
            }
        }

        // Свойство для получения количества элементов в списке
        public int Count => count;

        // Индексатор для доступа к элементам списка по индексу
        public T this[int index]
        {
            get
            {
                // Проверяем допустимость индекса
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Индекс находится за пределами диапазона.");

                // Итерируемся по списку, чтобы найти элемент с заданным индексом
                PartiallyOrderedNode<T> current = head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }

                return current.Value; // Возвращаем значение текущего элемента
            }
            set
            {
                // Проверяем допустимость индекса
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Индекс находится за пределами диапазона.");

                // Итерируемся по списку, чтобы найти элемент с заданным индексом и установить новое значение
                PartiallyOrderedNode<T> current = head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }

                current.Value = value; // Устанавливаем новое значение текущего элемента
            }
        }

        // Метод для добавления элемента в конец списка
        public int Add(T value)
        {
            PartiallyOrderedNode<T> newNode = new PartiallyOrderedNode<T>(value); // Создаем новый узел с заданным значением
            if (count == 0)
            {
                head = newNode; // Если список пуст, устанавливаем голову и хвост как новый узел
                tail = newNode;
            }
            else
            {
                tail.Next = newNode; // Иначе связываем текущий хвост с новым узлом
                newNode.Prev = tail; // Устанавливаем предыдущий хвост как предыдущий узел нового узла
                tail = newNode; // Обновляем хвост
            }
            count++; // Увеличиваем счетчик

            return count - 1; // Возвращаем индекс, куда был добавлен элемент
        }

        // Метод для очистки списка
        public void Clear()
        {
            head = null; // Устанавливаем голову как null
            tail = null; // Устанавливаем хвост как null
            count = 0; // Обнуляем счетчик
        }

        // Метод для проверки наличия элемента в списке
        public bool Contains(T value)
        {
            PartiallyOrderedNode<T> current = head; // Начинаем с головы списка
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, value))
                    return true; // Если значение текущего элемента совпадает с заданным, возвращаем true
                current = current.Next; // Переходим к следующему элементу
            }
            return false; // Если значение не найдено, возвращаем false
        }

        // Метод для поиска индекса элемента в списке
        public int IndexOf(T value)
        {
            PartiallyOrderedNode<T> current = head; // Начинаем с головы списка
            int index = 0;
            while (current != null)
            {
                if (current.IsComparable(value))
                    return index; // Если значение текущего элемента можно сравнить с заданным и совпадает, возвращаем индекс
                current = current.Next; // Переходим к следующему элементу
                index++; // Увеличиваем индекс
            }
            return -1; // Если значение не найдено, возвращаем -1
        }

        // Метод для вставки элемента по указанному индексу
        public void Insert(int index, T value)
        {
            if (index < 0 || index > count)
            {
                Console.WriteLine("Ошибка: Индекс находится за пределами диапазона.");
                return; // Выход из метода при недопустимом индексе
            }

            if (index == 0)
            {
                PartiallyOrderedNode<T> newNode = new PartiallyOrderedNode<T>(value); // Создаем новый узел с заданным значением
                newNode.Next = head; // Устанавливаем новый узел как следующий за головой
                head.Prev = newNode; // Устанавливаем предыдущий узел головы как новый узел
                head = newNode; // Обновляем голову
            }
            else if (index == count)
            {
                Add(value); // Если индекс равен количеству элементов, добавляем элемент в конец
            }
            else
            {
                PartiallyOrderedNode<T> current = head; // Начинаем с головы списка
                for (int i = 0; i < index; i++)
                {
                    current = current.Next; // Двигаемся к элементу с заданным индексом
                }

                PartiallyOrderedNode<T> newNode = new PartiallyOrderedNode<T>(value); // Создаем новый узел с заданным значением
                newNode.Next = current; // Устанавливаем новый узел как следующий за текущим элементом
                newNode.Prev = current.Prev; // Устанавливаем предыдущий узел текущего элемента как предыдущий нового узла
                current.Prev.Next = newNode; // Связываем предыдущий элемент с новым узлом
                current.Prev = newNode; // Обновляем предыдущий элемент текущего элемента
            }
            count++; // Увеличиваем счетчик
        }

        // Метод для удаления элемента из списка
        public void Remove(T value)
        {
            PartiallyOrderedNode<T> current = head; // Начинаем с головы списка
            while (current != null)
            {
                if (current.IsComparable(value))
                {
                    RemoveNode(current); // Если значение текущего элемента можно сравнить с заданным и совпадает, удаляем узел
                    return;
                }
                current = current.Next; // Переходим к следующему элементу
            }
        }

        // Метод для удаления элемента по указанному индексу
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                Console.WriteLine("Ошибка: Индекс находится за пределами диапазона.");
                return; // Выход из метода при недопустимом индексе
            }

            PartiallyOrderedNode<T> current = head; // Начинаем с головы списка
            for (int i = 0; i < index; i++)
            {
                current = current.Next; // Двигаемся к элементу с заданным индексом
            }

            RemoveNode(current); // Удаляем узел
        }

        // Приватный метод для удаления узла из списка
        private void RemoveNode(PartiallyOrderedNode<T> node)
        {
            if (node.Prev != null)
                node.Prev.Next = node.Next; // Если узел имеет предыдущий элемент, обновляем ссылку следующего элемента у предыдущего
            else
                head = node.Next; // Иначе обновляем голову

            if (node.Next != null)
                node.Next.Prev = node.Prev; // Если узел имеет следующий элемент, обновляем ссылку предыдущего элемента у следующего
            else
                tail = node.Prev; // Иначе обновляем хвост

            count--; // Уменьшаем счетчик
        }

        // Метод для создания подсписка на основе заданных индексов
        public IList<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= count || toIndex < 0 || toIndex > count || fromIndex > toIndex)
            {
                Console.WriteLine("Ошибка: Недопустимые индексы для подсписка.");
                return null; // Возвращаем null, чтобы обозначить ошибку
            }

            LinkedList<T> subList = new LinkedList<T>(); // Создаем новый связанный список для подсписка
            PartiallyOrderedNode<T> current = head; // Начинаем с головы списка
            for (int i = 0; i < fromIndex; i++)
            {
                current = current.Next; // Двигаемся к элементу с начальным индексом
            }

            for (int i = fromIndex; i <= toIndex; i++) // Увеличил до toIndex + 1
            {
                subList.Add(current.Value); // Добавляем элемент в подсписок
                current = current.Next; // Переходим к следующему элементу
            }

            if (subList.Count > 0)
            {
                Console.WriteLine("Подсписок:");
                foreach (var item in subList)
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Подсписок пуст.");
            }

            return subList; // Возвращаем созданный подсписок
        }

        // Реализация интерфейса IEnumerable<T> для перечисления элементов
        public IEnumerator<T> GetEnumerator()
        {
            PartiallyOrderedNode<T> current = head; // Начинаем с головы списка
            while (current != null)
            {
                yield return current.Value; // Возвращаем значение текущего элемента
                current = current.Next; // Переходим к следующему элементу
            }
        }

        // Реализация интерфейса IEnumerable для перечисления элементов
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Метод для проверки, можно ли сравнить значение с элементами списка
        public bool IsComparable(T value)
        {
            PartiallyOrderedNode<T> current = head; // Начинаем с головы списка
            while (current != null)
            {
                if (current.IsComparable(value))
                    return true; // Если значение текущего элемента можно сравнить с заданным и совпадает, возвращаем true
                current = current.Next; // Переходим к следующему элементу
            }
            return false; // Если значение не найдено, возвращаем false
        }
    }
}