using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;

namespace Store.Domain.Commands
{
    public class CreateOrderCommand : Notifiable<Notification>, ICommand
    {
        public CreateOrderCommand()
        {
            Items = new List<CreateOrderItemCommand>();
        }

        public CreateOrderCommand(string customer, string zipCode, string promoCode, IList<CreateOrderItemCommand> items)
        {
            Customer = customer;
            ZipCode = zipCode;
            PromoCode = promoCode;
            Items = items;
        }

        public string? Customer { get; set; }
        public string? ZipCode { get; set; }
        public string? PromoCode { get; set; }
        public IList<CreateOrderItemCommand> Items { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Order>()
               .Requires()
               .IsGreaterThan(Customer, 11, "Customer", "Cliente inválido")
               .IsGreaterThan(ZipCode, 8, "ZipCode", "CEP inválido")
               .IsGreaterThan(PromoCode, 8, "PromoCode", "Codigo de Promoção inválido")
               .IsEmpty(Items, "Items")
           );
        }
    }
}
