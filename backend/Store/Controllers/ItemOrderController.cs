using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.BL;
using Dto;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/v1/itemorders/")]
    public class ItemOrderController : Controller
    {
        private readonly ItemOrderService itemOrderService;
        private readonly IMapper _mapper;

        public ItemOrderController(ItemOrderService itemOrderService, IMapper iMapper)
        {
            this.itemOrderService = itemOrderService;
            _mapper = iMapper;
        }

        /// <summary>
        /// Retrieves all item orders or filters by order ID
        /// </summary>
        /// <param name="id_order">Optional order ID to filter items (returns all items if not provided or 0)</param>
        /// <returns>
        /// List of item orders mapped to ItemOrderDto objects
        /// </returns>
        /// <response code="200">Returns the list of item orders</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If there was a server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ItemOrderDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getItemOrders([FromQuery] int id_order)
        {
            List<ItemOrderDto> itemorders = null;
            if (id_order > 0)
            {
                itemorders = _mapper.Map<List<ItemOrderDto>>(itemOrderService.GetItemOrderByIdOrder(id_order));
            }
            else
            {
                itemorders = _mapper.Map<List<ItemOrderDto>>(itemOrderService.GetItemOrders());
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(itemorders);
        }

        /// <summary>
        /// Adds a new item to an order
        /// </summary>
        /// <param name="itemOrder">Item order data to create</param>
        /// <returns>
        /// The newly created item order
        /// </returns>
        /// <response code="201">Item order successfully created</response>
        /// <response code="409">If item order already exists or data conflict occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ItemOrder))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddItemOrder([FromBody] ItemOrder itemOrder)
        {
            try
            {
                itemOrderService.AddItemOrder(itemOrder);
                return StatusCode(StatusCodes.Status201Created, itemOrder);
            }
            catch
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
        }

        /// <summary>
        /// Retrieves a specific item order by ID
        /// </summary>
        /// <param name="id">The item order ID</param>
        /// <returns>
        /// The requested item order details mapped to ItemOrderDto
        /// </returns>
        /// <response code="200">Returns the item order details</response>
        /// <response code="404">If item order is not found</response>
        /// <response code="400">If the request is invalid</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemOrderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult getItemOrder(int id)
        {
            var itemOrder = _mapper.Map<ItemOrderDto>(itemOrderService.GetItemOrderById(id));
            if (itemOrder == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(itemOrder);
        }

        /// <summary>
        /// Updates an existing item order
        /// </summary>
        /// <param name="itemOrder">Updated item order data (must include ID)</param>
        /// <returns>
        /// Empty response on success
        /// </returns>
        /// <response code="200">Item order successfully updated</response>
        /// <response code="404">If item order is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult updateItemOrder([FromBody] ItemOrder itemOrder)
        {
            try
            {
                itemOrderService.UpdateItemOrder(itemOrder);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes an item from an order
        /// </summary>
        /// <param name="id">ID of the item order to delete</param>
        /// <returns>
        /// Empty response on success
        /// </returns>
        /// <response code="200">Item order successfully deleted</response>
        /// <response code="404">If item order is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DelItemOrder(int id)
        {
            try
            {
                itemOrderService.DelItemOrder(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}