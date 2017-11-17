using System.Collections.Generic;
using System.Data.Services.Client;
using System.Threading.Tasks;

namespace NorthwindModel
{
    public static class ExtensionMethods
    {
        public static async Task<IEnumerable<T>> ExecuteAsync<T>(this DataServiceQuery<T> query)
        {
            return await Task.Factory.FromAsync<IEnumerable<T>>(query.BeginExecute(null, null), query.EndExecute);
        }
    }
}
