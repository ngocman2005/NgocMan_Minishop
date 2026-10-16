namespace MiniSupermarket.WinForms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Vòng lặp phiên làm việc: Login -> Shell -> (Đăng xuất) -> Login ...
        // Thoát hẳn khi người dùng đóng form Login hoặc đóng Shell bằng nút X.
        while (true)
        {
            using (var login = new FormLogin())
            {
                if (login.ShowDialog() != DialogResult.OK) break;
            }

            using var shell = new FormMainShell();
            Application.Run(shell);

            if (!shell.LogoutRequested) break;
        }
    }
}
