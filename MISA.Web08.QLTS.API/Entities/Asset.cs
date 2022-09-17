namespace MISA.Web08.QLTS.API.Entities
{
    public class Asset
    {
        public Guid AssetID { get; set; }

        public string AssetCode { get; set; }

        public string AssetName { get; set; }

        public Guid DepartmentID { get; set; }

        public string DepartmentCode { get; set; }

        public string DeoartmentName { get; set; }

        public Guid AssetCategoryID { get; set; }

        public string AssetCategoryCode { get; set; }

        public string AssetCategoryName { get; set; }

        public int Quantity { get; set; }

        public float Cost { get; set; }
    }
}
