using BlazorApp1.Models;
using System;
using System.Collections.Generic;
using TableDependency.SqlClient.Base.Enums;

namespace BlazorApp1.Service
{
    public delegate void StockChangeDelegate(object sender, StockChangeEventArgs args);

    public class StockChangeEventArgs : EventArgs
    {
        public Stock NewValue { get; }
        public Stock OldValue { get; }
        public ChangeType ChangeType { get; }

        public StockChangeEventArgs(Stock newValue, Stock oldValue, ChangeType changeType)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
            this.ChangeType = changeType;
        }
    }

    public interface ITableChangeBroadcastService : IDisposable
    {
        event StockChangeDelegate OnStockChanged;
        IList<Stock> GetCurrentValues();
    }
}