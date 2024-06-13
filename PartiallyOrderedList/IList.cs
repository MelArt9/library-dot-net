namespace PartiallyOrderedList
{
    public interface IList<T> : IEnumerable<T>
    {
        int Count { get; } // Получить количество элементов в списке
        T this[int index] { get; set; } // Получить или установить элемент по индексу

        int Add(T value); // Добавить элемент в список и вернуть его индекс
        void Clear(); // Очистить список
        bool Contains(T value); // Проверить, содержит ли список элемент
        int IndexOf(T value); // Получить индекс первого вхождения элемента
        void Insert(int index, T value); // Вставить элемент по индексу
        void Remove(T value); // Удалить первое вхождение элемента
        void RemoveAt(int index); // Удалить элемент по индексу
        IList<T> SubList(int fromIndex, int toIndex); // Получить подсписок

        // Добавляем специфичный метод для частично упорядоченного списка
        bool IsComparable(T value); // Проверить, сравним ли элемент с переданным значением
    }
}