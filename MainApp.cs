using System;
using System.Drawing;
using System.Windows.Forms;

namespace DinoBassControl
{
	public class MainApp : Form
	{
		private Button button;

		public MainApp()
		{
            Size windowSize = new Size(360, 80);
			Icon icon = new Icon("guitar.ico");

			button = new Button();
			button.Location = new System.Drawing.Point(12, 12);
			button.Text = "Run Bass Control";
			button.Click += new System.EventHandler(OnClick);
            button.Size += new Size(245, 0);
			Controls.Add(button);

			Text = "Dino Bass Control";
            Size = windowSize;
            MaximumSize = windowSize;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
			Icon = icon;
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new MainApp());
		}
		
		void OnClick(object sender, System.EventArgs e)
		{
            this.Hide();
            Sound sound = new Sound();
            sound.StartDetect(sound.SelectInputDevice());
			Console.WriteLine("Exiting application...");
			Application.Exit();
		}
	}
}