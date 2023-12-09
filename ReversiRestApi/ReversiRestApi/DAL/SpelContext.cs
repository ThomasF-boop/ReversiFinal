using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Drawing;

namespace ReversiRestApi.DAL
{
    public class SpelContext : DbContext
    {
        public DbSet<Spel> spellen { get; set; }

        public SpelContext(DbContextOptions<SpelContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BoardConfiguration());
        }

        public class BoardConfiguration : IEntityTypeConfiguration<Spel>
        {
            public void Configure(EntityTypeBuilder<Spel> builder)
            {
                builder.Property(e => e.Bord).HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Kleur[,]>(v));
            }
        }

        //private string ConvertMultiDimensionalEnumToString(Kleur[,] multiDimensionalEnum)
        //{
        //    // Implement your conversion logic here
        //    // Convert kleur[,] to a string representation
        //    // For example, you could serialize it to JSON
        //    // using Newtonsoft.Json.JsonConvert.SerializeObject
        //    // Example:
        //    return Newtonsoft.Json.JsonConvert.SerializeObject(multiDimensionalEnum);
        //}
        //private Kleur[,] ConvertStringToMultiDimensionalEnum(string value)
        //{
        //    // Implement your conversion logic here
        //    // Convert the string representation back to kleur[,]
        //    // For example, you could deserialize it from JSON
        //    // using Newtonsoft.Json.JsonConvert.DeserializeObject
        //    // Example:
        //    return Newtonsoft.Json.JsonConvert.DeserializeObject<Kleur[,]>(value);
        //}

    }
}
