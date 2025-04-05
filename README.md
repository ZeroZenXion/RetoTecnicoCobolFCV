# Reto Técnico: Procesamiento de Transacciones Bancarias (CLI)

Esta es una aplicación de línea de comandos desarrollada en C# (.NET) para procesar archivos CSV que contienen transacciones bancarias. El programa calcula el balance final (suma de créditos menos débitos), identifica la transacción de mayor monto y cuenta las transacciones por tipo.

## Instalación y Ejecución

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/ZeroZenXion/RetoTecnicoCobolFCV.git
2. Preparar el archivo CSV:

Coloca el archivo CSV (por ejemplo, data.csv) en la carpeta Transacciones en la raíz del proyecto.

Asegúrate de que el CSV tenga el formato:

id,tipo,monto
1,Crédito,100.00
2,Débito,50.00
3,Crédito,200.00
4,Débito,75.00
5,Crédito,150.00


3. Ejecucion de la Aplicacion:

dotnet run -- Transacciones/data.csv

Esto lo hacemos abriendo una terminal dentro de la carpeta

Para mas consultas y una documentacion mas detallada, leer el README del Proyecto.

Este proyecto esta bajo derechos de autor, cualquier uso esta prohibido  ---------ZEROZENDEV
