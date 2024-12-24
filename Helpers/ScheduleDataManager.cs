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

        // 儲存資料到 JSON 檔案
        //public static void SaveSchedules(List<ScheduleInfo> schedules)
        //{
        //    string json = JsonConvert.SerializeObject(schedules, Formatting.Indented);
        //    File.WriteAllText(filePath, json);
        //}

        public static void SaveSchedules(List<ScheduleInfo> schedules)
        {
            try
            {
                // 序列化物件為 JSON 字串
                string json = JsonConvert.SerializeObject(schedules, Formatting.Indented);

                //// 確保目錄存在
                //string directoryPath = Path.GetDirectoryName(filePath);
                //if (!Directory.Exists(directoryPath))
                //{
                //    Directory.CreateDirectory(directoryPath); // 如果不存在，創建目錄
                //}

                // 寫入 JSON 檔案
                File.WriteAllText(filePath, json);
                // 使用 FileStream 和 FileShare 來避免檔案鎖定
                //using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                //{
                //    using (StreamWriter writer = new StreamWriter(fs))
                //    {
                //        writer.Write(json);
                //    }
                //}
                Console.WriteLine("儲存排程資料成功");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"錯誤: 無法訪問檔案路徑。{ex.Message}");
                MessageBox.Show("無法儲存資料，請檢查檔案權限。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"錯誤: 輸入輸出錯誤。{ex.Message}");
                MessageBox.Show("儲存資料時發生錯誤，請檢查磁碟空間或檔案是否被鎖定。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // 捕獲其他未預期的錯誤
                Console.WriteLine($"錯誤: 發生未預期的錯誤。{ex.Message}");
                MessageBox.Show($"儲存資料時發生錯誤: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Console.WriteLine("Loaded JSON:");
            Console.WriteLine(json);

            var schedules = JsonConvert.DeserializeObject<List<ScheduleInfo>>(json);
            return schedules ?? new List<ScheduleInfo>();
        }
    }
}
