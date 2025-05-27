using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.DAL
{

	public interface IDAOAsset : IDAO<Asset>
	{
		Task<Asset> GetAssetDetailed(string id);
	}
	public class DAOAsset : DAOBase<Asset>, IDAOAsset
	{
		public DAOAsset(Context context) : base(context) { }
		public override async Task<Asset[]> GetAll()
			=> await _context.Assets.AsNoTracking().Include(a => a.TypeAsset).ToArrayAsync();
		public async Task<Asset> GetAssetDetailed(string id)
			=> await _context.Assets.Where(a => a.Id == id)
				.Include(a => a.TypeAsset).AsNoTracking().FirstOrDefaultAsync();

	}

}
