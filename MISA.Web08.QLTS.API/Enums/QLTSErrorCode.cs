namespace MISA.Web08.QLTS.API.Enums
{
    /// <summary>
    /// Danh sách mã lỗi khi gọi API
    /// </summary>
    public enum QLTSErrorCode
    {
        /// <summary>
        /// Lỗi do exeption
        /// </summary>
        Exeption = 1, 


        /// <summary>
        /// Lỗi do trùng mã 
        /// </summary>
        DuplicateCode = 2,


        /// <summary>
        /// Lỗi do mã bị để trống
        /// </summary>
        EmptyCode = 3,

        /// <summary>
        /// Lấy toàn bộ bản ghi từ DB, thất bại
        /// </summary>
        GetAllFailed = 4,

        /// <summary>
        /// Lấy chi tiết 1 bản ghi từ DB, thất bại
        /// </summary>
        GetDetailOneFailed = 5,

        /// <summary>
        /// Gọi vào DB để insert, thất bại
        /// </summary>
        /// 
        InsertFailed = 6,

        /// <summary>
        /// Gọi vào DB để cập nhật bản ghi, thất bại
        /// </summary>
        UpdateFailed = 7,

        /// <summary>
        /// Gọi vào DB để xóa bản ghi, thất bại
        /// </summary>
        DeleteFailed = 8,

        /// <summary>
        /// Gọi vào DB để xóa nhiều bản ghi, thất bại
        /// </summary>
        BatchDeleteFailed = 9,

        /// <summary>
        /// Lọc tài sản thất bại
        /// </summary>
        FilterFailed = 10,

        /// <summary>
        /// Validate input bắt buộc
        /// </summary>
        InvalidInput = 11,
    }
}
