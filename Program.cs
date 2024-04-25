using System.Globalization;

namespace Sistema_de_Gerenciamento_de_Conferências
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            CultureInfo culture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;

            Application.Run(new Form1());
        }
    }
}