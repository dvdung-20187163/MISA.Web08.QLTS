using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MISA.Web08.QLTS.API.Entities;
using MISA.Web08.QLTS.API.Entities.DTO;

namespace MISA.Web08.QLTS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        [HttpGet] // attribute
        [Route("12")] // nếu không có route này thì sẽ được set mặc định là trống
        public void Greeting()
        {
            Console.WriteLine("xin chao");
        }

        /// <summary>
        /// API Lấy danh sách toàn bộ tài sản
        /// </summary>
        /// <returns>Danh sách toàn bộ tài sản</returns>
        /// Create By: DVDUNG (17/09/2022)
        [HttpGet]
        [Route("")]
        public List<Asset> GetAllAssets()
        {
            return new List<Asset>
            {
                new Asset
                {
                    AssetID = Guid.NewGuid(),
                    AssetCode = "TS00001",
                    AssetName = "Máy sấy tóc",
                    DepartmentID = Guid.NewGuid(),
                    DepartmentCode = "KHO",
                    DeoartmentName = "Phòng kho",
                    AssetCategoryID = Guid.NewGuid(),
                    AssetCategoryCode = "201",
                    AssetCategoryName = "Vật dụng cá nhân",
                    Quantity = 3,
                    Cost = 1000000,
                },
                new Asset
                {
                    AssetID = Guid.NewGuid(),
                    AssetCode = "TS00002",
                    AssetName = "Máy in",
                    DepartmentID = Guid.NewGuid(),
                    DepartmentCode = "PDT",
                    DeoartmentName = "Phòng đào tạo",
                    AssetCategoryID = Guid.NewGuid(),
                    AssetCategoryCode = "304",
                    AssetCategoryName = "Tài sản chuyên dụng",
                    Quantity = 1,
                    Cost = 5000000,
                },
            };
        }

        /// <summary>
        /// API lấy thông tin tài sản theo ID
        /// </summary>
        /// <param name="assetID"></param>
        /// <returns>Thông tin tài sản cần tìm</returns>
        /// Create By: DVDUNG (17/09/2022)
        [HttpGet]
        [Route("{assetID}")]
        
        public Asset GetAssetID(Guid assetID)
        {
            return new Asset
            {
                AssetID = Guid.NewGuid(),
                AssetCode = "TS00001",
                AssetName = "Máy sấy tóc",
                DepartmentID = Guid.NewGuid(),
                DepartmentCode = "KHO",
                DeoartmentName = "Phòng kho",
                AssetCategoryID = Guid.NewGuid(),
                AssetCategoryCode = "201",
                AssetCategoryName = "Vật dụng cá nhân",
                Quantity = 3,
                Cost = 1000000,
            };
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
        public PagingData FilterAsset(
            [FromQuery] string? keyword,
            [FromQuery] string? assetCategoryID,
            [FromQuery] string? departmentID,
            [FromQuery] int? limit,
            [FromQuery] int? offset)
        {
            return new PagingData
            {
                Data = new List<Asset>
                {
                    new Asset
                    {
                        AssetID = Guid.NewGuid(),
                        AssetCode = "TS00001",
                        AssetName = "Máy sấy tóc",
                        DepartmentID = Guid.NewGuid(),
                        DepartmentCode = "KHO",
                        DeoartmentName = "Phòng kho",
                        AssetCategoryID = Guid.NewGuid(),
                        AssetCategoryCode = "201",
                        AssetCategoryName = "Vật dụng cá nhân",
                        Quantity = 3,
                        Cost = 1000000,
                    },
                    new Asset
                    {
                        AssetID = Guid.NewGuid(),
                        AssetCode = "TS00002",
                        AssetName = "Máy in",
                        DepartmentID = Guid.NewGuid(),
                        DepartmentCode = "PDT",
                        DeoartmentName = "Phòng đào tạo",
                        AssetCategoryID = Guid.NewGuid(),
                        AssetCategoryCode = "304",
                        AssetCategoryName = "Tài sản chuyên dụng",
                        Quantity = 1,
                        Cost = 5000000,
                    },
                }
            };
        }

        /// <summary>
        /// Thêm mới 1 tài sản
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>ID của tài sản vừa được thêm mới</returns>
        /// Create By: DVDUNG (17/09/2022)
        [HttpPost]
        public IActionResult InsertAsset([FromBody] Asset asset)
        {
            return StatusCode(StatusCodes.Status201Created, Guid.NewGuid());
        }

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
            return StatusCode(StatusCodes.Status200OK, assetID);
        }

        /// <summary>
        /// Xóa 1 tài sản
        /// </summary>
        /// <param name="assetID"></param>
        /// <param name="asset"></param>
        /// <returns>ID của tài sản bị xóa</returns>
        /// Create By: DVDUNG (17/09/2022)
        [HttpDelete("{assetID}")]
        public IActionResult DeleteAsset([FromQuery] Guid assetID, [FromBody] Asset asset)
        {
            return StatusCode(StatusCodes.Status200OK, assetID);
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
            return StatusCode(StatusCodes.Status200OK);
        }
  
    }
}
