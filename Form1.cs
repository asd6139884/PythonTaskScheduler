using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PythonTaskScheduler.Models;
using PythonTaskScheduler.Helpers;
using System.Diagnostics;


namespace PythonTaskScheduler
{
    public partial class PythonTaskScheduler : Form
    {
        private List<ScheduleInfo> schedules;
        private System.Windows.Forms.Timer timeUpdateTimer;  // 用於更新時間的計時器
        private System.Windows.Forms.Timer errorTimer; // 用於錯誤訊息計時器
        private Task backgroundTask; // 用於定期檢查排程計時器
        private SemaphoreSlim semaphore = new SemaphoreSlim(5); // 限制最多同時執行 5 個任務

        public PythonTaskScheduler()
        {
            InitializeComponent();
            Console.WriteLine("InitializeComponent() 成功");

            schedules = new List<ScheduleInfo>();
            //this.Load += new System.EventHandler(this.PythonTaskScheduler_Load); // 連接 Load 事件到 PythonTaskScheduler_Load 方法
            this.Load += PythonTaskScheduler_Load;

            // 初始化 timeUpdateTimer（用來更新目前時間）
            timeUpdateTimer = new System.Windows.Forms.Timer();
            timeUpdateTimer.Interval = 1000;  // 1 秒更新一次
            timeUpdateTimer.Tick += (s, e) =>
            {
                NowTime_ScheduleManagementPage.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // 更新 NowTime 顯示
                NowTime_SettingPage.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // 更新 NowTime 顯示
            };

            // 初始化 errorTimer（用來隱藏錯誤訊息）
            errorTimer = new System.Windows.Forms.Timer();
            errorTimer.Interval = 3000; // 設置為 3 秒
            errorTimer.Tick += (s, e) =>
            {
                ErrorMessage.Visible = false; // 隱藏錯誤訊息
                errorTimer.Stop(); // 停止計時器
            };
        }

        // 表單載入時執行
        private async void PythonTaskScheduler_Load(object sender, EventArgs e)
        {
            try
            {
                // 開始顯示時間更新
                timeUpdateTimer.Start();

                // 載入排程資料
                schedules = ScheduleDataManager.LoadSchedules();
                Console.WriteLine("Schedules資料讀取 成功");

                // 啟動非同步背景任務來檢查排程
                Console.WriteLine("檢查排程");
                backgroundTask = Task.Run(() => MonitorSchedules());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Schedules資料讀取失敗: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Schedules資料讀取 失敗");
                schedules = new List<ScheduleInfo>();
            }

            // 顯示排程資料
            DisplaySchedules();
            Console.WriteLine("顯示排程資料 成功");
        }

