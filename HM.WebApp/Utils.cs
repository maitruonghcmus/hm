using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HM.WebApp
{
    public class ApiUtils
    {
        public static readonly string CUSTOMER = "Customer";
        public static readonly string CUSTOMERTYPE = "CustomerType";
        public static readonly string EXTRASERVICE = "ExtraService";
        public static readonly string HOTEL = "Hotel";
        public static readonly string ORDER = "Order";
        public static readonly string ORDERDETAIL = "OrderDetail";
        public static readonly string PAYMENT = "Paymet";
        public static readonly string ROLE = "Role";
        public static readonly string ROOM = "Room";
        public static readonly string ROOMTYPE = "RoomType";
        public static readonly string USER = "User";

        public static readonly string GETALL = "GetAll";
        public static readonly string GETBYID = "GetById";
        public static readonly string CREATE = "Create";
        public static readonly string UPDATE = "Update";
        public static readonly string DELETE = "Delete";
    }

    public class DateTimeUtils
    {
        public static readonly string YYYY_MM_DD_HH_MM = "yyyy-MM-dd HH:mm";
    }

    public class NumberUtils
    {
        public static readonly string NumberFormatByComma = "#,##0";
        public static readonly string NumberByFormatSpacing = "# ##0";
    }
}