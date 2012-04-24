namespace domain
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using utility;

  public class GasPlant
  {
    IList<IWell> wells;
    Capacity capacity;

    public GasPlant()
    {
      this.wells = new List<IWell>();
      this.capacity = new Capacity(0m.ToQuantity<MCF>());
    }

    public void IncreaseCapacityTo(IQuantity quantity, Month month)
    {
      this.capacity.IncreaseCapacity(quantity, month);
    }

    public void AcceptFlowFrom(IWell well)
    {
      this.wells.Add(well);
    }

    public IEnumerable<Month> MonthsOverAvailableCapacity()
    {
      return MonthsOverAvailableCapacity(Month.Now().UpTo(new Month(2099, 12)));
    }

    public IEnumerable<Month> MonthsOverAvailableCapacity(IRange<Month> months)
    {
      var results = new List<Month>();
      months.Accept(month =>
      {
        if(IsOverCapacity(month)){
          results.Add(month);
        }
      });
      return results;
    }

    bool IsOverCapacity(Month month)
    {
        var production = TotalProductionFor(month);
        if( capacity.AvailableFor(month).IsGreaterThan(production) ){
          return true;
        }
        return false;
    }
    IQuantity TotalProductionFor(Month month)
    {
      IQuantity result = new Quantity(0, new BOED());
      wells.Each(x =>
      {
        result = result.Plus( x.GrossProductionFor<Gas>(month));
      });
      return result;
    }
  }

  public class Capacity
  {
    IList<Increase> increases;

    public Capacity(IQuantity initialCapacity):this(initialCapacity, Month.Now())
    {
    }

    public Capacity(IQuantity initialCapacity, Month month)
    {
      this.increases = new List<Increase>();
      this.IncreaseCapacity(initialCapacity, month);
    }

    public void IncreaseCapacity(IQuantity quantity, Month month)
    {
      this.increases.Add(new Increase(quantity, month));
    }

    public IQuantity AvailableFor(Month month)
    {
      return new Quantity(0, new BOED());
      //return this
        //.increases
        //.Where(x => x.IsBeforeOrOn(month))
        //.Sum(x => x.IncreasedCapacity());
      return null;
    }

    class Increase
    {
      IQuantity quantity;
      Month month;

      public Increase(IQuantity quantity, Month month)
      {
        this.quantity = quantity;
        this.month = month;
      }

      public bool IsBeforeOrOn(Month other)
      {
        return month.IsBefore(other) || month.Equals(other);
      }

      public IQuantity IncreasedCapacity()
      {
        return this.quantity;
      }
    }
  }
  public static class Summation
  {
    static public IQuantity Sum(this IEnumerable<IQuantity> items)
    {
      var result = 0m.BOED();
      foreach (var item in items) {
        result = result.Plus(item);
      }
      return result;
    }
  }
}
