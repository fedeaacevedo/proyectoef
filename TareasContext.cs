using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

namespace proyectoef
{
    public class TareasContext: DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(categoria =>
            {
                categoria.ToTable("Categoria");
                categoria.HasKey(p => p.CategoriaId);

                categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);

                categoria.Property(p => p.Descripcion);
            });

            modelBuilder.Entity<Tarea>(tarea => {

                tarea.ToTable("Tarea");
                tarea.HasKey(t => t.TareaId);
                tarea.Property(t => t.CategoriaId);
                tarea.Property(t => t.Titulo).IsRequired().HasMaxLength(200);
                tarea.Property(t => t.Descripcion).HasMaxLength(500);
                tarea.Property(t => t.PrioridadTarea).IsRequired();
                tarea.Property(t => t.FechaCreacion);
                tarea.Ignore(t => t.Resumen);

            });
        }
    }
}
