using System.Collections.Generic;
using System.Linq;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Library.DataAccess
{
    public class UserData
    {
        public UserModel GetUserById(string id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new {Id = id};

            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "RMData").First();

            return output;
        }
    }
}
