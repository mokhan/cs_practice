namespace domain
{
  public class Production
  {
    Month month;
    IQuantity produced;
    IComposition split;

    public Production(Month month, IQuantity produced, IComposition split)
    {
      this.month = month;
      this.produced = produced;
      this.split = split;
    }

    public bool IsFor(Month otherMonth)
    {
      return month.Equals(otherMonth);
    }

    public IQuantity ProductionOf<T>() where T : ICommodity, new()
    {
      return split.PercentageOf<T>(produced);
    }
  }
}
