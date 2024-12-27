using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PythonTaskScheduler.Models;

namespace PythonTaskScheduler.Helpers
{
    public static class ScheduleDataManager
    {
        private static readonly string filePath = "schedules.json"; // JSON 檔案的預設路徑

        public static void SaveSchedules(List<ScheduleInfo> schedules)
        {
            try
            {
                // 序列化物件為 JSON 字串
                string json = JsonConvert.SerializeObject(schedules, Formatting.Indented);

                // 確保目錄存在
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // 使用 FileStream 和 StreamWriter 進行安全寫入
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"儲存資料失敗: {ex.Message}");
                throw; // 保持例外以便調試
            }
        }





        // 從 JSON 檔案讀取資料
        public static List<ScheduleInfo> LoadSchedules()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {Path.GetFullPath(filePath)}");
                return new List<ScheduleInfo>(); // 如果檔案不存在，回傳空列表
            }
            string json = File.ReadAllText(filePath);

            var schedules = JsonConvert.DeserializeObject<List<ScheduleInfo>>(json);
            return schedules ?? new List<ScheduleInfo>();
        }
    }
}
