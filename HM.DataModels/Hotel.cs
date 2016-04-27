using System;

namespace HM.DataModels
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string TaxCode { get; set; }
        public string Address { get; set; }

        /// <summary>
        /// Người liên hệ của KS đó
        /// </summary>
        public string Contact { get; set; }
        public string ContactPhone { get; set; }
        public string ContactMail { get; set; }

        public bool Inactive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
