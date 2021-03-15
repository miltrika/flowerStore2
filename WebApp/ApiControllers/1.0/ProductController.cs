using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Product controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ProductMapper _mapper = new();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uow"></param>
        public ProductController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }
        
        /// <summary>
        /// Get list of all products in paginated manner
        /// </summary>
        /// <param name="pageNumber">Insert number of page wanted</param>
        /// <param name="pageSize">Specify how many products will be displayed on page</param>
        /// <returns>List of products</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<ActionResult<IEnumerable<Product>>> GetPaginatedProducts(int pageNumber, int pageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest(new MessageDTO("pageNumber and pageSize must have a positive number (starting from 1)"));
            }
            var products = (await _uow.Products.GetAllPaginatedAsync(pageNumber, pageSize)).ToArray();
            if (!products.Any())
            {
                return NotFound(new MessageDTO("No products found"));
            }

            return Ok(products.Select(p => _mapper.Map(p)));
        }
        
        /// <summary>
        /// Get other products that have been purchased alongside the given product
        /// </summary>
        /// <param name="id">Insert chosen product id</param>
        /// <returns>List of products that are ordered by popularity</returns>
        [HttpGet("related/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<IEnumerable<Product>>> GetRelatedProducts(Guid id)
        {
            var products = (await _uow.Products.GetAllRelatedProductsByIdAndPopularity(id)).ToArray();
            if (!products.Any())
            {
                return NotFound(new MessageDTO("No products found"));
            }

            return Ok(products.Select(p => _mapper.Map(p)));
        }
    }
}