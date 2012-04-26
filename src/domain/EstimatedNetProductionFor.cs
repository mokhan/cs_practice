namespace domain
{
  using utility;

  public class EstimatedNetProductionFor<Commodity> : IValueReturningVisitor<IWell, IQuantity> where Commodity : ICommodity, new()
  {
    Month month;
    IQuantity result;

    public EstimatedNetProductionFor(Month month)
    {
      this.month = month;
      result = new Quantity(0, new BOED());
    }

    public void Visit(IWell well)
    {
      result = result.Plus(well.NetProductionFor<Commodity>(month));
    }

    public IQuantity Result()
    {
      return this.result;
    }
  }
}
