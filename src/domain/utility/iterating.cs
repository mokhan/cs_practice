namespace utility
{
  using System;
  using System.Linq;
  using System.Collections.Generic;

  public static class Iterating
  {
    public static void Each<T>(this IEnumerable<T> items, Action<T> visitor){
      foreach (var item in items ?? Enumerable.Empty<T>()) 
        visitor(item);
    }
  }
}
