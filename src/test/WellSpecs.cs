namespace test
{
  using Machine.Specifications;
  using domain;
  using System;

  public class WellSpecs
  {
    Establish context = () =>
    {
      var typeCurve = Mock.An<TypeCurve>();
      sut = new Well(Month.Now(), 100m.Percent(), typeCurve );
    };
    static IWell sut;

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
