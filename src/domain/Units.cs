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
      return quantity.ToQuantity<BOED>();
    }

    static public IQuantity MCF(this decimal quantity)
    {
      return quantity.ToQuantity<MCF>();
    }

    static public IQuantity ToQuantity<T>(this decimal quantity) where T: IUnitOfMeasure,new()
    {
      return new Quantity(quantity, new T());
    }
  }
}
