using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AbaJohn.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;


namespace AbaJohn.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
        {
            
        }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base (options)
        {
        
            
        }


        //this is just overlode u can delete it if u want 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=AbaJohn;Integrated Security=True");
        }


        public DbSet<Product> products { get; set; }

        public DbSet<Order> orders { get; set; }
        public DbSet<Payment> payments{ get; set; }
        public DbSet<Category> categories{ get; set; }
        public DbSet<CartItem> cartItems{ get; set; }
        public DbSet<ProductImage> productImages{ get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Item> item { get; set; }
    }


    public class ApplicationUser : IdentityUser
    {

        public string Name { get; set; }
        public int age { get; set; }

        public String? img { get; set; }
      
        public string Gender { get; set; }
      
        public Address address { get; set; }
        public CartItem CartItem { get; set; }
        public List<Product> products { get; set; }

    }
 
}
