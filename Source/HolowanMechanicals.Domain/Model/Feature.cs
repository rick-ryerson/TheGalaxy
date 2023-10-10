namespace HolowanMechanicals.Domain.Model {
   public class Feature {
      public int Id { get; set; }
      public string Description { get; set; }
   }

   public class Interaction {
      public Feature Selected { get; set; }
      public Feature DependentOn { get; set; }
      public Product Product { get; set; }

   }
}