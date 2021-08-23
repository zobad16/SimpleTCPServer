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
        public enum MessageType{
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
            MD_UNSUBSCRIBE = 10,
            MARKET_DATA = 11,
            ORDER_SNAPSHOT = 12,
            POSITION_SNAPSHOT = 13
        };
        public enum MessageField
        {
            MESSAGE_TYPE = 0,
            CLIENT_ID =1,
            DATE =2,
            DATA =3,
            EOF = 4
        }
        public static int GetIntValue(OrderType obj){
            return (int)obj;
        }
        public static int GetIntValue(MessageType obj)
        {
            return (int)obj;
        }
        public static int GetIntValue(MessageField obj)
        {
            return (int)obj;
        }
    }
}
