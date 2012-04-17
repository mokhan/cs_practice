namespace test
{
  public static class Mock
  {
    public static T An<T>()
    {
      return MockRepository.GenerateMock<T>();
    }
  }
}
