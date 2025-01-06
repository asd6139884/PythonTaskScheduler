using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PythonTaskScheduler.Models;
using PythonTaskScheduler.Helpers;
using System.Diagnostics;
using System.Threading.Tasks;


namespace PythonTaskScheduler
{
    public partial class PythonTaskScheduler : Form
    {
        private List<ScheduleInfo> schedules;
        private System.Windows.Forms.Timer timeUpdateTimer;  // 用於更新時間的計時器
        private System.Windows.Forms.Timer errorTimer; // 用於錯誤訊息計時器
        private Task backgroundTask; // 用於定期檢查排程計時器
        //private SemaphoreSlim semaphore = new SemaphoreSlim(5); // 限制最多同時執行 5 個任務

        public PythonTaskScheduler()
        {
            InitializeComponent();

            schedules = new List<ScheduleInfo>();
            //this.Load += new System.EventHandler(this.PythonTaskScheduler_Load); // 連接 Load 事件到 PythonTaskScheduler_Load 方法
            this.Load += PythonTaskScheduler_Load;
            dataGridView1.CellFormatting += DataGridView1_CellFormatting; // 註冊 CellFormatting 事件

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

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.CellStyle != null) // "執行狀態" 欄位
            {
                if (e.Value is string status)
                {
                    switch (status.ToLower())
                    {
                        case "執行完成":
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.Black;
                            break;
                        case "執行中":
                            e.CellStyle.BackColor = Color.Yellow;
                            e.CellStyle.ForeColor = Color.Black;
                            break;
                        case "執行失敗":
                            e.CellStyle.BackColor = Color.Red;
                            e.CellStyle.ForeColor = Color.Black;
                            break;
                        case "python失敗":
                            e.CellStyle.BackColor = Color.Red;
                            e.CellStyle.ForeColor = Color.Black;
                            break;
                        case "等待":
                            e.CellStyle.BackColor = Color.Gray;
                            e.CellStyle.ForeColor = Color.Black;
                            break;
                        default:
                            e.CellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                            e.CellStyle.ForeColor = dataGridView1.DefaultCellStyle.ForeColor;
                            break;
                    }
                }
            }
        }

        // 表單載入時執行
        private void PythonTaskScheduler_Load(object sender, EventArgs e)
        {
            try
            {
                // 開始顯示時間更新
                timeUpdateTimer.Start();

                // 讀取目前的排程資料
                var _ = ScheduleDataManager.LoadSchedules();

                // 更新狀態
                foreach (var schedule in _)
                {
                    if (schedule.ExecutionStatus == "執行中")
                    {
                        schedule.ExecutionStatus = "等待";
                    }
                }

                // 儲存更新後的排程資料
                ScheduleDataManager.SaveSchedules(_);

                // 載入排程資料
                schedules = ScheduleDataManager.LoadSchedules();

                // 啟動非同步背景任務來檢查排程
                backgroundTask = Task.Run(() => MonitorSchedules());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Schedules資料讀取失敗: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                schedules = new List<ScheduleInfo>();
            }

            // 顯示排程資料
            Invoke(new Action(() => DisplaySchedules()));
        }

        private async void MonitorSchedules()
        {
            while (true)
            {
                var now = DateTime.Now;
                var tasksToRun = schedules.Where(s => s.NextExecutionTime <= now && s.ExecutionStatus != "執行中").ToList();

                foreach (var schedule in tasksToRun)
                {
                    try
                    {
                        Console.WriteLine($"{schedule.Name} 執行中");
                        schedule.ExecutionStatus = "執行中"; // 設置狀態為 "執行中"
                        ScheduleDataManager.SaveSchedules(schedules); // 更新 JSON 檔案
                        Invoke(new Action(() => DisplaySchedules())); // 更新UI顯示

                        _ = Task.Run(async () =>
                        {
                            try
                            {
                                var startExecutionTime = DateTime.Now; // 記錄此次開始執行的時間

                                await ExecutePythonScriptAsync(schedule);

                                schedule.ExecutionStatus = "執行完成";
                                schedule.LastSuccessfulExecutionTime = DateTime.Now;
                                schedule.NextExecutionTime = startExecutionTime.AddMinutes(schedule.ExecutionFrequency);
                                ScheduleDataManager.SaveSchedules(schedules);
                                Invoke(new Action(() => DisplaySchedules()));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"捕捉例外: {ex.Message}");
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"捕捉例外: {ex.Message}");
                        schedule.ExecutionStatus = "執行失敗";
                        schedule.NextExecutionTime = DateTime.Now.AddMinutes(schedule.ExecutionFrequency);
                        ScheduleDataManager.SaveSchedules(schedules);
                        Invoke(new Action(() => DisplaySchedules()));
                    }
                }
                // 動態調整檢查間隔
                int delaySeconds = tasksToRun.Any() ? 1 : 10;
                await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
            }
        }

        private async Task ExecutePythonScriptAsync(ScheduleInfo schedule)
        {
            try
            {
                Console.WriteLine("Python開始執行");
                //throw new Exception("這是模擬錯誤，讓程式進入catch區塊");

                // 執行Python脚本
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = schedule.PythonInterpreterPath,
                    Arguments = $"\"{schedule.PythonScriptPath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true, // 確保重定向標準錯誤輸出
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    Console.WriteLine($"{schedule.Name} Process.Start");

                    // 讀取標準輸出和錯誤輸出
                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    await Task.Run(() => process.WaitForExit());
                    Console.WriteLine($"{schedule.Name} Process.End");

                    if (!string.IsNullOrEmpty(error))
                    {
                        throw new InvalidOperationException($"Python腳本執行錯誤: {error}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing script: {ex.Message}");
                schedule.ExecutionStatus = "Python失敗";
                schedule.NextExecutionTime = DateTime.Now.AddMinutes(schedule.ExecutionFrequency);
                ScheduleDataManager.SaveSchedules(schedules); // 儲存排程資料並顯示
                Invoke(new Action(() => DisplaySchedules())); // 更新UI顯示
                throw; // 如果需要外部捕捉，重新拋出例外
            }
        }

        //排程管理分頁----------------------------------------------------------
        private void UpdateUI(Action updateAction)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(updateAction);
            }
            else
            {
                updateAction();
            }
        }
        // 顯示排程資料
        private void DisplaySchedules()
        {
            UpdateUI(() =>
            {
                dataGridView1.Rows.Clear(); // 清空 DataGridView
                foreach (var schedule in schedules)
                {
                    var newRow = dataGridView1.Rows[dataGridView1.Rows.Add()];

                    // 設定每個欄位的值
                    newRow.Cells["專案名稱"].Value = schedule.Name;
                    newRow.Cells["執行狀態"].Value = schedule.ExecutionStatus;
                    newRow.Cells["上次執行成功時間"].Value = schedule.LastSuccessfulExecutionTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "";
                    newRow.Cells["下次執行時間"].Value = schedule.NextExecutionTime.ToString("yyyy-MM-dd HH:mm:ss");

                    // 設定圖片欄位的值
                    newRow.Cells["編輯"].Value = Image.FromFile("./icon/Edit.png"); // 設定「編輯」欄位的圖片
                    newRow.Cells["立即執行"].Value = Image.FromFile("./icon/Start.jpg"); // 設定「立即執行」欄位的圖片
                }
            });
        }

