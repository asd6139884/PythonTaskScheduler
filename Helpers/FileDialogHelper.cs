using System;
using System.Windows.Forms;

namespace PythonTaskScheduler.Helpers
{
    public static class FileDialogHelper
    {
        // 通用的檔案選擇方法
        public static void SelectFile(string filter, string title, TextBox targetTextBox)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = filter;  // 設定檔案過濾器
                openFileDialog.Title = title;    // 設定對話框標題

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    targetTextBox.Text = openFileDialog.FileName; // 將選擇的檔案路徑顯示在對應的 TextBox 中
                }
            }
        }
    }
}