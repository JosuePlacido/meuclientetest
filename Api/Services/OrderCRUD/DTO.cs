using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Api.Models;

namespace Api.Services.OrderCRUD
{

	public class ViewOrderDetailedDTO
	{
		public string Id { get; private set; }
		public string ContractNumber { get; private set; }
		public DateTime CreatedAt { get; private set; }
		public DateTime UpdatedAt { get; private set; }
		public string SupplierId { get; private set; }
		public Supplier Supplier { get; private set; }
		public decimal Discount { get; private set; }
		public decimal Total { get; private set; }
		public ItemOrder[] Items { get; private set; }

		public ViewOrderDetailedDTO(Order supplier)
		{
			Id = supplier.Id;
			ContractNumber = supplier.ContractNumber;
			SupplierId = supplier.SupplierId;
			CreatedAt = supplier.CreatedAt;
			UpdatedAt = supplier.UpdatedAt;
			Supplier = supplier.Supplier;
			Discount = supplier.Discount;
			Total = supplier.Total;
			Items = supplier.Items != null ? supplier.Items.ToArray() : Array.Empty<ItemOrder>();
		}
	}
	public class ViewOrderItemListDTO
	{
		public string Id { get; private set; }
		public string ContractNumber { get; private set; }
		public DateTime CreatedAt { get; private set; }
		public DateTime UpdatedAt { get; private set; }
		public Supplier Supplier { get; private set; }
		public decimal Discount { get; private set; }
		public decimal Total { get; private set; }

		public ViewOrderItemListDTO(Order order)
		{
			Id = order.Id;
			ContractNumber = order.ContractNumber;
			CreatedAt = order.CreatedAt;
			UpdatedAt = order.UpdatedAt;
			Supplier = order.Supplier;
			Discount = order.Discount;
			Total = order.Total;
		}
	}
	public class CreateOrderDTO : IValidatableObject
	{
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public string ContractNumber { get; set; }
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public string SupplierId { get; set; }
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		[Range(0, double.MaxValue, ErrorMessage = "O desconto deve ser maior que zero.")]
		public decimal Discount { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		[MinLength(1, ErrorMessage = "O pedido deve conter ao menos {1} item.")]
		public ItemOrderEdit[] Items { get; set; }

		public Order ToOrder()
		{
			ItemOrder[] items = Items.Select(i => i.ToItemOrder()).ToArray();
			return new Order(ContractNumber, SupplierId, Discount, items);
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			for (int i = 0; i < Items.Length; i++)
			{
				var item = Items[i];

				if (string.IsNullOrWhiteSpace(item.AssetId))
				{
					yield return new ValidationResult($"O item {i + 1}: O campo AssetId é obrigatório.", new[] { $"{nameof(Items)}[{i}].{nameof(item.AssetId)}" });
				}
				if (item.Quantity <= 0)
				{
					yield return new ValidationResult($"O item {i + 1}: A quantidade deve ser maior que zero.", new[] { $"{nameof(Items)}[{i}].{nameof(item.Quantity)}" });
				}
				if (item.UnitPrice <= 0)
				{
					yield return new ValidationResult($"O item {i + 1}: O preço unitário deve ser maior que zero.", new[] { $"{nameof(Items)}[{i}].{nameof(item.UnitPrice)}" });
				}
			}
		}
	}
	public class ItemOrderEdit
	{
		public string AssetId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }

		public ItemOrder ToItemOrder()
		{
			return new ItemOrder(AssetId, Quantity, UnitPrice);
		}
	}

	public class UpdateOrderDTO : IValidatableObject
	{
		public string ContractNumber { get; set; }
		public string SupplierId { get; set; }

		[Range(0, double.MaxValue, ErrorMessage = "O desconto deve ser maior que zero.")]
		public decimal? Discount { get; set; }
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		[MinLength(1, ErrorMessage = "O pedido deve conter ao menos {1} item.")]
		public ItemOrderEdit[] Items { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			for (int i = 0; i < Items.Length; i++)
			{
				var item = Items[i];

				if (string.IsNullOrWhiteSpace(item.AssetId))
				{
					yield return new ValidationResult($"O item {i + 1}: O campo AssetId é obrigatório.", new[] { $"{nameof(Items)}[{i}].{nameof(item.AssetId)}" });
				}
				if (item.Quantity <= 0)
				{
					yield return new ValidationResult($"O item {i + 1}: A quantidade deve ser maior que zero.", new[] { $"{nameof(Items)}[{i}].{nameof(item.Quantity)}" });
				}
				if (item.UnitPrice <= 0)
				{
					yield return new ValidationResult($"O item {i + 1}: O preço unitário deve ser maior que zero.", new[] { $"{nameof(Items)}[{i}].{nameof(item.UnitPrice)}" });
				}
			}
		}
	}
}
