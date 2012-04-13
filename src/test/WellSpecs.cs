namespace test
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Machine.Specifications;
  using domain;

  public class WellSpecs
  {
    public class when_estimating_production
    {
      It should_be_able_to_tell_the_estimated_total_production_for_any_month= () =>
      {
        var parkland100Percent = new Oppurtunity();
        parkland100Percent.WorkingInterest(100m.Percent());
        var declineCurve = new DeclineCurve();
        declineCurve.Composition<Gas>(100m.Percent());
        declineCurve.Add(0, 100.BOED());
        parkland100Percent.DeclinesUsing(declineCurve);

        var schedule = new DrillSchedule();
        var jan2013 = new Month(2013, 01);
        schedule.Include(parkland100Percent.BringOnlineOn(jan2013));
        schedule.EstimatedGrossProductionFor<All>(jan2013).ShouldEqual(100.BOED());
        schedule.EstimatedGrossProductionFor<Gas>(jan2013).ShouldEqual(100.BOED());
        schedule.EstimatedGrossProductionFor<Oil>(jan2013).ShouldEqual(0.BOED());
        schedule.EstimatedGrossProductionFor<NGL>(jan2013).ShouldEqual(0.BOED());
        schedule.EstimatedGrossProductionFor<Condensate>(jan2013).ShouldEqual(0.BOED());
      };

      It should_be_able_to_tell_the_estimated_net_total_production_for_any_month = () =>
      {
        var parkland75Percent = new Oppurtunity();
        parkland75Percent.WorkingInterest(75m.Percent());
        var declineCurve = new DeclineCurve();
        declineCurve.Composition<Gas>(50m.Percent());
        declineCurve.Composition<Oil>(50m.Percent());
        declineCurve.Add(0, 100.BOED());
        parkland75Percent.DeclinesUsing(declineCurve);

        var schedule = new DrillSchedule();
        var jan2013 = new Month(2013, 01);
        schedule.Include(parkland75Percent.BringOnlineOn(jan2013));
        schedule.EstimatedNetProductionFor<All>(jan2013).ShouldEqual(75.BOED());
        schedule.EstimatedNetProductionFor<Gas>(jan2013).ShouldEqual(37.5m.BOED());
        schedule.EstimatedNetProductionFor<Oil>(jan2013).ShouldEqual(37.5m.BOED());
      };
    }
  }
}
