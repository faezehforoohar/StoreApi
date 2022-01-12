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
using System.ComponentModel;

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
                var data = _mapper.Map<List<PriceListDModel>>(priceListDs);
                int i = 1;

                data.ForEach(m => m.Row = i++);
                return Ok(new Result<List<PriceListDModel>>(data, true, SuccessType.Fetch.ToDescription(), new Error()));
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    message = "Error in fetching data :" + ex.Message,
                    success = false,
                    error = new Error()
                    {
                        code = 1,
                        data = new List<string>()
                    }
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var priceListD = await _priceListDService.GetById(id);
                var data = _mapper.Map<PriceListDModel>(priceListD);
                //return Ok(new { data = data, success = true });
                return Ok(new Result<PriceListDModel>(data, true, SuccessType.Fetch.ToDescription(), new Error()));

            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    message = ErrorType.Fetch.ToDescription() + ":" + ex.Message,
                    success = false,
                    error = new Error()
                    {
                        code = 1,
                        data = new List<string>()
                    }
                });
                //return Ok(new { message = "Error in fetching data :" + ex.Message, success = false, error = "" });
            }
        }

        [HttpGet("getByPriceListId/{id}")]
        public async Task<ActionResult> GetByPriceListId(int id)
        {
            try
            {
                var priceListDs = await _priceListDService.GetAllByPriceListId(id);
                var data = _mapper.Map<List<PriceListDModel>>(priceListDs);

                return Ok(new Result<List<PriceListDModel>>(data, true, SuccessType.Fetch.ToDescription(), new Error()));
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    message = ErrorType.Fetch.ToDescription() + ":" + ex.Message,
                    success = false,
                    error = new Error()
                    {
                        code = 1,
                        data = new List<string>()
                    }
                });
                // return Ok(new { message = "Error in fetching data :" + ex.Message, success = false, error = "" });
            }
        }

        [HttpGet("getColors")]
        public async Task<ActionResult> GetColors()
        {
            try
            {
                List<Option> data = new List<Option>();

                data.Add(new Option() { Value = Color.Black.ToID(), Title = Color.Black.ToDescription() });
                data.Add(new Option() { Value = Color.Blue.ToID(), Title = Color.Blue.ToDescription() });
                data.Add(new Option() { Value = Color.BlueBlack.ToID(), Title = Color.BlueBlack.ToDescription() });
                data.Add(new Option() { Value = Color.White.ToID(), Title = Color.White.ToDescription() });

                return Ok(new Result<List<Option>>(data, true, SuccessType.Fetch.ToDescription(), new Error()));
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    message = ErrorType.Fetch.ToDescription() + ":" + ex.Message,
                    success = false,
                    error = new Error()
                    {
                        code = 1,
                        data = new List<string>()
                    }
                });
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
                return Ok(new Result<PriceListDModel>(data, true, SuccessType.Create.ToDescription(), new Error()));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                //return Ok(new { message = "Error in creating data :" + ex.Message, success = false, error = "" });
                return Ok(new
                {
                    message = ErrorType.Create.ToDescription() + ":" + ex.Message,
                    success = false,
                    error = new Error()
                    {
                        code = 1,
                        data = new List<string>()
                    }
                });
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

                return Ok(new Result<PriceListDModel>(data, true, SuccessType.Update.ToDescription(), new Error()));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                //return Ok(new { message = "Error in updating data :" + ex.Message, success = false, error = "" });
                return Ok(new
                {
                    message = ErrorType.Update.ToDescription() + ":" + ex.Message,
                    success = false,
                    error = new Error()
                    {
                        code = 1,
                        data = new List<string>()
                    }
                });
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

                //return Ok(new { success = true, message = "Delete successfully", error = "" });
                return Ok(new Result<string>(true, SuccessType.Delete.ToDescription(), new Error()));

            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                //return Ok(new { message = "Error in updating data :" + ex.Message, success = false, error = "" });
                return Ok(new
                {
                    message = ErrorType.Delete.ToDescription() + ":" + ex.Message,
                    success = false,
                    error = new Error()
                    {
                        code = 1,
                        data = new List<string>()
                    }
                });
            }
        }


    }
}
