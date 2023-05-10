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

            List<Categoria> categoriasInit = new List<Categoria>();
            categoriasInit.Add(new Categoria { CategoriaId = Guid.Parse("bbc9d52c-b213-4558-93bc-0b957b8b5af4") ,Nombre = "Actividades pendientes", Peso= 20 });
            categoriasInit.Add(new Categoria { CategoriaId = Guid.Parse("bbc9d52c-b213-4558-93bc-0b957b8b5a02"), Nombre = "Actividades personales", Peso = 65 });


            modelBuilder.Entity<Categoria>(categoria =>
            {
                categoria.ToTable("Categoria");
                categoria.HasKey(p => p.CategoriaId);

                categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);

                categoria.Property(p => p.Descripcion).IsRequired(false);
                categoria.Property(p => p.Peso);

                categoria.HasData(categoriasInit);
            });

            List<Tarea> tareasInit = new List<Tarea>();
            tareasInit.Add(new Tarea { TareaId = Guid.Parse("bbc9d52c-b213-4558-93bc-0b957b8b5a10"), CategoriaId =Guid.Parse("bbc9d52c-b213-4558-93bc-0b957b8b5af4"), PrioridadTarea = Prioridad.Media , Titulo="Revisar pagos de servicios", FechaCreacion= DateTime.Now});
            tareasInit.Add(new Tarea { TareaId = Guid.Parse("bbc9d52c-b213-4558-93bc-0b957b8b5a11"), CategoriaId = Guid.Parse("bbc9d52c-b213-4558-93bc-0b957b8b5a02"), PrioridadTarea = Prioridad.Alta, Titulo = "Pendientes trabajo", FechaCreacion = DateTime.Now });


            modelBuilder.Entity<Tarea>(tarea => {

                tarea.ToTable("Tarea");
                tarea.HasKey(t => t.TareaId);
                tarea.Property(t => t.CategoriaId).IsRequired();
                tarea.Property(t => t.Titulo).IsRequired().HasMaxLength(200);
                tarea.Property(t => t.Descripcion).HasMaxLength(500).IsRequired(false);
                tarea.Property(t => t.PrioridadTarea).IsRequired();
                tarea.Property(t => t.FechaCreacion);
                tarea.Ignore(t => t.Resumen);

                tarea.HasData(tareasInit);

            });
        }
    }
}
