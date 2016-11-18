using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeiboVCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://weibo.cn/interface/f/ttt/captcha/show.php?r="+DateTime.Now.ToString("mmss");
            string filepath = DateTime.Now.ToString("mmss") + "pic.gif";
            WebClient mywebclient = new WebClient();
            mywebclient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36");
            mywebclient.DownloadFile(url, filepath);

            Bitmap Picbit = new Bitmap(Bitmap.FromFile(filepath));
            Bitmap bm =WAPcode.Binaryzation(WAPcode.CleanPic(Picbit));
            pictureBox1.Image = Image.FromHbitmap(bm.GetHbitmap());
            int[,] position = WAPcode.DividePic(bm);
            try
            {
                Bitmap newImg = new Bitmap(position[0, 1] - position[0, 0], position[0, 3] - position[0, 2]);
                Graphics g = Graphics.FromImage(newImg);
                g.DrawImage(bm, 0, 0, new Rectangle(position[0, 0], position[0, 2], position[0, 1] - position[0, 0], position[0, 3] - position[0, 2]), GraphicsUnit.Pixel);
                pictureBox2.Image = Image.FromHbitmap(newImg.GetHbitmap());

                Bitmap newImg2 = new Bitmap(position[1, 1] - position[1, 0], position[1, 3] - position[1, 2]);
                Graphics g2 = Graphics.FromImage(newImg2);
                g2.DrawImage(bm, 0, 0, new Rectangle(position[1, 0], position[1, 2], position[1, 1] - position[1, 0], position[1, 3] - position[1, 2]), GraphicsUnit.Pixel);
                pictureBox3.Image = Image.FromHbitmap(newImg2.GetHbitmap());

                Bitmap newImg3 = new Bitmap(position[2, 1] - position[2, 0], position[2, 3] - position[2, 2]);
                Graphics g3 = Graphics.FromImage(newImg3);
                g3.DrawImage(bm, 0, 0, new Rectangle(position[2, 0], position[2, 2], position[2, 1] - position[2, 0], position[2, 3] - position[2, 2]), GraphicsUnit.Pixel);
                pictureBox4.Image = Image.FromHbitmap(newImg3.GetHbitmap());

                Bitmap newImg4 = new Bitmap(position[3, 1] - position[3, 0], position[3, 3] - position[3, 2]);
                Graphics g4 = Graphics.FromImage(newImg4);
                g4.DrawImage(bm, 0, 0, new Rectangle(position[3, 0], position[3, 2], position[3, 1] - position[3, 0], position[3, 3] - position[3, 2]), GraphicsUnit.Pixel);
                pictureBox5.Image = Image.FromHbitmap(newImg4.GetHbitmap());

                Bitmap newImg5 = new Bitmap(position[4, 1] - position[4, 0], position[4, 3] - position[4, 2]);
                Graphics g5 = Graphics.FromImage(newImg5);
                g5.DrawImage(bm, 0, 0, new Rectangle(position[4, 0], position[4, 2], position[4, 1] - position[4, 0], position[4, 3] - position[4, 2]), GraphicsUnit.Pixel);
                pictureBox6.Image = Image.FromHbitmap(newImg5.GetHbitmap());

            }
            catch
            {
                MessageBox.Show("分离失败...");
            }
            //Console.WriteLine(WAPcode.Similarity(newImg, newImg));

            //Bitmap bm = WAPcode.CleanPic(new Bitmap(Bitmap.FromFile(textBox1.Text)));
            
            //Bitmap bm = ConvertTo1Bpp1(new Bitmap(Bitmap.FromFile(textBox1.Text)));
            
            //FileStream fs = new FileStream("code.txt", FileMode.Create);
            //StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            //for (int i = 0; i < bm.Height; i++)
            //{
            //    for (int j = 0; j < bm.Width; j++)
            //    {
            //        //获取该点的像素的RGB的颜色
            //        Color color = bm.GetPixel(j, i);
            //        if (color.R == 0)
            //            sw.Write("1");
            //        else
            //            sw.Write(" ");
            //    }
            //    sw.Write("\r\n");
            //}
            //sw.Close();
            //fs.Close();
            //int[] arrayx=new int[999];
            //int[] arrayy = new int[999];
            //int p = 0;
            //for (int i = 1; i < bm.Height-1; i++)
            //{
            //    for (int j = 1; j < bm.Width-1; j++)
            //    {
            //        if(bm.GetPixel(j,i).R == 0)
            //            if (SearchNeighbor(j, i, bm) > 3)
            //            {
            //                bm.SetPixel(j, i, Color.FromArgb(255, 255, 255));
            //                arrayx[p] = i;
            //                arrayy[p] = j;
            //                p++;
            //            }
            //    }
                
            //}
            //for (p=p-1; p > 0; p--)
            //{
            //    bm.SetPixel(arrayy[p], arrayx[p] + 1, Color.FromArgb(255, 255, 255));
            //    //bm.SetPixel(arrayy[p]-1, arrayx[p] + 1, Color.FromArgb(0, 255, 255));
            //    //bm.SetPixel(arrayy[p]+1, arrayx[p] + 1, Color.FromArgb(0, 255, 255));
            //    //bm.SetPixel(arrayy[p]+1, arrayx[p] - 1, Color.FromArgb(0, 255, 255));
            //    //bm.SetPixel(arrayy[p]-1, arrayx[p] - 1, Color.FromArgb(0, 255, 255));
            //    //bm.SetPixel(arrayy[p], arrayx[p] - 1, Color.FromArgb(0, 255, 255));
            //    //bm.SetPixel(arrayy[p] + 1, arrayx[p] , Color.FromArgb(0, 255, 255));
            //    //bm.SetPixel(arrayy[p] - 1, arrayx[p] , Color.FromArgb(0, 255, 255));
            //}
            //bm = hough_line(bm,40);
            

        }



        /// <summary>
        /// 检测直线
        /// </summary>
        /// <param name="cross_num">hough变换后的曲线交点个数，取值越大，找出的直线越少</param>
        public Bitmap hough_line(Bitmap bmpobj, int cross_num)
        {
            int x = bmpobj.Width;
            int y = bmpobj.Height;
            int rho_max = (int)Math.Floor(Math.Sqrt(x * x + y * y)) + 1; //由原图数组坐标算出ρ最大值，并取整数部分加1
            //此值作为ρ，θ坐标系ρ最大值
            int[,] accarray = new int[rho_max, 180]; //定义ρ，θ坐标系的数组，初值为0。
            //θ的最大值，180度

            double[] Theta = new double[180];
            //定义θ数组，确定θ取值范围
            double i = 0;
            for (int index = 0; index < 180; index++)
            {
                Theta[index] = i;
                i += Math.PI / 180;
            }

            double rho;
            int rho_int;
            for (int n = 0; n < x; n++)
            {
                for (int m = 0; m < y; m++)
                {
                    Color pixel = bmpobj.GetPixel(n, m);
                    if (pixel.R == 0)
                    {
                        for (int k = 0; k < 180; k++)
                        {
                            //将θ值代入hough变换方程，求ρ值
                            rho = (m * Math.Cos(Theta[k])) + (n * Math.Sin(Theta[k]));
                            //将ρ值与ρ最大值的和的一半作为ρ的坐标值（数组坐标），这样做是为了防止ρ值出现负数
                            rho_int = (int)Math.Round(rho / 2 + rho_max / 2);
                            //在ρθ坐标（数组）中标识点，即计数累加
                            accarray[rho_int, k] = accarray[rho_int, k] + 1;
                        }
                    }
                }
            }

            //=======利用hough变换提取直线======
            //寻找100个像素以上的直线在hough变换后形成的点
            const int max_line = 100;
            int[] case_accarray_n = new int[max_line];
            int[] case_accarray_m = new int[max_line];
            int K = 0; //存储数组计数器
            for (int rho_n = 0; rho_n < rho_max; rho_n++) //在hough变换后的数组中搜索
            {
                for (int theta_m = 0; theta_m < 180; theta_m++)
                {
                    if (accarray[rho_n, theta_m] >= cross_num && K < max_line) //设定直线的最小值
                    {
                        case_accarray_n[K] = rho_n; //存储搜索出的数组下标
                        case_accarray_m[K] = theta_m;
                        K = K + 1;
                    }
                }
            }

            //把这些点构成的直线提取出来,输出图像数组为I_out
            //I_out=ones(x,y).*255;
            Bitmap I_out = new Bitmap(x, y);
            for (int n = 0; n < x; n++)
            {
                for (int m = 0; m < y; m++)
                {
                    //首先设置为白色
                    I_out.SetPixel(n, m, Color.White);
                    Color pixel = bmpobj.GetPixel(n, m);
                    if (pixel.R == 0)
                    {
                        for (int k = 0; k < 180; k++)
                        {
                            rho = (m * Math.Cos(Theta[k])) + (n * Math.Sin(Theta[k]));
                            rho_int = (int)Math.Round(rho / 2 + rho_max / 2);
                            //如果正在计算的点属于100像素以上点，则把它提取出来
                            for (int a = 0; a < K - 1; a++)
                            {

                                if (rho_int == case_accarray_n[a] && k == case_accarray_m[a])
                                    I_out.SetPixel(n, m, Color.Black);
                            }
                        }
                    }

                }
            }
            return I_out;
        }


        public int SearchNeighbor(int i, int j, Bitmap box1)//返回周围相同点的个数
        {
            
            int  r1, r2, r3, r4, r5, r6, r7, r8, r9;
            int count = 0;
            r1 = box1.GetPixel(i - 1, j - 1).R;
            r2 = box1.GetPixel(i, j - 1).R;
            r3 = box1.GetPixel(i + 1, j - 1).R;
            r4 = box1.GetPixel(i - 1, j).R;
            r5 = box1.GetPixel(i, j).R;
            r6 = box1.GetPixel(i + 1, j).R;
            r7 = box1.GetPixel(i - 1, j + 1).R;
            r8 = box1.GetPixel(i , j + 1).R;
            r9 = box1.GetPixel(i + 1, j + 1).R;
            if (r5 == 255) //查找白点
                count = (r1 + r2 + r3 + r4 + r6 + r7 + r8 + r9) / 255;
            else//查找黑点
                count = 8 - (r1 + r2 + r3 + r4 + r6 + r7 + r8 + r9) / 255;
            return count;
        }

        public int SearchFourNeighbor(int i, int j, Bitmap box1)//返回周围相同点的个数
        {
            int r2, r4, r5, r6, r8;
            int count = 0;
           
            r2 = box1.GetPixel(i, j - 1).R;

            r4 = box1.GetPixel(i - 1, j).R;
            r5 = box1.GetPixel(i, j).R;
            r6 = box1.GetPixel(i + 1, j).R;
 
            r8 = box1.GetPixel(i, j + 1).R;
    
            if (r5 == 255) //查找白点
                count = ( r2  + r4 + r6 + r8) / 255;
            else//查找黑点
                count = 4 - (r2 + r4 + r6 + r8) / 255;
            return count;
        }


        /// <summary>
        /// 图像二值化1：取图片的平均灰度作为阈值，低于该值的全都为0，高于该值的全都为255
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public Bitmap ConvertTo1Bpp1(Bitmap bmp)
        {
            int average = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    average += (color.R + color.B) / 2;
                }
            }
            average = (int)average / (bmp.Width * bmp.Height) / trackBar1.Value;
            //average = trackBar1.Value;
            Console.WriteLine(average.ToString());
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = bmp.GetPixel(i, j);
                    int value = 255 - (color.R + color.B)/2;
                    Color newColor = value > average ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255,

