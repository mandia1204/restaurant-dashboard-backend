using System;
using System.Collections.Generic;
using System.Linq;
using Mappers;
using Models;
using NUnit.Framework;
using Util;

namespace src.Mappers
{
    [TestFixture]
    public class AnulacionMapperTest
    {
        [Test]
        public void Map_PassingList_ReturnsValuesTransformed() {
            var source = new List<Anulacion>{
                new Anulacion { Fecha = new DateTime(2018,10,5, 11, 30, 10), Tipo = "000", Observacion ="Test 1" },
                new Anulacion { Fecha = new DateTime(2018,10,6), Tipo = "001", Observacion ="Test 2" }
            };

            var mapper = new AnulacionMapper();
            var result = mapper.Map(source);

            var first = result.First();
            Assert.AreEqual(2, result.Count(), "should have 2 items");
            Assert.AreEqual(Constants.MotivosEliminacion["000"], first.Tipo, "should map Tipo '000' correctly");
            Assert.AreEqual("05/10/2018 11:30", first.Fecha, "should map Fecha correctly");
            Assert.AreEqual("test 2", result.Last().Observacion, "should map Observacion correctly");
        }
        [Test]
        public void Map_PassingNull_ReturnsEmptyList() {
            var mapper = new AnulacionMapper();

            var result = mapper.Map(null);

            Assert.AreEqual(0, result.Count(), "should return empty list");
        }
        [Test]
        public void Map_PassingTipoNotInConstants_MapsDefaultTipo() {
            var source = new List<Anulacion>{
                new Anulacion { Fecha = new DateTime(2018,10,5), Tipo = "999", Observacion ="Test 1" }
            };
            var mapper = new AnulacionMapper();

            var result = mapper.Map(source).First();

            Assert.AreEqual(Constants.MotivosEliminacion["default"], result.Tipo, "maps default tipo");
        }
    }
}