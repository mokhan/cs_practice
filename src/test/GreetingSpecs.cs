namespace test
{
  using Machine.Specifications;
  using domain;

    public class GreetingSpecs
    {
      Establish context = () =>
      {
        sut  = new Greeting();
      };

      Because of = () =>
      {
        result = sut.Hello();
      };

      It should_say_hello = () =>
      {
        result.ShouldEqual("hello");
      };

      static Greeting sut;
      static string result;
    }
}