        // 編輯icon的事件處理器
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 判斷是否點擊到「編輯」按鈕
                if (e.ColumnIndex == dataGridView1.Columns["編輯"].Index)
                {
                    // 取得選中的 ScheduleInfo
                    var selectedSchedule = schedules[e.RowIndex];

                    // 創建 EditForm 並傳遞選中的排程資料
                    EditForm editForm = new EditForm(selectedSchedule);
                    editForm.ShowDialog();

                    schedules = ScheduleDataManager.LoadSchedules(); // 重新載入最新資料
                    DisplaySchedules(); // 修改資料後，更新 DataGridView 顯示
                }

                // 判斷是否點擊到「立即執行」按鈕
                if (e.ColumnIndex == dataGridView1.Columns["立即執行"].Index)
                {
                    var schedule = schedules[e.RowIndex];
                    // 取得選中的專案名稱
                    try
                    {
                        schedule.ExecutionStatus = "執行中"; // 設置狀態為 "執行中"
                        ScheduleDataManager.SaveSchedules(schedules); // 更新 JSON 檔案
                        Invoke(new Action(() => DisplaySchedules())); // 更新UI顯示

                        _ = Task.Run(async () =>
                        {
                            try
                            {
                                var startExecutionTime = DateTime.Now; // 記錄此次開始執行的時間

                                await ExecutePythonScriptAsync(schedule);

                                schedule.ExecutionStatus = "執行完成";
                                schedule.LastSuccessfulExecutionTime = DateTime.Now;
                                schedule.NextExecutionTime = startExecutionTime.AddMinutes(schedule.ExecutionFrequency);
                                ScheduleDataManager.SaveSchedules(schedules);
                                Invoke(new Action(() => DisplaySchedules()));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"捕捉例外: {ex.Message}");
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"捕捉例外: {ex.Message}");
                        schedule.ExecutionStatus = "執行失敗";
                        schedule.NextExecutionTime = DateTime.Now.AddMinutes(schedule.ExecutionFrequency);
                        ScheduleDataManager.SaveSchedules(schedules);
                        Invoke(new Action(() => DisplaySchedules()));
                    }

                }
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
            }

            // 建立新的排程資料
            var newSchedule = new ScheduleInfo
            {
                Name = name,
                PythonInterpreterPath = pythonInterpreterPath,
                PythonScriptPath = pythonScriptPath,
                ExecutionFrequency = intervalMinutes,  // 儲存為分鐘數
                CreatedAt = DateTime.Now,
                ExecutionStatus = "等待",
                LastSuccessfulExecutionTime = null
            };
            Console.WriteLine("建立新的排程資料 成功");

            // 加入到排程列表中
            schedules.Add(newSchedule);

            // 儲存到 JSON 檔案
            ScheduleDataManager.SaveSchedules(schedules);

            // 返回排程管理頁面（假設是 tabControl 的某個頁面）
            TabControl1.SelectedTab = ScheduleManagementPage;

            // 更新顯示的資料
            DisplaySchedules();

            MessageBox.Show("設定已儲存！");

            // 清除所有 TextBox 的內容
            ClearTextBoxes();
        }
        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(); // 清空設定頁面上的輸入欄位
            TabControl1.SelectedTab = ScheduleManagementPage; // 返回排程管理頁面
            DisplaySchedules(); // 更新顯示的資料
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
