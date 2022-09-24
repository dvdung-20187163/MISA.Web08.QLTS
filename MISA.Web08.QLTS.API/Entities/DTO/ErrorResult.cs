using MISA.Web08.QLTS.API.Enums;

namespace MISA.Web08.QLTS.API.Entities.DTO
{
    public class ErrorResult
    {
        #region Property
        public QLTSErrorCode ErrorCode { get; set; }

        public string DevMsg { get; set; }

        public string UserMsg { get; set; }

        public string MoreInfo { get; set; }

        public string TraceId { get; set; } 
        #endregion

        #region Constructor

        /// <summary>
        /// Hàm khởi tạo không tham số, mặc định là tự sinh ra nhưng nó sẽ không tự sinh ra khi có hàm khởi tạo có tham số
        /// </summary>
        public ErrorResult() { }

        /// <summary>
        /// Hàm khởi tạo có tham số, khi dùng hàm khởi tạo có tham số thì cần khởi tạo thêm hàm không số.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="devMsg"></param>
        /// <param name="userMsg"></param>
        /// <param name="moreInfo"></param>
        /// <param name="traceId"></param>
        public ErrorResult(QLTSErrorCode errorCode, string devMsg, string userMsg, string moreInfo, string traceId)
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
            TraceId = traceId;
        } 

        #endregion
    }
}
