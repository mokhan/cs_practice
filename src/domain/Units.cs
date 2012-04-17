namespace domain
{
  using System;

  public static class Units
  {
    public static Percent Percent(this decimal percentage)
    {
      return new Percent(percentage/100);
    }

    public static IQuantity BOED(this int quantity)
    {
      return BOED(Convert.ToDecimal(quantity));
    }

    public static IQuantity BOED(this decimal quantity)
    {
      return new Quantity(quantity, new BOED());
    }
  }
}
