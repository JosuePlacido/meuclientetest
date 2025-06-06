using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
	[Table("tb_supplier")]
	public class Supplier : InternalScoped
	{
		[Required]
		public CNPJ CNPJ { get; private set; }

		protected Supplier() { }
		public Supplier(string name, string code, string cnpj)
		{
			Name = name;
			Code = code;
			CNPJ = new CNPJ(cnpj);
		}

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
	}
}
