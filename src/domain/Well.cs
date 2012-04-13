namespace domain
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public class Gas : ICommodity
  {
    public Percent PercentageFrom(IComposition composition)
    {
      return composition.PercentageFor<Gas>();
    }
  }

  public class Oil : ICommodity
  {
    public Percent PercentageFrom(IComposition composition)
    {
      return composition.PercentageFor<Oil>();
    }
  }

  public class NGL : ICommodity
  {
    public Percent PercentageFrom(IComposition composition)
    {
      return composition.PercentageFor<NGL>();
    }
  }

  public class Condensate : ICommodity
  {
    public Percent PercentageFrom(IComposition composition)
    {
      return composition.PercentageFor<Condensate>();
    }
  }

  public class All : ICommodity
  {
    public Percent PercentageFrom(IComposition composition)
    {
      return composition.PercentageFor<Gas>()
        .Plus(composition.PercentageFor<Oil>())
        .Plus(composition.PercentageFor<NGL>())
        .Plus(composition.PercentageFor<Condensate>());
    }
  }

  public interface ICommodity
  {
    Percent PercentageFrom(IComposition composition);
  }

  public class DrillSchedule
  {
    ICollection<IWell> wells = new List<IWell>();

    public void Include(IWell well)
    {
      wells.Add(well);
    }

    public IQuantity EstimatedGrossProductionFor<Commodity>(Month month) where Commodity : ICommodity, new()
    {
      IQuantity result = new Quantity(0, new BOED());
      Accept(well =>
      {
        result = result.Plus(well.GrossProductionFor<Commodity>(month));
      });
      return result;
    }

    public IQuantity EstimatedNetProductionFor<Commodity>(Month month) where Commodity : ICommodity, new()
    {
      IQuantity result = new Quantity(0, new BOED());
      Accept(well =>
      {
        result = result.Plus(well.NetProductionFor<Commodity>(month));
      });
      return result;
    }

    void Accept(Action<IWell> visitor )
    {
      wells.Each(well =>
      {
        visitor(well);
      });
    }
  }

  public interface IComposition
  {
    void SplitFor<Commodity>(Percent percent) where Commodity : ICommodity;
    IQuantity PercentageOf<Commodity>(IQuantity quantity) where Commodity : ICommodity, new();
    Percent PercentageFor<Commodity>() where Commodity : ICommodity;
  }

  public class DeclineCurve
  {
    IDictionary<int, IQuantity> production = new Dictionary<int, IQuantity>();
    IComposition split = new CommoditySplits();

    public void Add(int month, IQuantity quantity)
    {
      production[month] = quantity;
    }

    public TypeCurve StartingOn(Month initialProductionMonth)
    {
      return new TypeCurve(CreateProductionFor(initialProductionMonth));
    }

    IEnumerable<Production> CreateProductionFor(Month initialProductionMonth)
    {
      foreach (var quantity in production)
        yield return new Production(initialProductionMonth.Plus(quantity.Key), quantity.Value, split);
    }

    public void Composition<Commodity>(Percent percent) where Commodity : ICommodity
    {
      split.SplitFor<Commodity>(percent);
    }
  }

  public class CommoditySplits : IComposition
  {
    IDictionary<Type, Percent> splits = new Dictionary<Type, Percent>();

    public void SplitFor<Commodity>(Percent percent) where Commodity : ICommodity
    {
      splits[typeof (Commodity)] = percent;
    }

    public IQuantity PercentageOf<Commodity>(IQuantity quantity) where Commodity : ICommodity, new()
    {
      return new Commodity().PercentageFrom(this).Reduce(quantity);
    }

    public Percent PercentageFor<Commodity>() where Commodity : ICommodity
    {
      return splits.ContainsKey(typeof(Commodity)) ? splits[typeof (Commodity)] : Percent.Zero;
    }
  }

  public class Production
  {
    Month month;
    IQuantity produced;
    IComposition split;

    public Production(Month month, IQuantity produced, IComposition split)
    {
      this.month = month;
      this.produced = produced;
      this.split = split;
    }

    public bool IsFor(Month otherMonth)
    {
      return month.Equals(otherMonth);
    }

    public IQuantity ProductionOf<T>() where T : ICommodity, new()
    {
      return split.PercentageOf<T>(produced);
    }
  }

  public class TypeCurve
  {
    IEnumerable<Production> production;

    public TypeCurve(IEnumerable<Production> production)
    {
      this.production = production.ToList();
    }

    public IQuantity ProductionFor<Commodity>(Month month) where Commodity : ICommodity, new()
    {
      return production.Single(x => x.IsFor(month)).ProductionOf<Commodity>();
    }
  }

  public interface IQuantity
  {
    IQuantity Plus(IQuantity other);
    IQuantity ConvertTo(IUnitOfMeasure units);
    decimal Amount { get; }
    IUnitOfMeasure Units { get; }
  }

  public class Oppurtunity
  {
    Percent workingInterest;
    DeclineCurve declineCurve;

    public Oppurtunity()
    {
      workingInterest = 100m.Percent();
    }

    public void WorkingInterest(Percent percent)
    {
      workingInterest = percent;
    }

    public void DeclinesUsing(DeclineCurve declineCurve)
    {
      this.declineCurve = declineCurve;
    }

    public IWell BringOnlineOn(Month initialProductionMonth)
    {
      return new Well(initialProductionMonth, workingInterest, declineCurve.StartingOn(initialProductionMonth));
    }

    public IEnumerable<IWell> BringOnlineOn(Month initialProductionMonth, int numberOfWells)
    {
      for (var i = 0; i < numberOfWells; i++)
        yield return BringOnlineOn(initialProductionMonth);
    }
  }

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

  public class Quantity : IQuantity
  {
    public Quantity(decimal amount, IUnitOfMeasure units)
    {
      Amount = amount;
      Units = units;
    }

    public decimal Amount { get;private set; }

    public IUnitOfMeasure Units { get; private set; }

    public IQuantity Plus(IQuantity other)
    {
      return new Quantity(Amount + other.ConvertTo(Units).Amount, Units);
    }

    public IQuantity ConvertTo(IUnitOfMeasure unitOfMeasure)
    {
      return new Quantity(unitOfMeasure.Convert(Amount, this.Units), unitOfMeasure);
    }

    public override string ToString()
    {
      return string.Format("{0} {1}", Amount, Units);
    }

    public bool Equals(Quantity other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return other.Amount == Amount && Equals(other.Units, Units);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != typeof (Quantity)) return false;
      return Equals((Quantity) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (Amount.GetHashCode()*397) ^ (Units != null ? Units.GetHashCode() : 0);
      }
    }
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
