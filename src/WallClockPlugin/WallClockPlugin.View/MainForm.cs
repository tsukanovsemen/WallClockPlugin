using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallClockPlugin.View
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BuildButton_MouseEnter(object sender, EventArgs e)
        {
            BuildButton.Image = Properties.Resources.BuildButtonHovered_svg_52x52;
            ToolTip.Show("Построить объект", this.BuildButton);
        }

        private void BuildButton_MouseLeave(object sender, EventArgs e)
        {
            BuildButton.Image = Properties.Resources.BuildButton_svg_52x521;
        }

        private void GitHubButton_MouseEnter(object sender, EventArgs e)
        {
            ToolTip.Show("GitHub разработчика", this.GitHubButton);

        }

        private void SolidWorksButton_MouseEnter(object sender, EventArgs e)
        {
            ToolTip.Show("Сайт SolidWorks", this.SolidWorksButton);
        }
    }
}
