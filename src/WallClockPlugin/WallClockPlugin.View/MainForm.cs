namespace WallClockPlugin.View
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Forms;
    using WallClockPlugin.Model;

    /// <summary>
    /// Главная форма класса.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Создание объекта главной формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeArgumentErrors();
            SetValueLabels();
        }

        /// <summary>
        /// Builder для построения модели часов.
        /// </summary>
        public WallClockBuilder Builder { get; private set; } = new WallClockBuilder();

        /// <summary>
        /// Параметры задаваемые для проверки и построения.
        /// </summary>
        public WallClockParameters Parameters { get; private set; } = new WallClockParameters();

        /// <summary>
        /// Словарь ошибок.
        /// </summary>
        private Dictionary<TextBox, string> ArgumentErrors { get; set; }

        /// <summary>
        /// Инициализация словаря ошибок.
        /// </summary>
        private void InitializeArgumentErrors()
        {
            ArgumentErrors = new Dictionary<TextBox, string>()
            {
                { RadiusTextBox, string.Empty },
                { MinuteHandLengthTextBox, string.Empty },
                { HourHandLengthTextBox, string.Empty },
                { SideWidthTextBox, string.Empty },
                { SideHeightTextBox, string.Empty },
                { CutRadiusTextBox, string.Empty },
                { CutsCountTextBox, string.Empty },
            };
        }

        /// <summary>
        /// Проверка формы на ввод ошибок.
        /// </summary>
        /// <returns>True - если ошибок нет, False - если ошибки есть.</returns>
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

        /// <summary>
        /// Проверка на правильность ввода символа в поле.
        /// </summary>
        /// <param name="character">Символ.</param>
        /// <returns>true - если введен некорректный символ, false - если символ корректен.</returns>
        private bool CheckIncorrectedInputCharacter(char character)
        {
            return !char.IsDigit(character) && !char.IsControl(character);
        }

        /// <summary>
        /// Установка начальных значений для всех Label параметров.
        /// </summary>
        private void SetValueLabels()
        {
            RadiusValueLabel.Text = $"{Parameters.MinRadius()} - {Parameters.MaxRadius()}";

            LengthMinuteHandValueLabel.Text = $"{Parameters.MinMinuteHandLength()} " +
                $"- {Parameters.MaxMinuteHandLength()}";

            LengthHourHandValueLabel.Text = $"{Parameters.MinHourHandLength()} " +
                $"- {Parameters.MaxHourHandLength()}";

            SideWidthValueLabel.Text = $"{Parameters.MinSideWidth()} " +
                $"- {Parameters.MaxSideWidth()}";

            SideHeightValueLabel.Text = $"{Parameters.MinSideHeight()} " +
                $"- {Parameters.MaxSideHeight()}";

            CutRadiusValueLabel.Text = $"{Parameters.MinCutRadius()}" +
                $" - {Parameters.MaxCutRadius()}";

            CutsCountValueLabel.Text = $"{Parameters.MinCutsCount()} " +
                $"- {Parameters.MaxCutsCount()}";
        }

        private void BuildButton_MouseEnter(object sender, EventArgs e)
        {
            BuildButton.Image = Properties.Resources.BuildButton_hovered_52x52;
            ToolTip.Active = true;
            ToolTip.Show("Построить объект", BuildButton);
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
            ToolTip.Show("GitHub разработчика", GitHubButton);
        }

        private void SolidWorksButton_MouseEnter(object sender, EventArgs e)
        {
            SolidWorksButton.Image = Properties.Resources.SolidWorksButton_hovered_52x52;
            ToolTip.Active = true;
            ToolTip.Show("Сайт SolidWorks", SolidWorksButton);
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

            CutsCountValueLabel.Text = $"{Parameters.MinCutsCount()} - {Parameters.MaxCutsCount()}";
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

            CutRadiusValueLabel.Text = $"{Parameters.MinCutRadius()} - {Parameters.MaxCutRadius()}";
            CutsCountValueLabel.Text = $"{Parameters.MinCutsCount()} - {Parameters.MaxCutsCount()}";
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

        private void RadiusTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var character = e.KeyChar;
            e.Handled = CheckIncorrectedInputCharacter(character);
        }

        private void MinuteHandLengthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var character = e.KeyChar;
            e.Handled = CheckIncorrectedInputCharacter(character);
        }

        private void HourHandLengthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var character = e.KeyChar;
            e.Handled = CheckIncorrectedInputCharacter(character);
        }

        private void SideWidthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var character = e.KeyChar;
            e.Handled = CheckIncorrectedInputCharacter(character);
        }

        private void SideHeightTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var character = e.KeyChar;
            e.Handled = CheckIncorrectedInputCharacter(character);
        }

        private void AddSideCutsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CutsCountTextBox.Enabled = AddSideCutsCheckBox.Checked;
            CutRadiusTextBox.Enabled = AddSideCutsCheckBox.Checked;
            Parameters.SideCuts = AddSideCutsCheckBox.Checked;
        }

        private void CutRadiusTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Parameters.CutRadius = !string.IsNullOrEmpty(CutRadiusTextBox.Text)
                    ? float.Parse(CutRadiusTextBox.Text) : Parameters.MinCutRadius();
            }
            catch (ArgumentException exception)
            {
                CutRadiusTextBox.BackColor = ColorsWallClockPlugin.COLOR_ERROR;
                ArgumentErrors[CutRadiusTextBox] = exception.Message;

                return;
            }

            CutRadiusTextBox.BackColor = ColorsWallClockPlugin.COLOR_CORRECTLY;
            ArgumentErrors[CutRadiusTextBox] = string.Empty;

            CutsCountValueLabel.Text = $"{Parameters.MinCutsCount()} - {Parameters.MaxCutsCount()}";
        }

        private void CutRadiusTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var character = e.KeyChar;
            e.Handled = CheckIncorrectedInputCharacter(character);
        }

        private void CutsCountTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Parameters.CutsCount = !string.IsNullOrEmpty(CutsCountTextBox.Text)
                    ? int.Parse(CutsCountTextBox.Text) : Parameters.MinCutsCount();
            }
            catch (ArgumentException exception)
            {
                CutsCountTextBox.BackColor = ColorsWallClockPlugin.COLOR_ERROR;
                ArgumentErrors[CutsCountTextBox] = exception.Message;

                return;
            }

            CutsCountTextBox.BackColor = ColorsWallClockPlugin.COLOR_CORRECTLY;
            ArgumentErrors[CutsCountTextBox] = string.Empty;
        }

        private void CutsCountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var character = e.KeyChar;
            e.Handled = CheckIncorrectedInputCharacter(character);
        }
    }
}