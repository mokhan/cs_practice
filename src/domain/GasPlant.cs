namespace domain
{
  using System;
  using System.Linq;
  using System.Collections.Generic;

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
      return Enumerable.Empty<Month>();
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

    class Increase
    {
      public Increase(IQuantity quantity, Month month)
      {
      }
    }
  }

  public interface IRange<T> where T : IComparable<T>
  {
  }
}
