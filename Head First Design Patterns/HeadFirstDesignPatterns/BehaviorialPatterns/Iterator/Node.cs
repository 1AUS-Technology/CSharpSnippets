namespace HeadFirstDesignPatterns.BehaviorialPatterns.Iterator;

public class Node<T>
{
    public Node(T value) : this(value, null, null)
    {
    }

    public Node(T value, Node<T>? left, Node<T>? right)
    {
        Value = value;
        Left = left;
        Right = right;

        if (left != null)
            left.Parent = this;

        if (right != null)
            right.Parent = this;
    }

    public T Value { get; set; }
    public Node<T>? Left { get; set; }
    public Node<T>? Right { get; set; }
    public Node<T>? Parent { get; set; }
}