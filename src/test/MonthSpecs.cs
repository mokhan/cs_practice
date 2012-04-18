namespace test
{
  using Machine.Specifications;
  using domain;

  public class MonthSpecs
  {
    public class when_comparing_months
    {
      It should_return_a_positive_number_when_the_first_month_is_greater_than_the_second_month =()=>
      {
        new Month(2012, 05).CompareTo(new Month(2012, 04)).ShouldBeGreaterThan(0);
      };
      It should_return_a_negative_number_when_the_first_month_is_less_than_the_second_month =()=>
      {
        new Month(2012, 04).CompareTo(new Month(2012, 05)).ShouldBeLessThan(0);
      };
      It should_return_a_zero_when_the_first_month_is_equal_to_the_second_month =()=>
      {
        new Month(2012, 04).CompareTo(new Month(2012, 04)).ShouldEqual(0);
      };
    }
  }
}
