using AsynchServer.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer.Model
{
   public class Order
    {

        public Order(DateTime time, int type, int direction, double volume, double price, double takeProfit, double stopLoss, int ticket, string symbol, int execution)
        {
            Time = time;
            Type = (AppProperties.OrderType)type;        
            Volume = volume;
            Price = price;
            TakeProfit = takeProfit;
            StopLoss = stopLoss;
            this.ticket = ticket;
            this.symbol = symbol;
            this.execution = execution;
        }
        public Order()
        {
            Time = new DateTime();
            Type = AppProperties.OrderType.NONE;
            Volume = 0.0;
            Price = 0.0;
            TakeProfit = 0.0;
            StopLoss = 0.0;
            Ticket = 0;
            Symbol = "";
            Execution = -1;
        }
        private DateTime time;
        private int ticket;
        private string symbol;
        private int execution;
        private AppProperties.OrderType type;
        private double volume;
        private double price;
        private double takeProfit;
        private double stopLoss;

        public DateTime Time { get => time; set => time = value; }
        public AppProperties.OrderType Type { get => type; set => type = value; }
        public double Volume { get => volume; set => volume = value; }
        public double Price { get => price; set => price = value; }
        public double TakeProfit { get => takeProfit; set => takeProfit = value; }
        public double StopLoss { get => stopLoss; set => stopLoss = value; }
        public int Ticket { get => ticket; set => ticket = value; }
        public string Symbol { get => symbol; set => symbol = value; }
        public int Execution { get => execution; set => execution = value; }
    }
}
