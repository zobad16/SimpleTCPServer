using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer.BusinessLogic
{
    public class MockStrategy : IStrategy
    {
        public void OnOrderExecution()
        {
            throw new NotImplementedException();
        }

        public void OnTick()
        {
            throw new NotImplementedException();
        }
    }
}
