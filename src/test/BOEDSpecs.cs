namespace test
{
  using Machine.Specifications;
  using domain;

  public class BOEDSpecs
  {
    Establish context = ()=>
    {
      sut = new BOED();
    };

    static IUnitOfMeasure sut;

    public class when_converting_one_BOE_to_MCF
    {
      It should_return_6_MCF=()=>
      {
        result.ShouldEqual(6m);
      };

      Because of = ()=>
      {
        result = sut.Convert(1, new MCF());
      };

      static decimal result;
    }
  }
}
