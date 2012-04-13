namespace domain
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public class DeclineCurve
  {
    IDictionary<int, IQuantity> production = new Dictionary<int, IQuantity>();
    IComposition split = new CommoditySplits();

    public void Add(int month, IQuantity quantity)
    {
      production[month] = quantity;
    }

    public TypeCurve StartingOn(Month initialProductionMonth)
    {
      return new TypeCurve(CreateProductionFor(initialProductionMonth));
    }

    IEnumerable<Production> CreateProductionFor(Month initialProductionMonth)
    {
      foreach (var quantity in production)
        yield return new Production(initialProductionMonth.Plus(quantity.Key), quantity.Value, split);
    }

    public void Composition<Commodity>(Percent percent) where Commodity : ICommodity
    {
      split.SplitFor<Commodity>(percent);
    }
  }
}
