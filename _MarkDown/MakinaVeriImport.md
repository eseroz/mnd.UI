
  Tarih Saati Böyle Birleştir
  =METNEÇEVİR($A2;"g.aa.yyyy") & " " & METNEÇEVİR(s2;"ss:dd")

````
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WpfApp1;

namespace Intro
{
    public class BloggingContext : DbContext
    {
        public DbSet<UretimTablo> MakinaVerileri { get; set; }
   

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder
                .UseSqlServer(@"Server=localhost,1501;Database=MNDAPPDB;user id=sa;password=;");

            optionsBuilder.EnableDetailedErrors(true);

        }
    }

    
    [Table("UretimTablo", Schema ="Uretim")]
    public class UretimTablo
    {
        [Key]
        public long Id { get; set; }
        public DateTime? BaşlangıçSaati { get; set; }

        public DateTime? BitişSaati { get; set; }

    }

   
}
````




````
      private void Button_Click(object sender, RoutedEventArgs e)
        {
            BloggingContext dc = new BloggingContext();

            var x = dc.MakinaVerileri.ToList();


            for (int i = 1; i < x.Count; i++)
            {
                if (x[i].BitişSaati == null || x[i - 1].BitişSaati==null) continue;

                if (x[i].BitişSaati.Value < x[i - 1].BitişSaati.Value)
                {
                    x[i].BitişSaati = x[i].BitişSaati.Value.AddDays(1);
                }

                if (x[i].BaşlangıçSaati.Value < x[i - 1].BaşlangıçSaati.Value)
                {
                    x[i].BaşlangıçSaati = x[i].BaşlangıçSaati.Value.AddDays(1);
                }
            }

            dc.SaveChanges();
        }

    ````