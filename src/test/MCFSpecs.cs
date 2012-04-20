namespace test
{
  using Machine.Specifications;
  using domain;
  public class MCFSpecs{
    Establish context = ()=>
    {
      sut = new MCF();
    };
    static MCF sut;
    public class when_converting_6_mcf_to_boed
    {
      It should_return_one_boed=()=>
      {
        result.ShouldEqual(1m);
      };

      Because of = ()=>
      {
        result = sut.Convert(6m, new BOED());
      };

      static decimal result;
    }
  }
}
