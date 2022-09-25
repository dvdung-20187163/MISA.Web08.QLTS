using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MISA.Web08.QLTS.API.Entities;
using MISA.Web08.QLTS.API.Entities.DTO;
using MISA.Web08.QLTS.API.Enums;
using MISA.Web08.QLTS.API.Properties;
using MySqlConnector;
using System.Data;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace MISA.Web08.QLTS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {

        #region API GET
        /// <summary>
        /// API Lấy danh sách toàn bộ tài sản
        /// </summary>
        /// <returns>Danh sách toàn bộ tài sản</returns>
        /// Create By: DVDUNG (17/09/2022)
        [HttpGet]
        [Route("")]
        public IActionResult GetAllAssets()
        {
            try
            {
                // B1: Khởi tạo kết nối tới DB MySQL
                string connectionString = "Server=localhost;Port=3307;Database=misa.web08.hcsn.dvdung;Uid=root;Pwd=dungday123@;";
                var mysqlConnection = new MySqlConnection(connectionString);  // gạch chân đỏ --> using 1 package

                // B2: Chuẩn bị câu lệnh SQL
                string storedProcedureName = "Proc_asset_GetAll";
                var parameters = new DynamicParameters();
                parameters.Add("d_asset_id", Guid.NewGuid());
                // B4: Thực hiện gọi vào Db
                var assets = mysqlConnection.Query(
                    storedProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
                if (assets != null)
                {
                return StatusCode(StatusCodes.Status200OK, assets);

                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                        QLTSErrorCode.Exeption,
                        Resource.DevMsg_GetAllFailed,
                        Resource.UserMsg_GetAllFailed,
                        Resource.MoreInfo_GetAllFailed,
                        HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    QLTSErrorCode.Exeption,
                    Resource.DevMsg_Exeption,
                    Resource.UserMsg_Exeption,
                    Resource.MoreInfo_Exeption,
                    HttpContext.TraceIdentifier));
            }
        }

        /// <summary>
        /// API lấy thông tin tài sản theo ID
        /// </summary>
        /// <param name="assetID"></param>
        /// <returns>Thông tin tài sản cần tìm</returns>
        /// Create By: DVDUNG (17/09/2022)
        [HttpGet]
        [Route("{assetID}")]

        public IActionResult GetAssetID([FromRoute] Guid assetID)
        {
            try
            {
                // Khởi tạo kết nối tới DB
                string connectionString = "Server=localhost;Port=3307;Database=misa.web08.hcsn.dvdung;Uid=root;Pwd=dungday123@;";
                var mysqlConnection = new MySqlConnection(connectionString);  // gạch chân đỏ --> using 1 package

                // Khai báo tên Procedure Get Detail One
                var storedProcedureName = "Proc_asset_GetDetailOne";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("d_fixed_asset_id", assetID);

                // Thực hiện gọi vào DB để chạy procedure
                var asset = mysqlConnection.QueryFirstOrDefault<Asset>(
                    storedProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (asset != null)
                {
                    return StatusCode(StatusCodes.Status200OK, asset);  
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        QLTSErrorCode.GetDetailOneFailed,
                        Resource.DevMsg_GetDetailOneFailed,
                        Resource.UserMsg_GetDetailOneFailed,
                        Resource.MoreInfo_GetDetailOneFailed,
                        HttpContext.TraceIdentifier));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    QLTSErrorCode.Exeption,
                    Resource.DevMsg_Exeption,
                    Resource.UserMsg_Exeption,
                    Resource.MoreInfo_Exeption,
                    HttpContext.TraceIdentifier));
            }
        }

        /// <summary>
        /// API lọc danh sách tài sản, phân trang
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="assetCategoryID"></param>
        /// <param name="departmentID"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns>Danh sách tài sản sau khi được lọc</returns>
        /// Create By: DVDUNG (17/09/2022)
        [HttpGet("filter")]
        public IActionResult FilterAsset(
            [FromQuery] string? keyword,
            [FromQuery] string? assetCategoryID,
            [FromQuery] string? departmentID,
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 0
            )
        {
            try
            {
                // Khởi tạo kết nôi tới Database
                string connectionString = "Server=localhost;Port=3307;Database=misa.web08.hcsn.dvdung;Uid=root;Pwd=dungday123@;";
                var mysqlConnection = new MySqlConnection(connectionString);  // gạch chân đỏ --> using 1 package

                // Khai báo tên Procedure GetPaging
                var storedProcedureName = "Proc_asset_GetPaging";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("v_Where", $"fixed_asset_code LIKE '%{keyword}%' OR fixed_asset_name LIKE '%{keyword}%'");
                parameters.Add("v_And1", $"department_id LIKE '%{departmentID}%'");
                parameters.Add("v_And2", $"fixed_asset_category_id LIKE '%{assetCategoryID}%'");
                parameters.Add("v_Limit", limit);
                parameters.Add("v_Offset", offset);
                parameters.Add("v_Sort", "");

                // Thực hiện gọi vào DB để chạy procedure
                var multiAssets = mysqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý dữ liệu trả về
                if (multiAssets != null)
                {
                    var assets = multiAssets.Read<Asset>();
                    var totalCount = multiAssets.Read<int>().Single();
                    return StatusCode(StatusCodes.Status200OK, new PagingData<Asset>()
                    {
                        Data = assets.ToList(),
                        TotalCount = totalCount,
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        QLTSErrorCode.FilterFailed,
                        Resource.DevMsg_FilterFailed,
                        Resource.UserMsg_FilterFailed,
                        Resource.MoreInfo_FilterFailed,
                        HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    QLTSErrorCode.Exeption,
                    Resource.DevMsg_Exeption,
                    Resource.UserMsg_Exeption,
                    Resource.MoreInfo_Exeption,
                    HttpContext.TraceIdentifier));
            }

        }
        #endregion

        #region API POST

        /// <summary>
        /// Thêm mới 1 tài sản
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>ID của tài sản vừa được thêm mới</returns>
        /// Create By: DVDUNG (17/09/2022)
        [HttpPost]
        public IActionResult InsertAsset([FromBody] Asset asset)
        {
            try
            {
                // Khởi tạo kết nối tói DB MySQL
                string connectionString = "Server=localhost;Port=3307;Database=misa.web08.hcsn.dvdung;Uid=root;Pwd=dungday123@;";
                var mysqlConnetion = new MySqlConnection(connectionString);

                // Khai báo tên procedure Insert
                var storedProcedureName = "Proc_asset_InsertOne";

            // Chuẩn bị tham số đầu vào
                var assetID = Guid.NewGuid();
                var parameters = new DynamicParameters();
                parameters.Add("d_fixed_asset_id", assetID);
                parameters.Add("d_fixed_asset_code", asset.fixed_asset_code);
                parameters.Add("d_fixed_asset_name", asset.fixed_asset_name);
                parameters.Add("d_department_id", asset.department_id);
                parameters.Add("d_department_code", asset.department_code);
                parameters.Add("d_department_name", asset.department_name);
                parameters.Add("d_fixed_asset_category_id", asset.fixed_asset_category_id);
                parameters.Add("d_fixed_asset_category_code", asset.fixed_asset_category_code);
                parameters.Add("d_fixed_asset_category_name", asset.fixed_asset_category_name);
                parameters.Add("d_quantity", asset.quantity);
                parameters.Add("d_cost", asset.cost);
                parameters.Add("d_deprecization_rate", asset.depreciation_rate);
                parameters.Add("d_purchase_date", DateTime.Now);
                parameters.Add("d_production_year", asset.production_year);
                parameters.Add("d_tracked_year", asset.tracked_year);
                parameters.Add("d_life_time", asset.life_time);
                parameters.Add("d_create_by", "Đặng Văn Dũng");
                parameters.Add("d_create_date", DateTime.Now);
                parameters.Add("d_modified_by","Đặng Văn Dũng");
                parameters.Add("d_modified_date", DateTime.Now);

                // Thực hiện gọi vào DB để chạy procedure
                var numberOfAffectedRows = mysqlConnetion.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                
                // Xử lý dữ liệu trả về
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, assetID); // 
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        QLTSErrorCode.InsertFailed,
                        Resource.DevMsg_InsertFailed,
                        Resource.UserMsg_InsertFailed,
                        Resource.UserMsg_InsertFailed,
                        HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    QLTSErrorCode.Exeption,
                    Resource.DevMsg_Exeption,
                    Resource.UserMsg_Exeption,
                    Resource.MoreInfo_Exeption,
                    HttpContext.TraceIdentifier));
            }
        }

        #endregion

        #region API PUT
        /// <summary>
        /// Sửa thông tin tài sản
        /// </summary>
        /// <param name="assetID"></param>
        /// <param name="asset"></param>
        /// <returns>ID của tài sản đươc chỉnh sửa</returns>
        /// Create By: DVDUNG (17/09/2022)
        [HttpPut("{assetID}")]
        public IActionResult UpdateAsset([FromRoute] Guid assetID, [FromBody] Asset asset)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                string connectionString = "Server=localhost;Port=3307;Database=misa.web08.hcsn.dvdung;Uid=root;Pwd=dungday123@;";
                var mysqlConnetion = new MySqlConnection(connectionString);

                // Khai báo tên procedure Insert
                var storedProcedureName = "Proc_asset_UpdateAsset";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("d_fixed_asset_id", assetID);
                parameters.Add("d_fixed_asset_code", asset.fixed_asset_code);
                parameters.Add("d_fixed_asset_name", asset.fixed_asset_name);
                parameters.Add("d_department_id", asset.department_id);
                parameters.Add("d_department_code", asset.department_code);
                parameters.Add("d_department_name", asset.department_name);
                parameters.Add("d_fixed_asset_category_id", asset.fixed_asset_category_id);
                parameters.Add("d_fixed_asset_category_code", asset.fixed_asset_category_code);
                parameters.Add("d_fixed_asset_category_name", asset.fixed_asset_category_name);
                parameters.Add("d_quantity", asset.quantity);
                parameters.Add("d_cost", asset.cost);
                parameters.Add("d_deprecization_rate", asset.depreciation_rate);
                parameters.Add("d_purchase_date", DateTime.Now);
                parameters.Add("d_production_year", asset.production_year);
                parameters.Add("d_tracked_year", asset.tracked_year);
                parameters.Add("d_life_time", asset.life_time);
                parameters.Add("d_modified_by", "Đặng Văn Dũng");
                parameters.Add("d_modified_date", DateTime.Now);


                // Thực hiện gọi vào DB để chạy Proc
                var updateAsset = mysqlConnetion.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (updateAsset > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, assetID); // 
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                    QLTSErrorCode.UpdateFailed,
                    Resource.DevMsg_UpdateFailed,
                    Resource.UserMsg_UpdateFailed,
                    Resource.MoreInfo_UpdateFailed,
                    HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    QLTSErrorCode.Exeption,
                    Resource.DevMsg_Exeption,
                    Resource.UserMsg_Exeption,
                    Resource.MoreInfo_Exeption,
                    HttpContext.TraceIdentifier));
            }
            
        }
        #endregion

        #region API DELETE
        /// <summary>
        /// Xóa 1 tài sản
        /// </summary>
        /// <param name="assetID"></param>
        /// <param name="asset"></param>
        /// <returns>ID của tài sản bị xóa</returns>
        /// Create By: DVDUNG (17/09/2022)
        [HttpDelete("{assetID}")]
        public IActionResult DeleteAsset([FromRoute] Guid assetID)
        {
            try
            {
                // Khởi tạo kết nối tói DB MySQL
                string connectionString = "Server=localhost;Port=3307;Database=misa.web08.hcsn.dvdung;Uid=root;Pwd=dungday123@;";
                var mysqlConnection = new MySqlConnection(connectionString);

                // Khai báo tên procedure DeleteAsset
                var storedProcedureName = "Proc_asset_DeleteAsset";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("d_fixed_asset_id", assetID);

                // Thực hiện gọi vào DB để chạy proc
                var deleteAsset = mysqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (deleteAsset > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, assetID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        QLTSErrorCode.Exeption,
                        Resource.DevMsg_DeleteAssetFailed,
                        Resource.UserMsg_DeleteAssetFailed,
                        Resource.MoreInfo_DeleteAssetFailed,
                        HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    QLTSErrorCode.Exeption,
                    Resource.DevMsg_Exeption,
                    Resource.UserMsg_Exeption,
                    Resource.MoreInfo_Exeption,
                    HttpContext.TraceIdentifier));
            }
        }

        /// <summary>
        /// Xóa nhiều tài sản cùng lúc
        /// </summary>
        /// <param name="assetIDs"></param>
        /// <returns></returns>
        /// Create By: DVDUNG (17/09/2022)
        [HttpPost("batch-delete")]
        public IActionResult DeleteMultipleAssets([FromBody] List<string> assetIDs)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                string connectionString = "Server=localhost;Port=3307;Database=misa.web08.hcsn.dvdung;Uid=root;Pwd=dungday123@;";
                var mysqlConnection = new MySqlConnection(connectionString);

                // Khai báo tên prodecure BatchDelete
                string storedProcedureName = "Proc_asset_BatchDelete";

                // Chuẩn bị tham số đầu vào cho procedure
                var parameters = new DynamicParameters();
                var queryList = new List<string>();
                for (int i = 0; i < assetIDs.Count; i++)
                {
                    queryList.Add($"fixed_asset_id= \'{assetIDs[i]}\'");
                }
                string assetIDList = string.Join(" OR ", queryList);
                parameters.Add("d_fixed_asset_id_list", assetIDList);

                // Xử lý dữ liệu trả về
                var numberOfAffectedRows = mysqlConnection.Execute(storedProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý dữ liệu trả về
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, assetIDs);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        QLTSErrorCode.BatchDeleteFailed,
                        Resource.DevMsg_BatchDeleteFailed,
                        Resource.UserMsg_BatchDeleteFailed,
                        Resource.MoreInfo_BatchDeleteFailed,
                        HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    QLTSErrorCode.Exeption,
                    Resource.DevMsg_Exeption,
                    Resource.UserMsg_Exeption,
                    Resource.MoreInfo_Exeption,
                    HttpContext.TraceIdentifier));
            }
        } 
        #endregion

    }
}
