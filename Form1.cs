using System;
using System.Drawing;
using System.Windows.Forms;


namespace Test
{
    public partial class Form1 : Form
    {
        private Pen _pen;
        private int _height;
        private int _width;
        private int[] x;
        private int[] y;


        public Form1()
        {
            InitializeComponent();
            _pen = new Pen(Color.Black);
            _height = pictureBox1.Height;
            _width = pictureBox1.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                _pen = new Pen(Color.Black);

                var str = textBox1.Text.Split(", ");

                var n = Decimal.ToInt32(Math.Truncate((decimal)str.Length / 2));

                x = new int[n];
                y = new int[n];

                g.Clear(Color.White);
                g.DrawLine(_pen, new Point(_width / 2, 0), new Point(_width / 2, _height));
                g.DrawLine(_pen, new Point(0, _height / 2), new Point(_width, _height / 2));
                _pen.Color = Color.Red;
                int res;
                try
                {
                    for (int i = 0; i < str.Length; i += 2)
                    {
                        x[i / 2] = int.TryParse(str[i], out res) ? res : throw new Exception();
                    }

                    for (int i = 1; i < str.Length; i += 2)
                    {
                        y[Decimal.ToInt32(Math.Truncate((decimal)i / 2))] = int.TryParse(str[i], out res) ? res : throw new Exception();

                    }
                }
                catch (Exception ex)
                {
                    label1.Text = "Error";
                }
                var square = 0;
                for (int i = 0; i < n - 1; i++)
                {
                    g.DrawLine(_pen, new Point(x[i] + _width / 2, _height - (y[i] + _height / 2)), new Point(x[i + 1] + _width / 2, _height - (y[i + 1] + _height / 2)));
                    square += x[i] * y[i + 1] - y[i] * x[i + 1]; // cчитаем площадь
                }

                g.DrawLine(_pen, new Point(x[n - 1] + _width / 2, _height - (y[n - 1] + _height / 2)), new Point(x[0] + _width / 2, _height - (y[0] + _height / 2)));

                square += x[n - 1] * y[0] - y[n - 1] * x[0];
                square = Math.Abs(square / 2);
                label1.Text = "Square = " + square;
            }
        }
    }
}
