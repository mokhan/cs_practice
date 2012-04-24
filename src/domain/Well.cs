namespace domain
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using utility;

  public class Well : IWell
  {
    Month initialProductionMonth;
    Percent workingInterest;
    TypeCurve curve;
    IFacility facility;

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

    public void FlowInto(IFacility facility)
    {
      ensure_that_this_well_is_not_already_flowing_into_a_plant();
      this.facility = facility;
      facility.AcceptFlowFrom(this);
    }

    void ensure_that_this_well_is_not_already_flowing_into_a_plant()
    {
      if(null != this.facility) throw new Exception();
    }
  }

  public interface IWell
  {
    IQuantity GrossProductionFor<Commodity>(Month month) where Commodity : ICommodity, new();
    IQuantity NetProductionFor<Commodity>(Month month) where Commodity : ICommodity, new();
    void FlowInto(IFacility facility);
  }
}
