namespace domain
{
  using System.Collections.Generic;
  using System.Linq;

  public class Oppurtunity
  {
    Percent workingInterest;
    DeclineCurve declineCurve;

    public Oppurtunity()
    {
      workingInterest = 100m.Percent();
    }

    public void WorkingInterest(Percent percent)
    {
      workingInterest = percent;
    }

    public void DeclinesUsing(DeclineCurve declineCurve)
    {
      this.declineCurve = declineCurve;
    }

    public IWell BringOnlineOn(Month initialProductionMonth)
    {
      return new Well(initialProductionMonth, workingInterest, declineCurve.StartingOn(initialProductionMonth));
    }

    public IEnumerable<IWell> BringOnlineOn(Month initialProductionMonth, int numberOfWells)
    {
      for (var i = 0; i < numberOfWells; i++)
        yield return BringOnlineOn(initialProductionMonth);
    }
  }
}
