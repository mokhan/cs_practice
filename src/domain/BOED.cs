namespace domain
{
  public class BOED : IUnitOfMeasure
  {
    public decimal Convert(decimal amount, IUnitOfMeasure units)
    {
      return ( units is MCF ) ? amount / 6: amount;
    }

    public override string ToString()
    {
      return name;
    }

    readonly string name = "BOED";
  }
}
