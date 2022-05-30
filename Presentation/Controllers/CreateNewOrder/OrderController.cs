using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Application.CreateNewOrder;
using Domain.Abstracts.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers.CreateNewOrder
{
    /// <summary>
    /// Manage orders
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public sealed class OrderController : ControllerBase
    {
        private readonly CreateNewOrderPresenter _presenter;
        private readonly IProcessor _processor;

        /// <summary>
        ///     Create an instance of <see cref="OrderController"/>
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public OrderController(CreateNewOrderPresenter presenter, IProcessor processor)
        {
            _processor = processor;
            _presenter = presenter;
        }
        
        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CreateNewOrderAsync([Required][FromBody] CreateNewOrderInput order)
        {
            var command = new CreateNewOrderCommand(1);
            await _processor.ProcessCommandAsync(command);
            return _presenter.ViewModel;
        }
    }
}