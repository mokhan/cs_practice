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

    public class when_converting_6_MCF_to_BOE
    {
      It should_return_1_BOE=()=>
      {
        result.ShouldEqual(1m);
      };

      Because of = ()=>
      {
        result = sut.Convert(6, new MCF());
      };

      static decimal result;
    }
  }
}
