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
    [ApiController]
    [Route("[controller]")]
    public class PriceListController : ControllerBase
    {
        private IPriceListService _priceListService;
        private IUserService _userService;
        private IMapper _mapper;
        private long _userId;

        public PriceListController(IUserService userService,
            IPriceListService priceListService,
            IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _priceListService = priceListService;
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
                var priceLists = await _priceListService.GetAll();
                var data = _mapper.Map<List<PriceListModel>>(priceLists);
                int i = 1;

                data.ForEach(m => m.Row = i++);
                return Ok(new Result<List<PriceListModel>>(data, true, SuccessType.Fetch.ToDescription(), new Error()));

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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var priceList = await _priceListService.GetById(id);
                var data = _mapper.Map<PriceListModel>(priceList);
                return Ok(new Result<PriceListModel>(data, true, SuccessType.Fetch.ToDescription(), new Error()));

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
        public async Task<ActionResult> Create([FromBody] PriceListCreate model)
        {
            try
            {
                // map model to entity
                if (_userId == 0)
                    throw new Exception("Authentication failed.");

                var priceList = _mapper.Map<PriceList>(model);
                priceList.UserId = _userId;

                await _priceListService.Create(priceList);
                var data = _mapper.Map<PriceListModel>(priceList);
                return Ok(new Result<PriceListModel>(data, true, SuccessType.Create.ToDescription(), new Error()));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
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
        public async Task<ActionResult> Update([FromBody] PriceListUpdate model)
        {
            // map model to entity
            if (_userId == 0)
                throw new Exception("Authentication failed.");
            var priceList = _mapper.Map<PriceList>(model);
            priceList.UserId = _userId;
            try
            {
                // update priceList 
                await _priceListService.Update(priceList);
                var data = _mapper.Map<PriceListModel>(priceList);

                return Ok(new Result<PriceListModel>(data, true, SuccessType.Update.ToDescription(), new Error()));
            }
            catch (AppException ex)
            {
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
                // update priceList 
                await _priceListService.Delete(id);

                return Ok(new Result<string>(true, SuccessType.Delete.ToDescription(), new Error()));
            }
            catch (AppException ex)
            {
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
