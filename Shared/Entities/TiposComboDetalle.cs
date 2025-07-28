
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Entities
{
	[Table("TiposComboDetalle")]
	public class TiposComboDetalle
	{
		[Key]
		public int IdTipoComboDetalle { get; set; }
		public int IdTipo { get; set; }
		[JsonIgnore]
		public TiposCombo Tipo { get; set; } = null!;		
	
		[StringLength(150)]
		public string Name { get; set; } = null!;
		public bool Statu { get; set; }		

		[Column(TypeName = "datetime")]
		public DateTime CreationDaten { get; set; }
		public int UserCreateId { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime ModificationDate { get; set; }
		public int UserModificationId { get; set; }

	}
}
