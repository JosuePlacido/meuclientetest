using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Application.Exceptions;

namespace Api.Models
{
	[Table("tb_order")]
	public class Order : Entity
	{
		[Required]
		public string ContractNumber { get; private set; }
		[DataType(DataType.DateTime)]
		public DateTime CreatedAt { get; private set; }
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
		private Order()
		{
			Items = new List<ItemOrder>();
		}
		public Order(string contractNumber, string supplierId, decimal discount, ItemOrder[] items)
		{
			ContractNumber = contractNumber;
			SupplierId = supplierId;
			Discount = discount;
			Items = items;
		}

		public void CalculateTotal()
		{
			decimal totalRaw = Items.Sum(i => i.Quantity * i.UnitPrice);
			if (totalRaw < Discount)
			{
				throw new BusinesRuleException("O desconto não pode ser maior que o Total", this);
			}
			Total = totalRaw - Discount;
		}

		public void Update(string supplierId, string contractNumber, decimal? discount, List<ItemOrder> items)
		{
			if (!string.IsNullOrEmpty(supplierId))
				SupplierId = supplierId;
			if (!string.IsNullOrEmpty(contractNumber))
				ContractNumber = contractNumber;
			if (discount != null)
				Discount = (decimal)discount;
			Items = items;
			CalculateTotal();
		}
	}
}
