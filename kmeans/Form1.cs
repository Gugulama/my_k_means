using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kmeans
{
    public partial class Form1 : Form
    {
        Form2 f;
        List<MyPoint> Points = new List<MyPoint>();
        int pointSize = 20;
        List<Centroid> Centroids = new List<Centroid>();
        Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Pink, Color.Purple, Color.Yellow };
        public Form1()
        {
            InitializeComponent();
            f = new Form2();
            //button3.Enabled = false;
            f.Owner = this;
            f.ShowDialog();
            this.Name = "My K-means";
            Init();
            reDraw();
        }
        void Init()
        {
            int x;
            int y;
            MyPoint mp;
            Centroid cd;
            Random r = new Random();
            for (int i = 0; i < f.pointCount; i++)
            {                
                x = r.Next(pointSize, pictureBox1.Width - pointSize);
                y = r.Next(pointSize, pictureBox1.Height - pointSize);
                mp = new MyPoint(x, y);
                Points.Add(mp);
            }
            for (int i = 0; i < f.clasters; i++)
            {
                x = r.Next(pictureBox1.Width);
                y = r.Next(pictureBox1.Height);
                cd = new Centroid(x, y, colors[i]);
                Centroids.Add(cd);
            }
        }
        void reDraw()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            Pen pen = new Pen(Color.Black, 5);
            SolidBrush brush = new SolidBrush(Color.Gray);
            pictureBox1.Image = bmp;
            foreach (MyPoint item in Points)
            {
                brush.Color = item.color;
                g.FillEllipse(brush, item.getRectForSize(pointSize));
            }
            foreach (Centroid item in Centroids)
            {
                brush.Color = item.color;
                g.DrawEllipse(pen, item.getRectForSize(pointSize));
                g.FillEllipse(brush, item.getRectForSize(pointSize));
            }
        }
        void calcDist()
        {            
            foreach (MyPoint point in Points)
            {
                int x = point.location.X;
                int y = point.location.Y;
                Dictionary<Centroid, double> dists = new Dictionary<Centroid, double>();
                foreach (Centroid centroid in Centroids)
                {
                    int cX = centroid.location.X;
                    int cY = centroid.location.Y;
                    double dist = Math.Sqrt(Math.Pow(x - cX, 2) + Math.Pow(y - cY, 2));
                    dists.Add(centroid, dist);
                }
                point.color = dists.Where(dist => dist.Value == dists.Values.Min()).ToDictionary(c => c.Key, c => c.Value).Keys.First().color;
            }
        }
        void relocateCentroids()
        {            
            foreach (Centroid centroid in Centroids)
            {
                int sumX = 0;
                int sumY = 0;
                int count = 0;
                foreach (MyPoint point in Points)
                {
                    if (centroid.color == point.color)
                    {
                        sumX += point.location.X;
                        sumY += point.location.Y;
                        count++;
                    }
                }
                if (count != 0)
                {
                    centroid.location.X = sumX / count;
                    centroid.location.Y = sumY / count;
                }

            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            calcDist();
            reDraw();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            relocateCentroids();
            reDraw();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Init();
            reDraw();
        }
    }
    public class MyPoint
    {
        public Point location;
        public Color color;

        public MyPoint(int x, int y, Color color)
        {
            this.location.X = x;
            this.location.Y = y;
            this.color = color;
        }
        public MyPoint(int x, int y)
        {
            this.location.X = x;
            this.location.Y = y;
            this.color = Color.Black;
        }
        public Point getLocation()
        {
            return location;
        }
        public Rectangle getRectForSize(int size)
        {
            return new Rectangle(location.X - size/2, location.Y - size/2, size, size);
        }
    }
    public class Centroid
    {
        public Point location;
        public Color color;
        public Centroid(int x, int y, Color color)
        {
            this.location.X = x;
            this.location.Y = y;
            this.color = color;
        }
        public Centroid(int x, int y)
        {
            this.location.X = x;
            this.location.Y = y;
            this.color = Color.Black;
        }
        public Point getLocation()
        {
            return location;
        }
        public Rectangle getRectForSize(int size)
        {
            return new Rectangle(location.X - size / 2, location.Y - size / 2, size, size);
        }
    }
}
