namespace domain
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public class Well : IWell
  {
    Month initialProductionMonth;
    Percent workingInterest;
    TypeCurve curve;

    public Well(Month initialProductionMonth, Percent workingInterest, TypeCurve curve)
    {
      this.initialProductionMonth = initialProductionMonth;
      this.workingInterest = workingInterest;
      this.curve = curve;
    }

    public IQuantity GrossProductionFor<Commodity>(Month month) where Commodity : ICommodity, new()
    {
      return curve.ProductionFor<Commodity>(month);
    }

    public IQuantity NetProductionFor<Commodity>(Month month) where Commodity : ICommodity, new()
    {
      return workingInterest.Reduce(GrossProductionFor<Commodity>(month));
    }
  }

  public class Month
  {
    readonly int year;
    readonly int month;

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

  public interface IWell
  {
    IQuantity GrossProductionFor<T>(Month month) where T : ICommodity, new();
    IQuantity NetProductionFor<T>(Month month) where T : ICommodity, new();
  }

  public class Percent
  {
    readonly decimal percentage;
    public static Percent Zero = new Percent(0);

    public Percent(decimal percentage)
    {
      this.percentage = percentage;
    }

    public bool Equals(Percent other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return other.percentage == percentage;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != typeof (Percent)) return false;
      return Equals((Percent) obj);
    }

    public override int GetHashCode()
    {
      return percentage.GetHashCode();
    }

    public IQuantity Reduce(IQuantity original)
    {
      //return new ProratedQuantity(original, this);
      return new Quantity(PortionOf(original.Amount), original.Units);
    }

    public Percent Plus(Percent other)
    {
      return new Percent(percentage + other.percentage);
    }

    public decimal PortionOf(decimal amount)
    {
      return amount*percentage;
    }

    public override string ToString()
    {
      return string.Format("{0} %", percentage);
    }
  }

  public static class Units
  {
    public static Percent Percent(this decimal percentage)
    {
      return new Percent(percentage/100);
    }

    public static IQuantity BOED(this int quantity)
    {
      return BOED(Convert.ToDecimal(quantity));
    }

    public static IQuantity BOED(this decimal quantity)
    {
      return new Quantity(quantity, new BOED());
    }
  }

  public class BOED : IUnitOfMeasure
  {
    public decimal Convert(decimal amount, IUnitOfMeasure units)
    {
      // need to do actual conversion here;
      return amount;
    }

    public bool Equals(BOED other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return true;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != typeof (BOED)) return false;
      return Equals((BOED) obj);
    }

    public override int GetHashCode()
    {
      return (name != null ? name.GetHashCode() : 0);
    }

    public override string ToString()
    {
      return name;
    }

    readonly string name = "BOED";
  }

  public interface IUnitOfMeasure
  {
    decimal Convert(decimal amount, IUnitOfMeasure units);
  }

  public static class Iterating
  {
    public static void Each<T>(this IEnumerable<T> items, Action<T> visitor){
      foreach (var item in items ?? Enumerable.Empty<T>()) 
        visitor(item);
    }
  }
}
