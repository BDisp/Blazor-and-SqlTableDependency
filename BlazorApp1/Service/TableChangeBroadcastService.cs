using BlazorApp1.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace BlazorApp1.Service
{
    public class TableChangeBroadcastService : ITableChangeBroadcastService
    {
        private const string TableName = "Stocks";
        private SqlTableDependency<Stock> _notifier;
        private IConfiguration _configuration;

        public event StockChangeDelegate OnStockChanged;

        public TableChangeBroadcastService(IConfiguration configuration)
        {
            _configuration = configuration;

            // SqlTableDependency will trigger an event for any record change on monitored table
            _notifier = new SqlTableDependency<Stock>(_configuration["ConnectionString"], TableName);
            _notifier.OnChanged += this.TableDependency_Changed;
            _notifier.Start();
        }

        /// <summary>
        /// This method will notify the Blazor component about the stock price change
        /// </summary>
        private void TableDependency_Changed(object sender, RecordChangedEventArgs<Stock> e)
        {
            this.OnStockChanged(this, new StockChangeEventArgs(e.Entity, e.EntityOldValues, e.ChangeType));
        }

        /// <summary>
        /// This method is use to populate the HTML view when it is rendered for the firt time
        /// </summary>
        public IList<Stock> GetCurrentValues()
        {
            var result = new List<Stock>();

            using (var sqlConnection = new SqlConnection(_configuration["ConnectionString"]))
            {
                sqlConnection.Open();

                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM " + TableName;
                    command.CommandType = CommandType.Text;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new Stock
                                {
                                    Code = reader.GetString(reader.GetOrdinal("Code")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price"))
                                });
                            }
                        }
                    }
                }
            }

            return result;
        }

        public void Dispose()
        {
            _notifier.Stop();
            _notifier.Dispose();
        }
    }
}