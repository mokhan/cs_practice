namespace utility
{
  public interface IVisitor<T>
  {
    void Visit(T item);
  }

  public interface IVisitable<T>
  {
    void Accept(IVisitor<T> visitor);
  }
  public interface IValueReturningVisitor<T, TResult> : IVisitor<T>
  {
    TResult Result();
  }
  public static class Visiting{
    public static TResult AcceptAndReturnResultFrom<T, TResult>(this IVisitable<T> visitable, IValueReturningVisitor<T, TResult> visitor)
    {
      visitable.Accept(visitor);
      return visitor.Result();
    }
  }
}
