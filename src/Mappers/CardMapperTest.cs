using Models;
using NUnit.Framework;

namespace Mappers
{
    [TestFixture]
    public class CardMapperTest
    {
        [Test]
        public void Map_CardWithInt_ReturnsCard  () {
            var model = new Card<int> { Value =100};

            var result = (new CardMapper()).Map(model);

            Assert.AreEqual("100", result.Value);
        }

        [Test]
        public void Map_CardWith2Values_ReturnsCard() {
            var model = new Card<int, double> { Value = 100, Value2 = 200.55 };

            var result = (new CardMapper()).Map(model);

            Assert.AreEqual("100", result.Value);
        }

        [Test]
        public void Map_CardWithDouble_ReturnsCard() {
            var model = new Card<double> { Value = 200.55 };

            var result = (new CardMapper()).Map(model);

            Assert.AreEqual("200.55", result.Value);
        }

        [Test]
        public void MapTicketPromedio_ValidParams_ReturnsCard() {
            var produccion = new Card<double, double> { Value = 100.10, Value2 =200.5 };
            var pax = new Card<int> {Value=10};

            var result = (new CardMapper()).MapTicketPromedio(produccion, pax);

            Assert.AreEqual("20.05", result.Value);
        }

        [Test]
        public void MapTicketPromedio_PaxZero_ReturnsCardWith0() {
            var produccion = new Card<double, double> { Value = 100.10, Value2 =200.5 };
            var pax = new Card<int> {Value=0};

            var result = (new CardMapper()).MapTicketPromedio(produccion, pax);

            Assert.AreEqual("0.00", result.Value);
        }

        [Test]
        public void MapTicketPromedio_PaxZeroAndProduccionZero_ReturnsCardWith0() {
            var produccion = new Card<double, double> { Value = 0, Value2 =0 };
            var pax = new Card<int> {Value=0};

            var result = (new CardMapper()).MapTicketPromedio(produccion, pax);

            Assert.AreEqual("0.00", result.Value);
        }

        [Test]
        public void MapTicketPromedio_ProduccionNull_ReturnsNull() {
            Card<double, double>  produccion = null;
            var pax = new Card<int> {Value=0};

            var result = (new CardMapper()).MapTicketPromedio(produccion, pax);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void MapTicketPromedio_PaxNull_ReturnsNull() {
            var produccion = new Card<double, double> { Value = 0, Value2 =0 };
            Card<int> pax = null;

            var result = (new CardMapper()).MapTicketPromedio(produccion, pax);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void Map_NullModel_ReturnsNull() {
            var result = (new CardMapper()).Map<int>(null);
            var result2 = (new CardMapper()).Map<int, double>(null);

            Assert.AreEqual(null, result);
            Assert.AreEqual(null, result2);
        }
    }
}