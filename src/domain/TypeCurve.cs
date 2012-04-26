namespace domain
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using utility;

  public class TypeCurve : IVisitable<Production>
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

    public virtual void Accept(Action<Production> visitor)
    {
      this.production.Each(x => 
      {
        visitor(x);
      });
    }

    public virtual void Accept(IVisitor<Production> visitor)
    {
      Accept(visitor.Visit);
    }
  }
}
