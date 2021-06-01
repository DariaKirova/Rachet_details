using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
        public Form1()
		{
			InitializeComponent();           
        }
        
 
    private void Form1_Load(object sender, EventArgs e)
		{
			this.ControlBox = false;
            Null.Text = "";
            Plus.Text = "";
            Minus.Text = "";
            Graph_x.Text = "";
            Graph_ei.Text = "";
            Graph_es.Text = "";
            Min_number.Text = "";
            Plus_number.Text = "";
            one.Text = "";
            two.Text = "";
        }

	    private void Closer_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void Collapser_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}


        //расчеты
        private static double Simpson(Func<double, double> f, double a, double b, int n)
        {
            var h = (b - a) / n;
            var sum1 = 0d;
            var sum2 = 0d;
            for (var k = 1; k <= n; k++)
            {
                var xk = a + k * h;
                if (k <= n - 1)
                {
                    sum1 += f(xk);
                }

                var xk_1 = a + (k - 1) * h;
                sum2 += f((xk + xk_1) / 2);
            }

            var result = h / 3d * (1d / 2d * f(a) + sum1 + 2 * sum2 + 1d / 2d * f(b));
            return result;
        }
        double f(double x)
        {
            return Math.Pow(Math.E, (-x * x / 2));
        }
        private double F(double temp)
        {
            double probability;
            probability = 1 / Math.Sqrt(2 * Math.PI) * Simpson(f, 0, temp,
            1000);
            return probability;
        }
        private void CleanPicture()
        {
            Bitmap bmp = new Bitmap(For_graph.Width, For_graph.Height);
            Graphics graph = Graphics.FromImage(bmp);
            Pen pen = new Pen(Color.White, 2f);
            For_graph.Image = bmp;
        }
        private void Draw(ref double EI, double ES, double O, double X)
        {
            Bitmap bmp = new Bitmap(For_graph.Width, For_graph.Height);
            Graphics graph = Graphics.FromImage(bmp);
            Pen pen = new Pen(Color.Black, 2f);
            Pen pen1 = new Pen(Color.Black, 1f);
            Pen pen2 = new Pen(Color.Black, 0.2f);
            graph.DrawLine(pen, 0, 290, 600, 290);//горизонтальная линия
            graph.DrawLine(pen, 310, 0, 310, 600);//вертикальная линия           
            
            graph.DrawLine(pen1, 180, 50, 310, 50);
            graph.DrawLine(pen2, 180, 20, 180, 590);//0
            graph.DrawLine(pen1, Convert.ToInt64(200+EI*20), 100, Convert.ToInt64(200 + EI*20), 590);//ei
            graph.DrawLine(pen1, 50, 260, 50, 590);//3-
            graph.DrawLine(pen1, 560, 260, 560, 590);//3+
            graph.DrawLine(pen1, Convert.ToInt64(560 - ES*20), 100, Convert.ToInt64(560 - ES*20), 590);//es            

            /*Point[] points = new Point[10000];
            for (int i=0;i<points.Length;i++)
            {
                points[i] = new Point(i, (int)(1 / (O * Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.E, Math.Pow(O - X, 2) / (-2 * O * O))+200));
            }
            graph.DrawLines(pen, points);*/
            
            //dug1
            Rectangle rect = new Rectangle(-95, 80, 340, 200);
            float startAngle = 5.0F;
            float sweepAngle = 100.0F;
            graph.DrawArc(pen, rect, startAngle, sweepAngle);
            //dug2
            Rectangle rect2 = new Rectangle(238, 95, 150, 280);
            float startAngle2 = 200.0F;
            float sweepAngle2 = 135.0F;
            graph.DrawArc(pen, rect2, startAngle2, sweepAngle2);
            //dug3
            Rectangle rect1 = new Rectangle(380, 58, 400, 230);
            float startAngle1 = 95.0F;
            float sweepAngle1 = 78.0F;
            graph.DrawArc(pen, rect1, startAngle1, sweepAngle1);
          
            //green lines
            Pen pen_green = new Pen(Color.Green, 3f);

            double step = 0;
            int hight = 0;
            double a = ((200 + EI * 20) - 55) / 15;
            double nach = 55;
            for (int i=0; i<15;i++)
            {                                     
                graph.DrawLine(pen_green, Convert.ToInt64(nach+step), 280-hight, Convert.ToInt64(nach +step), 290);
                step += a;
                hight += 2;
            }
            
            //red lines
            Pen pen_red = new Pen(Color.Red, 3f);
            double step1 = 0;
            int hight1 = 0;
            double a1 = (560-(560 - ES * 20)) / 10;
            double nach1 = Convert.ToInt64(560 - ES * 20) + 5;
            for (int i = 0; i < 10; i++)
            {              
                graph.DrawLine(pen_red, Convert.ToInt64(nach1 + step1), 262 + hight1, Convert.ToInt64(nach1 + step1), 290);
                step += a1;
                hight1 += 2;
            }

           /* graph.DrawLine(pen_red, 452, 262, 452, 290);
            graph.DrawLine(pen_red, 460, 264, 460, 290);
            graph.DrawLine(pen_red, 470, 268, 470, 290);
            graph.DrawLine(pen_red, 480, 272, 480, 290);
            graph.DrawLine(pen_red, 490, 275, 490, 290);
            graph.DrawLine(pen_red, 500, 278, 500, 290);
            graph.DrawLine(pen_red, 510, 279, 510, 290);
            graph.DrawLine(pen_red, 520, 282, 520, 290);
            graph.DrawLine(pen_red, 530, 283, 530, 290);
            graph.DrawLine(pen_red, 540, 286, 540, 290);
            graph.DrawLine(pen_red, 550, 286, 550, 290);*/

            For_graph.Image = bmp;
        }
        private void Do_drawing_Click(object sender, EventArgs e)
        {
            double ES = Convert.ToDouble(es.Text);
            double EI = Convert.ToDouble(ei.Text);
            double X = Convert.ToDouble(x.Text);
            double O = Convert.ToDouble(o.Text);
            
            double Xnew = EI - (X - 3 * O) + X;
            

            Godnei_detal.Text = String.Format("{0:f2}", Math.Round((F((ES - X) / O) - F((EI - X) / O)) * 100, 2));
            Esprav_brak.Text = String.Format("{0:f2}", Math.Round((F((X + 3 * O - X) / O) - F((ES - X) / O)) * 100, 2));
            Neesprav_brak.Text = String.Format("{0:f2}", Math.Round((F((EI - X) / O) - F((X - 3 * O - X) / O)) * 100, 2));
            
            Godnei_detal_new.Text = String.Format("{0:f2}", Math.Round((F((ES - Xnew) / O) - F((EI - Xnew) / O)) * 100, 2));
            Esprav_brak_new.Text = String.Format("{0:f2}", Math.Round((F((Xnew + 3 * O - Xnew) / O) - F((ES - Xnew) / O)) * 100, 2));
            
            one.Text = String.Format("{0:f3}", EI - (X - 3 *O));
            two.Text = String.Format("{0:f3}", X- (EI - (X - 3 * O)));      
            
            Draw(ref EI, ES, O, X);
            
            //notes on graph
            Null.Text = String.Format("{0:f2}", "0");
            Plus.Text = String.Format("{0:f2}", "+3σ");
            Minus.Text = String.Format("{0:f2}", "-3σ");
            Graph_x.Text = String.Format("x = " + X);
            Graph_ei.Text = String.Format("ei = " + EI);
            Graph_es.Text = String.Format("es = " + ES);
            Min_number.Text = String.Format("{0:f2}", Math.Round(X - 3 * O, 3));
            Plus_number.Text = String.Format("{0:f2}", Math.Round(X + 3 * O, 3));


        }
        private void Do_new_drawing_Click(object sender, EventArgs e)
		{
            es.Text = "";
            ei.Text = "";
            x.Text = "";
            o.Text = "";
            Godnei_detal.Text = "";
            Esprav_brak.Text = "";
            Neesprav_brak.Text = "";
            Godnei_detal_new.Text = "";
            Esprav_brak_new.Text = "";
            one.Text = "";
            two.Text = "";
            
            CleanPicture();

            Null.Text = "";
            Plus.Text = "";
            Minus.Text = "";
            Graph_x.Text = "";
            Graph_ei.Text = "";
            Graph_es.Text = "";
            Min_number.Text = "";
            Plus_number.Text = "";

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void Graph_ei_Click(object sender, EventArgs e)
        {

        }
    }
}
