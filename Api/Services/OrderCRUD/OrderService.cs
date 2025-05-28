using System.Threading.Tasks;
using System.Linq;
using Api.DAL;
using Api.Models;
using Application.Exceptions;
using System;
using Api.DTO;

namespace Api.Services.OrderCRUD
{
	public class OrderService : IOrderService
	{
		private readonly IDAOOrder _daoOrder;
		private readonly IDAOSupplier _daoSupplier;
		private readonly IDAOAsset _daoAsset;
		public OrderService(IDAOOrder daoOrder, IDAOSupplier daoSupplier, IDAOAsset daoAsset)
		{
			_daoOrder = daoOrder;
			_daoSupplier = daoSupplier;
			_daoAsset = daoAsset;
		}


		public async Task<ViewOrderDetailedDTO> Get(string id)
		{
			Order order = await _daoOrder.GetDetailedById(id);
			if (order == null) return null;
			return new ViewOrderDetailedDTO(order);
		}

		public async Task<ViewOrderItemListDTO[]> GetAll()
		{
			var orders = await _daoOrder.GetAll();
			return orders.Select(s => new ViewOrderItemListDTO(s)).ToArray();
		}
		public async Task<PaginationDTO<ViewOrderDetailedDTO>> GetAllList(int page, int take)
		{
			if (page < 1)
			{
				page = 1;
			}
			if (take < 1 || take > 100)
			{
				take = 10;
			}
			var result = await _daoOrder.List(page, take);
			return new PaginationDTO<ViewOrderDetailedDTO>()
			{
				Page = result.Page,
				Take = result.Take,
				Total = result.Total,
				Items = result.Items.Select(i => new ViewOrderDetailedDTO(i)).ToArray()
			};
		}

		public async Task<ViewOrderDetailedDTO> Remove(string id)
		{
			if (!await _daoOrder.Exists(id))
			{
				throw new ValidationException("Contrato de Venda não encontrado",
					new ValidationError("id", "nenhum Contrato de Venda encontrado com este id"));
			}
			Order order = await _daoOrder.GetById(id);
			order = _daoOrder.Delete(order);
			await _daoOrder.Commit();

			return new ViewOrderDetailedDTO(order);
		}
		public async Task<ViewOrderDetailedDTO> Register(CreateOrderDTO newValues)
		{
			if (!await _daoSupplier.Exists(newValues.SupplierId))
			{
				throw new ValidationException("Falha ao registrar Contrato de Venda",
					new ValidationError("supplierId", "Fornecedor inexistente"));
			}
			foreach (ItemOrderEdit item in newValues.Items)
			{
				if (!await _daoAsset.Exists(item.AssetId))
				{
					throw new ValidationException("Falha ao registrar Contrato de Venda",
						new ValidationError("items", $"Ativo {item.AssetId} inexistente."));
				}
			}
			Order order = newValues.ToOrder();
			order.CalculateTotal();
			order = await _daoOrder.Add(order);
			await _daoOrder.Commit();
			return new ViewOrderDetailedDTO(order);
		}

		public async Task<ViewOrderDetailedDTO> Update(string id, UpdateOrderDTO newValues)
		{
			if (!string.IsNullOrEmpty(newValues.SupplierId) && !await _daoSupplier.Exists(newValues.SupplierId))
			{
				throw new ValidationException("Falha ao alterar Contrato de Venda",
					new ValidationError("supplierId", "Fornecedor inexistente"));
			}
			foreach (ItemOrderEdit item in newValues.Items)
			{
				if (!await _daoAsset.Exists(item.AssetId))
				{
					throw new ValidationException("Falha ao alterar Contrato de Venda",
						new ValidationError("items", $"Ativo {item.AssetId} inexistente."));
				}
			}
			Order order = await _daoOrder.GetById(id);
			if (order == null)
			{
				throw new ValidationException("Contrato de Venda não encontrado",
					new ValidationError("id", "nenhum Contrato de Venda encontrado com este id"));
			}
			order.Update(newValues.SupplierId, newValues.ContractNumber, newValues.Discount,
				newValues.Items.Select(i => i.ToItemOrder()).ToList());
			order = _daoOrder.Update(order);
			await _daoOrder.Commit();

			return new ViewOrderDetailedDTO(order);
		}
	}
}
