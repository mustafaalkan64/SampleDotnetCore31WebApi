using AutoMapper;
using Microsoft.Extensions.Logging;
using PenaltyCalculationApp.Uow;
using SampleWebApi.Business.Abstract;
using SampleWebApi.Entities;
using SampleWebApi.Models;
using SampleWebApi.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SampleWebApi.Business.Services
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<ProductManager> _logger;
        private readonly IMapper _mapper;

        public ProductManager(IUnitOfWork unit,
            ILogger<ProductManager> logger,
            IMapper mapper)
        {
            _uow = unit;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// // Create Movie
        /// </summary>
        /// <param name="movie">Movie Model Parameter</param>
        /// <returns></returns>
        public async Task<int> AddAsync(ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                await _uow.ProductRepository.AddAsync(product);
                await _uow.Commit();
                return product.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception Error During Add Product", productDto);
                throw e;
            }

        }


        public async Task<WebApiResponse> UpdateAsync(int? productId, ProductDto productDto)
        {
            try
            {
                var product = await _uow.ProductRepository.GetByIdAsync(productId);
                if (product != null)
                {

                    product.Name = productDto.Name;
                    product.Price = productDto.Price;
                    product.Summary = productDto.Summary;
                    product.Thumbnail = productDto.Thumbnail;

                    await _uow.ProductRepository.UpdateAsync(product);
                    await _uow.Commit();
                    
                    return new WebApiResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = true
                    };
                }
                else
                {
                    return new WebApiResponse()
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Status = false
                    };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception Error During Update Product", productDto);
                throw e;
            }
        }

        public async Task<WebApiResponse> DeleteAsync(int? todoId)
        {
            try
            {
                var todo = await _uow.ProductRepository.FindByAsync(a => a.Id == todoId);
                if (todo != null)
                {
                    await _uow.ProductRepository.DeleteAsync(todo);
                    await _uow.Commit();
                    return new WebApiResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = true
                    };
                }
                else
                {
                    return new WebApiResponse()
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Status = false
                    };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception Error During Delete Todo", todoId);
                throw e;
            }
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            try
            {
                return await _uow.ProductRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        // Todo Get By Id Method
        public async Task<Product> GetByIdAsync(int? productId)
        {
            try
            {
                var result = await _uow.ProductRepository.GetByIdAsync(productId);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
