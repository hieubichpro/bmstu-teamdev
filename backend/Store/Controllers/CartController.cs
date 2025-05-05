using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.BL;
using Dto;
using AutoMapper;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// Controller for managing shopping carts
    /// </summary>
    [ApiController]
    [Route("/api/v1/carts/")]
    public class CartController : Controller
    {
        private readonly CartService cartService;
        private readonly IMapper _imapper;

        /// <summary>
        /// Initializes a new instance of the CartController
        /// </summary>
        /// <param name="cartService">Service for cart operations</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        public CartController(CartService cartService, IMapper mapper)
        {
            this.cartService = cartService;
            _imapper = mapper;
        }

        /// <summary>
        /// Retrieves all shopping carts
        /// </summary>
        /// <returns>
        /// List of all shopping carts
        /// </returns>
        /// <response code="200">Returns the list of carts</response>
        /// <response code="400">If the request model state is invalid</response>
        /// <response code="500">If there was a server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<Cart>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getAllCarts()
        {
            var carts = cartService.GetAllCarts();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(carts);
        }

        /// <summary>
        /// Creates a new shopping cart
        /// </summary>
        /// <param name="cart">Cart data to create</param>
        /// <returns>
        /// The newly created cart
        /// </returns>
        /// <response code="201">Cart successfully created</response>
        /// <response code="409">If cart already exists or data conflict occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Cart))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddCart([FromBody] Cart cart)
        {
            try
            {
                cartService.AddCart(cart);
                return StatusCode(StatusCodes.Status201Created, cart);
            }
            catch
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
        }

        /// <summary>
        /// Retrieves a specific cart by ID
        /// </summary>
        /// <param name="id">The cart ID</param>
        /// <returns>
        /// The requested cart details mapped to CartDto
        /// </returns>
        /// <response code="200">Returns the cart details</response>
        /// <response code="404">If cart is not found</response>
        /// <response code="400">If the request model state is invalid</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult getCart(int id)
        {
            var cart = _imapper.Map<CartDto>(cartService.GetCartById(id));
            if (cart == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(cart);
        }

        /// <summary>
        /// Updates an existing shopping cart
        /// </summary>
        /// <param name="id">ID of the cart to update</param>
        /// <param name="cart">Updated cart data</param>
        /// <returns>
        /// Empty response on success
        /// </returns>
        /// <response code="200">Cart successfully updated</response>
        /// <response code="404">If cart is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult updateCart(int id, [FromBody] Cart cart)
        {
            try
            {
                cartService.UpdateCart(id, cart);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes a shopping cart
        /// </summary>
        /// <param name="id">ID of the cart to delete</param>
        /// <returns>
        /// Empty response on success
        /// </returns>
        /// <response code="200">Cart successfully deleted</response>
        /// <response code="404">If cart is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DelCart(int id)
        {
            try
            {
                cartService.DelCart(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}