namespace HM.DataModels
{
    /// <summary>
    /// API sẽ trả về thông qua Result
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu cụ thể sẽ được gọi</typeparam>
    public class Result
    {
        /// <summary>
        /// Mã lỗi (nếu có) khi gọi API thất bại
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Kết quả thành công hoặc thất bại khi gọi API
        /// </summary>
        public bool IsSuccess()
        {
            return string.IsNullOrEmpty(Code);
        }
    }

    public class Result<T> : Result
    {
        /// <summary>
        /// Nếu không có lỗi, dữ liệu sẽ trả về qua biến DATA
        /// </summary>
        public T Data { get; set; }
    }
}