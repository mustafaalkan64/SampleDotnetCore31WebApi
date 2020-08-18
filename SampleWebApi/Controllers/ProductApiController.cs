using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Business.Abstract;
using SampleWebApi.Models.Dtos;

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private IProductService _productService;
        private IMapper _mapper;
        public ProductApiController(IProductService productService,
            IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        // GET: api/productList
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var productList = await _productService.GetAsync();
                if (productList == null)
                    return NotFound();

                return Ok(productList);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        // GET: api/productList/5
        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // POST: api/productList
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductDto productModel)
        {
            try
            {
                var productId = await _productService.AddAsync(productModel);
                if (productId > 0)
                    return Ok(productId);
                else
                    return BadRequest("An Error Occured While Creating New product");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/productList/5
        [HttpPut]
        public async Task<IActionResult> Put(int? id, [FromBody]ProductDto productModel)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var result = await _productService.UpdateAsync(id, productModel);
                if (result.Status)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var result = await _productService.DeleteAsync(id);
                if (result.Status)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
