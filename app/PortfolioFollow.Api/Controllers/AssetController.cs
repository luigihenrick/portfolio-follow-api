using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortfolioFollow.Common.Interfaces;
using PortfolioFollow.Domain;
using PortfolioFollow.Service.Commons;

namespace PortfolioFollow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetBusiness _assetBusiness;
        private readonly IOptions<GlobalVariables> _config;

        public AssetController(IAssetBusiness assetBusiness, IOptions<GlobalVariables> config)
        {
            _assetBusiness = assetBusiness;
            _config = config;
        }

        [HttpGet()]
        public ActionResult<string> GetAll()
        {
            using (var stream = typeof(AssetController).GetTypeInfo().Assembly.GetManifestResourceStream("PortfolioFollow.Api.Content.Assets.csv"))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            using (var csv = new CsvReader(reader))
            {
                var assets = csv.GetRecords<Asset>();

                return JsonConvert.SerializeObject(assets);
            }
        }

        [HttpGet("{symbol}")]
        public ActionResult<string> Get(string symbol)
        {
            return JsonConvert.SerializeObject(_assetBusiness.FindBySymbol(symbol));
        }

        [HttpPost]
        public Asset Insert([FromBody] Asset asset)
        {
            return _assetBusiness.Insert(asset);
        }

        [HttpPost("import")]
        public void Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new InvalidOperationException("File not selected");
            
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Seek(0, SeekOrigin.Begin);
                _assetBusiness.Import(stream);
            }
        }
    }
}
