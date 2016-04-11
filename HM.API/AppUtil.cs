using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HM.API
{
    /// <summary>
    /// Database Utilities
    /// </summary>
    public static class DbUtils
    {
        private static string mongoServerUrl = ConfigurationManager.AppSettings["MongoServerUrl"] ?? "mongodb://truongmv:truongmv123$@ds048878.mlab.com:48878/truongmvmongodb";
        public static string MongoServerUrl
        {
            get
            {
                return mongoServerUrl;
            }
        }

        private static string mongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"] ?? "truongmvmongodb";
        public static string MongoDatabaseName
        {
            get
            {
                return mongoDatabaseName;
            }
        }

        private static string apiKey = ConfigurationManager.AppSettings["ApiKey"] ?? "apiKey";
        public static string ApiKey
        {
            get
            {
                return apiKey;
            }
        }

        public const string CustomerCollection = "Customer";
        public const string CustomerTypeCollection = "CustomerType";
        public const string ExtraServiceCollection = "ExtraService";
        public const string HotelCollection = "Hotel";
        public const string OrderCollection = "Order";
        public const string OrderDetailCollection = "OrderDetail";
        public const string PaymentCollection = "Payment";
        public const string RoleCollection = "Role";
        public const string RoomCollection = "Room";
        public const string RoomTypeCollection = "RoomType";
        public const string UserCollection = "User";
    }
}