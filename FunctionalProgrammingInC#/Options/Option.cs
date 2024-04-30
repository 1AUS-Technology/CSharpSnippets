namespace FunctionalProgrammingInC_.Options;

public interface Option<T> { }
record None : Option<T>;
record Some<T>(T Value) : Option<T>;

record None<T> : Option<T>;

