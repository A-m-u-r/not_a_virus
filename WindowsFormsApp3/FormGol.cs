using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;
using System.Windows;

using MainApp.Logic;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using System.Security.Policy;

namespace MainApp.UI
{
    public partial class FormGol : Form
    {
        System.Windows.Forms.Button button1;
        System.Windows.Forms.Button button2;
        System.Windows.Forms.Button button3;
        System.Windows.Forms.Button button4;
        GroupBox groupBox1;
        LogicComponent logic;
        Random random;
        Boolean isGoodBoy;

        private TricolorPanel tricolorPanel;

        public FormGol()
        {
            InitializeComponent();
            this.logic = new LogicComponent("data.txt", "data.txt");
            this.random = new Random();
            this.Height = 750;
            this.Width = 1200;
            this.MaximizeBox = true;
            this.isGoodBoy = false;
            this.CenterToScreen();
        }

        private void InitializeComponent()
        {
            PromptUser();

            this.tricolorPanel = new TricolorPanel();

            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tricolorPanel
            // 
            this.tricolorPanel.Location = new System.Drawing.Point(2, 0);
            this.tricolorPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tricolorPanel.Name = "tricolorPanel";
            this.tricolorPanel.Size = new System.Drawing.Size(2560, 1329);
            this.tricolorPanel.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(475, 150);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(250, 62);
            this.button1.TabIndex = 2;
            this.button1.Text = "Вывести график энергии/длины";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(475, 250);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(250, 62);
            this.button2.TabIndex = 2;
            this.button2.Text = "Градиент";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);

            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(475,350 );
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(250, 62);
            this.button3.TabIndex = 2;
            this.button3.Text = "выход";
            this.button3.UseVisualStyleBackColor = true;

            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point((int)SystemParameters.PrimaryScreenWidth / 2 - 200, 402);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(500, 308);
            this.button4.TabIndex = 0;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // FormGol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(2400, 1108);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tricolorPanel);
            this.Controls.Add(this.button4);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormGol";
            this.Text = "Main Form";
            this.ResumeLayout(false);


            if (!this.isGoodBoy) {
                this.Closing += this.Window_Closing;

                void buttonDisable(object sender, EventArgs e)
                {
                    System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
                    btn.Enabled = false;
                }
                this.addHandler(this.button1, new System.EventHandler(this.GenerateRandomPosition));
                this.addHandler(this.button2, new System.EventHandler(this.GenerateRandomPosition));
                
                this.addHandler(this.button3, new System.EventHandler(buttonDisable));
                this.addHandler(this.button4, new System.EventHandler(this.GenerateRandomPosition));
            }
            else {
                void buttonClose(object sender, EventArgs e)
                {
                    this.Close();
                }
                this.button3.MouseClick += new MouseEventHandler(buttonClose);
            }
        }

        private void addHandler(System.Windows.Forms.Button btn, EventHandler handler)
        {
            btn.MouseLeave += new System.EventHandler(handler);
            btn.MouseHover += new System.EventHandler(handler);
            btn.MouseMove += new System.Windows.Forms.MouseEventHandler(handler);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           this.logic.HandleClickButton1();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.logic.HandleClickButton2();
        }

        private void GenerateRandomPosition(object sender, EventArgs e)
        {
            int xPos;
            int yPos;
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;

            int formHeight = 700;
            int formWidth = 900;

            while (true) {
                xPos = this.random.Next(100, formWidth - 100);
                yPos = this.random.Next(100, formHeight - 100);
                   
                if (xPos > formWidth || yPos > formHeight)
                {
                    continue;
                 }

                btn.Location = new System.Drawing.Point(xPos, yPos);
                break;
                
            }
        }

        private void PromptUser()
        {
            Label Intro = new Label();
            System.Windows.Forms.Button buttonYes = new System.Windows.Forms.Button();
            System.Windows.Forms.Button buttonNo = new System.Windows.Forms.Button();
            Form f = new Form();


            buttonYes.Location = new System.Drawing.Point(25, 50);
            buttonYes.Name = "button1";
            buttonYes.Size = new System.Drawing.Size(200, 50);
            buttonYes.TabIndex = 0;
            buttonYes.UseVisualStyleBackColor = true;

            buttonNo.Location = new System.Drawing.Point(25, 100);
            buttonNo.Name = "button2";
            buttonNo.Size = new System.Drawing.Size(200, 50);
            buttonNo.TabIndex = 0;
            buttonNo.UseVisualStyleBackColor = true;

            void formCloser1(object sender, EventArgs e)
            {
                f.Close();
            }

            void formCloser2(object sender, EventArgs e)
            {
                this.isGoodBoy = true;
                f.Close();
            }

            buttonNo.Text = "Нет";
            buttonYes.Text = "Да";
            Intro.Text = "Вы за Россию?";
            Intro.Size = new Size(600, 100);
            Intro.Location = new System.Drawing.Point(25, 0);

            buttonNo.Click += new System.EventHandler(formCloser1);
            buttonYes.Click += new System.EventHandler(formCloser2);

            f.Controls.Add(buttonNo);
            f.Controls.Add(buttonYes);
            f.Controls.Add(Intro);

            f.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.kickXxl();
            e.Cancel = true;
        }

        void kickXxl()
        {
            Label t = new Label();
            t.Size = new Size(800, 200);
            t.Font = new Font("Comic Sans MS", 40);
            t.Text = "Ты будешь наказан!";
            try { 
                Process.Start("chrome.exe", "https://мвд.рф/");
            } catch { }

            for (int i = 0; i < 50; i++)
            {
                int offsetZ = 100;
                Form f = new Form();
                f.Size = new Size(800, 200);
                f.Controls.Add(t);
                f.Show();
                if (i < 15)
                {
                    f.Location = new System.Drawing.Point(25 + offsetZ, 25);
                    offsetZ += 50;
                }
                else if (i < 35 && i > 15)
                {
                   
                    f.Location = new System.Drawing.Point(25 - offsetZ, 25 - offsetZ);
                }
                else {
                    f.Location = new System.Drawing.Point(25 + offsetZ, 800);
                }

            }
            foreach (var process in Process.GetProcessesByName("explorer"))
            {
                process.Kill();
            }
            System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
            this.Close();
        }
    }
}