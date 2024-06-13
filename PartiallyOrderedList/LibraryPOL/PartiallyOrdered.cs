namespace PartiallyOrderedList.LibraryPOL
{
    public class PartiallyOrderedNode<T>
    {
        public PartiallyOrderedNode<T> Next; // Ссылка на следующий узел
        public PartiallyOrderedNode<T> Prev; // Ссылка на предыдущий узел
        public T Value; // Значение узла

        // Конструктор класса PartiallyOrderedNode, принимающий значение типа T
        public PartiallyOrderedNode(T value)
        {
            Value = value;
        }

        // Переопределенный метод ToString для возвращения строкового представления значения узла
        public override string ToString()
        {
            return Value.ToString();
        }

        // Метод IsComparable, проверяющий, можно ли сравнивать значение узла с заданным значением
        public bool IsComparable(T value)
        {
            // Если значение реализует интерфейс IComparable<T>, используем его для сравнения
            if (Value is IComparable<T> comparable)
            {
                return comparable.CompareTo(value) == 0;
            }
            // В противном случае используем сравнение по умолчанию через EqualityComparer<T>.Default
            return EqualityComparer<T>.Default.Equals(Value, value);
        }
    }
}