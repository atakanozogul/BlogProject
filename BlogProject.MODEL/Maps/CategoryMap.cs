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
    public class CategoryMap : CoreMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            //Category tablosunu bu şekilde maplemiş olduk
            builder.ToTable("Categories"); //bu tabloya yaz dedik
            builder.Property(x => x.CategoryName).HasMaxLength(50).IsRequired(true);
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired(false);

            //silmiyoruz çünkü yukarıdaki üzerine eklediğimiz kısımları da ekleyip o şekilde mapping yapmasını istediğimiz için
            base.Configure(builder);
        }
    }
}
