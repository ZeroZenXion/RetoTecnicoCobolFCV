# Reto Técnico: Procesamiento de Transacciones Bancarias (CLI)

## Introducción
Este proyecto es una aplicación de línea de comandos desarrollada en C# utilizando .NET.  
El reto consiste en procesar un archivo CSV que contiene transacciones bancarias, para generar un reporte que incluya:
- **Balance Final:** Suma de los montos de transacciones de tipo "Crédito" menos la suma de los montos de transacciones de tipo "Débito".
- **Transacción de Mayor Monto:** Identificar la transacción con el monto más alto (mostrando su ID y monto).
- **Conteo de Transacciones:** Número total de transacciones para cada tipo ("Crédito" y "Débito").

El propósito es demostrar buenas prácticas en el diseño de aplicaciones CLI, con una estructura modular y validación robusta de datos, asegurando la portabilidad y facilidad de mantenimiento.

## Instrucciones de Ejecución
### Requisitos Previos
- **.NET SDK:** Se recomienda tener instalada la versión .NET 6 o .NET 7. Puedes descargarlo desde [la página oficial de .NET](https://dotnet.microsoft.com/download).
- **Editor de Código:** Visual Studio Code, Visual Studio o cualquier otro editor de tu preferencia.

### Instalación y Ejecución
1. **Clonar o Descargar el Repositorio:**
   ```bash
   git clone https://github.com/ZeroZenXion/RetoTecnicoCobolFCV.git




## Estructura del Archivo CSV

Debe tener la siguiente estructura el archivo de datos:

id,tipo,monto
1,Crédito,100.00
2,Débito,50.00
3,Crédito,200.00
4,Débito,75.00
5,Crédito,150.00


###EJECUCCION DE LA APLICACION

Tenemos que ejecutar una terminal o un PowerShell dentro de la carpeta del Proyecto y colocar el siguiente comando:

dotnet run -- Transacciones/data.csv

En este caso si lo hacemos por terminal podemos poner que archivo csv queremos que ejecute en caso tengamos varios archivos csv con la misma estructura de datos en la carpeta Transacciones.

Si en caso decidimos ejecutarlo por medio de Visual Studio Code hay una condicional que nos lee cuantos archivos hay en la carpeta y nos hace un listado de esa forma podemos elegir que archivo queremos que nos ejecute.


### Enfoque y Solución

Separación de Responsabilidades:
La solución está dividida en módulos:

Models: Define la clase Transaction que modela una transacción.

Services: Contiene la clase TransactionProcessor, que se encarga de leer el CSV, validar los datos (por ejemplo, identificar filas con campos vacíos o erróneos), calcular el balance final, identificar la transacción de mayor monto y contar las transacciones por tipo.

Program.cs: Orquesta la lectura de datos y muestra el reporte en la terminal.

Validación de Datos:
Se han implementado validaciones para cada fila del archivo CSV. Si algún dato (ID, Tipo o Monto) falta o es erróneo, el programa muestra un mensaje indicando qué campo está incompleto y continúa procesando las siguientes filas sin interrumpir la ejecución.

Portabilidad:
La aplicación utiliza rutas relativas, de modo que se puede mover el proyecto a otra ubicación o equipo sin necesidad de modificar rutas absolutas. Se asume que los archivos CSV se encuentran en una carpeta Transacciones ubicada en la raíz del proyecto.


### Estructura del Proyecto

RetoTecnicoCobol/
├── Models/
│   └── Transaction.cs         # Modelo que representa una transacción bancaria.
├── Services/
│   └── TransactionProcessor.cs  # Lógica para leer el CSV, validar datos y generar el reporte.
├── Transacciones/
│   └── data.csv 
│   └── data1.csv                # Archivo(s) CSV con las transacciones a procesar.
├── Program.cs                 # Punto de entrada de la aplicación CLI.
├── RetotecnicoCobol.csproj    # Archivo de proyecto de .NET.
└── README.md                  # Documentación del proyecto.


ESTE PROYECTO ESTA SUJETO A DERECHOS DE AUTOR, CUALQUIER USO SIN AUTORIZACION ESTA PROHIBIDO

## AUTOR

ZeroZenXion / Fabrizio Alessandro Capurro Villanueva


## LICENCIA

CODEABLE / INTERBANK
