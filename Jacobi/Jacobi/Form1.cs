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
using System.Diagnostics;

namespace Jacobi
{
    public partial class Jacoby : Form
    {
        public static int N;
        private OpenFileDialog openFileDialog1;
        static Matrix matrix;
        static Matrix rotationMatrix;
        static Matrix invertedRotationMatrix;
        static Matrix identityMatrix;
        int[] position;
        static double norma = 10000;
        static double E = 0;
        public Jacoby()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Выберите файл:";
            openFileDialog1.Filter = "Текстовые файлы|*.txt";
            openFileDialog1.InitialDirectory = @"Z:\Documents\Visual Studio 2013\Projects\Jacobi\Jacobi\bin\Debug";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Encrypt the selected file. I'll do this later. :)
            }
            label6.Text = openFileDialog1.FileName.Split('\\')[openFileDialog1.FileName.Split('\\').Length - 1];
        }

        private void Jacoby_Load(object sender, EventArgs e)
        {
            /*
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture; */
        }
        // 25x25 with 1 = 4.65
        // 25x25 with 5 = 4.69
        // 50x50 with 1 = 3.57.64

        private void button3_Click(object sender, EventArgs e)
        {
            N = int.Parse(textBox1.Text);
            matrix = new Matrix(N, N);
            StreamReader streamReader = File.OpenText(openFileDialog1.FileName);
            for (int i = 0; i < N; i++)
            {
                String text = streamReader.ReadLine();
                String[] arrayString = text.Split(' ');
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = double.Parse(arrayString[j]);
                }
            }
            streamReader.Close();
            
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        label7.Text += matrix[i, j];
                        label7.Text += "          ";
                    }
                    label7.Text += Environment.NewLine;
                    label7.Text += Environment.NewLine;
                }
            }
            label5.Text = "Сделано!!";
            rotationMatrix = new Matrix(N, N);
            invertedRotationMatrix = new Matrix(N, N);
            identityMatrix = Matrix.IdentityMatrix(N, N);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            E = double.Parse(textBox2.Text);
            /*for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    streamWriter.Write(matrix[i,j].ToString()+" ");
                }
                streamWriter.WriteLine();
            }*/
            //streamWriter.WriteLine(maxElement(matrix).ToString());
            position = new int[2];            
            rotationMatrix = matrix;
            position = rotationMatrix.maxElement();
            rotationMatrix.searchAngle();
            rotationMatrix = rotationMatrix.rotationMatrix();            
            invertedRotationMatrix = rotationMatrix;
            invertedRotationMatrix = invertedRotationMatrix.Invert();
            while (norma > E)
            {
                matrix = invertedRotationMatrix * matrix * rotationMatrix;
                rotationMatrix = identityMatrix * rotationMatrix;
                norma = matrix.Norma();
                rotationMatrix = matrix;
                position = rotationMatrix.maxElement();
                rotationMatrix.searchAngle();
                rotationMatrix = rotationMatrix.rotationMatrix();
                invertedRotationMatrix = rotationMatrix;
                invertedRotationMatrix = invertedRotationMatrix.Invert();
            }
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            string time = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            label8.Text = time;
            StreamWriter streamWriter = new StreamWriter("output.txt");
            streamWriter.WriteLine(matrix);
            streamWriter.WriteLine(norma);
            streamWriter.Close();
            Process.Start("notepad.exe", "output.txt");
        }
    }
}
