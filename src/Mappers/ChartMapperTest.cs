using System.Collections.Generic;
using System.Linq;
using Models;
using NUnit.Framework;

namespace Mappers
{
    [TestFixture]
    public class ChartMapperTest
    {
        [Test]
        public void Map_Passing1ChartModel_ReturnsChart() {
            var header = "2018";
            var source = new List<ChartRow<string,int>> {
                new ChartRow<string, int>{
                    Key = "tipo01", Value = 10
                }
            };

            var result = new ChartMapper().Map<string, int>(source, "name", header, null);

            Assert.That(result.Name, Is.EqualTo("name"), "name shoould be populated");
            Assert.That(result.Data.ContainsKey("2018"), Is.True, "should contain the 2018 key");
            Assert.That(result.Data["2018"]["tipo01"], Is.EqualTo(10), "should be 10");
        }

        [Test]
        public void Map_PassingValueMapper_ChartContainsMappedKeys() {
            var header = "2018";
            var source = new List<ChartRow<string,int>> {
                new ChartRow<string, int>{
                    Key = "tipo01", Value = 10
                },
                new ChartRow<string, int>{
                    Key = "tipo02", Value = 20
                }
            };
            var valueMapper = new Dictionary<string,string> {
                { "tipo01", "mi tipo 1"},
                { "tipo02", "mi tipo 2"}
            };

            var result =  new ChartMapper().Map<string, int>(source, "name", header, valueMapper);

            Assert.That(result.Data["2018"].ContainsKey("mi tipo 1"), Is.True, "should contain key mapped");
            Assert.That(result.Data["2018"].ContainsKey("mi tipo 2"), Is.True, "should contain key mapped");
        }

        [Test]
        public void Map_ValueNotInValueMapper_ChartContainsMappedKeys() {
            var header = "2018";
            var source = new List<ChartRow<string,int>> {
                new ChartRow<string, int>{
                    Key = "tipo01", Value = 10
                },
                new ChartRow<string, int>{
                    Key = "tipo03", Value = 20
                },
            };
            var valueMapper = new Dictionary<string,string> {
                { "tipo01", "mi tipo 1"},
                { "tipo02", "mi tipo 2"}
            };

            var result =  new ChartMapper().Map<string, int>(source, "name", header, valueMapper);

            Assert.That(result.Data["2018"].ContainsKey("mi tipo 1"), Is.True, "should contain key mapped");
            Assert.That(result.Data["2018"].ContainsKey("tipo03"), Is.True, "should contain key mapped");
        }

        [Test]
        public void Map_PassingGroups_ReturnsChartWithMultipleGroups() {
            var source = new List<ChartRow<string,int>> {
                new ChartRow<string, int>{
                    Group="2017", Key = "tipo01", Value = 10
                },
                new ChartRow<string, int>{
                    Group="2017", Key = "tipo02", Value = 100
                },
                new ChartRow<string, int>{
                    Group="2017", Key = "tipo03", Value = 200
                },
                new ChartRow<string, int>{
                    Group="2018", Key = "tipo01", Value = 20
                },
                new ChartRow<string, int>{
                    Group="2018", Key = "tipo02", Value = 200
                },
                new ChartRow<string, int>{
                    Group="2018", Key = "tipo03", Value = 300
                }
            };

            var result = new ChartMapper().Map<string, int>(source, "mychart", null);

            Assert.That(result.Name, Is.EqualTo("mychart"), "name should be populated");
            Assert.That(result.Data.ContainsKey("2017"), Is.True, "should contain the 2017 key");
            Assert.That(result.Data.ContainsKey("2018"), Is.True, "should contain the 2018 key");

            Assert.That(result.Data["2017"].Count, Is.EqualTo(3), "should contain 3 items");
            Assert.That(result.Data["2018"].Count, Is.EqualTo(3), "should contain 3 items");

            Assert.That(result.Data["2017"]["tipo01"], Is.EqualTo(10));
            Assert.That(result.Data["2017"]["tipo02"], Is.EqualTo(100));
            Assert.That(result.Data["2017"]["tipo03"], Is.EqualTo(200));
            Assert.That(result.Data["2018"]["tipo01"], Is.EqualTo(20));
            Assert.That(result.Data["2018"]["tipo02"], Is.EqualTo(200));
            Assert.That(result.Data["2018"]["tipo03"], Is.EqualTo(300));
        }

        [Test]
        public void Map_PassingGroupsUnsorted_ReturnsChartDataGroupsSorted() {
            var source = new List<ChartRow<string,int>> {
                new ChartRow<string, int>{
                    Group="2018", Key = "tipo02", Value = 200
                },
                new ChartRow<string, int>{
                    Group="2017", Key = "tipo02", Value = 100
                }
            };

            var result = new ChartMapper().Map<string, int>(source, "mychart", null);

            Assert.That(result.Data.Keys.ElementAt(0), Is.EqualTo("2017"));
            Assert.That(result.Data.Keys.ElementAt(1), Is.EqualTo("2018"));
        }

        [Test]
        public void Map_PassingDataUnsorted_ReturnsChartDataSorted() {
            var source = new List<ChartRow<string,int>> {
                new ChartRow<string, int>{
                    Group="2018", Key = "tipo02", Value = 200
                },
                new ChartRow<string, int>{
                    Group="2018", Key = "tipo03", Value = 300
                },
                new ChartRow<string, int>{
                    Group="2017", Key = "tipo02", Value = 100
                },
                new ChartRow<string, int>{
                    Group="2018", Key = "tipo01", Value = 20
                },
                new ChartRow<string, int>{
                    Group="2017", Key = "tipo01", Value = 10
                },
                new ChartRow<string, int>{
                    Group="2017", Key = "tipo03", Value = 200
                }
            };

            var result = new ChartMapper().Map<string, int>(source, "mychart", null);

            Assert.That(result.Data["2017"].Keys.ElementAt(0), Is.EqualTo("tipo01"));
            Assert.That(result.Data["2017"].Keys.ElementAt(2), Is.EqualTo("tipo03"));
            Assert.That(result.Data["2018"].Keys.ElementAt(0), Is.EqualTo("tipo01"));
            Assert.That(result.Data["2018"].Keys.ElementAt(2), Is.EqualTo("tipo03"));
        }

        [Test]
        public void Map_GroupPassingNullModel_ReturnsNull() {
            var result = new ChartMapper().Map<string, int>(null, "mychart", null);
            
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Map_PassingNullModel_ReturnsNull() {
            var result = new ChartMapper().Map<string, int>(null, null, "mychart", null);
            
            Assert.That(result, Is.Null);
        }
    }
}