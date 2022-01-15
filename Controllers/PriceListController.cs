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
using static StoreApi.Helpers.Enums;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
//using Twilio.Rest.Api.V2010.Account;
//using Twilio.Types;

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
        [HttpGet("getAll/{sort}")]
        public async Task<ActionResult> GetAll(string sort = "asc")
        {
            try
            {
                ///
                // Find your Account SID and Auth Token at twilio.com/console
                 //and set the environment variables. See http://twil.io/secure
                string accountSid = Environment.GetEnvironmentVariable("AC277cf3d34498f7d6bea3f979ac8a659c");
                string authToken = Environment.GetEnvironmentVariable("a722c399ecad0ec0fc69b2a394ee51ef");

                TwilioClient.Init(accountSid, authToken);
               // TwilioClient.Init("AC8d721852ccbcf690587c6d3ef35eb34b", "d1604bd9238dd7b225539ab66ee044ff");


                var message = MessageResource.Create(
                               from: new PhoneNumber("whatsapp:+989355242297"),
                               to: new PhoneNumber("whatsapp:+989011312651"),
                               body: "Ahoy from Twilio!"
                           );



                var priceLists = await _priceListService.GetAll(sort);
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

        [HttpPost("sendMessage")]
        public async Task<ActionResult> SendMessage(long priceListId)
        {
            string error = string.Empty;

            //if (string.IsNullOrEmpty(date))
            //    throw new Exception("Authentication failed.");
            //if (!date.CheckDate(ref error))
            //    throw new Exception(error);
            try
            {
                // update priceListD 
                await _priceListService.SendMessage(priceListId);

                return Ok(new Result<string>(true, SuccessType.SendingMessage.ToDescription(), new Error()));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                //return Ok(new { message = "Error in updating data :" + ex.Message, success = false, error = "" });
                return Ok(new
                {
                    message = ErrorType.SendingMessage.ToDescription() + ":" + ex.Message,
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
