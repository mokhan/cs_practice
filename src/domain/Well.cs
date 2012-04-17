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
  }

  public interface IWell
  {
    IQuantity GrossProductionFor<T>(Month month) where T : ICommodity, new();
    IQuantity NetProductionFor<T>(Month month) where T : ICommodity, new();
  }
}
