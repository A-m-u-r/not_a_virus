using System;
using System.Windows.Forms;
using MainApp.UI;

namespace MainApp
{
    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormGol formGol = new FormGol();

            System.Media.SoundPlayer player = new System.Media.SoundPlayer();

            player.SoundLocation = "putinbossfightOST.wav";
            player.Play();

            Application.Run(formGol);
        }
    }
}