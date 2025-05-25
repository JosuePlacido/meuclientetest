using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data
{
	public class Context : DbContext
	{
		public Context(DbContextOptions options) : base(options) { }

		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<Asset> Assets { get; set; }
		public DbSet<TypeAsset> AssetTypes { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<ItemOrder> ItemOrders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			SetPrefixColumnName(modelBuilder.Entity<Order>(), "tor_",
				nameof(Order.Id), nameof(Order.ContractNumber),
				nameof(Order.CreatedAt),
				nameof(Order.Discount),
				nameof(Order.SupplierId),
				nameof(Order.Total),
				nameof(Order.UpdatedAt));
			modelBuilder.Entity<Order>(entity =>
			{
				entity.HasMany(o => o.Items).WithOne(io => io.Order).HasForeignKey(io => io.OrderId);
				entity.HasOne(o => o.Supplier);
				entity.Property(o => o.Discount).HasDefaultValue(0);
			});

			SetPrefixColumnName(modelBuilder.Entity<ItemOrder>(), "tio_",
				nameof(ItemOrder.Id), nameof(ItemOrder.AssetId),
				nameof(ItemOrder.OrderId),
				nameof(ItemOrder.Quantity),
				nameof(ItemOrder.UnitPrice));
			modelBuilder.Entity<ItemOrder>(entity =>
			{
				entity.HasOne(io => io.Asset);
			});


			SetPrefixColumnName(modelBuilder.Entity<TypeAsset>(), "tta_",
				nameof(TypeAsset.Id), nameof(TypeAsset.Code),
				nameof(TypeAsset.Name));

			SetPrefixColumnName(modelBuilder.Entity<Asset>(), "tas_",
				nameof(Asset.Id), nameof(Asset.Code), nameof(Asset.TypeAssetId),
				nameof(Asset.Name), nameof(Asset.Price));
			modelBuilder.Entity<Asset>(entity =>
			{
				entity.HasOne(a => a.TypeAsset);
			});

			SetPrefixColumnName(modelBuilder.Entity<Supplier>(), "tsu_",
				nameof(Supplier.Id), nameof(Supplier.Code),
				nameof(Supplier.Name));
			modelBuilder.Entity<Supplier>(entity =>
			{
				entity.Property(s => s.CNPJ).HasConversion(
					v => v.ToString(),
					v => new CNPJ(v)).HasColumnName("tsu_cnpj");
			});
		}
		private void SetPrefixColumnName<TEntity>(EntityTypeBuilder<TEntity> entity, string prefixo, params string[] propertyNames) where TEntity : class
		{
			foreach (var prop in propertyNames)
			{
				entity.Property(prop).HasColumnName(CamelCaseToSnakeCase(prefixo + prop));
			}
		}

		private string CamelCaseToSnakeCase(string input)
		{
			if (string.IsNullOrEmpty(input))
				return input;

			var sb = new StringBuilder();
			for (int i = 0; i < input.Length; i++)
			{
				char c = input[i];
				if (char.IsUpper(c))
				{
					if (i > 0)
						sb.Append('_');

					sb.Append(char.ToLower(c));
				}
				else
				{
					sb.Append(c);
				}
			}
			return sb.ToString();
		}

	}
}
