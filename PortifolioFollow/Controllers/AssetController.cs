﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortifolioFollow.Domain;
using PortifolioFollow.Service.Repositories;

namespace PortifolioFollow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly AssetRepository _assetRepository;

        public AssetController()
        {
            _assetRepository = new AssetRepository();
        }

        [HttpGet("{symbol}")]
        public ActionResult<string> Get(string symbol)
        {
            return JsonConvert.SerializeObject(_assetRepository.FindBySymbol(symbol));
        }

        [HttpPost]
        public void Post([FromBody] Asset asset)
        {
            _assetRepository.Insert(asset);
        }
    }
}