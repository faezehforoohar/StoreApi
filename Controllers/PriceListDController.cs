using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using StoreApi.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using StoreApi.Services;
using StoreApi.Entities;
using StoreApi.Models.PriceListD;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceListDController : ControllerBase
    {
        private IPriceListDService _priceListDService;
        private IUserService _userService;
        private IMapper _mapper;
        private long _userId;


        public PriceListDController(IUserService userService,
    IPriceListDService priceListDService,
    IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _priceListDService = priceListDService;
            _mapper = mapper;
            _userService = userService;
            _userId = _userService.GetFirst().Id;
            //_userId = (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null ? long.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value) : 0);
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var priceListDs = await _priceListDService.GetAll();
                var data = _mapper.Map<IList<PriceListDModel>>(priceListDs);

                return Ok(new { data, result = true });
            }
            catch (Exception ex)
            {
                return Ok(new { message = "Error in fetching data :" + ex.Message, result = false });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var priceListD = await _priceListDService.GetById(id);
                var data = _mapper.Map<PriceListDModel>(priceListD);
                return Ok(new { data, result = true });
            }
            catch (Exception ex)
            {
                return Ok(new { message = "Error in fetching data :" + ex.Message, result = false });
            }
        }

        [HttpGet("getByPriceListId/{id}")]
        public async Task<ActionResult> GetByPriceListId(int id)
        {
            try
            {
                var priceListDs = await _priceListDService.GetAllByPriceListId(id);
                var data = _mapper.Map<IList<PriceListDModel>>(priceListDs);

                return Ok(new { data, result = true });
            }
            catch (Exception ex)
            {
                return Ok(new { message = "Error in fetching data :" + ex.Message, result = false });
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] PriceListDCreate model)
        {
            try
            {
                // map model to entity
                if (_userId == 0)
                    throw new Exception("Authentication failed.");
                var priceListD = _mapper.Map<PriceListD>(model);

                var result = await _priceListDService.Create(priceListD, _userId);
                var data = _mapper.Map<PriceListDModel>(priceListD);
                return Ok(new { data, result = true });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return Ok(new { message = "Error in creating data :" + ex.Message, result = false });
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] PriceListDUpdate model)
        {
            // map model to entity
            if (_userId == 0)
                throw new Exception("Authentication failed.");
            var priceListD = _mapper.Map<PriceListD>(model);

            try
            {
                // update priceListD 
                await _priceListDService.Update(priceListD, _userId);
                var data = _mapper.Map<PriceListDModel>(priceListD);

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
                // update priceListD 
                await _priceListDService.Delete(id);

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