        private async void MonitorSchedules()
        {
            List<Task> tasks = new List<Task>(); // 儲存所有執行的任務

            while (true)
            {
                foreach (var schedule in schedules)
                {
                    //if (schedule.NextExecutionTime <= DateTime.Now)
                    //{
                    //    Console.WriteLine($"{schedule.Name}專案執行開始");
                    //    await ExecutePythonScriptAsync(schedule);
                    //    Console.WriteLine($"{schedule.Name}專案執行結束");
                    //}
                    if (schedule.NextExecutionTime <= DateTime.Now)
                    {
                        // 啟動一個新的任務來執行該 Python 腳本
                        var task = Task.Run(async () =>
                        {
                            await semaphore.WaitAsync(); // 確保不超過設定的最大併發數
                            try
                            {
                                Console.WriteLine($"{schedule.Name} 專案執行開始");
                                await ExecutePythonScriptAsync(schedule);
                                Console.WriteLine($"{schedule.Name} 專案執行結束");
                            }
                            finally
                            {
                                semaphore.Release(); // 任務完成後釋放
                            }
                        });

                        tasks.Add(task); // 將任務加入列表
                    }

                    // 等待 5 秒鐘後再次檢查
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }

        private async Task ExecutePythonScriptAsync(ScheduleInfo schedule)
        {
            try
            {
                // 執行Python脚本
                Console.WriteLine("確認腳本資訊");
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = schedule.PythonInterpreterPath,
                    Arguments = $"\"{schedule.PythonScriptPath}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                Console.WriteLine($"FileName:{startInfo.FileName}, Arguments:{startInfo.Arguments}");

                using (Process process = Process.Start(startInfo))
                {
                    Console.WriteLine("Process.Start");
                    await Task.Run(() => process.WaitForExit());
                    Console.WriteLine("Process.End");
                }

                // 更新排程狀態
                Console.WriteLine("更新排程資料 Start");
                schedule.ExecutionStatus = "成功";
                schedule.LastExecutionTime = DateTime.Now;
                schedule.NextExecutionTime = DateTime.Now.AddMinutes(schedule.ExecutionFrequency);

                ScheduleDataManager.SaveSchedules(schedules); // 儲存排程資料
                Console.WriteLine("更新排程資料 End");

                // 使用 Invoke 確保 UI 更新操作發送到 UI 執行緒
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        DisplaySchedules();  // 在 UI 執行緒中更新排程資料顯示
                        Console.WriteLine("在 UI 執行緒中更新排程資料顯示");
                    }));
                }
                else
                {
                    // 如果已經在 UI 執行緒中，直接更新
                    DisplaySchedules();
                    Console.WriteLine("直接更新排程資料顯示");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing script: {ex.Message}");
                schedule.ExecutionStatus = "失敗";
                ScheduleDataManager.SaveSchedules(schedules);

                // 使用 Invoke 確保 UI 更新操作發送到 UI 執行緒
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        DisplaySchedules();  // 在 UI 執行緒中更新排程資料顯示
                    }));
                }
                else
                {
                    // 如果已經在 UI 執行緒中，直接更新
                    DisplaySchedules();
                }
            }
        }




        //排程管理分頁----------------------------------------------------------
        // 顯示排程資料
        private void DisplaySchedules()
        {
            dataGridView1.Rows.Clear(); // 清空 DataGridView

            foreach (var schedule in schedules)
            {
                dataGridView1.Rows.Add(
                    schedule.Name,
                    string.IsNullOrEmpty(schedule.ExecutionStatus) ? "" : schedule.ExecutionStatus,
                    schedule.LastExecutionTime.HasValue ? schedule.LastExecutionTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    schedule.NextExecutionTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    string.IsNullOrEmpty(schedule.ExecutionStatus) ? "" : schedule.ExecutionStatus
                );
            }
        }

        //設定分頁----------------------------------------------------------
        // Python 解釋器路徑選擇的處理程式
        private void button_Select_Python_Interpreter_Click(object sender, EventArgs e)
        {
            // 呼叫 FileDialogHelper 的 SelectFile 方法
            FileDialogHelper.SelectFile("所有檔案 (*.*)|*.*", "選擇 Python 解釋器", Show_Python_Interpreter);
            Console.WriteLine("Python 解釋器路徑選擇 成功");
        }

        // Python 腳本路徑選擇的處理程式
        private void button_Select_Python_Script_Click(object sender, EventArgs e)
        {
            // 呼叫 FileDialogHelper 的 SelectFile 方法
            FileDialogHelper.SelectFile("Python 腳本 (*.py)|*.py|所有檔案 (*.*)|*.*", "選擇 Python 腳本", Show_Python_Script);
            Console.WriteLine("Python 腳本路徑選擇 成功");
        }

        // 顯示錯誤訊息
        private void ShowErrorMessage(List<string> errorMessages)
        {
            // 添加 "錯誤訊息:" 作為標題
            string header = "錯誤訊息:";

            // 將每條錯誤訊息加上編號
            for (int i = 0; i < errorMessages.Count; i++)
            {
                errorMessages[i] = $"{i + 1}. {errorMessages[i]}";
            }

            // 將錯誤訊息與標題組合，使用 Environment.NewLine 換行
            ErrorMessage.Text = header + Environment.NewLine + string.Join(Environment.NewLine, errorMessages);

            // textbox設置
            ErrorMessage.Size = new System.Drawing.Size(300, 80); // (寬,高)
            ErrorMessage.ForeColor = Color.Red;
            ErrorMessage.Multiline = true; // 確保 TextBox 支援多行文字
            ErrorMessage.Visible = true;

            // 啟動計時器，3 秒後隱藏錯誤訊息
            errorTimer.Start();
        }

        // 儲存按鈕的事件處理器
        private void Save_Button_Click(object sender, EventArgs e)
        {
            List<string> errorMessages = new List<string>();

            // 取得使用者輸入的資料
            string name = Input_Name.Text;
            string pythonInterpreterPath = Show_Python_Interpreter.Text;
            string pythonScriptPath = Show_Python_Script.Text;
            string selectedOption = Options_Execution_Freq.SelectedItem?.ToString();
            int intervalMinutes = 0;

            // 檢查欄位資訊
            var existingSchedules = ScheduleDataManager.LoadSchedules();
            if (existingSchedules.Any(schedule => schedule.Name.Equals(Input_Name.Text, StringComparison.OrdinalIgnoreCase))) // 檢查是否有相同的專案名稱
            {
                errorMessages.Add("專案名稱已經存在！");
            }

            if (string.IsNullOrWhiteSpace(Input_Name.Text))
            {
                errorMessages.Add("請填寫專案名稱！");
            }

            if (string.IsNullOrWhiteSpace(Show_Python_Interpreter.Text))
            {
                errorMessages.Add("請選擇 Python 執行檔路徑！");
            }

            if (string.IsNullOrWhiteSpace(Show_Python_Script.Text))
            {
                errorMessages.Add("請選擇 Python 腳本路徑！");
            }

            if (Options_Execution_Freq.SelectedItem == null)
            {
                errorMessages.Add("請選擇執行頻率！");
            }

            // 如果有錯誤訊息，顯示並停止儲存動作
            if (errorMessages.Count > 0)
            {
                ShowErrorMessage(errorMessages);
                return;
            }

            // 使用 TimeMapping 字典進行轉換
            if (!string.IsNullOrEmpty(selectedOption) && TimeMapping.TimeToMinutes.TryGetValue(selectedOption, out int minutes))
            {
                intervalMinutes = minutes;
                Console.WriteLine("執行頻率單位轉換成分鐘 成功");
            }
            Console.WriteLine("取得使用者輸入的資料 成功");

            // 建立新的排程資料
            var newSchedule = new ScheduleInfo
            {
                Name = name,
                PythonInterpreterPath = pythonInterpreterPath,
                PythonScriptPath = pythonScriptPath,
                ExecutionFrequency = intervalMinutes,  // 儲存為分鐘數
                CreatedAt = DateTime.Now,
                ExecutionStatus = "Pending",
                LastExecutionTime = null
            };
            Console.WriteLine("建立新的排程資料 成功");

            // 加入到排程列表中
            schedules.Add(newSchedule);
            Console.WriteLine("加入到排程列表中 成功");

            // 儲存到 JSON 檔案
            ScheduleDataManager.SaveSchedules(schedules);
            Console.WriteLine("儲存到 JSON 檔案 成功");

            // 返回排程管理頁面（假設是 tabControl 的某個頁面）
            TabControl1.SelectedTab = ScheduleManagementPage;
            Console.WriteLine("返回排程管理頁面 成功");

            // 更新顯示的資料
            DisplaySchedules();
            Console.WriteLine("更新顯示的資料 成功");

            MessageBox.Show("設定已儲存！");

            // 清除所有 TextBox 的內容
            ClearTextBoxes();
        }
        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(); // 清空設定頁面上的輸入欄位
            Console.WriteLine("清空設定頁面上的資訊 成功");

            TabControl1.SelectedTab = ScheduleManagementPage; // 返回排程管理頁面
            Console.WriteLine("返回排程管理頁面 成功");

            DisplaySchedules(); // 更新顯示的資料
            Console.WriteLine("更新顯示的資料 成功");
        }

        // 清除所有 TextBox 的內容
        private void ClearTextBoxes()
        {
            // 清空設定頁面上的輸入欄位
            Input_Name.Text = string.Empty;
            Show_Python_Interpreter.Text = string.Empty;
            Show_Python_Script.Text = string.Empty;
            Options_Execution_Freq.SelectedItem = "1天";  // 設定預設值
        }

        //沒做分類----------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ExecutionFrequency_TextChanged(object sender, EventArgs e)
        {

        }

        private void SettingPage_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void ScheduleManagementPage_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Input_Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void ErrorMessage_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
