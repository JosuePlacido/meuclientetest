using Api.DAL;
using Api.Services.AssetCRUD;
using Api.Services.OrderCRUD;
using Api.Services.SupplierCRUD;
using Api.Services.TypeAssetCRUD;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<ISupplierService, SupplierService>();
			services.AddScoped<IDAOSupplier, DAOSupplier>();

			services.AddScoped<ITypeAssetService, TypeAssetService>();
			services.AddScoped<IDAOTypeAsset, DAOTypeAsset>();

			services.AddScoped<IAssetService, AssetService>();
			services.AddScoped<IDAOAsset, DAOAsset>();

			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IDAOOrder, DAOOrder>();

			return services;
		}
	}
}
