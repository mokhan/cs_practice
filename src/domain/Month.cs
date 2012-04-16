namespace domain
{
  using System;

  public class Month
  {
    int year;
    int month;

    public Month(int year, int month)
    {
      this.year = year;
      this.month = month;
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

    public Month Plus(int months)
    {
      var newMonth = new DateTime(year, month, 01).AddMonths(months);
      return new Month(newMonth.Year, newMonth.Month);
    }

    public override string ToString()
    {
      return string.Format("{0} {1}", year, month);
    }
  }
}
