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

    public class when_greeting_someone
    {
      Because of = () =>
      {
        result = sut.Hello();
      };

      It should_say_hello = () =>
      {
        result.ShouldEqual("hello");
      };

      static string result;
    }

    public class when_saying_goodbye
    {
      Because of = () =>
      {
        result = sut.Goodbye();
      };

      It should_say_goodbye = () =>
      {
        result.ShouldEqual("goodbye");
      };

      static string result;
    }

    static Greeting sut;
  }
}
