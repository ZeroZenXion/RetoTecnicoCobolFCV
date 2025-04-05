using System;

namespace RetoTecnicoCobol.Models
{
    /// Representa una transacción bancaria.
    public class Transaction
    {
        /// Identificador único de la transacción.
        public int Id { get; set; }

        /// Tipo de transacción ("Crédito" o "Débito").

        public string Type { get; set; }


        /// Monto de la transacción.
        public decimal Amount { get; set; }
    }
}
