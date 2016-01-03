using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.UI.Popups;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void KelvinToCelsiusTest()
        {                               
            Assert.AreEqual(-272.15, OpenWeatherClientInfoteria.TemperatureUnitConvert.KelvinToCelsius(1), 0.001);
            Assert.AreEqual(-273.15, OpenWeatherClientInfoteria.TemperatureUnitConvert.KelvinToCelsius(0), 0.001);

            try
            {
                OpenWeatherClientInfoteria.TemperatureUnitConvert.KelvinToCelsius(-1);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Kelvin < 0!");
            }

            Assert.AreEqual(0, OpenWeatherClientInfoteria.TemperatureUnitConvert.KelvinToCelsius(273.15));
        }
                         
        [TestMethod]
        public void KelvinToFarhenheitTest()
        {                            
            Assert.AreEqual(-457.87, OpenWeatherClientInfoteria.TemperatureUnitConvert.KelvinToFahrenheit(1), 0.001);
            Assert.AreEqual(-459.67, OpenWeatherClientInfoteria.TemperatureUnitConvert.KelvinToFahrenheit(0), 0.001);

            try
            {
                OpenWeatherClientInfoteria.TemperatureUnitConvert.KelvinToFahrenheit(-1);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Kelvin < 0!");
            }
                                                                                                    
        }

        [TestMethod]
        public void FahrenheitToKelvinTest()
        {
            Assert.AreEqual(255.927778, OpenWeatherClientInfoteria.TemperatureUnitConvert.FahrenheitToKelvin(1), 0.001);
            Assert.AreEqual(255.372222, OpenWeatherClientInfoteria.TemperatureUnitConvert.FahrenheitToKelvin(0), 0.001);

        }

        [TestMethod]
        public void FarhenheitToCelsiusTest()
        {
            Assert.AreEqual(-17.22222, OpenWeatherClientInfoteria.TemperatureUnitConvert.FahrenheitToCelsius(1), 0.001);
            Assert.AreEqual(-17.77778, OpenWeatherClientInfoteria.TemperatureUnitConvert.FahrenheitToCelsius(0), 0.001); 
            
        }
    }
}
