using System;
using System.Collections.Generic;
using ProyectoTicketTurno.Business.Models;

using System.Text.RegularExpressions;

namespace ProyectoTicketTurno.Infrastructure.Utilities
{
    public static class FormatoCURPHelper
    {
        private static readonly Dictionary<string, string> AbreviaturasPorEstado = new Dictionary<string, string>
        {
            { "Aguascalientes", "AGS" },
            { "Baja California", "BC" },
            { "Baja California Sur", "BCS" },
            { "Campeche", "CAMP" },
            { "Coahuila", "CL" },
            { "Colima", "COL" },
            { "Chiapas", "CHIS" },
            { "Chihuahua", "CHIH" },
            { "Ciudad de México", "CDMX" },
            { "Durango", "DGO" },
            { "Guanajuato", "GTO" },
            { "Guerrero", "GRO" },
            { "Hidalgo", "HGO" },
            { "Jalisco", "JAL" },
            { "México", "MEX" },
            { "Michoacán", "MICH" },
            { "Morelos", "MOR" },
            { "Nayarit", "NAY" },
            { "Nuevo León", "NL" },
            { "Oaxaca", "OAX" },
            { "Puebla", "PUE" },
            { "Querétaro", "QRO" },
            { "Quintana Roo", "QROO" },
            { "San Luis Potosí", "SLP" },
            { "Sinaloa", "SIN" },
            { "Sonora", "SON" },
            { "Tabasco", "TAB" },
            { "Tamaulipas", "TAMS" },
            { "Tlaxcala", "TLAX" },
            { "Veracruz", "VER" },
            { "Yucatán", "YUC" },
            { "Zacatecas", "ZAC" }
        };

        /// <summary>
        /// Genera un CURP basado en los datos del estudiante
        /// </summary>
        public static string GenerarCURP(string nombre, string apellidoPaterno, string apellidoMaterno,
            DateTime fechaNacimiento, char sexo, string abreviaturaEstado)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidoPaterno) ||
                string.IsNullOrWhiteSpace(apellidoMaterno))
                return string.Empty;

            // Primeras 4 letras: 1ª del apellido paterno, 1ª del materno, 1ª del nombre, 1ª consonante del apellido paterno
            string parte1 = apellidoPaterno.Substring(0, 1) +
                           apellidoMaterno.Substring(0, 1) +
                           nombre.Substring(0, 1) +
                           ObtenerPrimeraConsonante(apellidoPaterno);

            // Fecha: YYMMDD
            string parte2 = fechaNacimiento.ToString("yyMMdd");

            // Sexo: H o M
            string parte3 = sexo.ToString().ToUpper();

            // Abreviatura del estado (2 caracteres)
            string parte4 = abreviaturaEstado.Length >= 2 ? abreviaturaEstado.Substring(0, 2) : "XX";

            // Últimas 3 letras: consonantes del apellido paterno, materno y nombre
            string consonante1 = ObtenerPrimeraConsonante(apellidoPaterno);
            string consonante2 = ObtenerPrimeraConsonante(apellidoMaterno);
            string consonante3 = ObtenerPrimeraConsonante(nombre);
            string parte5 = (consonante1 + consonante2 + consonante3).ToUpper();

            // Últimos 2 caracteres: secuencial (00-99)
            string parte6 = "01";

            return (parte1 + parte2 + parte3 + parte4 + parte5 + parte6).ToUpper();
        }

        /// <summary>
        /// Valida el formato del CURP (18 caracteres, formato válido)
        /// </summary>
        public static bool ValidarFormatoCURP(string curp)
        {
            if (string.IsNullOrWhiteSpace(curp) || curp.Length != 18)
                return false;

            return Regex.IsMatch(curp, @"^[A-Z]{4}\d{6}[HM][A-Z]{2}[A-Z]{3}[A-Z0-9]{2}$");
        }

        /// <summary>
        /// Valida el CURP completo (formato + coherencia con datos personales)
        /// </summary>
        public static bool ValidarFormatoCURP(string curp, string nombre, string apellidoPaterno,
            string apellidoMaterno, DateTime fechaNacimiento, char sexo, string abreviaturaEstado)
        {
            // Validar formato básico
            if (!ValidarFormatoCURP(curp))
                return false;

            // Generar CURP esperado
            string curpEsperado = GenerarCURP(nombre, apellidoPaterno, apellidoMaterno,
                fechaNacimiento, sexo, abreviaturaEstado);

            // Comparar (sin considerar los últimos 2 dígitos que son secuenciales)
            return curp.Substring(0, 16) == curpEsperado.Substring(0, 16);
        }

        /// <summary>
        /// Obtiene la primera consonante de una palabra
        /// </summary>
        private static string ObtenerPrimeraConsonante(string palabra)
        {
            if (string.IsNullOrWhiteSpace(palabra))
                return "X";

            string vocales = "AEIOUÁÉÍÓÚ";
            foreach (char c in palabra.ToUpper())
            {
                if (!vocales.Contains(c.ToString()))
                    return c.ToString();
            }

            return "X";
        }

        /// <summary>
        /// Obtiene la abreviatura de un estado
        /// </summary>
        public static string ObtenerAbreviaturaPorEstado(string estado)
        {
            return AbreviaturasPorEstado.TryGetValue(estado, out var abreviatura) ? abreviatura : "XX";
        }

        /// <summary>
        /// Formatea el CURP con guiones para legibilidad
        /// </summary>
        public static string FormattearCURP(string curp)
        {
            if (string.IsNullOrWhiteSpace(curp) || curp.Length != 18)
                return curp;

            return $"{curp.Substring(0, 4)}-{curp.Substring(4, 6)}-{curp.Substring(10, 3)}-{curp.Substring(13)}";
        }
    }
}