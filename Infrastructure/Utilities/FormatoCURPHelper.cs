using System;
using System.Collections.Generic;
using ProyectoTicketTurno.Business.Models;

using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ProyectoTicketTurno.Infrastructure.Utilities
{
    public static class FormatoCURPHelper
    {
        private static readonly Dictionary<string, string> AbreviaturasPorEstado = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "AGUASCALIENTES", "AS" }, { "BAJA CALIFORNIA", "BC" }, { "BAJA CALIFORNIA SUR", "BS" },
            { "CAMPECHE", "CC" }, { "COAHUILA", "CL" }, { "COLIMA", "CM" }, { "CHIAPAS", "CS" },
            { "CHIHUAHUA", "CH" }, { "CIUDAD DE MEXICO", "DF" }, { "DISTRITO FEDERAL", "DF" },
            { "DURANGO", "DG" }, { "GUANAJUATO", "GT" }, { "GUERRERO", "GR" }, { "HIDALGO", "HG" },
            { "JALISCO", "JC" }, { "MEXICO", "MC" }, { "MICHOACAN", "MN" }, { "MORELOS", "MS" },
            { "NAYARIT", "NT" }, { "NUEVO LEON", "NL" }, { "OAXACA", "OC" }, { "PUEBLA", "PL" },
            { "QUERETARO", "QT" }, { "QUINTANA ROO", "QR" }, { "SAN LUIS POTOSI", "SP" },
            { "SINALOA", "SL" }, { "SONORA", "SR" }, { "TABASCO", "TC" }, { "TAMAULIPAS", "TS" },
            { "TLAXCALA", "TL" }, { "VERACRUZ", "VZ" }, { "YUCATAN", "YN" }, { "ZACATECAS", "ZS" },
            { "NACIDO EN EL EXTRANJERO", "NE" }, { "EXTRANJERO", "NE" }
        };

        private static readonly HashSet<string> EstadosValidos = new HashSet<string>(AbreviaturasPorEstado.Values);

        public static string GenerarCURP(
            string nombre,
            string apellidoPaterno,
            string apellidoMaterno,
            DateTime fechaNacimiento,
            char sexo,
            string abreviaturaEstado)
        {
            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(apellidoPaterno) ||
                string.IsNullOrWhiteSpace(apellidoMaterno))
            {
                return string.Empty;
            }

            string n = Limpiar(nombre);
            string ap = Limpiar(apellidoPaterno);
            string am = Limpiar(apellidoMaterno);
            string estado = Limpiar(abreviaturaEstado);
            char sx = char.ToUpperInvariant(sexo);

            if (n.Length == 0 || ap.Length == 0 || am.Length == 0)
                return string.Empty;

            if (sx != 'H' && sx != 'M')
                return string.Empty;

            if (estado.Length != 2)
                estado = "NE";

            string parte1 = $"{ap[0]}{ObtenerPrimeraVocalInterna(ap)}{am[0]}{n[0]}";
            string parte2 = fechaNacimiento.ToString("yyMMdd");
            string parte3 = sx.ToString();
            string parte4 = estado;
            string parte5 = $"{ObtenerPrimeraConsonanteInterna(ap)}{ObtenerPrimeraConsonanteInterna(am)}{ObtenerPrimeraConsonanteInterna(n)}";
            string parte6 = GenerarHomoclaveBasica();

            return (parte1 + parte2 + parte3 + parte4 + parte5 + parte6).ToUpperInvariant();
        }

        public static bool ValidarFormatoCURP(string curp)
        {
            if (string.IsNullOrWhiteSpace(curp))
                return false;

            string valor = curp.Trim().ToUpperInvariant();

            if (valor.Length != 18)
                return false;

            bool estructuraValida = Regex.IsMatch(
                valor,
                @"^[A-Z][AEIOUX][A-Z]{2}\d{6}[HM][A-Z]{2}[B-DF-HJ-NP-TV-Z]{3}[A-Z0-9]{2}$");

            if (!estructuraValida)
                return false;

            string entidad = valor.Substring(11, 2);
            return EstadosValidos.Contains(entidad);
        }

        public static bool ValidarFormatoCURP(
            string curp,
            string nombre,
            string apellidoPaterno,
            string apellidoMaterno,
            DateTime fechaNacimiento,
            char sexo,
            string abreviaturaEstado)
        {
            if (!ValidarFormatoCURP(curp))
                return false;

            string esperado = GenerarCURP(nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento, sexo, abreviaturaEstado);
            if (string.IsNullOrWhiteSpace(esperado) || esperado.Length != 18)
                return false;

            string actual = curp.Trim().ToUpperInvariant();

            // Compara primeros 16 (sin homoclave final aleatoria)
            return actual.Substring(0, 16) == esperado.Substring(0, 16);
        }

        public static string ObtenerAbreviaturaPorEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return "NE";

            string limpio = Limpiar(estado);
            return AbreviaturasPorEstado.TryGetValue(limpio, out var abreviatura) ? abreviatura : "NE";
        }

        // Mantengo el nombre actual para no romper llamadas existentes
        public static string FormattearCURP(string curp)
        {
            if (string.IsNullOrWhiteSpace(curp) || curp.Length != 18)
                return curp;

            string valor = curp.ToUpperInvariant();
            return $"{valor.Substring(0, 4)}-{valor.Substring(4, 6)}-{valor.Substring(10, 3)}-{valor.Substring(13, 5)}";
        }

        private static string Limpiar(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return string.Empty;

            string normalizado = valor.Trim().ToUpperInvariant().Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in normalizado)
            {
                var cat = CharUnicodeInfo.GetUnicodeCategory(c);
                if (cat != UnicodeCategory.NonSpacingMark && (char.IsLetterOrDigit(c) || c == ' '))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        private static char ObtenerPrimeraVocalInterna(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto) || texto.Length < 2)
                return 'X';

            for (int i = 1; i < texto.Length; i++)
            {
                char c = texto[i];
                if ("AEIOU".Contains(c))
                    return c;
            }

            return 'X';
        }

        private static char ObtenerPrimeraConsonanteInterna(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto) || texto.Length < 2)
                return 'X';

            for (int i = 1; i < texto.Length; i++)
            {
                char c = texto[i];
                if (char.IsLetter(c) && !"AEIOU".Contains(c))
                    return c;
            }

            return 'X';
        }

        private static string GenerarHomoclaveBasica()
        {
            const string alfanumerico = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Range(0, 2)
                .Select(_ => alfanumerico[random.Next(alfanumerico.Length)])
                .ToArray());
        }
    }
}