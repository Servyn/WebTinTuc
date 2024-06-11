using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace WebTinTuc.Helper
{
    public class StringHelper
    {
        public static string ConvertToUnaccentedLowercaseTrimmed(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Chuẩn hóa chuỗi ký tự
            string normalizedString = input.Normalize(NormalizationForm.FormD);

            // Loại bỏ dấu diacritics
            StringBuilder sb = new StringBuilder();
            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            // Chuyển đổi thành chữ thường và loại bỏ khoảng trắng
            string result = sb.ToString().Normalize(NormalizationForm.FormC).ToLower().Trim();

            return result;
        }
    }
}