namespace domain
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public class Gas : ICommodity
  {
    public Percent PercentageFrom(IComposition composition)
    {
      return composition.PercentageFor<Gas>();
    }
  }

  public class Oil : ICommodity
  {
    public Percent PercentageFrom(IComposition composition)
    {
      return composition.PercentageFor<Oil>();
    }
  }

  public class NGL : ICommodity
  {
    public Percent PercentageFrom(IComposition composition)
    {
      return composition.PercentageFor<NGL>();
    }
  }

  public class Condensate : ICommodity
  {
    public Percent PercentageFrom(IComposition composition)
    {
      return composition.PercentageFor<Condensate>();
    }
  }

  public class All : ICommodity
  {
    public Percent PercentageFrom(IComposition composition)
    {
      return composition.PercentageFor<Gas>()
        .Plus(composition.PercentageFor<Oil>())
        .Plus(composition.PercentageFor<NGL>())
        .Plus(composition.PercentageFor<Condensate>());
    }
  }

  public interface ICommodity
  {
    Percent PercentageFrom(IComposition composition);
  }
}
