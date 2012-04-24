namespace test
{
  using Machine.Specifications;
  using domain;

  public class MCFSpecs
  {
    Establish context = ()=>
    {
      sut = new MCF();
    };

    static MCF sut;

    public class when_converting_1_BOED_to_MCF
    {
      It should_return_6_BOED=()=>
      {
        result.ShouldEqual(6m);
      };

      Because of = ()=>
      {
        result = sut.Convert(1m, new BOED());
      };

      static decimal result;
    }
  }
}
