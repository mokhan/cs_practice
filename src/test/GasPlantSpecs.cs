namespace test
{
  using System.Linq;
  using System.Collections.Generic;
  using Machine.Specifications;
  using Rhino.Mocks;
  using domain;
  using utility;

  public class GasPlantSpecs
  {
    Establish context = ()=>
    {
      sut = new GasPlant();
    };

    static GasPlant sut;

    public class when_exceeding_a_plants_available_capacity
    {
      It should_indicate_the_month_that_the_plant_is_scheduled_to_be_over_capacity =() => 
      {
        results.ShouldContain(new Month(2013, 02));
      };

      Establish context = () =>
      {
        firstWell = Mock.An<IWell>();
        secondWell = Mock.An<IWell>();
        firstWell.Stub(x => x.GrossProductionFor<Gas>(2013.January())).Return(30m.MCF());
        secondWell.Stub(x => x.GrossProductionFor<Gas>(2013.January())).Return(31m.MCF());
      };

      Because of = ()=>
      {
        sut.IncreaseCapacityTo(60m.MCF(),2013.January());

        sut.AcceptFlowFrom(firstWell);
        sut.AcceptFlowFrom(secondWell);
        results = sut.MonthsOverAvailableCapacity().ToList();
      };

      static IEnumerable<Month> results;
      static IWell firstWell;
      static IWell secondWell;
    }
  }
}
