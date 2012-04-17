namespace domain
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using utility;

  public class DrillSchedule : IVisitable<IWell>
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

    public void Accept(IVisitor<IWell> visitor )
    {
      Accept(visitor.Visit);
    }

    void Accept(Action<IWell> visitor )
    {
      wells.Each(well =>
      {
        visitor(well);
      });
    }
  }
  public class EstimatedNetProductionFor<T> : IValueReturningVisitor<IWell, IQuantity> where T : ICommodity, new()
  {
    Month month;
    IQuantity result;

    public EstimatedNetProductionFor(Month month)
    {
      this.month = month;
      result = new Quantity(0, new BOED());
    }

    public void Visit(IWell well)
    {
      result = result.Plus(well.NetProductionFor<T>(month));
    }

    public IQuantity Result()
    {
      return this.result;
    }
  }
}
