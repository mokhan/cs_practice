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

    public virtual bool OccursDuring(IRange<Month> period)
    {
      return period.Contains(this.month);
    }

    public virtual bool IsGreaterThanAvailableAt(IFacility facility)
    {
      return facility.AvailableCapacityFor(this.month).IsGreaterThan(produced);
    }

    public IQuantity ProductionOf<T>() where T : ICommodity, new()
    {
      return split.PercentageOf<T>(produced);
    }
  }
}
