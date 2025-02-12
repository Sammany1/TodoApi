using Microsoft.EntityFrameworkCore;
using TraskioApi.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Dashboard> Dashboards { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<Subtask> Subtasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map all tables to lowercase naming convention
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<Dashboard>().ToTable("dashboards");
        modelBuilder.Entity<Todo>().ToTable("todos");
        modelBuilder.Entity<Subtask>().ToTable("subtasks");

        // Map all columns to lowercase naming convention
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        });

        modelBuilder.Entity<Dashboard>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        });

        modelBuilder.Entity<Todo>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DashboardId).HasColumnName("dashboard_id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<Subtask>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TodoId).HasColumnName("todo_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsDone).HasColumnName("is_done");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        });
    }
}
