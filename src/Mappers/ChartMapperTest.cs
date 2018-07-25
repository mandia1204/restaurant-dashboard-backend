using System.Collections.Generic;
using Models;
using NUnit.Framework;

namespace Mappers
{
    [TestFixture]
    public class ChartMapperTest
    {
        [Test]
        public void Map_Passing1ChartModel_ReturnsChart() {
            var headers = new string[] {
                "2018"
            };
            var source = new List<ChartRow<string,int>> {
                new ChartRow<string, int>{
                    Key = "tipo01", Value = 10
                }
            };

            var mapper = new ChartMapper();
            var result = mapper.Map<string, int>(source, "name", headers, null);

            Assert.AreEqual("name", result.Name, "name shoould be populated");
            Assert.IsTrue(result.Data.ContainsKey("2018"), "should contain the 2018 key");
            Assert.AreEqual(10, result.Data["2018"]["tipo01"], "should be 10");
        }

        // [Test]
        // public void Map_Passing2Headers_ReturnsChartWith2Datasets() {
        //     var headers = new string[] {
        //         "2017",
        //         "2018"
        //     };
        //     var source = new List<ChartRow<string,int>> {
        //         new ChartRow<string, int>{
        //             Key = "tipo01", Value = 10
        //         }
        //     };

        //     var mapper = new ChartMapper();
        //     var result = mapper.Map<string, int>(source, "name", headers, null);

        //     Assert.AreEqual("name", result.Name, "name shoould be populated");
        //     Assert.IsTrue(result.Data.ContainsKey("2018"), "should contain the 2018 key");
        //     Assert.AreEqual(10, result.Data["2018"]["tipo01"], "should be 10");
        // }
    }
}