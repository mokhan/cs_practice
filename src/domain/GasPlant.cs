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
        if(IsOverCapacity(month))
          results.Add(month);
      });
      return results;
    }

    bool IsOverCapacity(Month month)
    {
      return capacity.AvailableFor(month).IsGreaterThan(TotalProductionFor(month));
    }

    IQuantity TotalProductionFor(Month month)
    {
      return wells.Select(well => well.GrossProductionFor<Gas>(month)).Sum();
    }
  }
}
