En FactInternetSales
- Crear una medida InternetAVGSalesAmount = Promedio de importe de venta por renglon
- Crear una medida InternetMaxSalesAmount = Máximo 
- Crear una medida InternetMinSalesAmount = Mínimo

Crear KPI basado en la medida InternetTotalSales objetivo valor absoluto 1.000.000.
- Ubrales Rojo <80%
- Amarillo  >= 80% y <= 100%
- Verde >= 100%

Crear Jerarquía llamada "Geografía" en DimGeography:
- CountryRegionName
- StateProvinceName
- CityName
- En DimCustomer crear columna calculada CountryRegionName que obtenga el nombre del país desde DimGeography.
