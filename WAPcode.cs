using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeiboVCode
{
    class WAPcode
    {
        public static Bitmap CleanPic(Bitmap bm)
        {
            for (int i = 0; i < bm.Height; i++)
            {
                for (int j = 0; j < bm.Width; j++)
                {
                    if ((bm.GetPixel(j, i).R + bm.GetPixel(j, i).G + bm.GetPixel(j, i).B) == 10 || (bm.GetPixel(j, i).R + bm.GetPixel(j, i).G + bm.GetPixel(j, i).B) == 30)
                        bm.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                    if(bm.GetPixel(j, i).R>170)
                        bm.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                    if (bm.GetPixel(j, i).B > 170)
                        bm.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                    //if (bm.GetPixel(j, i).B != bm.GetPixel(j, i).R)
                    //    bm.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                }

            }
            return bm;
            
        }

        public static string GetChar(Bitmap bm)
        {
            double max = 0;
            string maxchar = "";

            var file = File.Open("code.txt", FileMode.Open);
            List<string> txt = new List<string>();
            using (var stream = new StreamReader(file))
            {
                while (!stream.EndOfStream)
                {
                    txt.Add(stream.ReadLine());
                }
            }
            foreach (string str in txt)
            {
                Bitmap temp = ConvertToBmp(str.Substring(0, str.Length - 2));
                double ts = Similarity(temp, bm);
                Console.WriteLine(str.Substring(str.Length - 1, 1) +" " + ts.ToString()  + " ");
                if (ts > max)
                {
                    max = ts;
                    maxchar = str.Substring(str.Length - 1, 1);
                }
            }
            Console.WriteLine("最大ts为" + max.ToString());
            return maxchar;

        }


        /// <summary>
        /// 图像二值化1：取图片的平均灰度作为阈值，低于该值的全都为0，高于该值的全都为255
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap Binaryzation(Bitmap bmp)
        {
            int average = 120;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = bmp.GetPixel(i, j);
                    int value = (color.R + color.B +color.G) / 3;
                    Color newColor = value < average ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255,255, 255);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }

        public static int[,] DividePic(Bitmap bm)
        {
            int[] arrayInt = new int[bm.Width];
            for (int i = 0; i < bm.Width; i++)
            {
                int count = 0;
                for (int j = 0; j < bm.Height; j++)
                {
                    if (bm.GetPixel(i, j).R == 0)
                        count++;
                }
                arrayInt[i] = count;
                //Console.Write(count.ToString() +" ");
            }
            int start1=0,start2=0,start3=0,start4=0,start5=0;
            int end1=0,end2=0,end3=0,end4=0,end5=0;
            int up1 = 0, up2 = 0, up3 = 0, up4 = 0, up5 = 0;
            int down1 = 20, down2 = 20, down3 = 20, down4 = 20, down5 = 20;
            int sign = 0;//当前x轴标识
            int signy = 0; //当前y轴标识
            int minwidth=3;//允许最小宽度
            int minspace =2;//允许最小宽度
            for (int i = 0; i < bm.Width - 3; i++)
            {
                if (arrayInt[i] > 0 && sign==0)
                {
                    start1 = i;
                    sign = 1;
                }
                if (arrayInt[i] == 0 && sign == 1)
                {
                    if (i - start1 >=minwidth)
                    {
                        end1 = i;
                        sign = 2;
                    }
                    else
                    {
                        i = start1 + 1;
                        sign = 0;
                    }
                }
                if (arrayInt[i] > 0 && sign == 2)
                {
                    if (i - end1 >= minspace)
                    {
                        start2 = i;
                        sign = 3;
                    }
                    else
                    {
                        i = end1 + 1;
                        sign = 1;
                    }
                }
                if (arrayInt[i] == 0 && sign == 3)
                {
                    if (i - start2 >= minwidth)
                    {
                        end2 = i;
                        sign = 4;
                    }
                    else
                    {
                        i = start2 + 1;
                        sign = 2;
                    }
                }

                if (arrayInt[i] > 0 && sign == 4)
                {
                    if (i - end2 >= minspace)
                    {
                        start3 = i;
                        sign = 5;
                    }
                    else
                    {
                        i = end2 + 1;
                        sign = 3;
                    }
                }

                if (arrayInt[i] == 0 && sign == 5)
                {
                    if (i - start3 >= minwidth)
                    {
                        end3 = i;
                        sign = 6;
                    }
                    else
                    {
                        i = start3 + 1;
                        sign = 3;
                    }
                }

                if (arrayInt[i] > 0 && sign == 6)
                {
                    if (i - end3 >= minspace)
                    {
                        start4 = i;
                        sign = 7;
                    }
                    else
                    {
                        i = end3 + 1;
                        sign = 5;
                    }
                }

                if (arrayInt[i] == 0 && sign == 7)
                {
                    if (i - start4 >= minwidth)
                    {
                        end4 = i;
                        sign = 8;
                    }
                    else
                    {
                        i = start4 + 1;
                        sign = 6;
                    }
                }

                if (arrayInt[i] > 0 && sign == 8)
                {
                    if (i - end4 >= minspace)
                    {
                        start5 = i;
                        sign = 9;
                    }
                    else
                    {
                        i = end4 + 1;
                        sign = 7;
                    }
                }


                if (arrayInt[i] == 0 && sign == 9)
                {
                    if (i - start5 >= minwidth)
                    {
                        end5 = i;
                        sign = 10;
                    }
                    else
                    {
                        i = start5 + 1;
                        sign = 8;
                    }
                }


                if (sign == 10)//找到10条分界线
                {
                    //对每个字符取上下界
                    
                    //第一个字符
                    signy = 0;
                    int[] arrayIntY1 = new int[bm.Height];
                    for (int x = 0; x < bm.Height; x++)
                    {
                        int count = 0;
                        for (int y = start1; y < end1; y++)
                        {
                            if (bm.GetPixel(y, x).R == 0)
                                count++;
                        }
                        arrayIntY1[x] = count;
                    }

                    for (int x = 0; x < bm.Height-1; x++)
                    {
                        if (arrayIntY1[x] != 0 && arrayIntY1[x + 1] != 0 && signy==0)
                        {
                            up1 = x;
                            signy = 1;
                        }
                        if (arrayIntY1[x] == 0 && arrayIntY1[x + 1] == 0 && signy == 1)
                        {
                            down1 = x;
                            signy = 2;
                        }
                    }

                    //第二个字符
                    signy = 0;
                    int[] arrayIntY2 = new int[bm.Height];
                    for (int x = 0; x < bm.Height; x++)
                    {
                        int count = 0;
                        for (int y = start2; y < end2; y++)
                        {
                            if (bm.GetPixel(y, x).R == 0)
                                count++;
                        }
                        arrayIntY2[x] = count;
                    }

                    for (int x = 0; x < bm.Height-1; x++)
                    {
                        if (arrayIntY2[x] != 0 && arrayIntY2[x + 1] != 0 && signy == 0)
                        {
                            up2 = x;
                            signy = 1;
                        }
                        if (arrayIntY2[x] == 0 && arrayIntY2[x + 1] == 0 && signy == 1)
                        {
                            down2 = x;
                            signy = 2;
                        }
                    }

                    //第三个字符
                    signy = 0;
                    int[] arrayIntY3 = new int[bm.Height];
                    for (int x = 0; x < bm.Height; x++)
                    {
                        int count = 0;
                        for (int y = start3; y < end3; y++)
                        {
                            if (bm.GetPixel(y, x).R == 0)
                                count++;
                        }
                        arrayIntY3[x] = count;
                    }

                    for (int x = 0; x < bm.Height - 1; x++)
                    {
                        if (arrayIntY3[x] != 0 && arrayIntY3[x + 1] != 0 && signy == 0)
                        {
                            up3 = x;
                            signy = 1;
                        }
                        if (arrayIntY3[x] == 0 && arrayIntY3[x + 1] == 0 && signy == 1)
                        {
                            down3 = x;
                            signy = 2;
                        }
                    }

                    //第四个字符
                    signy = 0;
                    int[] arrayIntY4 = new int[bm.Height];
                    for (int x = 0; x < bm.Height; x++)
                    {
                        int count = 0;
                        for (int y = start4; y < end4; y++)
                        {
                            if (bm.GetPixel(y, x).R == 0)
                                count++;
                        }
                        arrayIntY4[x] = count;
                    }

                    for (int x = 0; x < bm.Height - 1; x++)
                    {
                        if (arrayIntY4[x] != 0 && arrayIntY4[x + 1] != 0 && signy == 0)
                        {
                            up4 = x;
                            signy = 1;
                        }
                        if (arrayIntY4[x] == 0 && arrayIntY4[x + 1] == 0 && signy == 1)
                        {
                            down4 = x;
                            signy = 2;
                        }
                    }

                    //第五个字符
                    signy = 0;
                    int[] arrayIntY5 = new int[bm.Height];
                    for (int x = 0; x < bm.Height; x++)
                    {
                        int count = 0;
                        for (int y = start5; y < end5; y++)
                        {
                            if (bm.GetPixel(y, x).R == 0)
                                count++;
                        }
                        arrayIntY5[x] = count;
                    }

                    for (int x = 0; x < bm.Height - 1; x++)
                    {
                        if (arrayIntY5[x] != 0 && arrayIntY5[x + 1] != 0 && signy == 0)
                        {
                            up5 = x;
                            signy = 1;
                        }
                        if (arrayIntY5[x] == 0 && arrayIntY5[x + 1] == 0 && signy == 1)
                        {
                            down5 = x;
                            signy = 2;
                        }
                    }
                   
                        //for (int x = 0; x < bm.Width; x++)
                        //{
                        //    if (x == start1 || x == end1 || x == start2 || x == end2 || x == start3 || x == end3 || x == start4 || x == end4 || x == start5 || x == end5)
                        //    {
                        //        for (int y = 0; y < bm.Height; y++)
                        //            bm.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        //    }
                        //}
                    int[,] position = new int[5,4];
                    position[0, 0] = start1;
                    position[0, 1] = end1;
                    position[0, 2] = up1;
                    position[0, 3] = down1;
                    position[1, 0] = start2;
                    position[1, 1] = end2;
                    position[1, 2] = up2;
                    position[1, 3] = down2;
                    position[2, 0] = start3;
                    position[2, 1] = end3;
                    position[2, 2] = up3;
                    position[2, 3] = down3;
                    position[3, 0] = start4;
                    position[3, 1] = end4;
                    position[3, 2] = up4;
                    position[3, 3] = down4;
                    position[4, 0] = start5;
                    position[4, 1] = end5;
                    position[4, 2] = up5;
                    position[4, 3] = down5;
                    return position;
                    
                }
            }
            return null;
        }


        public static string ConvertToStr(Bitmap bm)
        {
            string str = "";
            str += bm.Width;
            str += " ";
            str += bm.Height;
            str += " ";
            for (int j = 0; j < bm.Height; j++)
            {
                for (int i = 0; i < bm.Width; i++)
                {
                    str += bm.GetPixel(i, j).R/255;
                    str += " ";
                }
            }
            str += "#";
            return str;
        }

        public static Bitmap ConvertToBmp(string str)
        {
            string[] arrayStr = Regex.Split(str, " ");
            Bitmap newImg = new Bitmap(Convert.ToInt32(arrayStr[0]), Convert.ToInt32(arrayStr[1]));
            for (int j = 0; j < Convert.ToInt32(arrayStr[1]); j++)
            {
                for (int i = 0; i < Convert.ToInt32(arrayStr[0]); i++)
                {
                    newImg.SetPixel(i, j, Color.FromArgb(Convert.ToInt32(arrayStr[j * Convert.ToInt32(arrayStr[0]) + i + 2]) * 255, Convert.ToInt32(arrayStr[j * Convert.ToInt32(arrayStr[0]) + i + 2]) * 255, Convert.ToInt32(arrayStr[j * Convert.ToInt32(arrayStr[0]) + i + 2]) * 255));
                }
            }
            return newImg;
        }

        public static double Similarity(Bitmap bm1, Bitmap bm2)
        {
            int w = bm1.Width > bm2.Width ? bm2.Width : bm1.Width;
            int h = bm1.Height > bm2.Height ? bm2.Height : bm1.Height;

            int count = 0;

            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    if (bm1.GetPixel(i, j) == bm2.GetPixel(i, j))
                        count++;
                }
            }
            return ((double)count) / (w * h);
        }


    }
}
