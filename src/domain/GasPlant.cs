namespace domain
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using utility;

  public interface IFacility 
  {
    void AcceptFlowFrom(IWell well);
    IQuantity AvailableCapacityFor(Month month);
  }

  public class GasPlant : IFacility
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
      return MonthsOverAvailableCapacity(Month.Now().UpTo(Month.Infinity));
    }

    public virtual IQuantity AvailableCapacityFor(Month month)
    {
      return capacity.AvailableFor(month);
    }

    public IEnumerable<Month> MonthsOverAvailableCapacity(IRange<Month> months)
    {
      return months.Collect( month =>
      {
        return IsOverCapacity(month);
      });
    }

    bool IsOverCapacity(Month month)
    {
      return AvailableCapacityFor(month).IsGreaterThan(TotalProductionFor(month));
    }

    IQuantity TotalProductionFor(Month month)
    {
      return wells.Select(well => well.GrossProductionFor<Gas>(month)).Sum<BOED>();
    }
  }
}