255, 255);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Bitmap bm = ConvertTo1Bpp1(new Bitmap(Bitmap.FromFile(textBox1.Text)));
            pictureBox1.Image = Image.FromHbitmap(bm.GetHbitmap());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length < 5)
            {
                MessageBox.Show("输入无效...长度错误...");
                return;
            }
            for (int x = 0; x < 5; x++)
            {
                if (x == 0 && textBox2.Text.Substring(0, 1)!=" ")
                {
                    string s = WAPcode.ConvertToStr(pictureBox2.Image as Bitmap) + " " + textBox2.Text.Substring(0, 1) + "\r\n";
                    FileStream fs = new FileStream("code.txt", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.Write(s);
                    sw.Close();
                    fs.Close();
                }
                if (x == 1 && textBox2.Text.Substring(1, 1) != " ")
                {
                    string s = WAPcode.ConvertToStr(pictureBox3.Image as Bitmap) + " " + textBox2.Text.Substring(1, 1) + "\r\n";
                    FileStream fs = new FileStream("code.txt", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.Write(s);
                    sw.Close();
                    fs.Close();
                }
                if (x == 2 && textBox2.Text.Substring(2, 1) != " ")
                {
                    string s = WAPcode.ConvertToStr(pictureBox4.Image as Bitmap) + " " + textBox2.Text.Substring(2, 1) + "\r\n";
                    FileStream fs = new FileStream("code.txt", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.Write(s);
                    sw.Close();
                    fs.Close();
                }
                if (x == 3 && textBox2.Text.Substring(3, 1) != " ")
                {
                    string s = WAPcode.ConvertToStr(pictureBox5.Image as Bitmap) + " " + textBox2.Text.Substring(3, 1) + "\r\n";
                    FileStream fs = new FileStream("code.txt", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.Write(s);
                    sw.Close();
                    fs.Close();
                }
                if (x == 4 && textBox2.Text.Substring(4, 1) != " ")
                {
                    string s = WAPcode.ConvertToStr(pictureBox6.Image as Bitmap) + " " + textBox2.Text.Substring(4, 1) + "\r\n";
                    FileStream fs = new FileStream("code.txt", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.Write(s);
                    sw.Close();
                    fs.Close();
                }
            }
            textBox2.Text = "";
            label1.Text = DateTime.Now.ToString() + " 提交成功...";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show( WAPcode.GetChar(pictureBox2.Image as Bitmap));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(WAPcode.GetChar(pictureBox3.Image as Bitmap));
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(WAPcode.GetChar(pictureBox4.Image as Bitmap));
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(WAPcode.GetChar(pictureBox5.Image as Bitmap));
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(WAPcode.GetChar(pictureBox6.Image as Bitmap));
        }


    }
}
