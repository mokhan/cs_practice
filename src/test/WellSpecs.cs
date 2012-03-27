namespace test
{
  using Machine.Specifications;
  using domain;

  public class WellSpecs
  {
    Establish context = ()=>
    {
      sut = new Well();
    };
    It should = () => {
      true.ShouldBeTrue();
    };

    static readonly Well sut;
  }
  public class Well{}
}
