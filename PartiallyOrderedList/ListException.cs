namespace PartiallyOrderedList
{
    // Базовый класс исключений для списков
    public class ListException : Exception
    {
        public ListException(string message) : base(message)
        {
        }
    }

    // Производный класс исключения для операции Add, которая не поддерживается
    public class AddNotSupportedException : ListException
    {
        public AddNotSupportedException(string message) : base(message)
        {
        }
    }

    // Производный класс исключения для операции Clear, которая не поддерживается
    public class ClearNotSupportedException : ListException
    {
        public ClearNotSupportedException(string message) : base(message)
        {
        }
    }

    // Производный класс исключения для операции Remove, которая не поддерживается
    public class RemoveNotSupportedException : ListException
    {
        public RemoveNotSupportedException(string message) : base(message)
        {
        }
    }

    // Производный класс исключения для операции Insert, которая не поддерживается
    public class InsertNotSupportedException : ListException
    {
        public InsertNotSupportedException(string message) : base(message)
        {
        }
    }

    // Класс исключения для неизменяемых списков
    public class UnmutableListException : Exception
    {
        public UnmutableListException(string message) : base(message)
        {
        }
    }
}