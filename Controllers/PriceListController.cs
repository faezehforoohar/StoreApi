using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using StoreApi.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using StoreApi.Services;
using StoreApi.Entities;
using StoreApi.Models.PriceList;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PriceListController : ControllerBase
    {
        private IPriceListService _priceListService;
        private IMapper _mapper;
        private long _userId;

        public PriceListController(
            IPriceListService priceListService,
            IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _priceListService = priceListService;
            _mapper = mapper;
            _userId = (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null ? long.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value) : 0);
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var priceLists = await _priceListService.GetAll();
                var data = _mapper.Map<IList<PriceListModel>>(priceLists);

                return Ok(new { data, result = true });
            }
            catch(Exception ex)
            {
                return Ok(new { message = "Error in fetching data :" + ex.Message, result = false });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var priceList = await _priceListService.GetById(id);
                var data = _mapper.Map<PriceListModel>(priceList);
                return Ok(new { data, result = true });
            }
            catch (Exception ex)
            {
                return Ok(new { message = "Error in fetching data :" + ex.Message, result = false });
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] PriceListSave model)
        {
            try
            {
                // map model to entity
                if (_userId == 0)
                    throw new Exception("Authentication failed.");
                model.UserId = _userId;
                var priceList = _mapper.Map<PriceList>(model);

                await _priceListService.Create(priceList);
                var data = _mapper.Map<PriceListModel>(priceList);
                return Ok(new { data, result = true });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return Ok(new { message = "Error in creating data :" + ex.Message, result = false });
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] PriceListSave model)
        {
            // map model to entity
            if (_userId == 0)
                throw new Exception("Authentication failed.");
            model.UserId = _userId;
            var priceList = _mapper.Map<PriceList>(model);

            try
            {
                // update priceList 
                await _priceListService.Update(priceList);
                var data = _mapper.Map<PriceListModel>(priceList);

                return Ok(new { data, result = true });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return Ok(new { message = "Error in updating data :" + ex.Message, result = false });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (_userId == 0)
                throw new Exception("Authentication failed.");
            try
            {
                // update priceList 
                await _priceListService.Delete(id);

                return Ok(new { result = true });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return Ok(new { message = "Error in updating data :" + ex.Message, result = false });
            }
        }

    }
}
