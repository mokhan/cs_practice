namespace test
{
  using Machine.Specifications;
  using domain;
  using System;
  using Rhino.Mocks;

  public class WellSpecs
  {
    Establish context = () =>
    {
      typeCurve = Mock.An<TypeCurve>();
      sut = new Well(Month.Now(), 100m.Percent(), typeCurve );
    };
    static IWell sut;
    static TypeCurve typeCurve;

    public class when_flowing_a_gas_well_into_a_gas_plant
    {
      Establish context = ()=>
      {
        gasPlant = Mock.An<IFacility>();
      };

      static IFacility gasPlant;

      public class when_the_plant_has_enough_capacity_to_accept_flow
      {
        It should_attempt_to_flow_in_to_the_plant=()=>
        {
          gasPlant.received(x => x.AcceptFlowFrom(sut));
        };

        Because of = ()=>
        {
          sut.FlowInto(gasPlant);
        };
      }

      public class when_the_plant_would_overflow_if_it_accepted_flow_from_this_well
      {
        It should_not_let_you_flow_this_well_into_the_facility=()=>
        {
          exception.ShouldNotBeNull();
        };

        Establish context = ()=>
        {
          var jan2013 = 2013.January();
          var feb2013 = 2013.February();
          typeCurve
            .Stub(x => x.Accept( Arg<Action<Production>>.Is.Anything ))
            .WhenCalled(x => 
            {
              var splits = new CommoditySplits();
              splits.SplitFor<Gas>(100m.Percent());
              var action = x.Arguments[0] as Action<Production>;
              action(new Production(jan2013,100m.BOED(),splits ));
              action(new Production(feb2013, 91m.BOED(), splits));
            });
          gasPlant.Stub(x => x.AvailableCapacityFor(jan2013)).Return(100m.BOED());
          gasPlant.Stub(x => x.AvailableCapacityFor(feb2013)).Return(90m.BOED());
        };

        Because of = ()=>
        {
          exception = Catch.Exception(()=> sut.FlowInto(gasPlant));
        };

        static Exception exception;
      }

      public class when_a_well_is_already_flowing_into_a_facility 
      {
        It should_not_allow_you_to_flow_into_another_plant=()=>
        {
          exception.ShouldNotBeNull();
        };
        Establish context = ()=>
        {
          otherFacility = Mock.An<IFacility>();
        };

        Because of = ()=>
        {
          sut.FlowInto(gasPlant);
          exception = Catch.Exception(()=> sut.FlowInto(otherFacility));
        };

        static IFacility otherFacility;
        static Exception exception;
      }
    }
  }
}
