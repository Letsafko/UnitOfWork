using Application.CreateNewOrder;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers.CreateNewOrder
{
    /// <summary>
    /// Create new order output
    /// </summary>
    public sealed class CreateNewOrderPresenter : ICreateNewOrderOutput
    {
        /// <summary>
        /// View model
        /// </summary>
        public IActionResult ViewModel { get; private set; }
        
        /// <summary>
        /// ok output
        /// </summary>
        /// <param name="createOrderDto"></param>
        public void Ok(CreateOrderDto createOrderDto)
        {
            ViewModel = new OkObjectResult(new OrderViewModel(createOrderDto));
        }

        /// <summary>
        /// invalid output
        /// </summary>
        /// <param name="message"></param>
        public void Invalid(string message)
        {
            ViewModel = new BadRequestObjectResult(message);
        }
    }
}