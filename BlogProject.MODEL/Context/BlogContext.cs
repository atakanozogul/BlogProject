using BlogProject.CORE.Entity;
using BlogProject.MODEL.Entities;
using BlogProject.MODEL.Maps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.MODEL.Context
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) //BlogContext için çalış ve basede çalışmasını sağladık.
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            //Silmiyoruz
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=DESKTOP-U45D5S4\\SQLEXPRESS;database=BlogDB;integrated security=true;");
        //    base.OnConfiguring(optionsBuilder);
        //}

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added).ToList();
            //Bilgisayarımızın makine adını almaya yarıyor
            string computerName = Environment.MachineName;
            //ip adresimizi giriyoruz
            string ipAddress = "127.0.0.1";
            DateTime date = DateTime.Now;

            foreach (var item in modifiedEntries)
            {
                CoreEntity entity = item.Entity as CoreEntity; //as anahtarı ile tür dönüşümü gibi yaptık
                //kaydetme işlemi esnasında eklemek istediğimiz verileri eklicez
                if (item != null)
                {
                    switch (item.State)
                    {
                        case EntityState.Modified:
                            entity.UpdatedComputerName = computerName;
                            entity.UpdatedIP = ipAddress;
                            entity.UpdatedDate = date;
                            break;
                        case EntityState.Added:
                            entity.CreatedComputerName = computerName;
                            entity.CreatedIP = ipAddress;
                            entity.CreatedDate = date;
                            break;
                    }
                }
            }
            //Savechanges işlemi için buraya girmiş oluyoruz ve burada ekleme işlemi yapıyoruz.

            //Silmiyoruz ve base e gönderiyoruz.
            return base.SaveChanges();
        }
    }
}
