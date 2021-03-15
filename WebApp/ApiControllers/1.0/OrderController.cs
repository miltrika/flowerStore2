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
    /// Order controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly OrderMapper _mapper = new();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uow"></param>
        public OrderController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }
        
        /// <summary>
        /// Get list of all orders in paginated manner
        /// </summary>
        /// <param name="pageNumber">Insert number of page wanted</param>
        /// <param name="pageSize">Specify how many orders will be displayed on page</param>
        /// <returns>List of orders</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Order>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<ActionResult<IEnumerable<Order>>> GetPaginatedOrders(int pageNumber, int pageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest(new MessageDTO("pageNumber and pageSize must have a positive number (starting from 1)"));
            }
            var orders = (await _uow.Orders.GetAllPaginatedAsync(pageNumber, pageSize)).ToArray();
            if (!orders.Any())
            {
                return NotFound(new MessageDTO("No orders found"));
            }

            return Ok(orders.Select(p => _mapper.Map(p)));
        }
    }
}