using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
	[Table("tb_order")]
	public class Order : Entity
	{
		[Required]
		public string ContractNumber { get; private set; }
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime CreatedAt { get; private set; }
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime UpdatedAt { get; private set; }
		[Required]
		public string SupplierId { get; private set; }
		public Supplier Supplier { get; private set; }
		[Column(TypeName = "Money")]
		public decimal Discount { get; private set; }
		[Required]
		[Column(TypeName = "Money")]
		public decimal Total { get; private set; }
		public virtual ICollection<ItemOrder> Items { get; private set; }
	}
}
