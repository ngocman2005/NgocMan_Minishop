namespace MiniSupermarket.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Thay đổi Form khởi chạy đầu tiên là FormLogin thay vì FormCategoryManagement
            Application.Run(new FormLogin());
        }
    }
}
