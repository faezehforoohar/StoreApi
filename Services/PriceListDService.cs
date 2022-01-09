using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreApi.Entities;
using StoreApi.Helpers;

namespace StoreApi.Services
{
    public interface IPriceListDService
    {
        Task<List<PriceListD>> GetAll();
        Task<List<PriceListD>> GetAllByPriceListId(long priceListId);
        Task<PriceListD> GetById(long id);
        Task<int> Create(PriceListD priceListD, long userId);
        Task<int> Update(PriceListD priceListD, long userId);
        Task<int> Delete(long id);
    }

    public class PriceListDService : IPriceListDService
    {
        private DataContext _context;
        private IMapper _mapper;

        public PriceListDService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PriceListD> GetById(long id)
        {
            return await _context.PriceListDs.Include(m=>m.PriceList).FirstOrDefaultAsync(m=>m.Id==id);
        }

        public async Task<List<PriceListD>> GetAll()
        {
            return await _context.PriceListDs.Include(m => m.PriceList).ToListAsync();
        }

        public async Task<List<PriceListD>> GetAllByPriceListId(long priceListId)
        {
            return await _context.PriceListDs.Include(m => m.PriceList).Where(m=>m.PriceListId==priceListId).ToListAsync();
        }

        public async Task<int> Create(PriceListD priceListD, long userId)
        {
            if (string.IsNullOrWhiteSpace(priceListD.Title))
                throw new AppException("Title is required.");
            if (priceListD.PriceListId == 0)
                throw new AppException("PriceListId is required.");
            if(_context.PriceLists.FirstOrDefault(m=>m.Id==priceListD.PriceListId)==null)
                throw new AppException("PriceListId is invalid.");
            priceListD.CreatorUserId = userId;
            priceListD.CreationTime = DateTime.Now;

            _context.PriceListDs.Add(priceListD);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(PriceListD priceListDParam,long userId)
        {
            var priceListD = _context.PriceListDs.FirstOrDefault(m=>m.Id==priceListDParam.Id);

            if (priceListD == null)
                throw new AppException("PriceListD not found");

            // update priceListDname if it has changed
            if (string.IsNullOrWhiteSpace(priceListDParam.Title))
                throw new AppException("Title is required.");
            if (priceListDParam.PriceListId ==0)
                throw new AppException("PriceListId is required.");
            if (_context.PriceLists.FirstOrDefault(m => m.Id == priceListDParam.PriceListId) == null)
                throw new AppException("PriceListId is invalid.");
            priceListD.Title = priceListDParam.Title;
            priceListD.PriceListId = priceListDParam.PriceListId;
            priceListD.Price = priceListDParam.PriceListId;
            priceListD.PortNumber = priceListDParam.PortNumber;
            priceListD.Color = priceListDParam.Color;
            priceListD.HasGuarantee = priceListDParam.HasGuarantee;
            priceListD.YearModel = priceListDParam.YearModel;
            priceListD.Description = priceListDParam.Description;

            priceListD.LastModifierUserId = userId;
            priceListD.LastModificationTime = DateTime.Now;
            _context.PriceListDs.Update(priceListD);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(long id)
        {
            var priceListD = _context.PriceListDs.FirstOrDefault(m=>m.Id==id);

            if (priceListD == null)
                throw new AppException("Invalid PriceListD.");
            _context.PriceListDs.Remove(priceListD);
            return await _context.SaveChangesAsync();
        }

    }
}