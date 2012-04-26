namespace test
{
  using System.Collections.Generic;
  using Machine.Specifications;
  using domain;
  using utility;

  public class TypeCurveSpecs 
  {
    Establish context = () =>
    {
      production = new Production(Month.Now(), 0m.BOED(), null);
      sut = new TypeCurve(new List<Production>()
      {
        production
      });
    };

    static TypeCurve sut;
    static Production production;

    public class when_poking_at_the_production_for_each_month
    {
      It should_allow_visitors_to_visit_each_one = () => 
      {
        visitor.received(x => x.Visit(production));
      };
      Establish context = () =>
      {
        visitor = Mock.An<IVisitor<Production>>();
      };
      Because of = () =>
      {
        sut.Accept(visitor);
      };

      static IVisitor<Production> visitor;
    }
  }
}
