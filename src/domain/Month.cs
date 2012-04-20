namespace domain
{
  using System;

  public class Month : IComparable<Month>, IIncrementable<Month>
  {
    int year;
    int month;

    public Month(int year, int month)
    {
      this.year = year;
      this.month = month;
    }

    public Month Plus(int months)
    {
      var newMonth = ToDateTime().AddMonths(months);
      return ToMonth(newMonth);
    }

    DateTime ToDateTime()
    {
      return new DateTime(year, month, 01);
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
      return new DateTime(year, month, 01).CompareTo(new DateTime(other.year, other.month, 01));
    }

    public Month Next()
    {
      return Plus(1);
    }

    public bool Equals(Month other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return other.year == year && other.month == month;
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
        return (year*397) ^ month;
      }
    }

    public override string ToString()
    {
      return string.Format("{0} {1}", year, month);
    }
  }

  public static class Months
  {
    public static Month January(this int year)
    {
      return new Month(year,01);
    }
  }
}
