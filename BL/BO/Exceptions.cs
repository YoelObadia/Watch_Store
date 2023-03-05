using System.Runtime.Serialization;



namespace BO;





[Serializable]

internal class NoExistingItemException : Exception

{

    public NoExistingItemException() : base() { }

    public NoExistingItemException(string message) : base(message) { }

    public NoExistingItemException(string message, Exception inner) : base(message, inner) { }

    protected NoExistingItemException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}
[Serializable]

internal class OrderAlreadyExpeditedException : Exception

{

    public OrderAlreadyExpeditedException() : base() { }

    public OrderAlreadyExpeditedException(string message) : base(message) { }

    public OrderAlreadyExpeditedException(string message, Exception inner) : base(message, inner) { }

    protected OrderAlreadyExpeditedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}

[Serializable]

internal class OrderAlreadyReceivedException : Exception

{

    public OrderAlreadyReceivedException() : base() { }

    public OrderAlreadyReceivedException(string message) : base(message) { }

    public OrderAlreadyReceivedException(string message, Exception inner) : base(message, inner) { }

    protected OrderAlreadyReceivedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}

[Serializable]

internal class ItemAlreadyExistException : Exception

{

    public ItemAlreadyExistException() : base() { }

    public ItemAlreadyExistException(string message) : base(message) { }

    public ItemAlreadyExistException(string message, Exception inner) : base(message, inner) { }

    protected ItemAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}
[Serializable]

internal class ItemNotExistInCartException : Exception

{

    public ItemNotExistInCartException() : base() { }

    public ItemNotExistInCartException(string message) : base(message) { }

    public ItemNotExistInCartException(string message, Exception inner) : base(message, inner) { }

    protected ItemNotExistInCartException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}
[Serializable]
internal class NotEnoughInStockException : Exception

{
    public NotEnoughInStockException() : base() { }

    public NotEnoughInStockException(string message) : base(message) { }

    public NotEnoughInStockException(string message, Exception inner) : base(message, inner) { }

    protected NotEnoughInStockException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}
[Serializable]
internal class NegativeAmountException : Exception

{
    public NegativeAmountException() : base() { }

    public NegativeAmountException(string message) : base(message) { }

    public NegativeAmountException(string message, Exception inner) : base(message, inner) { }

    protected NegativeAmountException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}
[Serializable]
internal class InvalidStringFormatException : Exception

{
    public InvalidStringFormatException() : base() { }

    public InvalidStringFormatException(string message) : base(message) { }

    public InvalidStringFormatException(string message, Exception inner) : base(message, inner) { }

    protected InvalidStringFormatException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}


[Serializable]

internal class IdNotValidExcpetion : Exception

{

    public IdNotValidExcpetion() : base() { }

    public IdNotValidExcpetion(string message) : base(message) { }

    public IdNotValidExcpetion(string message, Exception inner) : base(message, inner) { }

    protected IdNotValidExcpetion(SerializationInfo info, StreamingContext context) : base(info, context) { }

}



[Serializable]

internal class NameNotValidExcpetion : Exception

{

    public NameNotValidExcpetion() : base() { }

    public NameNotValidExcpetion(string message) : base(message) { }

    public NameNotValidExcpetion(string message, Exception inner) : base(message, inner) { }

    protected NameNotValidExcpetion(SerializationInfo info, StreamingContext context) : base(info, context) { }

}



[Serializable]

internal class PriceNotValidExcpetion : Exception

{

    public PriceNotValidExcpetion() : base() { }

    public PriceNotValidExcpetion(string message) : base(message) { }

    public PriceNotValidExcpetion(string message, Exception inner) : base(message, inner) { }

    protected PriceNotValidExcpetion(SerializationInfo info, StreamingContext context) : base(info, context) { }

}



[Serializable]

internal class StockNotValidExcpetion : Exception

{

    public StockNotValidExcpetion() : base() { }

    public StockNotValidExcpetion(string message) : base(message) { }

    public StockNotValidExcpetion(string message, Exception inner) : base(message, inner) { }

    protected StockNotValidExcpetion(SerializationInfo info, StreamingContext context) : base(info, context) { }

}



[Serializable]

internal class DeleteItemNotValidExcpetion : Exception

{

    public DeleteItemNotValidExcpetion() : base() { }

    public DeleteItemNotValidExcpetion(string message) : base(message) { }

    public DeleteItemNotValidExcpetion(string message, Exception inner) : base(message, inner) { }

    protected DeleteItemNotValidExcpetion(SerializationInfo info, StreamingContext context) : base(info, context) { }

}