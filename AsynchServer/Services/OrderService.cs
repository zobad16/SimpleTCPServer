using AsynchServer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer.Services
{
    public class OrderService
    {
        public void Initialize() { 
        
        }

        public void SendOrder(string source, Order order)
        {

            throw new NotImplementedException();
        }
        public List<Order> RequestOrders(string source)
        {
            throw new NotImplementedException();
            return new List<Order>();
        }
        public Order RequestOrder(string source, Order order)
        {
            throw new NotImplementedException();
            return new Order();
        }
        public void RequestModifyOrder(string source, Order order)
        {
            throw new NotImplementedException();
        }
        public void RequestDeletePosition(string source, Order order)
        {
            throw new NotImplementedException();
        }
        public void RequestModifyPosition(string source, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
