namespace HM.DataModels
{
    public class Role
    {
        public int Id { get; set; }

        /// <summary>
        /// Tên role, ví dụ: administrator, manager, quản lý, thu ngân
        /// </summary>
        public string Name { get; set; }
        public bool? Inactive { get; set; }
    }
}
