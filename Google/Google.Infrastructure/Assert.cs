using System;
using System.Text.RegularExpressions;

namespace Google.Infrastructure
{

    public class Assert
    {
        public static void IsNotNull(string name, object obj)
        {
            if (obj == null)
            {
                throw new ArgumentException(string.Format("{0}不能为空", name));
            }
        }
        public static void IsNotNullOrEmpty(string name, string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException(string.Format("{0}不能为空", name));
            }
        }
        public static void IsNotNullOrWhiteSpace(string name, string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException(string.Format("{0}不能为空", name));
            }
        }
        public static void AreEqual(string id1, string id2, string errorMessageFormat)
        {
            if (id1 != id2)
            {
                throw new ArgumentException(string.Format(errorMessageFormat, id1, id2));
            }
        }
        public static void IsCellPhoneNumber(string input)
        {
            var cellPhoneRegex = new Regex("^((13[0-9])|(15[^4])|(18[0-9])|(17[0-8])|(147,145))\\d{8}$");
            if (!cellPhoneRegex.IsMatch(input))
            {
                throw new ArgumentException("手机号格式填写错误");
            }
        }
    }
}
