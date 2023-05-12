using Microsoft.EntityFrameworkCore;
using Chat.Models;

namespace Chat.DataAccess;

public class MysqlDB : DbContext, IDB
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString: @"Data Source=localhost;port=3306;Initial Catalog=chat;User Id=chat;password=chat",
            new MySqlServerVersion(new Version(10, 11, 2)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Invite> Invites { get; set; }
    public DbSet<Channel> Channels { get; set; }
}
