using BlogProject.CORE.Map;
using BlogProject.MODEL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.MODEL.Maps
{
    public class UserMap : CoreMap<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            //Users tablosunu bu şekilde maplemiş olduk
            builder.ToTable("Users"); //bu tabloya yaz dedik
            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Email).HasMaxLength(255).IsRequired(true);
            builder.Property(x => x.Password).HasMaxLength(1000).IsRequired(true);
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.ImageUrl).HasMaxLength(100).IsRequired(false);
            builder.Property(x => x.LastLogin).IsRequired(false);
            builder.Property(x => x.LastIpAddress).HasMaxLength(20).IsRequired(false);

            //silmiyoruz çünkü yukarıdaki üzerine eklediğimiz kısımları da ekleyip o şekilde mapping yapmasını istediğimiz için
            base.Configure(builder);
        }
    }
}
