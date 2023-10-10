namespace HolowanMechanicals.Domain.Model {
   public class ProductCategoryClassification {
      public Guid ProductId { get; set; }
      public int CategoryId { get; set; }

      public DateTime From { get; set; }
      public DateTime? Thru { get; set; }
      public bool IsPrimary { get; set; }
      public string Comment { get; set; }

      public virtual Product Product { get; set; }
      public virtual Category Category { get; set; }
   }
}
