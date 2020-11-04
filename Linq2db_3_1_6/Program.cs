using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.DataProvider.PostgreSQL;
using Npgsql;

namespace Linq2dbLoadWithTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started building sql...");
           
            var timer = Stopwatch.StartNew();
            
            var task = Task.Run(BuildSql);

            const int timeoutInSeconds = 10;

            if (task.Wait(TimeSpan.FromSeconds(timeoutInSeconds)))
            {
                timer.Stop();

                Console.WriteLine($"Sql built in {timer.Elapsed.TotalSeconds} seconds");
            }
            else
            {
                timer.Stop();
                
                Console.WriteLine($"Sql build TIMED OUT after {timer.Elapsed.TotalSeconds} seconds");
            }
        }

        private static void BuildSql()
        {
            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
            const string connectionString = "Server=127.0.0.1;Database=testdb;User Id=postgres;Password=postgres; Timeout=180;";
            using var db = new Db(new PostgreSQLDataProvider(PostgreSQLVersion.v95), new NpgsqlConnection(connectionString));
            var query = db
                .GetTable<CustomInvoice>()
                .LoadWith(i => i.Invoice)
                .LoadWith(i => i.Invoice.Rectifying)
                .LoadWith(i => i.Invoice.RefundBy)
                .LoadWith(i => i.Invoice.PendingState)
                .LoadWith(i => i.Invoice.Lines)
                .LoadWith(i => i.Invoice.Lines.First().ProductUnit)
                .LoadWith(i => i.Invoice.Lines.First().Product)
                .LoadWith(i => i.Invoice.TaxLines)
                .LoadWith(i => i.Invoice.TaxLines.First().Tax)
                .LoadWith(i => i.Contract)
                .LoadWith(i => i.AccessTariff)
                .LoadWith(i => i.ServicePoint)
                .LoadWith(i => i.ServicePoint.Town)
                .LoadWith(i => i.ServicePoint.Town.State.Community)
                .LoadWith(i => i.ServicePoint.StreetType)
                .LoadWith(i => i.InvoiceLines)
                .LoadWith(i => i.TypeAMeasures)
                .LoadWith(i => i.TypeAMeasures.First().Source)
                .LoadWith(i => i.TypeAMeasures.First().PreviousSource)
                .LoadWith(i => i.TypeBMeasures)
                .LoadWith(i => i.PriceList)
                .Where(f => f.Id == 1);


            var _ = query.ToString();
        }
    }
}