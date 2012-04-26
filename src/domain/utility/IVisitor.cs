namespace utility
{
  using System;
  using System.Collections.Generic;

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

  public static class Visiting
  {
    public static TResult AcceptAndReturnResultFrom<T, TResult>(this IVisitable<T> visitable, IValueReturningVisitor<T, TResult> visitor)
    {
      visitable.Accept(visitor);
      return visitor.Result();
    }

    public static IEnumerable<T> Collect<T>( this IVisitable<T> visitable, Func<T, bool> predicate)
    {
      return new Collectable<T>(predicate).CollectFrom(visitable);
    }
  }

  public interface ICollectable<T>
  {
    IEnumerable<T> CollectFrom(IVisitable<T> visitable);
  }

  public class Collectable<T> : ICollectable<T>
  {
    Func<T, bool> predicate;

    public Collectable(Func<T, bool> predicate) 
    {
      this.predicate = predicate;
    }

    public IEnumerable<T> CollectFrom(IVisitable<T> visitable)
    {
      var results = new List<T>();
      visitable.Accept(new AnonymousVisitor<T>(item =>
      {
        if(predicate(item)) results.Add(item);
      }));
      return results;
    }
  }

  public class AnonymousVisitor<T> : IVisitor<T>
  {
    Action<T> visitor;

    public AnonymousVisitor(Action<T> visitor) 
    {
      this.visitor = visitor;
    }

    public void Visit(T item)
    {
      visitor(item);
    }
  }
}
