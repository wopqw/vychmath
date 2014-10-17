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

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        double a = 0;
        double b = 0;
        double h = 0;
        double y = 0;
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
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            num = double.TryParse(textBox1.Text, out a);
            if (!num)
            {
                MessageBox.Show("А не является числом. Исправьте а.");
                textBox1.Text = "";
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
            }
            if (h<0.0001)
            {
                MessageBox.Show("Введено слишком маленькое h. Возможно неадекватнео поведение программы");
            }
            if (h==0)
            {
                MessageBox.Show("Извините, но h не должен быть нулём :(");
            }
            StreamWriter wr = new StreamWriter("input.txt");
            wr.WriteLine("   x    ||     y");
            for (double i=a;i<=b+0.01;i+=h)
            {
                //listx.Add(i);//нужно ли
                listxy.Add(i);
                y = Math.Round(Math.Pow(Math.Sin(i), 2) * Math.Pow(Math.E, i),4);
                //listy.Add(y);//нужно ли
                listxy.Add(y);
            }
            for (int i=0;i<listxy.Count;i+=2)
            {
                wr.WriteLine("   {0}   ||   {1}   ",Math.Round(listxy[i],2),listxy[i+1]);
                textBox4.Text+= ("   "+Math.Round(listxy[i],2)+"   ||   "+listxy[i+1].ToString()+"   "+Environment.NewLine);
            }
            wr.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
