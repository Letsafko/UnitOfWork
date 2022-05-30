namespace Application.CreateNewOrder
{
    public interface ICreateNewOrderOutput
    {
        void Ok(CreateOrderDto createOrderDto);
        void Invalid(string message);
    }
}