using Entities.LoginModel;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public ApplicationDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(new Book() { Id=1,Title="Sample Book",Author="Arihant",AvailableCopies=10});
            modelBuilder.Entity<UserModel>().HasOne(x => x.Password).WithOne(y => y.User).HasForeignKey<Password>(x => x.UserId);
            modelBuilder.Entity<UserModel>().HasOne(x => x.Role).WithMany(y => y.Users).HasForeignKey(x => x.RoleId);
        }
        public DbSet<Book> books { get; set; }
        public DbSet<Member> member { get; set; }
        public DbSet<BorrowRecord> borrowRecords { get; set; }

        public virtual DbSet<UserModel> users { get; set; }
        public DbSet<Password> passwords { get; set; }
        public virtual DbSet<Roles> roles { get; set; }
    }
}
