﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        //кажется, нужно профичить всякие там ифы в воде порядка и числа на обыск
        double a = 0;
        double b = 0;
        double h = 0;
        double y = 0;
        int order = 0;
        double x = 0;
        List<double> listx = new List<double>();
        List<double> listy = new List<double>();
        List<double> listxy = new List<double>();
        bool num = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox4.ReadOnly = true;
            textBox4.ScrollBars = ScrollBars.Vertical;
            button3.Enabled = false;
            textBox7.ReadOnly = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            listx.Clear();
            listxy.Clear();
            listy.Clear();
            int count = 0;
            num = double.TryParse(textBox1.Text, out a);
            if (!num)
            {
                MessageBox.Show("А не является числом. Исправьте а.");
                textBox1.Text = "";
                count++;
            }
            if (a>999)
            {
                MessageBox.Show("Введено слишком большое a. Возможно неадеватное поведение программы");
            }
            num = double.TryParse(textBox2.Text, out b);
            if (!num)
            {
                MessageBox.Show("B не является числом. Исправьте b");
                textBox2.Text = "";
                count++;
            }
            if (b>999)
            {
                MessageBox.Show("Введено слишком большое a. Возможно неадеватное поведение программы");
            }
            num = double.TryParse(textBox3.Text, out h);
            if (!num)
            {
                MessageBox.Show("h не является числом. Исправьте h");
                textBox3.Text = "";
                count++;
            }
            if (count == 0)
            {
                
                if (h == 0)
                {
                    MessageBox.Show("Извините, но h не должен быть нулём :(");
                    textBox3.Text = "";
                    count++;
                }
                if (count == 0)
                {
                    if (h < 0.0001)
                    {
                        MessageBox.Show("Введено слишком маленькое h. Возможно неадекватнео поведение программы");
                    }
                }
            }
            if (count == 0)
            {
                StreamWriter wr = new StreamWriter("input.txt");
                wr.WriteLine("   x    ||     y");
                if (b < a)
                {
                    double g = b;
                    b = a;
                    a = g;
                }
                for (double i = a; i <= b + 0.01; i += h)
                {
                    listx.Add(i);
                    listxy.Add(i);
                    y = Math.Round(Math.Pow(Math.Sin(i), 2) * Math.Pow(Math.E, i), 4);
                    listy.Add(y);
                    listxy.Add(y);
                }
                for (int i = 0; i < listxy.Count; i += 2)
                {
                    wr.WriteLine("   {0}   ||   {1}   ", Math.Round(listxy[i], 2), listxy[i + 1]);
                    textBox4.Text += ("   " + Math.Round(listxy[i], 2) + "   ||   " + listxy[i + 1].ToString() + "   " + Environment.NewLine);
                }
                wr.Close();
                if (listx.Count < 5)
                {
                    MessageBox.Show("Извините, но количество точек должно быть больше или 5");
                }
                else
                {
                    button3.Enabled = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Series["Функция"].Points.Clear();
            chart1.Series["Лагранж"].Points.Clear();
            chart1.Series["Ньютон"].Points.Clear();
            int count = 0;
            num = int.TryParse(textBox5.Text, out order);
            if (!num)
            {
                MessageBox.Show("Вы не ввели конкретный порядок :(");
                textBox5.Text = "";
                count++;
            }
            if (count == 0)
            {
                if (order < 1 || order > 4)
                {
                    MessageBox.Show("Вы ввели неправильный порядок. Он может быть только от 1 до 4");
                    textBox5.Text = "";
                }
            }
            if (order == 3)
            {
                order = 4;
            }
            num = double.TryParse(textBox6.Text,out x);
            if (!num)
            {
                MessageBox.Show("Вы ввели что-то непонятное в поле для х");
                textBox6.Text = "";
                count++;
            }
            if (x==b)
            {
                MessageBox.Show("К сожалению, вы ввели x = b. Но это число не может быть вычислено интерполированием :(");
            }
            //looking for index of i
            List<double> checking = new List<double>();
            if (count == 0)
            {
                if (x < a || x > b)
                {
                    MessageBox.Show("Конечный х может находиться только среди значений, для которых определен ответ.\nЭти значения: " + a + " и " + b);
                    textBox6.Text = "";
                    count++;
                }
                if (count == 0)
                {
                    if (radioButton1.Checked == false && radioButton2.Checked == false)
                    {
                        MessageBox.Show("Вы, кажется, не выбрали метод, которым нужно считать");
                    }
                    if (radioButton1.Checked == true)
                    {
                        //Lagrandge
                        textBox7.Text = "Решение:  y = " + Math.Round(lagrandg(order, h, x),4).ToString();//tut
                        double y = 0;
                        for (int i = 0; i < listx.Count-1; i++)
                        {
                            
                                y = lagrandg(order, h, listx[i]);
                                chart1.Series["Лагранж"].Points.AddXY(listx[i], y);
                            

                        }
                        chart1.Series["Лагранж"].ChartType = SeriesChartType.Line;

                    }
                    if (radioButton2.Checked == true)
                    {
                        //Newton
                        textBox7.Text = "Решение:  y = " + Math.Round(neuton(order, h, x), 4).ToString();//tut
                        double y = 0;

                        for (int i = 0; i < listx.Count-1; i++)
                        {
                            y = neuton(order, h, listx[i]);
                            chart1.Series["Ньютон"].Points.AddXY(listx[i], y);
                        }
                        
                        chart1.Series["Ньютон"].ChartType = SeriesChartType.Line;
                    }


                    //начинается работа с графиком

                    for (int i = 0; i < listx.Count; i++)
                    {
                        chart1.Series["Функция"].Points.AddXY(listx[i], listy[i]);
                    }
                    
                    chart1.Series["Функция"].ChartType = SeriesChartType.Line;
                    //как началась, так и закончилась
                }
            }
           
            //lagrandge poor-poor lagrange :(
            
        }
        public double lagrandg (int n, double step,double x)
        {
            double lagrangePol = 0;
            int k = 0;
            for (int i = 0; i < listx.Count-2;i++ )
            {
                if (listx[i]<x&&listx[i+2]>x)
                {
                    k = i;
                    if (k+n>listx.Count)
                    {
                        k = k - (k + n - listx.Count);
                    }
                    break;
                }
            }
                for (int i = k; i < n+k; i++)
                {
                    double basicsPol = 1;
                    for (int j = k; j < n+k; j++)
                    {
                        if (i != j)
                        {
                            basicsPol *= (x - listx[j]) / (listx[i] - listx[j]);
                        }
                    }
                    lagrangePol += basicsPol * listy[i];
                }
            return lagrangePol;
        }
        public double neuton (int n, double step,double x)
        {
            /*double[,] mas = new double[n + 2, n + 1];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < n + 1; j++)
                {
                    if (i == 0)
                        mas[i, j] = listx[j];
                    else if (i == 1)
                        mas[i, j] = listy[j];
                }
            }
            int m = n;
            for (int i = 2; i < n + 2; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    mas[i, j] = mas[i - 1, j + 1] - mas[i - 1, j];
                }
                m--;
            }

            double[] dy0 = new double[n + 1];

            for (int i = 0; i < n + 1; i++)
            {
                dy0[i] = mas[i + 1, 0];
            }

            double res = dy0[0];
            double[] xn = new double[n];
            xn[0] = x - mas[0, 0];

            for (int i = 1; i < n; i++)
            {
                double ans = xn[i - 1] * (x - mas[0, i]);
                xn[i] = ans;
                ans = 0;
            }

            int m1 = n + 1;
            int fact = 1;
            for (int i = 1; i < m1; i++)
            {
                fact = fact * i;
                res = res + (dy0[i] * xn[i - 1]) / (fact * Math.Pow(step, i));
            }
            return res;*/
            int k = 0;
            for (int i = 0; i < listx.Count - 2; i++)
            {
                if (listx[i] < x && listx[i + 2] > x)
                {
                    k = i;
                    if (k + n > listx.Count)
                    {
                        k = k - (k + n - listx.Count);
                    }
                    break;
                }
            }
            double Polinom_Nutona = listy[k];
            int z = 1;
            double xx = 1;
            double yy = 0;
            double yy_2 = 0;
            double yy_3 = 0;
            double fact = 1;
            for (int i = k; i < n + k; i++)
            {
                fact = z * fact;
                xx = xx * (x - listx[i]);
                if (z < 3)
                {
                    yy = listy[i + 1] - listy[i] - yy;
                    if (n >= 3)
                    {
                        //исправить для порядка три
                        yy_2 = listy[i + 2] - listy[i + 1] - yy_2;
                    }
                    if (n == 4 && i + 3 < listy.Count)
                    {
                        yy_3 = listy[i + 3] - listy[i + 3] - yy_3;
                    }
                }
                if (z == 3)
                {
                    yy = yy_2 - yy;
                    if (n >= 3)
                    {
                        yy_2 = yy_3 - yy_2;
                    }
                }
                if (z == 4)
                {
                    yy = yy + yy_2;
                }
                Polinom_Nutona += yy * xx / (fact * Math.Pow(step, z));
                z++;
                
            }
            return Polinom_Nutona;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", "input.txt");
        }
    }
}
