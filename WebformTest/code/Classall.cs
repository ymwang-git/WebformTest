using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Firstbankvirusreport.code
{
    public class Classall
    {
        public string DbConn() // DB連線字串
        {
            string conn = ConfigurationManager.ConnectionStrings["METAREPORTEntities"].ConnectionString;
            conn = DecryptIfBase64(conn);
            //conn = Encoding.UTF8.GetString(Convert.FromBase64String(conn));
            return conn;
        }

        public static string DecryptIfBase64(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;

            // 1. 初步檢查：長度必須是 4 的倍數，且符合 Base64 字元規則
            // Base64 規則：A-Z, a-z, 0-9, +, /, 並以 = 結尾
            bool isBase64 = input.Length % 4 == 0 &&
                            Regex.IsMatch(input.Trim(), @"^[a-zA-Z0-9\+/]*={0,2}$", RegexOptions.None);

            if (!isBase64) return input;

            try
            {
                // 2. 嘗試進行解碼
                byte[] base64EncodedBytes = Convert.FromBase64String(input);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch
            {
                // 如果解碼失敗（例如格式正確但內容不合法），則回傳原始字串
                return input;
            }
        }


    }
}