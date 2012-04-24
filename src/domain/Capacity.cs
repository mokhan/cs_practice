namespace domain
{
  using System.Linq;
  using System.Collections.Generic;
  using domain;
  using utility;

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
      return this.increases.Where(x => x.IsBeforeOrOn(month)).Select(x => x.IncreasedCapacity()).Sum<MCF>();
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
}
