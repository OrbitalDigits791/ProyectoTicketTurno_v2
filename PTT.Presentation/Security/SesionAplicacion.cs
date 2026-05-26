namespace PTT.Presentation.Security
{
    public static class SesionAplicacion
    {
        public static string UsuarioActual { get; private set; }
        public static bool SesionActiva => !string.IsNullOrWhiteSpace(UsuarioActual);

        public static void IniciarSesion(string usuario)
        {
            UsuarioActual = usuario;
        }

        public static void CerrarSesion()
        {
            UsuarioActual = null;
        }
    }