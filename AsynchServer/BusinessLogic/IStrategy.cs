using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer.BusinessLogic
{
    public interface IStrategy
    {
        void OnTick();
        void OnOrderExecution();
    }
}
