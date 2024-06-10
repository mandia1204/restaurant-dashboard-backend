using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using NUnit.Framework;
using Util;

namespace Mappers
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
            Assert.That(result.Count(), Is.EqualTo(2), "should have 2 items");
            Assert.That(Constants.MotivosEliminacion["000"], Is.EqualTo(first.Tipo), "should map Tipo '000' correctly");
            Assert.That(first.Fecha, Is.EqualTo("05/10/2018 11:30"), "should map Fecha correctly");
            Assert.That(result.Last().Observacion, Is.EqualTo("test 2"), "should map Observacion correctly");
        }

        [Test]
        public void Map_PassingNull_ReturnsEmptyList() {
            var mapper = new AnulacionMapper();

            var result = mapper.Map(null);
            
            Assert.That(result.Count(), Is.EqualTo(0), "should return empty list");
        }
        [Test]
        public void Map_PassingTipoNotInConstants_MapsDefaultTipo() {
            var source = new List<Anulacion>{
                new Anulacion { Fecha = new DateTime(2018,10,5), Tipo = "999", Observacion ="Test 1" }
            };
            var mapper = new AnulacionMapper();

            var result = mapper.Map(source).First();

            Assert.That(result.Tipo, Is.EqualTo(Constants.MotivosEliminacion["default"]), "maps default tipo");
        }
    }
}