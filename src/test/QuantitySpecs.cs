namespace test
{
  using Machine.Specifications;
  using domain;

  public class QuantitySpecs
  {
    public class when_using_the_same_unit_of_measure
    {
      public class when_one_quantity_is_greater_than_another
      {
        It should_return_true=()=>
        {
          var result = new Quantity(2, new BOED()).IsGreaterThan(new Quantity(1, new BOED()));
          result.ShouldBeTrue();
        };
      }
      public class when_one_quantity_is_less_than_another
      {
        It should_return_false=()=>
        {
          var result = new Quantity(1, new BOED()).IsGreaterThan(new Quantity(2, new BOED()));
          result.ShouldBeFalse();
        };
      }
    }

    public class when_using_different_unit_of_measures
    {
      public class when_one_quantity_is_greater_than_another
      {
        It should_return_true=()=>
        {
          var result = new Quantity(2, new BOED()).IsGreaterThan(new Quantity(6, new MCF()));
          result.ShouldBeTrue();
        };
      }
      public class when_one_quantity_is_less_than_another
      {
        It should_return_false=()=>
        {
          var result = new Quantity(1, new BOED()).IsGreaterThan(new Quantity(7, new MCF()));
          result.ShouldBeFalse();
        };
      }
    }
  }
}
