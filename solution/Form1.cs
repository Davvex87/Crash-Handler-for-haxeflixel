using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using System.Diagnostics;

namespace HaxeflixelCrashHandler
{
    public partial class HaxeflixelCrashHandler : Form
    {
        private string[] crashInfo;
        string appPath = Directory.GetCurrentDirectory();
        public HaxeflixelCrashHandler(string[] args)
        {
            crashInfo = args;
            InitializeComponent();
        }
        public struct LoadedFont
        {
            public Font Font { get; set; }
            public FontFamily FontFamily { get; set; }
        }

        /**
        ** THIS IS FOR LOADING CUSTOM FONTS
        **/
        public static LoadedFont LoadFont(string file, int fontSize, FontStyle fontStyle)
        {
            var fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile(file);
            if (fontCollection.Families.Length < 0)
            {
                throw new InvalidOperationException("No font familiy found when loading font");
            }

            var loadedFont = new LoadedFont();
            loadedFont.FontFamily = fontCollection.Families[0];
            loadedFont.Font = new Font(loadedFont.FontFamily, fontSize, fontStyle, GraphicsUnit.Pixel);
            return loadedFont;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string info = "To prevent leaving you in total confusion, heres some information we've gathered:" + "\n \n" + crashInfo[0];
            if (!File.Exists(appPath+"/logs"))
            {
                Directory.CreateDirectory(appPath+"/logs");
                Console.WriteLine("logs folder not found, creating");
            }
            else
            {
                Console.WriteLine("logs folder found!!!");
            }

            string dateNTime = DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString();
            string dateNTimeValid = dateNTime;
            dateNTimeValid = dateNTimeValid.Replace('/', '-');
            dateNTimeValid = dateNTimeValid.Replace(':', '-');

            string logName = dateNTimeValid + " - crashlog.txt";
            StreamWriter sw = new StreamWriter(appPath+"/logs/"+ logName);
            sw.WriteLine("Engine crash at "+dateNTime);
            sw.WriteLine("");
            sw.WriteLine(crashInfo[0]);
            sw.Close();

            string reportInfo = "If your game keeps crashing with the same error, report it to our github. A " + logName + " file has been created.";
            Font PhantomMuff = LoadFont(appPath+"/assets/__engine/5.ttf", 16, FontStyle.Regular).Font;
            Font PhantomMuffBig = LoadFont(appPath + "/assets/__engine/6.ttf", 16, FontStyle.Regular).Font;

            background.ImageLocation = appPath + "/assets/__engine/3.png";

            infoLabel.Parent = background;
            infoLabel.BackColor = Color.Transparent;
            infoLabel.Text = info;
            infoLabel.ForeColor = Color.White;
            infoLabel.Font = PhantomMuff;

            reportThisText.Font = PhantomMuff;
            reportThisText.Text = reportInfo;
            reportThisText.Parent = background;
            reportThisText.BackColor = Color.Transparent;
            reportThisText.ForeColor = Color.White;

            RelaunchButton.Text = "Relaunch Game";
            RelaunchButton.Font = PhantomMuffBig;
            RelaunchButton.Parent = background;
            RelaunchButton.BackColor = Color.Transparent;
            RelaunchButton.ForeColor = Color.White;
            RelaunchButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            RelaunchButton.FlatAppearance.MouseOverBackColor = Color.Transparent;

            Exitbutton.Text = "Exit";
            Exitbutton.Font = PhantomMuffBig;
            Exitbutton.Parent = background;
            Exitbutton.BackColor = Color.Transparent;
            Exitbutton.ForeColor = Color.White;
            Exitbutton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            Exitbutton.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        private void RelaunchButton_Click(object sender, EventArgs e)
        {
            string command = "cd/D " + crashInfo[1].Replace("\"", "/") + @" && "+@""""+ crashInfo[2] + "";
            Console.WriteLine(command);
            Process ps = new Process();
            ps.StartInfo.FileName = "cmd.exe";
            ps.StartInfo.CreateNoWindow = true;
            ps.StartInfo.RedirectStandardInput = true;
            ps.StartInfo.RedirectStandardOutput = true;
            ps.StartInfo.UseShellExecute = false;
            ps.Start();
            ps.StandardInput.WriteLine(command);
            Application.Exit();
            ps.StandardInput.Flush();
            ps.StandardInput.Close();
        }

        private void Exitbutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
