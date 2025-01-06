using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using PythonTaskScheduler.Helpers;
using PythonTaskScheduler.Models;


namespace PythonTaskScheduler
{
    public partial class EditForm : Form
    {
        private ScheduleInfo _schedule;
        private readonly string _originalName; // 保存原始名稱

        public EditForm(ScheduleInfo schedule)
        {
            InitializeComponent();
            _schedule = schedule;
            _originalName = schedule.Name; // 保存原始名稱用於後續查找

            // 預設顯示排程資料
            ProjectName.Text = _schedule.Name;
            Python_Interpreter.Text = _schedule.PythonInterpreterPath;
            Python_Script.Text = _schedule.PythonScriptPath;
            Options_Execution_Freq.Text = _schedule.ExecutionFrequency.ToString();
            CreatedAt.Text = _schedule.CreatedAt.ToString();
            ExecutionStatus.Text = _schedule.ExecutionStatus;
            LastSuccessfulExecutionTime.Text = _schedule.LastSuccessfulExecutionTime?.ToString();
            NextExecutionTime.Text = _schedule.NextExecutionTime.ToString();

            // 設定預設選項為當前的 ExecutionFrequency
            SetDefaultExecutionFrequency();
        }
        private void SetDefaultExecutionFrequency()
        {
            // 根據 ExecutionFrequency 設定 ComboBox 的選擇
            foreach (var time in TimeMapping.TimeToMinutes)
            {
                if (time.Value == _schedule.ExecutionFrequency)
                {
                    Options_Execution_Freq.SelectedItem = time.Key;
                    break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 檢查必填欄位
            if (string.IsNullOrWhiteSpace(ProjectName.Text))
            {
                MessageBox.Show("專案名稱是必填的！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // 如果名稱為空，終止保存
            }
            if (string.IsNullOrWhiteSpace(Python_Interpreter.Text) || string.IsNullOrWhiteSpace(Python_Script.Text))
            {
                MessageBox.Show("Python執行檔路徑和腳本路徑不能為空！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 更新資料
                _schedule.Name = ProjectName.Text;
                _schedule.PythonInterpreterPath = Python_Interpreter.Text;
                _schedule.PythonScriptPath = Python_Script.Text;
                _schedule.ExecutionStatus = ExecutionStatus.Text;
                _schedule.LastSuccessfulExecutionTime = DateTime.TryParse(LastSuccessfulExecutionTime.Text, out DateTime lastTime) ? (DateTime?)lastTime : null;
                _schedule.NextExecutionTime = DateTime.TryParse(NextExecutionTime.Text, out DateTime nextTime) ? nextTime : _schedule.NextExecutionTime;

                // 解析並更新執行頻率
                var selectedTime = Options_Execution_Freq.SelectedItem as string;
                if (selectedTime != null && TimeMapping.TimeToMinutes.TryGetValue(selectedTime, out int frequencyInMinutes))
                {
                    _schedule.ExecutionFrequency = frequencyInMinutes;
                }


                var schedules = ScheduleDataManager.LoadSchedules(); // 讀取當前的排程資料
                var index = schedules.FindIndex(s => s.Name == _originalName); // 找到並更新對應的排程資料
                if (index >= 0)
                {
                    schedules[index] = _schedule; // 更新排程資料
                }
                else
                {
                    throw new Exception("找不到原始排程資料");
                }

                ScheduleDataManager.SaveSchedules(schedules); // 儲存更新過的排程資料回 schedules.json
                MessageBox.Show("資料已儲存!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information); // 顯示儲存成功訊息
                Close(); // 關閉小視窗
            }
            catch (Exception ex)
            {
                MessageBox.Show($"儲存資料時發生錯誤: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 關閉編輯頁面
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 顯示確認訊息
            var confirmResult = MessageBox.Show(
                $"確定要刪除專案 '{_schedule.Name}' 嗎？",
                "確認刪除",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // 讀取當前的排程資料
                    var schedules = ScheduleDataManager.LoadSchedules();

                    // 根據 Name 查找並移除資料
                    var scheduleToRemove = schedules.FirstOrDefault(s => s.Name == _schedule.Name);
                    if (scheduleToRemove != null)
                    {
                        schedules.Remove(scheduleToRemove);

                        ScheduleDataManager.SaveSchedules(schedules); // 儲存更新過的排程資料回 schedules.json
                        MessageBox.Show("資料已成功刪除!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information); // 顯示刪除成功訊息
                        Close(); // 關閉小視窗
                    }
                    else
                    {
                        MessageBox.Show("未找到對應的排程資料。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"刪除資料時發生錯誤: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // Python 解釋器路徑選擇的處理程式
        private void button_Select_Python_Interpreter_Click(object sender, EventArgs e)
        {
            // 呼叫 FileDialogHelper 的 SelectFile 方法
            FileDialogHelper.SelectFile("所有檔案 (*.*)|*.*", "選擇 Python 解釋器", Python_Interpreter);
            Console.WriteLine("Python 解釋器路徑選擇 成功");
        }

        // Python 腳本路徑選擇的處理程式
        private void button_Select_Python_Script_Click(object sender, EventArgs e)
        {
            // 呼叫 FileDialogHelper 的 SelectFile 方法
            FileDialogHelper.SelectFile("Python 腳本 (*.py)|*.py|所有檔案 (*.*)|*.*", "選擇 Python 腳本", Python_Script);
            Console.WriteLine("Python 腳本路徑選擇 成功");
        }

        private void Python_Script_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
