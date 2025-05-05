using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.BL;
using Dto;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/v1/orders/")]
    public class OrderController : Controller
    {
        private readonly OrderService orderService;
        private readonly IMapper _mapper;
        public OrderController(OrderService orderService, IMapper imapper)
        {
            this.orderService = orderService;
            _mapper = imapper;
        }
        /// <summary>
        /// Retrieves all orders or filters by user ID
        /// </summary>
        /// <param name="id_user">Optional user ID to filter orders (returns all orders if not provided)</param>
        /// <returns>
        /// List of orders mapped to OrderDto objects
        /// </returns>
        /// <response code="200">Returns the order list</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If there was a server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<Order>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getAllOrders([FromQuery] int id_user)
        {
            List<Order> orders;

            if (id_user > 0) 
            {
                orders = _mapper.Map<List<Order>>(orderService.GetOrdersByIdUser(id_user));
            }
            else
            {
                orders = _mapper.Map<List<Order>>(orderService.GetAllOrders()); 
            }

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            return Ok(orders);
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="order">Order data to create</param>
        /// <returns>
        /// The newly created order
        /// </returns>
        /// <response code="201">Order successfully created</response>
        /// <response code="409">If order already exists or data conflict occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Order))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddOrder([FromBody] Order order)
        {
            try
            {
                orderService.AddOrder(order);
                return StatusCode(StatusCodes.Status201Created, order);
            }
            catch
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
        }
        /// <summary>
        /// Retrieves a specific order by ID
        /// </summary>
        /// <param name="id">The order ID</param>
        /// <returns>
        /// The requested order details mapped to OrderDto
        /// </returns>
        /// <response code="200">Returns the order details</response>
        /// <response code="404">If order is not found</response>
        /// <response code="400">If the request is invalid</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  IActionResult getOrder(int id)
        {
            //Order order = orderService.GetOrderById(id);
            var order = _mapper.Map<OrderDto>(orderService.GetOrderById(id));
            if (order == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(order);
        }


        /// <summary>
        /// Updates an existing order
        /// </summary>
        /// <param name="id">ID of the order to update</param>
        /// <param name="order">Updated order data</param>
        /// <returns>
        /// Empty response on success
        /// </returns>
        /// <response code="200">Order successfully updated</response>
        /// <response code="404">If order is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult updateOrder(int id, [FromBody] Order order)
        {
            
            try
            {
                orderService.UpdateOrder(id, order);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes an order from the system
        /// </summary>
        /// <param name="id">ID of the order to delete</param>
        /// <returns>
        /// Empty response on success
        /// </returns>
        /// <response code="200">Order successfully deleted</response>
        /// <response code="404">If order is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DelOrder(int id)
        {
            
            try
            {
                orderService.DelOrder(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}