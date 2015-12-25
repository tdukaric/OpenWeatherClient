using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void KelvinToCelsiusTest()
        {                               
            Assert.AreEqual(-272.15, OpenWeatherClientInfoteria.Convert.KelvinToCelsius(1));
            Assert.AreEqual(-273.15, OpenWeatherClientInfoteria.Convert.KelvinToCelsius(0));

            try
            {
                OpenWeatherClientInfoteria.Convert.KelvinToCelsius(-1);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Kelvin < 0!");
            }

            Assert.AreEqual(0, OpenWeatherClientInfoteria.Convert.KelvinToCelsius(273.15));
        }
                         
        [TestMethod]
        public void KelvinToFarhenheitTest()
        {                            
            Assert.AreEqual(-457.87, OpenWeatherClientInfoteria.Convert.KelvinToFahrenheit(1));
            Assert.AreEqual(-459.67, OpenWeatherClientInfoteria.Convert.KelvinToFahrenheit(0));

            try
            {
                OpenWeatherClientInfoteria.Convert.KelvinToFahrenheit(-1);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Kelvin < 0!");
            }
                                                                                                    
        }
    }
}
