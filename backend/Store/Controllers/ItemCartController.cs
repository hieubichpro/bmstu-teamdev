using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.BL;
using Dto;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// Controller for managing shopping cart items
    /// </summary>
    [ApiController]
    [Route("/api/v1/itemcarts/")]
    public class ItemCartController : Controller
    {
        private readonly ItemCartService itemCartService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the ItemCartController
        /// </summary>
        /// <param name="itemCartService">Service for cart item operations</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        public ItemCartController(ItemCartService itemCartService, IMapper mapper)
        {
            this.itemCartService = itemCartService;
            this._mapper = mapper;
        }

        /// <summary>
        /// Retrieves all cart items or filters by cart ID
        /// </summary>
        /// <param name="id_cart">
        /// Optional cart ID filter:
        /// - If provided (greater than 0), returns items for specified cart
        /// - If not provided or 0, returns all cart items
        /// </param>
        /// <returns>
        /// List of cart items mapped to ItemCartDto objects
        /// </returns>
        /// <response code="200">Returns the list of cart items</response>
        /// <response code="400">If the request model state is invalid</response>
        /// <response code="500">If there was a server error</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ItemCartDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getItemCarts([FromQuery] int id_cart)
        {
            List<ItemCartDto> itemCarts;
            if (id_cart > 0)
            {
                itemCarts = _mapper.Map<List<ItemCartDto>>(itemCartService.GetAllItemCartByIdCart(id_cart));
            }
            else
            {
                itemCarts = _mapper.Map<List<ItemCartDto>>(itemCartService.GetAllItemCart());
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(itemCarts);
        }

        /// <summary>
        /// Adds a new item to a shopping cart
        /// </summary>
        /// <param name="itemCart">Cart item data to create</param>
        /// <returns>
        /// The newly created cart item
        /// </returns>
        /// <response code="201">Cart item successfully created</response>
        /// <response code="409">If cart item already exists or data conflict occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ItemCart))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddItemCart([FromBody] ItemCart itemCart)
        {
            try
            {
                itemCartService.AddItemCart(itemCart);
                return StatusCode(StatusCodes.Status201Created, itemCart);
            }
            catch
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
        }

        /// <summary>
        /// Retrieves a specific cart item by ID
        /// </summary>
        /// <param name="id">The cart item ID</param>
        /// <returns>
        /// The requested cart item details mapped to ItemCartDto
        /// </returns>
        /// <response code="200">Returns the cart item details</response>
        /// <response code="404">If cart item is not found</response>
        /// <response code="400">If the request model state is invalid</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemCartDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult getItemCart(int id)
        {
            var itemCart = _mapper.Map<ItemCartDto>(itemCartService.GetItemCartById(id));
            if (itemCart == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(itemCart);
        }

        /// <summary>
        /// Updates an existing cart item
        /// </summary>
        /// <param name="itemCart">Updated cart item data (must include ID)</param>
        /// <returns>
        /// Empty response on success
        /// </returns>
        /// <response code="200">Cart item successfully updated</response>
        /// <response code="404">If cart item is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult updateItemCart([FromBody] ItemCart itemCart)
        {
            try
            {
                itemCartService.UpdateItemCart(itemCart);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Removes an item from a shopping cart
        /// </summary>
        /// <param name="id">ID of the cart item to remove</param>
        /// <returns>
        /// Empty response on success
        /// </returns>
        /// <response code="200">Cart item successfully removed</response>
        /// <response code="404">If cart item is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DelItemCart(int id)
        {
            try
            {
                itemCartService.DelItemCart(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}