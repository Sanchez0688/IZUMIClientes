
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.Entities
{
	[Table("TiposCombos")]
	public class TiposCombo
	{
		[Key]
		public int IdTipo { get; set; }

		[StringLength(100)]
		public string Name { get; set; } = null!;
		[StringLength(100)]
		public string CodeName { get; set; } = null!;
		public bool Statu { get; set; } 		

		[Column(TypeName = "datetime")]
		public DateTime CreationDate { get; set; } 

		public int UserCreateId { get; set; }

		[ForeignKey("IdTipo")]
		public ICollection<TiposComboDetalle> ListTiposComboDetalles { get; set; } = new List<TiposComboDetalle>();
	}
}
