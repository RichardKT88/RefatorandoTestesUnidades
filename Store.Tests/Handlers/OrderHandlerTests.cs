using Store.Domain.Repositories;
using Store.Domain.Commands;
using Store.Domain.Handlers;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly ICustomerRepository? _customerRepository;
        private readonly IDeliveryFeeRepository? _deliveryFeeRepository;
        private readonly IDiscountRepository? _discountRepository;
        private readonly IOrderRepository? _orderRepository;
        private readonly IProductRepository? _productRepository;
        public OrderHandlerTests()
        {

        }
        public OrderHandlerTests(ICustomerRepository customerRepository, IDeliveryFeeRepository deliveryFeeRepository, IDiscountRepository discountRepository, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _customerRepository = customerRepository;
            _deliveryFeeRepository = deliveryFeeRepository;
            _discountRepository = discountRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(command.IsValid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cep_invalido_o_pedido_deve_ser_gerado_normalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "Richard";
            command.ZipCode = "134110804";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            var handler = new OrderHandler(
               _customerRepository!,
               _deliveryFeeRepository!,
               _discountRepository!,
               _productRepository!,
               _orderRepository!);

            handler.Handle(command);
            Assert.AreEqual(handler.IsValid, true);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_promocode_inexistente_o_pedido_deve_ser_gerado_normalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "Richard";
            command.ZipCode = "134110804";
            command.PromoCode = "123456789090";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            var handler = new OrderHandler(
               _customerRepository!,
               _deliveryFeeRepository!,
               _discountRepository!,
               _productRepository!,
               _orderRepository!);

            handler.Handle(command);
            Assert.AreEqual(handler.IsValid, true);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_pedido_sem_itens_o_mesmo_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "Richarddasdasd";
            command.ZipCode = "1341dasdasdasdas";
            command.PromoCode = "1234567dsdasdas8";
            command.Items.Add(new CreateOrderItemCommand());
            command.Validate();

            Assert.AreEqual(command.IsValid, false);

        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "";
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(command.IsValid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "Richard";
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(
                _customerRepository!,
                _deliveryFeeRepository!,
                _discountRepository!,
                _productRepository!,
                _orderRepository!);

            handler.Handle(command);
            Assert.AreEqual(handler.IsValid, true);
        }
    }
}
