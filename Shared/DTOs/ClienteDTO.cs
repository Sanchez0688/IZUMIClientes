using Shared.Validators;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
	public class ClienteDTO
	{
		public int IdCliente { get; set; }

		[Required(ErrorMessage = "El campo TipoDocumento es obligatorio.")]
		[Range(1, int.MaxValue, ErrorMessage = "El campo TipoDocumento es obligatorio.")]
		public int IdTipoDocumento { get; set; }

		[Required(ErrorMessage = "El campo NumeroDocumento es obligatorio.")]
		[StringLength(20, ErrorMessage = "El Número de Documento no puede tener más de 20 caracteres.")]
		public string NumeroDocumento { get; set; } = null!;

		[Required(ErrorMessage = "El campo Fecha de Nacimiento es obligatorio.")]
		[DataType(DataType.Date, ErrorMessage = "La fecha de nacimiento no es válida.")]
		public DateTime? FechaNacimiento { get; set; }

		[Required(ErrorMessage = "El campo Primer Nombre es obligatorio.")]
		[StringLength(100, ErrorMessage = "El Primer Nombre no puede tener más de 100 caracteres.")]
		public string PrimerNombre { get; set; } = null!;
		
		[StringLength(100, ErrorMessage = "El Segundo Nombre no puede tener más de 100 caracteres.")]
		public string? SegundoNombre { get; set; } 

		[Required(ErrorMessage = "El campo Primer Apellido es obligatorio.")]
		[StringLength(100, ErrorMessage = "El Primer Apellido no puede tener más de 100 caracteres.")]
		public string PrimerApellido { get; set; } = null!;

		[StringLength(100, ErrorMessage = "El Segundo Apellido no puede tener más de 100 caracteres.")]
		public string? SegundoApellido { get; set; }

		[Required(ErrorMessage = "El campo Dirección de Residencia es obligatorio.")]
		[StringLength(255, ErrorMessage = "La Dirección de Residencia no puede tener más de 255 caracteres.")]
		public string DireccionResidencia { get; set; } = null!;

		[Required(ErrorMessage = "El campo Número de Celular es obligatorio.")]
		[StringLength(20, ErrorMessage = "El Número de Celular no puede tener más de 20 caracteres.")]
		public string NumeroCelular { get; set; } = null!;

		[Required(ErrorMessage = "El campo Email es obligatorio.")]
		[StringLength(255, ErrorMessage = "El Email no puede tener más de 255 caracteres.")]
		[ValidEmailAddress(ErrorMessage = "El formato del Email no es válido.")]
		public string Email { get; set; } = null!;

		[Required(ErrorMessage = "El campo Plan es obligatorio.")]
		[Range(1, int.MaxValue, ErrorMessage = "El Plan es obligatorio.")]
		public int IdPlanPreferencia { get; set; }
	}
}
