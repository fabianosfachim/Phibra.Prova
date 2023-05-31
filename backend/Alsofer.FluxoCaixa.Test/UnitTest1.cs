using Moq;
using Phibra.Prova.Application.Services.Interfaces;

namespace Alsofer.FluxoCaixa.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test()
        {
            Assert.Pass();
        }

        [Test]
        public void TestListarCliente()
        {

            var mockRepo = new Mock<ITipoMovimentacaoServices>();
            var response = mockRepo.Setup(x => x.ListarTipoMovimentacao().Result);

            Assert.IsNotNull(response);
            Assert.Pass("Ok");
        }

       
    }
}