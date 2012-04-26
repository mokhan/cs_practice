namespace domain
{
  using System;
  using utility;

  public static class Ranges
  {
    public static IRange<T> UpTo<T>(this T start, T end) where T: IComparable<T>, IIncrementable<T>
    {
      return new Range<T>(start, end);
    }
  }

  public interface IRange<T> : IVisitable<T> where T : IComparable<T>
  {
    void Accept(Action<T> action);
    bool Contains(T item);
  }

  public class Range<T> : IRange<T> where T : IComparable<T>, IIncrementable<T>
  {
    T start;
    T end;

    public Range(T start, T end)
    {
      this.start = start;
      this.end = end;
    }

    public void Accept(IVisitor<T> visitor) 
    {
      Accept(visitor.Visit);
    }

    public void Accept(Action<T> visitor)
    {
      var next = this.start;
      var last = this.end.Next();
      while(!next.Equals(last))
      {
        visitor(next);
        next = next.Next();
      }
    }

    public bool Contains(T item)
    {
      return start.CompareTo(item) <= 0 && end.CompareTo(item) > 0;
    }
  }

  public interface IIncrementable<T>
  {
    T Next();
  }
}
