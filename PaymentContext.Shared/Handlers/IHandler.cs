using PaymentContext.Shared.Commands;

namespace PaymentContext.Shared.Handlers
{
    public interface IHandler<T> where T : ICommands
    {
        ICommandResult Handle(T command);
    } 
}