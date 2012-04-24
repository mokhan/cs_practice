namespace domain
{
  public class MCF : IUnitOfMeasure
  {
    public decimal Convert(decimal amount, IUnitOfMeasure units)
    {
      if(units is BOED) return amount*6;
      return amount;
    }

    public override string ToString()
    {
      return "MCF";
    }
  }
}
