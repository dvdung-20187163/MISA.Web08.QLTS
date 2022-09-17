namespace MISA.Web08.QLTS.API.Entities.DTO
{
    public class PagingData
    {
        /// <summary>
        /// Danh sách tài sản
        /// </summary>
        public List<Asset> Data { get; set; }

        /// <summary>
        /// Tổng số tài sản
        /// </summary>
        public int TotalCount { get; set; }
    }
}
