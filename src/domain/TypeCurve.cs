namespace domain
{
  using System.Linq;
  using System.Collections.Generic;

  public class TypeCurve
  {
    IEnumerable<Production> production;

    protected TypeCurve() { }

    public TypeCurve(IEnumerable<Production> production)
    {
      this.production = production.ToList();
    }

    public IQuantity ProductionFor<Commodity>(Month month) where Commodity : ICommodity, new()
    {
      return production.Single(x => x.IsFor(month)).ProductionOf<Commodity>();
    }
  }
}
