using Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Back.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}
		public DbSet<TiposCombo> TiposCombos { get; set; }
		public DbSet<TiposComboDetalle> TiposComboDetalles { get; set; }
		public DbSet<Cliente> Clientes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			DisableCascadingDelete(modelBuilder);			
		}

		private void DisableCascadingDelete(ModelBuilder modelBuilder)
		{
			var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
			foreach (var relationship in relationships)
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}
		}
	}
}
