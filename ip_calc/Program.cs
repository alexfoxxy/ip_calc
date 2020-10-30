using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ip_calc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вввдете ip адрес");
            string a = Console.ReadLine();
            string b = Console.ReadLine();
            string c = Console.ReadLine();
            string d = Console.ReadLine();
            string prefix = Console.ReadLine();

            Console.WriteLine("Ваш ip адресс");
            Console.Write(a + "." + b + "." + c + "." + d + "/" + prefix);
            int a1 = int.Parse(a);
            int b1 = int.Parse(b);
            int c1 = int.Parse(c);
            int d1 = int.Parse(d);
            int prefix1 = int.Parse(prefix);

            Console.WriteLine("Класс сети");

            if (a1 > 0 && a1 < 127)
            {
                Console.WriteLine("Класс сети: A");
                Console.WriteLine("Маска подсети: 255.0.0.0");
            }
            else if (a1 > 128 && a1 < 191)
            {
                Console.WriteLine("Класс сети: B");
                Console.WriteLine("Маска подсети: 255.255.0.0");
            }

            else if (a1 > 192 && a1 < 233)
            {
                Console.WriteLine("Класс сети: C");
                Console.WriteLine("Маска подсети: 255.255.255.0");
            }


            Console.WriteLine("Адрес сети:" + "{0}.0.0.0", a1);

            Console.WriteLine("Размер расширенного сетевого префикс: " + "/" + prefix1);

            Console.WriteLine("Маска подсети: " + prefix1);

            /*Так как размер расширенного сетевого префикса составляет 14 разрядов, маска подсети, 
             представленная в 2-ой системе счисления, состоит из 14 "1" и (32 - 14 = 18) 18 "0"*/

            string binSubNetMask = GetBinSubNetMask(prefix1);
            string decSubNetMask = GetDecSubNetMask(binSubNetMask);
            string hexSubNetMask = GetHexSubNetMask(binSubNetMask);

            GetBinSubNetMask(prefix1);

            Console.WriteLine("Во 2-ой: " + binSubNetMask);
            Console.WriteLine("Во 16-ой: " + hexSubNetMask);
            Console.WriteLine("Во 10-ой: " + decSubNetMask);

            Console.WriteLine("Адрес подсети");

            //Адрес подсети определяется первыми 14 разрядами заданного IP-адреса. Остальные 18 разрядов заполняются "0":


            string bin1 = Convert.ToString(a1, 2);
            string bin2 = Convert.ToString(b1, 2);
            string hex1 = Convert.ToString(a1, 16);
            string hex2 = Convert.ToString(b1, 16);
            Console.WriteLine("Во 2-ой: " + bin1 + "." + bin2 + ".00000000" + ".00000000");
            Console.WriteLine("Во 16-ой: " + hex1 + "." + hex2 + ".00" + ".00");
            Console.WriteLine("Во 10-ой: " + a1 + "." + b1 + ".0" + ".0");

            Console.WriteLine("Адрес хоста");
            //Адрес подсети определяется первыми 14 разрядами заданного IP-адреса. Остальные 18 разрядов заполняются "0":
            string bin3 = Convert.ToString(c1, 2);
            string bin4 = Convert.ToString(d1, 2);
            string hex3 = Convert.ToString(c1, 16);
            string hex4 = Convert.ToString(d1, 16);
            Console.WriteLine("Во 2-ой: " + "00000000" + ".00000000." + bin3 + "." + bin4);
            Console.WriteLine("Во 16-ой: " + "00" + ".00." + hex3 + "." + hex4);
            Console.WriteLine("Во 10-ой: " + "0" + ".0." + c1 + "." + d1);
            Console.ReadKey();
        }

        static string GetBinSubNetMask(int numberOfOnes)
        {
            String mask = new String('1', numberOfOnes);
            mask += new string('0', 32 - numberOfOnes);
            int number = 0;
            mask = Regex.Replace(mask, ".{1}", (f) => (++number % 8 == 0 && number < 32) ? f.Value + "." : f.Value);
            return mask;
        }

        static string GetDecSubNetMask(string binSubNetMask)
        {
            string[] subNetMask = binSubNetMask.Split('.');

            for (int i = 0; i < subNetMask.Length; i++)
            {
                subNetMask[i] = Convert.ToInt32(subNetMask[i], 2).ToString();
            }
            return String.Join(".", subNetMask);
        }


        static string GetHexSubNetMask(string binSubNetMask)
        {
            string[] subNetMask = binSubNetMask.Split('.');

            for (int i = 0; i < subNetMask.Length; i++)
            {
                subNetMask[i] = Convert.ToInt32(subNetMask[i], 2).ToString("X2");
            }
            return String.Join(".", subNetMask);
        }

    }
}
