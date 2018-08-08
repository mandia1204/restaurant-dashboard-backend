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

            Assert.AreEqual("name", result.Name, "name shoould be populated");
            Assert.IsTrue(result.Data.ContainsKey("2018"), "should contain the 2018 key");
            Assert.AreEqual(10, result.Data["2018"]["tipo01"], "should be 10");
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

            Assert.IsTrue(result.Data["2018"].ContainsKey("mi tipo 1"), "should contain key mapped");
            Assert.IsTrue(result.Data["2018"].ContainsKey("mi tipo 2"), "should contain key mapped");
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

            Assert.IsTrue(result.Data["2018"].ContainsKey("mi tipo 1"), "should contain key mapped");
            Assert.IsTrue(result.Data["2018"].ContainsKey("tipo03"), "should contain key mapped");
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

            Assert.AreEqual("mychart", result.Name, "name should be populated");
            Assert.IsTrue(result.Data.ContainsKey("2017"), "should contain the 2017 key");
            Assert.IsTrue(result.Data.ContainsKey("2018"), "should contain the 2018 key");
            Assert.AreEqual(3, result.Data["2017"].Count, "should contain 3 items");
            Assert.AreEqual(3, result.Data["2018"].Count, "should contain 3 items");
            Assert.AreEqual(10, result.Data["2017"]["tipo01"]);
            Assert.AreEqual(100, result.Data["2017"]["tipo02"]);
            Assert.AreEqual(200, result.Data["2017"]["tipo03"]);
            Assert.AreEqual(20, result.Data["2018"]["tipo01"]);
            Assert.AreEqual(200, result.Data["2018"]["tipo02"]);
            Assert.AreEqual(300, result.Data["2018"]["tipo03"]);
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

            Assert.AreEqual("2017", result.Data.Keys.ElementAt(0));
            Assert.AreEqual("2018", result.Data.Keys.ElementAt(1));
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

            Assert.AreEqual("tipo01", result.Data["2017"].Keys.ElementAt(0));
            Assert.AreEqual("tipo03", result.Data["2017"].Keys.ElementAt(2));
            Assert.AreEqual("tipo01", result.Data["2018"].Keys.ElementAt(0));
            Assert.AreEqual("tipo03", result.Data["2018"].Keys.ElementAt(2));
        }

        [Test]
        public void Map_GroupPassingNullModel_ReturnsNull() {
            var result = new ChartMapper().Map<string, int>(null, "mychart", null);
            Assert.AreEqual(null, result);
        }

        [Test]
        public void Map_PassingNullModel_ReturnsNull() {
            var result = new ChartMapper().Map<string, int>(null, null, "mychart", null);
            Assert.AreEqual(null, result);
        }
    }
}