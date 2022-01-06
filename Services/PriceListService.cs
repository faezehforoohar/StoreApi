using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreApi.Entities;
using StoreApi.Helpers;
using StoreApi.Models.PriceList;

namespace StoreApi.Services
{
    public interface IPriceListService
    {
        Task<List<PriceList>> GetAll();
        Task<PriceList> GetById(long id);
        Task<int> Create(PriceList priceList);
        Task<int> Update(PriceList priceList);
        Task<int> Delete(long id);
    }

    public class PriceListService : IPriceListService
    {
        private DataContext _context;
        private IMapper _mapper;

        public PriceListService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PriceList> GetById(long id)
        {
            return await _context.PriceLists.FindAsync(id);
        }

        public async Task<List<PriceList>> GetAll()
        {
            return await _context.PriceLists.ToListAsync();
        }

        public async Task<int> Create(PriceList priceList)
        {

            if (string.IsNullOrWhiteSpace(priceList.Title))
                throw new AppException("Title is required.");
            if (priceList.DateTime == null)
                throw new AppException("DateTime is required.");
            priceList.CreatorUserId = priceList.UserId;
            priceList.CreationTime = DateTime.Now;

            _context.PriceLists.Add(priceList);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(PriceList priceListParam)
        {
            var priceList = _context.PriceLists.Find(priceListParam.Id);

            if (priceList == null)
                throw new AppException("PriceList not found");

            // update priceListname if it has changed
            if (string.IsNullOrWhiteSpace(priceListParam.Title))
                throw new AppException("Title is required.");
            if (priceListParam.DateTime == null)
                throw new AppException("DateTime is required.");
            priceList.Title = priceListParam.Title;
            priceList.DateTime = priceListParam.DateTime;
            priceList.IsActive = priceListParam.IsActive;
            priceList.Description = priceListParam.Description;

            priceList.LastModifierUserId = priceList.UserId;
            priceList.LastModificationTime = DateTime.Now;
            _context.PriceLists.Update(priceList);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(long id)
        {
            var priceList = _context.PriceLists.Find(id);

            if (priceList == null)
                throw new AppException("Invalid PriceList.");
            if (_context.PriceListDs.Count(m => m.PriceListId == id) > 0)
                throw new AppException("First Delete PriceList Details.");

            _context.PriceLists.Remove(priceList);
            return await _context.SaveChangesAsync();
        }

    }
}