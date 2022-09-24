namespace MISA.Web08.QLTS.API.Entities.DTO
{
    public class PagingData <T>
    {
        /// <summary>
        /// Danh sách tài sản
        /// </summary>
        public List<Asset> Data { get; set; } = new List<Asset>();

        /// <summary>
        /// Tổng số tài sản
        /// </summary>
        public int TotalCount { get; set; }
    }
}
