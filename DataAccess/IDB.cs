using Chat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Chat.DataAccess;

public interface IDB
{
    public DbSet<User> Users { get; set; }
    public DbSet<Invite> Invites { get; set; }
    public int SaveChanges();
    public EntityEntry Remove(object entity);
}
