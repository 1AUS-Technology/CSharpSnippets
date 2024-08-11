namespace FunctionalProgrammingInC_.Options;

public interface Option<T> { }
record None : Option<object>;
record Some<T>(T Value) : Option<T>;

record None<T> : Option<T>;

