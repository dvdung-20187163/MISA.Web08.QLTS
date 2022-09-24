﻿using MISA.Web08.QLTS.API.Entities.DTO;

namespace MISA.Web08.QLTS.API.Entities
{
    public class Asset
    {
        /// <summary>
        /// ID tài sản
        /// </summary>
        public Guid fixed_asset_id { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string fixed_asset_code { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string fixed_asset_name { get; set; }

        /// <summary>
        /// ID tài sản
        /// </summary>
        public string? organization_id { get; set; }

        /// <summary>
        /// ID tài sản
        /// </summary>
        public string? organization_code { get; set; }

        /// <summary>
        /// ID tài sản
        /// </summary>
        public string? organization_name { get; set; }

        /// <summary>
        /// ID phòng ban
        /// </summary>
        public Guid department_id { get; set; }

        /// <summary>
        /// ID tài sản
        /// </summary>
        public string? department_code { get; set; }

        /// <summary>
        /// ID tài sản
        /// </summary>
        public string? department_name { get; set; }

        /// <summary>
        /// ID loại tài sản
        /// </summary>
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        /// ID tài sản
        /// </summary>
        public string? fixed_asset_category_code { get; set; }

        /// <summary>
        /// ID tài sản
        /// </summary>
        public string? fixed_asset_category_name { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        public DateTime purchase_date { get; set; }

        /// <summary>
        /// Giá tiền
        /// </summary>
        public double cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// Tỉ lệ hao mòn (%)
        /// </summary>
        public double depreciation_rate { get; set; }

        /// <summary>
        /// Năm theo dõi
        /// </summary>
        public int tracked_year { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int life_time { get; set; }

        /// <summary>
        /// Năm sử dụng
        /// </summary>
        public int production_year { get; set; }

        /// <summary>
        /// Còn hoạt động hay không
        /// </summary>
        public Boolean active { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? created_by { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? created_date { get; set; }

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string? modified_by { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime? modified_date { get; set; }
    }

}
