using Api.Data;
using Api.Models;

namespace Api.DAL
{

	public interface IDAOTypeAsset : IDAO<TypeAsset> { }
	public class DAOTypeAsset : DAOBase<TypeAsset>, IDAOTypeAsset
	{
		public DAOTypeAsset(Context context) : base(context) { }
	}
}
