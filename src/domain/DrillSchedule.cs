namespace domain
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using utility;

  public class ProductionSchedule : IVisitable<IWell>
  {
    ICollection<IWell> wells = new List<IWell>();

    public void Include(IWell well)
    {
      wells.Add(well);
    }

    public IQuantity EstimatedGrossProductionFor<Commodity>(Month month) where Commodity : ICommodity, new()
    {
      return wells.Select(well => well.GrossProductionFor<Commodity>(month)).Sum<BOED>();
    }

    public IQuantity EstimatedNetProductionFor<Commodity>(Month month) where Commodity : ICommodity, new()
    {
      return wells.Select(well => well.NetProductionFor<Commodity>(month)).Sum<BOED>();
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
}
