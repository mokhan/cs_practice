namespace test
{
  using Rhino.Mocks;
  
  public static class Mock
  {
    public static T An<T>() where T : class
    {
      return MockRepository.GenerateMock<T>();
    }
  }
  public static class Assertsions
  {
    public static void received<T>(this T mock,System.Action<T> action)
    {
      mock.AssertWasCalled(action);
    }
  }
}
