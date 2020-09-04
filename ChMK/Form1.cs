using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ChMK
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        double a, b, h;
        double n;
        double[] X;
        double[] Y;
        double[] X2;
        double[] Y2;
        public void Go()
        {
            try
            {
                a = Double.Parse(textBox_a.Text);
                b = Double.Parse(textBox_b.Text);
                h = 0.1;
                n = 10;
                X = new double[(int)n];
                Y = new double[(int)n];
                X[0] = a; Y[0] = Double.Parse(textBox_y0.Text);
                Eiler();
                Adams();
            }
            catch
            {
                button1.Text = "Error input";
            }
        }
        public void Print()
        {
            TextBox textboxtemp = new TextBox();
            for (int i = 1; i < n; i++)
            {
                textBox1.Text += "X[" + i + "]=" + X[i] + " \r\n";
                textBox1.Text += "Y[" + i + "]=" + Y[i] + " \r\n";
                textBox1.Text += "Pogr = " + Math.Abs(Y2[i] - Y[i]) + " \r\n";
                //textBox1.Text += "X[" + i + "]=" + X2[i] + " \r\n";
                //textBox1.Text += "Y[" + i + "]=" + Y2[i] + " \r\n";
                textBox1.Text += "\r\n";
            }
        }
        public void Eiler()
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            for (int i = 1; i < n; i++)
            {
                X[i] = a + i * h;
                Y[i] = Y[i - 1] + h * F(X[i - 1], Y[i - 1]);
            }
            s.Stop();
            label7.Text = s.Elapsed.TotalMilliseconds.ToString();
            h /= 2;
            n = (b - a) / h;
            X2 = new double[(int)n];
            Y2 = new double[(int)n];
            X2[0] = a;
            Y2[0] = Double.Parse(textBox_y0.Text);
            for (int i = 1; i < n; i++)
            {
                X2[i] = a + i * h;
                Y2[i] = Y2[i - 1] + h * F(X2[i - 1], Y2[i - 1]);
            }
            h *= 2;
            n = (b - a) / h;
            Print();
        }
        public double F(double x, double y)
        {
            //return x * x - 2 * y;
            return 2 * Math.Pow(x, 2) + x * y + 3 * Math.Pow(y, 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            a = Double.Parse(textBox_a.Text);
            b = Double.Parse(textBox_b.Text);
            n = 5;// (b - a) / h;
            h = 0.2;
            X = new double[(int)n];
            Y = new double[(int)n];
            X[0] = a; Y[0] = Double.Parse(textBox_y0.Text);
            textBox1.Text = "";
            textBox2.Text = "";
            Adams();
        }

        public void Adams()
        {
            double A, B, H, T, K1, K2, K3, K4;
            int I, N;
            double[] W;
            A = a;
            B = b;
            H = h;
            N = (int)n;
            T = A;
            W = new double[N];
            W[0] = 1;
            Stopwatch s = new Stopwatch();
            s.Start();
            for (I = 1; I <= 3; I++)
            {
                K1 = H * F(T, W[I - 1]);
                K2 = H * F(T + H / 2.0, W[I - 1] + K1 / 2.0);
                K3 = H * F(T + H / 2.0, W[I - 1] + K2 / 2.0);
                K4 = H * F(T + H, W[I - 1] + K3);
                W[I] = W[I - 1] + 1 / 6.0 * (K1 + 2.0 * K2 + 2.0 * K3 + K4);
                T = A + I * H;
                textBox2.Text += "X[" + I + "]=" + T + " \r\n";
                textBox2.Text += "Y[" + I + "]=" + W[I] + " \r\n";
                textBox2.Text += "\r\n";
            }
            for (I = 4; I < N; I++)
            {
                K1 = 55.0 * F(T, W[I - 1]) - 59.0 * F(T - H, W[I - 2]) + 37.0 * F(T - 2.0 * H, W[I - 3]) - 9.0 * F(T - 3.0 * H, W[I - 4]);
                W[I] = W[I - 1] + H / 24.0 * K1;
                T = A + I * H;
                textBox2.Text += "X[" + I + "]=" + T + " \r\n";
                textBox2.Text += "Y[" + I + "]=" + W[I] + " \r\n";
                textBox2.Text += "\r\n";
            }
            s.Stop();
            label8.Text = s.Elapsed.TotalMilliseconds.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Go();
        }
    }
}
