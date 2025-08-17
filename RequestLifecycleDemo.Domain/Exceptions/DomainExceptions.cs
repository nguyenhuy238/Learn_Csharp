//namespace Demo_Request.Services
//{
//    public class DomainExceptions
//    {
//    }
//}

namespace RequestLifecycleDemo.Services;

public class DomainNotFoundException : Exception { public DomainNotFoundException(string m) : base(m) { } }
public class DomainValidationException : Exception { public DomainValidationException(string m) : base(m) { } }
public class OutOfStockException : DomainValidationException { public OutOfStockException() : base("Out of stock") { } }
