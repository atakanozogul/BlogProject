using BlogProject.CORE.Entity; //Core katmanını ekledik çünlü CoreEntity'yi kullanmak için
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.MODEL.Entities
{
    public class User : CoreEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public string LastIpAddress { get; set; }
        
        public virtual List<Post> Posts { get; set; } //Navigation Property
    }
}
