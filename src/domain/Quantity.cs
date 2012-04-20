namespace domain
{
  public interface IQuantity
  {
    IQuantity Plus(IQuantity other);
    IQuantity ConvertTo(IUnitOfMeasure units);
    bool IsGreaterThan(IQuantity other);
    decimal Amount { get; }
    IUnitOfMeasure Units { get; }
  }

  public class Quantity : IQuantity
  {
    public Quantity(decimal amount, IUnitOfMeasure units)
    {
      Amount = amount;
      Units = units;
    }

    public decimal Amount { get;private set; }

    public IUnitOfMeasure Units { get; private set; }

    public IQuantity Plus(IQuantity other)
    {
      return new Quantity(Amount + other.ConvertTo(Units).Amount, Units);
    }

    public IQuantity ConvertTo(IUnitOfMeasure unitOfMeasure)
    {
      return new Quantity(unitOfMeasure.Convert(Amount, this.Units), unitOfMeasure);
    }

    public bool IsGreaterThan(IQuantity other)
    {
      return true;
    }

    public override string ToString()
    {
      return string.Format("{0} {1}", Amount, Units);
    }

    public bool Equals(Quantity other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return other.Amount == Amount && Equals(other.Units, Units);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != typeof (Quantity)) return false;
      return Equals((Quantity) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (Amount.GetHashCode()*397) ^ (Units != null ? Units.GetHashCode() : 0);
      }
    }
  }
}
