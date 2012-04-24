namespace domain
{
  using System.Collections.Generic;

  public static class Summation
  {
    static public IQuantity Sum<T>(this IEnumerable<IQuantity> items) where T: IUnitOfMeasure,new()
    {
      var result = 0m.ToQuantity<T>();
      foreach (var item in items) {
        result = result.Plus(item);
      }
      return result;
    }
  }
}
