namespace domain
{
  public static class Months
  {
    public static Month January(this int year)
    {
      return new Month(year,01);
    }
    public static Month February(this int year)
    {
      return new Month(year,2);
    }
    public static Month March(this int year)
    {
      return new Month(year,3);
    }
    public static Month April(this int year)
    {
      return new Month(year,04);
    }
    public static Month May(this int year)
    {
      return new Month(year,05);
    }
    public static Month June(this int year)
    {
      return new Month(year,06);
    }
    public static Month July(this int year)
    {
      return new Month(year,07);
    }
    public static Month August(this int year)
    {
      return new Month(year,08);
    }
    public static Month September(this int year)
    {
      return new Month(year,09);
    }
    public static Month October(this int year)
    {
      return new Month(year,10);
    }
    public static Month November(this int year)
    {
      return new Month(year,11);
    }
    public static Month December(this int year)
    {
      return new Month(year,12);
    }
  }
}
