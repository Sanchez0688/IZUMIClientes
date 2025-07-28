
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Shared.Entities
{
	[Table("Clientes")]
	public class Cliente
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IdCliente { get; set; }

		[ForeignKey("TipoDocumento")]
		public int IdTipoDocumento { get; set; }

		[Required(ErrorMessage = $"El campo NumeroDocumento es obligatorio.")]
		[StringLength(20)]
		public string NumeroDocumento { get; set; } = null!;

		[Required(ErrorMessage = $"El campo Fecha Nacimiento es obligatorio.")]
		public DateTime FechaNacimiento { get; set; }

		[Required(ErrorMessage = $"El campo Primer Nombre es obligatorio.")]
		[StringLength(100)]
		public string PrimerNombre { get; set; } = null!;

		
		[StringLength(100)]
		public string? SegundoNombre { get; set; } 

		[Required(ErrorMessage = $"El campo PrimerApellido es obligatorio.")]
		[StringLength(100)]
		public string PrimerApellido { get; set; } = null!;

		
		[StringLength(100)]
		public string? SegundoApellido { get; set; }

		[Required(ErrorMessage = $"El campo Direccion Residencia es obligatorio.")]
		[StringLength(255)]
		public string DireccionResidencia { get; set; } = null!;

		[Required(ErrorMessage = $"El campo Numero Celular es obligatorio.")]
		[StringLength(20)]
		public string NumeroCelular { get; set; } = null!;

		[Required(ErrorMessage = $"El campo Correo es obligatorio.")]
		[StringLength(255)]
		public string Email { get; set; } = null!;

		[ForeignKey("PlanPreferencia")]
		public int IdPlanPreferencia { get; set; }

		// Navegación a la tabla TiposComboDetalle para TipoDocumento
		public virtual TiposComboDetalle TipoDocumento { get; set; } = null!;

		// Navegación a la tabla TiposComboDetalle para PlanPreferencia
		public virtual TiposComboDetalle PlanPreferencia { get; set; } = null!;
	}
}
