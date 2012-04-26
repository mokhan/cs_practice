namespace domain
{
  using System;

  public class Month : IComparable<Month>, IIncrementable<Month>
  {
    DateTime date;
    public static readonly Month Infinity = new Month(2099, 12);

    public Month(int year, int month)
    {
      date = new DateTime(year, month, 01);
    }

    public Month Plus(int months)
    {
      return ToMonth(date.AddMonths(months));
    }

    public bool IsBefore(Month other)
    {
      return this.CompareTo(other) < 0;
    }

    Month ToMonth(DateTime date)
    {
      return new Month(date.Year, date.Month);
    }

    static public Month Now()
    {
      var now = Clock.Now();
      return new Month(now.Year, now.Month);
    }

    public int CompareTo(Month other)
    {
      return this.date.CompareTo(other.date);
    }

    public Month Next()
    {
      return Plus(1);
    }

    public bool Equals(Month other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return other.date == date;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != typeof (Month)) return false;
      return Equals((Month) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (date.Year*397) ^ date.Month;
      }
    }

    public override string ToString()
    {
      return string.Format("{0} {1}", date.Year, date.Month);
    }
  }
}
