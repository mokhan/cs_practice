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
    public class when_checking_if_a_month_is_before_another
    {
      It should_return_true_when_it_is =()=>
      {
        new Month(2012, 02).IsBefore(new Month(2012, 03)).ShouldBeTrue();
      };
      It should_return_false_when_it_is_not=()=>
      {
        new Month(2012, 03).IsBefore(new Month(2012, 02)).ShouldBeFalse();
      };
      It should_return_false_when_it_is_the_same_month=()=>
      {
        new Month(2012, 02).IsBefore(new Month(2012, 02)).ShouldBeFalse();
      };
    }
  }
}
