using MiniSupermarket.WinForms;
using MiniSupermarket.WinForms; // <-- THÊM DÒNG NÀY
using System;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms // Hoặc namespace mặc định của dự án bạn
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Chạy Form quản lý nhóm hàng
            Application.Run(new FormCategoryManagement());
        }
    }
}