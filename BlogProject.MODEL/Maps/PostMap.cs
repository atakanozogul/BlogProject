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
    public class PostMap : CoreMap<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            //Posts tablosunu bu şekilde maplemiş olduk
            builder.ToTable("Posts"); //bu tabloya yaz dedik
            builder.Property(x => x.Title).HasMaxLength(255).IsRequired(true);
            builder.Property(x => x.PostDetail).IsRequired(true);
            builder.Property(x => x.Tags).IsRequired(false);
            builder.Property(x => x.ImagePath).IsRequired(false);

            //silmiyoruz çünkü yukarıdaki üzerine eklediğimiz kısımları da ekleyip o şekilde mapping yapmasını istediğimiz için
            base.Configure(builder);
        }
    }
}
