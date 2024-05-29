using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms.DataVisualization.Charting;

namespace MainApp.Logic
{
    public class LogicComponent: Form
    {
        private PictureBox picBox;
        private List<Tuple<string, string, string>> data1;
        private List<Tuple<string, string, string>> data2;
        
        private Font ArialFontBold = new Font("Arial", 16, FontStyle.Bold);
        private Font ArialFontReg = new Font("Arial", 12, FontStyle.Regular);
        private Font ArialFontItalic = new Font("Arial", 12, FontStyle.Italic);

        private int leftPadding = 25;
        private int topPadding = 25;

        private int windowWidth = 600;
        private int windowheight = 600;


        public LogicComponent(string data1Path, string data2Path)
        {
            InitializeComponent();

            this.picBox = new PictureBox();

            this.picBox.Width = this.windowWidth;
            this.picBox.Height = this.windowheight;
            this.data1 = LogicComponent.ReadDataFromFile(data1Path);
            this.data2 = LogicComponent.ReadDataFromFile(data2Path);

            this.Size = new Size(windowWidth, windowheight);

        }

        public void HandleClickButton1()
        {
            Label textLabel = new Label() { Left = leftPadding, Top = topPadding, Width = windowWidth, Height = windowheight, Text = "Header", Font = this.ArialFontBold };
            LogicComponent.AppendRelationGraphicToPictureBox(this, this.data1);
            this.Controls.Add(this.picBox);
            this.Controls.Add(textLabel);

            this.ShowDialog();

        }

        public void HandleClickButton2()
        {
            string avg = GetGradientValues(this.data1);
            Form f = new Form();

            Label label = new Label();
            f.Size = new Size(600, 600);

            Label label1 = new Label();
            label1.Location = new System.Drawing.Point(0, 100);

            label.Text = avg;
            label.Font = this.ArialFontBold;
            label1.Font = this.ArialFontReg;

            label.Size = new Size(600, 100);
            label1.Size = new Size(600, 500);

            for (int i = 0; i < this.data1.Count(); i++)
            {
                label1.Text += this.data1[i] + "\n";
            }

            f.Controls.Add(label1);
            f.Controls.Add(label);
            f.ShowDialog();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogicComponent));
            this.SuspendLayout(); 
            this.Name = "LogicComponent";
            this.ResumeLayout(false);

        }

        private static List<Tuple<string, string, string>> ReadDataFromFile(string filePath)
        {
            List<Tuple<string, string, string>> data = new List<Tuple<string, string, string>>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] values = line.Split(';');

                    if (values.Length == 3)
                    {
                        string value1 = values[0].Trim();
                        string value2 = values[1].Trim();
                        string value3 = values[2].Trim();

                        data.Add(new Tuple<string, string, string>(value1, value2, value3));
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ошибка при чтении файла: {e.Message}");
            }

            return data;
        }

        private static void AppendRelationGraphicToPictureBox(Form parent, List<Tuple<string, string, string>> data)
        {  
            Chart chart = new Chart();
            chart.Parent = parent;
            chart.Dock = DockStyle.Fill;

            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.ChartType = SeriesChartType.Line;
            series.Name = "Dependency";

            List<Tuple<string, string, string>> xValues = data;
            for (int i = 0; i < xValues.Count(); i++)
            {
                Console.WriteLine(xValues[i].Item1);
                series.Points.AddXY(xValues[i].Item1, xValues[i].Item2);
            }

            chart.Series.Add(series);

            chart.Titles.Add("График стабильности соединений");
            chartArea.AxisX.Title = "Длина";
            chartArea.AxisY.Title = "Энергия";
        }

        private static string GetGradientValues(List<Tuple<string, string, string>> data)
        {

            double calcAvg()
            {
                double Summa = 0;

                for (int i = 0; i < data.Count(); i++)
                {
                    Console.WriteLine(data[i].Item3);
                    Summa += Convert.ToDouble(data[i].Item3.ToString().Replace('.', ','));
                }

                return Summa / data.Count();
            }

            double avg = calcAvg();
            return "Среднее значение градиента: " + avg; 
        }

        

        private void HideOnHover(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            btn.Enabled = false;
        }
        private void ShowOnHover(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            btn.Enabled = true;
        }

    }
}
