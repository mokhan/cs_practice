namespace domain
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

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
}
