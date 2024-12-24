namespace PythonTaskScheduler.Models
{
    public class ScheduleInfo
    {
        public string Name { get; set; } = string.Empty; // 專案名稱
        public string PythonInterpreterPath { get; set; } = string.Empty; // Python執行檔路徑+檔名
        public string PythonScriptPath { get; set; } = string.Empty; // Python腳本路徑+檔名
        public int ExecutionFrequency { get; set; } = 1440; // 執行頻率
        public DateTime CreatedAt { get; set; } = DateTime.Now; // 建立時間
        public string ExecutionStatus { get; set; } = string.Empty; // 執行狀態（可為空）
        public DateTime? LastExecutionTime { get; set; } // 上次執行時間（可為空）
        public DateTime NextExecutionTime { get; set; }
        // 構造函數
        public ScheduleInfo()
        {
            NextExecutionTime = CreatedAt.AddMinutes(1); // 預設 NextExecutionTime 為 CreatedAt 加 1 分鐘
        }
    }
}
