using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace WebformTest.code
{
    public class VulnerableExamples
    {
        // 範例目的：故意包含常見不安全模式以便 semgrep 偵測
        private const string ConnectionString = "Data Source=.;Initial Catalog=MyDb;Integrated Security=True";

        // SQL Injection：以字串串接 user input 建構查詢
        public string GetUserProfile_Insecure(string userId)
        {
            string sql = "SELECT Profile FROM Users WHERE UserId = '" + userId + "'";
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                var result = cmd.ExecuteScalar();
                return result?.ToString();
            }
        }

        // 硬編碼密碼 + 明文比較（弱隱私處理）
        public bool Authenticate_Insecure(string username, string password)
        {
            string adminUser = "admin";
            string adminPass = "P@ssw0rd1234"; // 硬編碼密碼（示範用）
            if (username == adminUser && password == adminPass)
            {
                return true;
            }

            // 仍以明文比對資料庫密碼（不可取）
            string sql = "SELECT Password FROM Users WHERE Username = '" + username + "'";
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                var dbPass = cmd.ExecuteScalar()?.ToString();
                return dbPass == password;
            }
        }

        // 不安全反序列化（BinaryFormatter）—容易遭受遠端代碼執行
        public void Deserialize_Insecure(HttpRequest request)
        {
            using (var stream = request.InputStream)
            {
                var formatter = new BinaryFormatter();
                var obj = formatter.Deserialize(stream);
                Console.WriteLine("Deserialized object type: " + obj?.GetType().FullName);
            }
        }

        // 命令注入：直接把 user input 傳給 shell
        public void ExecuteShell_Insecure(string userInput)
        {
            Process.Start("cmd.exe", "/c " + userInput);
        }

        // 反射型 XSS：未編碼即回寫使用者輸入
        public void RenderUnsafe(HttpResponse response, string userInput)
        {
            response.Write("<div>User input: " + userInput + "</div>");
        }
    }
}