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
    public class when_a_range_contains_an_item
    {
      It should_return_true=()=>
      {
        result.ShouldBeTrue();
      };

      Because of = ()=>
      {
        result = sut.Contains(new Month(2012, 02));
      };

      static bool result;
    }
    public class when_an_item_is_before_the_range{
      It should_return_false=()=>
      {
        var result = sut.Contains(new Month(2011, 12));
        result.ShouldBeFalse();
      };
    }
    public class when_an_item_is_after_the_range{
      It should_return_false=()=>
      {
        var result = sut.Contains(new Month(2012, 04));
        result.ShouldBeFalse();
      };
    }
  }
}
