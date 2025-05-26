using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.Services.SupplierCRUD
{
	public class ViewSupplierDTO
	{
		public string Name { get; private set; }
		public string Code { get; private set; }
		public string Id { get; private set; }
		public string CNPJ { get; private set; }

		public ViewSupplierDTO(Supplier supplier)
		{
			Name = supplier.Name;
			Code = supplier.Code;
			Id = supplier.Id;
			CNPJ = supplier.CNPJ.ToString();
		}
	}
	public class CreateSupplierDTO
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Code { get; set; }
		[Required]
		public string CNPJ { get; set; }

		public Supplier ToSuplier()
		{
			return new Supplier(Name, Code, CNPJ);
		}
	}
	public class UpdateSupplierDTO
	{
		public string Name { get; set; }
		public string Code { get; set; }
	}
}
