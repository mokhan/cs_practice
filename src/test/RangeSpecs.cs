namespace test
{
  using System.Collections.Generic;
  using Machine.Specifications;
  using domain;

  public class RangeSpecs
  {
    Establish context = () =>
    {
      sut = new Range<Month>(new Month(2012, 01), new Month(2012, 03));
    };

    static IRange<Month> sut;

    public class when_iterating_through_each_item_in_a_range
    {
      It should_visit_each_item_in_the_range =()=>
      {
        results.ShouldContainOnly(new Month(2012, 01),new Month(2012, 02),new Month(2012, 03));
      };

      Because of = () =>
      {
        sut.Accept(x =>
        {
          results.Add(x);
        });
      };

      static IList<Month> results = new List<Month>();
    }
  }
}
