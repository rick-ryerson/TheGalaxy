using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GalacticSenate.Data.Implementations.EntityFramework {
   /// <summary>
   /// This class is set up to allow migrations building and updating
   /// </summary>
   public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext> {
      public DataContext CreateDbContext(string[] args) {
         var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

         optionsBuilder.UseSqlServer("Server=localhost,14331;Database=GalacticSenate;User Id=sa;Password=Qwerty!@!");

         return new DataContext(optionsBuilder.Options);
      }  
   }
}
