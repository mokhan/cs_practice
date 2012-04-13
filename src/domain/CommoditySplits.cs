namespace domain
{
  using System;
  using System.Collections.Generic;

  public interface IComposition
  {
    void SplitFor<Commodity>(Percent percent) where Commodity : ICommodity;
    IQuantity PercentageOf<Commodity>(IQuantity quantity) where Commodity : ICommodity, new();
    Percent PercentageFor<Commodity>() where Commodity : ICommodity;
  }

  public class CommoditySplits : IComposition
  {
    IDictionary<Type, Percent> splits = new Dictionary<Type, Percent>();

    public void SplitFor<Commodity>(Percent percent) where Commodity : ICommodity
    {
      splits[typeof (Commodity)] = percent;
    }

    public IQuantity PercentageOf<Commodity>(IQuantity quantity) where Commodity : ICommodity, new()
    {
      return new Commodity().PercentageFrom(this).Reduce(quantity);
    }

    public Percent PercentageFor<Commodity>() where Commodity : ICommodity
    {
      return splits.ContainsKey(typeof(Commodity)) ? splits[typeof (Commodity)] : Percent.Zero;
    }
  }
}
