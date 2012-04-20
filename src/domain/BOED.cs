namespace domain
{
  public class BOED : IUnitOfMeasure
  {
    public decimal Convert(decimal amount, IUnitOfMeasure units)
    {
      return ( units is MCF ) ? amount * 6: amount;
    }

    public bool Equals(BOED other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return true;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != typeof (BOED)) return false;
      return Equals((BOED) obj);
    }

    public override int GetHashCode()
    {
      return (name != null ? name.GetHashCode() : 0);
    }

    public override string ToString()
    {
      return name;
    }

    readonly string name = "BOED";
  }

  public class MCF : IUnitOfMeasure
  {
    public decimal Convert(decimal amount, IUnitOfMeasure units)
    {
      // need to do actual conversion here;
      return amount;
    }
  }
}
