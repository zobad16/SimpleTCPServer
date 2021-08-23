using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer.Util
{
    public static class AppProperties
    {
        public enum OrderType { 
            NONE = 0,
            MARKET_ORDER = 1,
            BUY_LIMIT = 2,
            SELL_LIMIT= 3,
            BUY_STOP = 4,
            SELL_STOP =5,
            BUY_STOP_LIMIT = 6,
            SELL_STOP_LIMIT = 7
        };
        public enum ServerMessageType{
            HEART_BEAT = 0, 
            LOGIN = 1,
            NEW_ORDER = 2,
            UPDATE_ORDER = 3,
            DELETE_ORDER = 4,
            REQUEST_ORDERS_SNAPSHOT = 5,
            AMMEND_POSITION = 6,
            CLOSE_POSITION = 7,
            REQUEST_POSITION_SNAPSHOT = 8,
            MD_SUBSCRIBE = 9,
            MD_UNSUBSCRIBE = 10
        };
        public enum ClientMessageType
        {
            HEART_BEAT = 0,
            LOGIN = 1,
            MARKET_DATA = 2,
            ORDER_SNAPSHOT = 3,
            POSITION_SNAPSHOT = 4
            
        };
    }
}
