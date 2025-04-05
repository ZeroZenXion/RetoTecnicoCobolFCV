using RetoTecnicoCobol.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RetoTecnicoCobol.Services
{
    /// Encapsula la lógica para procesar transacciones bancarias a partir de un archivo CSV.
    public class TransactionProcessor
    {
        /// Lista de transacciones cargadas.
        public List<Transaction> Transactions { get; private set; } = new List<Transaction>();

        /// 
        /// Carga las transacciones desde un archivo CSV.
        /// Se espera que el CSV tenga el formato: id,tipo,monto
        /// Valida que no falte ningún dato en cada fila.
        ///
        /// <param name="filePath">Ruta del archivo CSV</param>
        public void LoadTransactions(string filePath)
        {
            // Leer todas las líneas del archivo
            var lines = File.ReadAllLines(filePath);

            // Se asume que la primera línea contiene los encabezados
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var parts = line.Split(',');

                // Validar que la línea tenga 3 columnas
                if (parts.Length != 3)
                {
                    Console.WriteLine($"Línea {i + 1} con formato incorrecto (se esperaban 3 columnas). Se omitirá.");
                    continue;
                }

                // Trim de cada parte
                var idText = parts[0].Trim();
                var typeText = parts[1].Trim();
                var amountText = parts[2].Trim();

                // Validar que ningún campo esté vacío
                bool missingField = false;
                string missingInfo = "";

                if (string.IsNullOrEmpty(idText))
                {
                    missingField = true;
                    missingInfo += " ID";
                }
                if (string.IsNullOrEmpty(typeText))
                {
                    missingField = true;
                    missingInfo += " Tipo";
                }
                if (string.IsNullOrEmpty(amountText))
                {
                    missingField = true;
                    missingInfo += " Monto";
                }

                if (missingField)
                {
                    // Si tenemos ID, lo mostramos; si no, se indica la fila.
                    if (!string.IsNullOrEmpty(idText))
                    {
                        Console.WriteLine($"La transacción con ID {idText} tiene datos incompletos: faltan{missingInfo}.");
                    }
                    else
                    {
                        Console.WriteLine($"La fila {i + 1} tiene datos incompletos: faltan{missingInfo}.");
                    }
                    // Se omite la fila y se continúa con la siguiente.
                    continue;
                }

                // Intentar parsear el ID y el Monto
                int id;
                decimal amount;
                try
                {
                    id = int.Parse(idText);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al convertir el ID en la fila {i + 1} ('{idText}'): {ex.Message}. Se omitirá esta transacción.");
                    continue;
                }

                try
                {
                    amount = decimal.Parse(amountText, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al convertir el monto en la transacción con ID {id}: {ex.Message}. Se omitirá esta transacción.");
                    continue;
                }

                // Crear el objeto Transaction y agregarlo a la lista
                var transaction = new Transaction
                {
                    Id = id,
                    Type = typeText,
                    Amount = amount
                };

                Transactions.Add(transaction);
            }
        }

        /// 
        /// Calcula el balance final: suma de montos de transacciones de "Crédito" menos los de "Débito".
        /// 
        /// <returns>El balance final.</returns>
        public decimal GetFinalBalance()
        {
            decimal totalCredit = Transactions
                .Where(t => t.Type.Equals("Crédito", StringComparison.OrdinalIgnoreCase) || t.Type.Equals("Credito", StringComparison.OrdinalIgnoreCase))
                .Sum(t => t.Amount);

            decimal totalDebit = Transactions
                .Where(t => t.Type.Equals("Débito", StringComparison.OrdinalIgnoreCase) || t.Type.Equals("Debito", StringComparison.OrdinalIgnoreCase))
                .Sum(t => t.Amount);

            return totalCredit - totalDebit;
        }

        /// 
        /// Obtiene la transacción con el monto más alto.
        /// 
        /// <returns>La transacción de mayor monto o null si no hay transacciones.</returns>
        public Transaction? GetHighestTransaction()
        {
            return Transactions.OrderByDescending(t => t.Amount).FirstOrDefault();
        }

        /// 
        /// Cuenta el número de transacciones por tipo (Crédito y Débito).
        /// 
        /// <returns>Una tupla con el conteo de créditos y débitos.</returns>
        public (int CreditCount, int DebitCount) GetTransactionCounts()
        {
            int creditCount = Transactions.Count(t => t.Type.Equals("Crédito", StringComparison.OrdinalIgnoreCase) || t.Type.Equals("Credito", StringComparison.OrdinalIgnoreCase));
            int debitCount = Transactions.Count(t => t.Type.Equals("Débito", StringComparison.OrdinalIgnoreCase) || t.Type.Equals("Debito", StringComparison.OrdinalIgnoreCase));

            return (creditCount, debitCount);
        }
    }
}
