namespace domain
{
  using System.Collections.Generic;

  public static class Summation
  {
    static public IQuantity Sum(this IEnumerable<IQuantity> items)
    {
      var result = 0m.BOED();
      foreach (var item in items) {
        result = result.Plus(item);
      }
      return result;
    }
  }
}
