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
      return MonthsOverAvailableCapacity(Month.Now());
    }

    public IEnumerable<Month> MonthsOverAvailableCapacity(Month month)
    {
      return MonthsOverAvailableCapacity(Month.Now().UpTo(month));
    }

    public IEnumerable<Month> MonthsOverAvailableCapacity(IRange<Month> months)
    {
      var results = new List<Month>();
      months.Accept(month =>
      {
        var production = TotalProductionFor(month);
        Console.Out.WriteLine(production);
        if( capacity.For(month).IsGreaterThan(production) ){
          results.Add(month);
        }
      });
      return results;
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

    public IQuantity For(Month month)
    {
      //return this.increases.Single(x => x.IsFor(month)).;
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

      public bool IsFor(Month other)
      {
        return month.Equals(other);
      }
    }
  }
}
