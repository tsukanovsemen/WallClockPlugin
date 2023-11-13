using System;
using System.Windows.Forms;
using System.Diagnostics;
using WallClockPlugin.Model;
using System.Collections.Generic;

namespace WallClockPlugin.View
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Builder для построения модели часов
        /// </summary>
        public WallClockBuilder Builder { get; private set; } = new WallClockBuilder();

        /// <summary>
        /// Параметры задаваемые для валидации и построения
        /// </summary>
        public WallClockParameters Parameters { get; private set; } = new WallClockParameters();

        /// <summary>
        /// Словарь ошибок
        /// </summary>
        private Dictionary<TextBox, string> ArgumentErrors { get; set; }

        /// <summary>
        /// Создание объекта главной формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeArgumentErrors();
        }

        /// <summary>
        /// Инициализация словаря ошибок
        /// </summary>
        private void InitializeArgumentErrors()
        {
            ArgumentErrors = new Dictionary<TextBox, string>()
            {
                { RadiusTextBox, string.Empty },
                { MinuteHandLengthTextBox, string.Empty },
                { HourHandLengthTextBox, string.Empty },
                { SideWidthTextBox, string.Empty },
                {SideHeightTextBox, string.Empty }
            };
        }

        /// <summary>
        /// Проверка формы на ввод ошибок
        /// </summary>
        /// <returns>True - если ошибок нет, False - если ошибки есть</returns>
        private bool CheckFormOnErrors()
        {
            string errors = string.Empty;
            bool resultCheck = true;

            foreach (var error in ArgumentErrors)
            {
                if (!string.IsNullOrEmpty(error.Value))
                {
                    errors += "- " + error.Value + "\n";
                    resultCheck = false;
                }
            }

            if (!resultCheck)
            {
                MessageBox.Show(errors, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return resultCheck;
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
            if (CheckFormOnErrors())
            {
                Builder.Build(Parameters);
            }
        }

        private void ClockStateOnlyHoursCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Parameters.OnlyHours = ClockStateOnlyHoursCheckBox.Checked;
        }

        private void RadiusTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Parameters.Radius = !string.IsNullOrEmpty(RadiusTextBox.Text)
                    ? float.Parse(RadiusTextBox.Text) : Parameters.MinRadius();
            }
            catch (ArgumentException exception)
            {
                RadiusTextBox.BackColor = ColorsWallClockPlugin.COLOR_ERROR;
                ArgumentErrors[RadiusTextBox] = exception.Message;

                return;
            }

            RadiusTextBox.BackColor = ColorsWallClockPlugin.COLOR_CORRECTLY;

            ArgumentErrors[RadiusTextBox] = string.Empty;

            LengthMinuteHandValueLabel.Text = Parameters.MinMinuteHandLength()
                + " - " + Parameters.MaxMinuteHandLength();

            LengthHourHandValueLabel.Text = Parameters.MinHourHandLength()
                + " - " + Parameters.MaxHourHandLength();
        }

        private void MinuteHandLengthTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Parameters.MinuteHandLength = !string.IsNullOrEmpty(MinuteHandLengthTextBox.Text)
                    ? float.Parse(MinuteHandLengthTextBox.Text) : Parameters.MinMinuteHandLength();
            }
            catch (ArgumentException exception)
            {
                MinuteHandLengthTextBox.BackColor = ColorsWallClockPlugin.COLOR_ERROR;
                ArgumentErrors[MinuteHandLengthTextBox] = exception.Message;

                return;
            }

            MinuteHandLengthTextBox.BackColor = ColorsWallClockPlugin.COLOR_CORRECTLY;
            ArgumentErrors[MinuteHandLengthTextBox] = string.Empty;
        }

        private void HourHandLengthTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Parameters.HourHandLength = !string.IsNullOrEmpty(HourHandLengthTextBox.Text)
                    ? float.Parse(HourHandLengthTextBox.Text) : Parameters.MinHourHandLength();
            }
            catch (ArgumentException exception)
            {
                HourHandLengthTextBox.BackColor = ColorsWallClockPlugin.COLOR_ERROR;
                ArgumentErrors[HourHandLengthTextBox] = exception.Message;

                return;
            }

            HourHandLengthTextBox.BackColor = ColorsWallClockPlugin.COLOR_CORRECTLY;
            ArgumentErrors[HourHandLengthTextBox] = string.Empty;
        }

        private void SideWidthTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Parameters.SideWidth = !string.IsNullOrEmpty(SideWidthTextBox.Text)
                    ? float.Parse(SideWidthTextBox.Text) : Parameters.MinSideWidth();
            }
            catch (ArgumentException exception)
            {
                SideWidthTextBox.BackColor = ColorsWallClockPlugin.COLOR_ERROR;
                ArgumentErrors[SideWidthTextBox] = exception.Message;

                return;
            }

            SideWidthTextBox.BackColor = ColorsWallClockPlugin.COLOR_CORRECTLY;
            ArgumentErrors[SideWidthTextBox] = string.Empty;
        }

        private void SideHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Parameters.SideHeight = !string.IsNullOrEmpty(SideHeightTextBox.Text)
                    ? float.Parse(SideHeightTextBox.Text) : Parameters.MinSideHeight();
            }
            catch (ArgumentException exception)
            {
                SideHeightTextBox.BackColor = ColorsWallClockPlugin.COLOR_ERROR;
                ArgumentErrors[SideHeightTextBox] = exception.Message;

                return;
            }

            SideHeightTextBox.BackColor = ColorsWallClockPlugin.COLOR_CORRECTLY;
            ArgumentErrors[SideHeightTextBox] = string.Empty;
        }
    }
}
