namespace domain
{
  using System;

  public static class Clock
  {
    public static DateTime Now()
    {
      return time();
    }

    static Func<DateTime> defaultTime =()=> DateTime.Now;
    static Func<DateTime> time = defaultTime;
  }
}
