using System;

namespace LeChengBaoTest
{
    class Program
    {
        static void Main(string[] args)
        {

        }
        public static bool Test1(string s, string[] str)
        {
            bool flag = false;
            if (s == null || str == null || s == "" || str.Length == 0)
            {
                return flag;
            }
            for (int i = 0; i < str.Length; i++)
            {
                s = s.Replace(str[i], "|");
            }
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                Console.WriteLine(s[i]);
                if (s[i] != '|')
                {
                    count++;
                }
            }
            if (count == 0)
            {
                flag = true;
            }
            return flag;
        }

        public struct Rect
        {
            public float x, y;//矩形左上角的x 坐标和y坐标
            public float width, height;//矩形的宽高
        }

        public static bool Test2(Rect rc1,Rect rc2)
        {
            //经过多种矩形重叠尝试 大致分为四种情况 具体看附图
            if (rc1.x + rc1.width > rc2.x &&
                rc2.x + rc2.width > rc1.x &&
                rc1.y + rc2.width > rc2.y &&
                rc2.y + rc2.height > rc1.y)
                return true;
            else
                return false;
        }
    }
}
