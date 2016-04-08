using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataModels.Utils
{
    public static class CurrencyUtils
    {
        public const string CURRENCY_FORMAT_COMMA = "#,##0";
        public const string CURRENCY_FORMAT_SPACE = "# ##0";
    }

    public static class DateTimeUtils
    {
        public const string ddMMyyyy = "dd-MM-yyyy";
        public const string HHmm = "HH:mm";
        public const string HHmmss = "HH:mm:ss";
    }

    public static class MessageUtils
    {
        public const string ERR_PERMISSION = "ERR_PERMISSION";
        public const string ERR_NO_FIELD_TO_UPDATE = "ERR_NO_FIELD_TO_UPDATE";
    }
}