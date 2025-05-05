using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.BL;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/v1/products/")]
    public class ProductController : Controller
    {
        private readonly ProductService productService;
        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Retrieves all products or filters by name
        /// </summary>
        /// <param name="name">Optional product name filter (case-sensitive)</param>
        /// <returns>
        /// List of products matching criteria
        /// </returns>
        /// <response code="200">Returns the product list</response>
        /// <response code="500">If there was a server error</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<Product>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getAllProducts([FromQuery] string? startWith)
        {
            ICollection<Product> products;
            if (startWith == null)
            {
                products = productService.GetAllProducts();
            }
            else
            {
                products = productService.GetProductByName(startWith);
            }
            return Ok(products);
        }
        /// <summary>
        /// Adds a new product to the system
        /// </summary>
        /// <param name="product">Product data to create</param>
        /// <returns>
        /// The newly created product
        /// </returns>
        /// <response code="201">Product successfully created</response>
        /// <response code="409">If product already exists or data conflict occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddProduct([FromBody] Product product)
        {
            try
            {
                productService.AddProduct(product);
                return StatusCode(StatusCodes.Status201Created, product);
            }
            catch
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
        }

        /// <summary>
        /// Retrieves a specific product by ID
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>
        /// The requested product details
        /// </returns>
        /// <response code="200">Returns the product details</response>
        /// <response code="404">If product is not found</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If there was a server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  IActionResult getProduct(int id)
        {
            Product product = productService.GetProductById(id);
            if (product == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(product);
        }
        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">ID of the product to update</param>
        /// <param name="product">Updated product data</param>
        /// <returns>
        /// Empty response on success
        /// </returns>
        /// <response code="200">Product successfully updated</response>
        /// <response code="404">If product is not found</response>

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult updateProduct(int id, [FromBody] Product product)
        {
            try
            {
                productService.UpdateProduct(id, product);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Deletes a product from the system
        /// </summary>
        /// <param name="id">ID of the product to delete</param>
        /// <returns>
        /// Empty response on success
        /// </returns>
        /// <response code="200">Product successfully deleted</response>
        /// <response code="404">If product is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DelProduct(int id)
        {
            try
            {
                productService.DelProduct(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}