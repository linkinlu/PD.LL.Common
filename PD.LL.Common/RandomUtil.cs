using System;

namespace PD.LL.Common
{
    public class RandomUtil
    {

        /// <summary>
        /// 随机生成指定长度的字符串
        /// </summary>
        /// <param name="pwdLength">字符长度</param>
        /// <returns></returns>
        public static string CreatePassword(int pwdLength)
        {
            string resultStr = "";
            string pwdchars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int iRandNum;
            Random rnd = new Random();
            for (int i = 0; i < pwdLength; i++)
            {
                iRandNum = rnd.Next(pwdchars.Length);
                resultStr += pwdchars[iRandNum];
            }

            return resultStr;

        }
    }
}