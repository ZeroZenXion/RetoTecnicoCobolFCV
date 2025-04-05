using System;
using System.IO;
using RetoTecnicoCobol.Services;

namespace RetoTecnicoCobol
{
    class Program
    {
        static void Main(string[] args)
        {
            // Intentamos determinar la raíz del proyecto asumiendo la estructura:
            // [ProjectRoot]\bin\Debug\net7.0
            string? projectRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
            if (string.IsNullOrEmpty(projectRoot))
            {
                Console.WriteLine("No se pudo determinar la raíz del proyecto.");
                return;
            }

            // Construimos la ruta a la carpeta "Transacciones" ubicada en la raíz del proyecto.
            string transaccionesFolder = Path.Combine(projectRoot, "Transacciones");

            // Variable para almacenar la ruta del archivo CSV a usar.
            string csvPath = "";

            if (args.Length > 0)
            {
                // Si se pasó un argumento, se usa esa ruta.
                csvPath = args[0];
            }
            else if (Directory.Exists(transaccionesFolder))
            {
                // Buscamos todos los archivos CSV en la carpeta Transacciones.
                string[] csvFiles = Directory.GetFiles(transaccionesFolder, "*.csv");

                if (csvFiles.Length == 1)
                {
                    // Si solo hay un archivo CSV, lo usamos.
                    csvPath = csvFiles[0];
                }
                else if (csvFiles.Length > 1)
                {
                    // Si hay múltiples archivos CSV, se muestra un menú para que el usuario seleccione.
                    Console.WriteLine("Se encontraron múltiples archivos CSV en la carpeta 'Transacciones':");
                    for (int i = 0; i < csvFiles.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}: {Path.GetFileName(csvFiles[i])}");
                    }
                    Console.Write("Seleccione el número del archivo que desea leer: ");
                    string? input = Console.ReadLine();
                    if (int.TryParse(input, out int index) && index > 0 && index <= csvFiles.Length)
                    {
                        csvPath = csvFiles[index - 1];
                    }
                    else
                    {
                        Console.WriteLine("Selección inválida. Saliendo.");
                        return;
                    }
                }
                else
                {
                    // Si no se encontraron archivos CSV, se intenta usar "data.csv" dentro de Transacciones.
                    csvPath = Path.Combine(transaccionesFolder, "data.csv");
                }
            }
            else
            {
                // Si la carpeta "Transacciones" no existe, se busca "data.csv" en la raíz del proyecto.
                csvPath = Path.Combine(projectRoot, "data.csv");
            }

            // Verificamos si el archivo CSV existe.
            if (!File.Exists(csvPath))
            {
                Console.WriteLine($"El archivo '{csvPath}' no se encontró.");
                return;
            }

            // Procesamos el archivo CSV utilizando TransactionProcessor.
            TransactionProcessor processor = new TransactionProcessor();
            processor.LoadTransactions(csvPath);

            // Calculamos los resultados.
            decimal finalBalance = processor.GetFinalBalance();
            var highestTransaction = processor.GetHighestTransaction();
            var (creditCount, debitCount) = processor.GetTransactionCounts();

            // Mostramos el reporte en la terminal.
            Console.WriteLine("Reporte de Transacciones");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Balance Final: {finalBalance:F2}");
            if (highestTransaction != null)
            {
                Console.WriteLine($"Transacción de Mayor Monto: ID {highestTransaction.Id} - {highestTransaction.Amount:F2}");
            }
            else
            {
                Console.WriteLine("No se encontró ninguna transacción para determinar la de mayor monto.");
            }
            Console.WriteLine($"Conteo de Transacciones: Crédito: {creditCount} Débito: {debitCount}");
        }
    }
}
