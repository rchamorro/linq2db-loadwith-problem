using System.Data;
using LinqToDB.DataProvider;

namespace Linq2dbLoadWithTest
{
    public class Db : LinqToDB.Data.DataConnection
    {
        public Db(
            IDataProvider dataProvider,
            IDbConnection connection) 
            : base(dataProvider, connection)
        {
        }
    }
}