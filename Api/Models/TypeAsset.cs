using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
	[Table("tb_type_asset")]
	public class TypeAsset : Item
	{
		protected TypeAsset() { }

		public void Update(string name = null, string code = null)
		{
			if (!string.IsNullOrEmpty(name))
			{
				Name = name;
			}
			if (!string.IsNullOrEmpty(code))
			{
				Code = code;
			}
		}
		public TypeAsset(string name, string code)
		{
			Name = name;
			Code = code;
		}
	}
}
