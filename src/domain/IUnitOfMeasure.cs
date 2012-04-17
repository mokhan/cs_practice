namespace domain
{
  public interface IUnitOfMeasure
  {
    decimal Convert(decimal amount, IUnitOfMeasure units);
  }
}
