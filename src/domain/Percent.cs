namespace domain
{
  using System;

  public class Percent
  {
    public static Percent Zero = new Percent(0);
    decimal percentage;

    public Percent(decimal percentage)
    {
      this.percentage = percentage;
    }

    public bool Equals(Percent other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return other.percentage == percentage;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != typeof (Percent)) return false;
      return Equals((Percent) obj);
    }

    public override int GetHashCode()
    {
      return percentage.GetHashCode();
    }

    public IQuantity Reduce(IQuantity original)
    {
      return new Quantity(PortionOf(original.Amount), original.Units);
    }

    public Percent Plus(Percent other)
    {
      return new Percent(percentage + other.percentage);
    }

    public decimal PortionOf(decimal amount)
    {
      return amount*percentage;
    }

    public override string ToString()
    {
      return string.Format("{0} %", percentage);
    }
  }
}
