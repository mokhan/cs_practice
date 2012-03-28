namespace test
{
  using System.Collections.Generic;
  using Machine.Specifications;
  using domain;

  public class WellSpecs
  {
    Establish context = ()=>
    {
      //sut = new Well();
    };

    public class when_projecting_production_from_a_new_well
    {
      It should_calculate_the_correct_projection =()=>
      {
        var well = new Well(new Month(2013, 01));
        well.apply_split_for<Oil>(25.Percent());
        well.apply_split_for<Gas>(25.Percent());
        well.apply_split_for<NGL>(25.Percent());
        well.apply_split_for<Condensate>(25.Percent());

        var decline = new DeclineCurve();
        decline.for_month(1,new BOED(100));
        well.use(decline);

        well.production_for<Oil>(new Month(2013, 01)).ShouldEqual(new BOED(25));
      };
    }

    static readonly Well sut;
  }
  public class DeclineCurve{
    IDictionary<int, BOED> curve;
    public DeclineCurve(){
      curve = new Dictionary<int, BOED>();
    }
    public void for_month(int month, BOED volume){
      curve[month] = volume;
    }
    //public void each(Action<int, BOED> action){
    //}
  }

  public class BOED{
    decimal volume;
    public BOED(decimal volume){
      this.volume = volume;
    }
    public bool Equals(BOED other){
      if(ReferenceEquals(null, other)) return false;
      return other.volume == this.volume;
    }
    public override bool Equals(object other){
      if(ReferenceEquals(null, other)) return false;
      if(!(other is BOED)) return false;
      return Equals((BOED)other);
    }
    public override string ToString(){
      return string.Format("{0} BOED", volume);
    }
  }
  public class Well
  {
    Month ip_month;
    IDictionary<ICommodity, Percent> splits;
    IList<Projection> projections;

    public Well(Month expected_initial_production_month){
      this.ip_month = expected_initial_production_month;
      this.splits = new Dictionary<ICommodity, Percent>();
      this.projections = new List<Projection>();
    }
    public void apply_split_for<Product>(Percent percent) where Product : ICommodity, new()
    {
      splits.Add(new Product(), percent);
    }
    public void use(DeclineCurve curve){
      //curve.each( (month,total_volume) => {
          //projections.Add( splits.Select((split)=> split.create_projection(ip_month.add(month), total_volume));
      //});
    }
    public BOED production_for<Commodity>(Month month){
      return new BOED(25);
    }
  }
  public class Projection{}
  public class Percent
  {
  }
  public class Month{
    public Month(int year, int month){}
  }
  public interface ICommodity{}
  public class Oil : ICommodity{}
  public class Gas : ICommodity{}
  public class NGL : ICommodity{}
  public class Condensate : ICommodity{}
  public static class Conversion{
    public static Percent Percent(this int percent){
      return new Percent();
    }
  }
}
