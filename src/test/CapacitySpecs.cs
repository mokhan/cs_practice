namespace test
{
  using domain;
  using Machine.Specifications;

  public class CapacitySpecs
  {
    Establish context = () =>
    {
      sut = new Capacity(0m.MCF());
    };

    static Capacity sut;

    public class when_checking_what_the_available_capacity_is_for_a_given_month
    {
      It should_return_the_correct_capacity=()=>
      {
        result.ShouldEqual(120m.MCF());
      };

      Because of = () =>
      {
        sut.IncreaseCapacity(60m.MCF(), Month.Now());
        sut.IncreaseCapacity(60m.MCF(), Month.Now().Next());
        result = sut.AvailableFor(Month.Now().Next());
      };

      static IQuantity result;
    }
  }
}
