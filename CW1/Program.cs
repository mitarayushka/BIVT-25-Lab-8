using CW1.Drivers.Buses.I2C;
using CW1.Drivers.Buses.SPI;

var mock_i2c = new MockI2C();
var mock_spi = MockSPI.Instance();

Console.WriteLine(mock_i2c);
Console.WriteLine(mock_spi);