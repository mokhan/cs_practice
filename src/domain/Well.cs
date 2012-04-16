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

  public interface IWell
  {
    IQuantity GrossProductionFor<T>(Month month) where T : ICommodity, new();
    IQuantity NetProductionFor<T>(Month month) where T : ICommodity, new();
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
