using System;
using System.Windows.Forms;
using System.Diagnostics;
using WallClockPlugin.Model;

namespace WallClockPlugin.View
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Builder для построения модели часов
        /// </summary>
        public WallClockBuilder Builder { get; private set; } = new WallClockBuilder();

        public WallClockParameters Parameters { get; private set; } = new WallClockParameters();

        public MainForm()
        {
            InitializeComponent();
        }

        private void BuildButton_MouseEnter(object sender, EventArgs e)
        {
            BuildButton.Image = Properties.Resources.BuildButton_hovered_52x52;
            ToolTip.Active = true;
            ToolTip.Show("Построить объект", this.BuildButton);
        }

        private void BuildButton_MouseLeave(object sender, EventArgs e)
        {
            BuildButton.Image = Properties.Resources.BuildButton_52x52;
            ToolTip.Active = false;
        }

        private void GitHubButton_MouseEnter(object sender, EventArgs e)
        {
            GitHubButton.Image = Properties.Resources.GitHubButton_hovered_52x52;
            ToolTip.Active = true;
            ToolTip.Show("GitHub разработчика", this.GitHubButton);
        }

        private void SolidWorksButton_MouseEnter(object sender, EventArgs e)
        {
            SolidWorksButton.Image = Properties.Resources.SolidWorksButton_hovered_52x52;
            ToolTip.Active = true;
            ToolTip.Show("Сайт SolidWorks", this.SolidWorksButton);
        }

        private void GitHubButton_MouseLeave(object sender, EventArgs e)
        {
            GitHubButton.Image = Properties.Resources.GitHubButton_52x52;
            ToolTip.Active = false;
        }

        private void SolidWorksButton_MouseLeave(object sender, EventArgs e)
        {
            SolidWorksButton.Image = Properties.Resources.SolidWorksButton_52x52;
            ToolTip.Active = false;
        }

        private void SolidWorksButton_Click(object sender, EventArgs e)
        {
            var url = "https://www.solidworks.com/";
            Process.Start(url);
        }

        private void GitHubButton_Click(object sender, EventArgs e)
        {
            var url = "https://github.com/tsukanovsemen";
            Process.Start(url);
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            Parameters.Radius = 200;
            Parameters.SideWidth = 60;
            Parameters.SideHeight = 40;
            Parameters.MinuteHandLength = 119;
            Parameters.HourHandLength = 59;

            Builder.Build(Parameters);
        }
    }
}
