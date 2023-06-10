using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace User_Controllers
{
    [DefaultEvent("ValueChanged")]
    public partial class DateComboBox : Control
    {

        #region Variables

        public event EventHandler ValueChanged;

        private readonly int PartConstWidth = 2 * SystemInformation.BorderSize.Width + SystemInformation.VerticalScrollBarWidth + 5;

        private bool onSizeChanged = true;

        private DateTime currentDate;

        private DateTime maximumDate = DateTime.Now.AddYears(3);

        private DateTime minimumDate = DateTime.Now.AddYears(-3);

        #endregion

        #region Constructors

        public DateComboBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        [Browsable(false)]
        public int Day => (int)DayPart.Items[DayPart.SelectedIndex];

        public DateTime MaximumDate
        {
            get => maximumDate;
            set
            {

                if (minimumDate > value)
                {
                    minimumDate = value;
                }

                if (maximumDate != value)
                {

                    maximumDate = value;

                    SetYearPart();

                }

            }
        }

        public DateTime MinimumDate
        {
            get => minimumDate;
            set
            {

                if (maximumDate < value)
                {
                    maximumDate = value;
                }

                if (minimumDate != value)
                {

                    minimumDate = value;

                    SetYearPart();

                }

            }
        }

        [Browsable(false)]
        public int Month => CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.ToList().IndexOf(MonthPart.Items[MonthPart.SelectedIndex].ToString()) + 1;

        public DateTime Value
        {
            get => DateTime.Parse(string.Format("{0}.{1}.{2}", DayPart.Items[DayPart.SelectedIndex], MonthPart.Items[MonthPart.SelectedIndex], YearPart.Items[YearPart.SelectedIndex]));
            set
            {
                if (minimumDate.Date <= value.Date && value.Date <= maximumDate.Date)
                {

                    YearPart.SelectedIndex = YearPart.Items.IndexOf(value.Year);

                    MonthPart.SelectedIndex = MonthPart.Items.IndexOf(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(value.Month));

                    DayPart.SelectedIndex = DayPart.Items.IndexOf(value.Day);

                    currentDate = value.Date;

                }
            }
        }

        [Browsable(false)]
        public int Year => (int)YearPart.Items[YearPart.SelectedIndex];

        #endregion

        #region Events

        private void DayPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DayPart.SelectedIndex != -1 && Day != currentDate.Day)
            {

                currentDate = Value;

                ValueChanged?.Invoke(this, EventArgs.Empty);

            }
        }

        private void MonthPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MonthPart.SelectedIndex != -1 && Month != currentDate.Month)
            {
                SetDayPart();
            }
        }

        private void YearPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (YearPart.SelectedIndex != -1 && Year != currentDate.Year)
            {

                var minMonth = Year == minimumDate.Year ? minimumDate.Month : 1;

                var maxMonth = Year == maximumDate.Year ? maximumDate.Month : 12;

                var c = MonthPart.Items.Count - 1;

                var change =
                    MonthPart.Items[0].ToString() != CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(minMonth) ||
                    MonthPart.Items[c].ToString() != CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(maxMonth);

                if (change)
                {

                    var month = Month;

                    var names = new List<string>();

                    for (var m = minMonth; m <= maxMonth; m++)
                    {
                        names.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m));
                    }

                    MonthPart.Items.Clear();

                    MonthPart.Items.AddRange(names.ToArray());

                    var index = MonthPart.Items.IndexOf(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month));

                    if (index == -1)
                    {
                        MonthPart.SelectedIndex = MonthPart.Items.Count - 1;
                    }

                    else
                    {

                        MonthPart.SelectedIndex = index;

                        change = false;

                    }

                }

                if (!change)
                {
                    SetDayPart();
                }

            }
        }

        #endregion

        #region Overrides

        protected override void OnFontChanged(EventArgs e)
        {

            var borderH = 2 * SystemInformation.BorderSize.Height;

            var itemH = TextRenderer.MeasureText("T", Font).Height;

            var dropH = 7 * (itemH * (int)Math.Log(itemH * itemH, Font.Size) - 1) + borderH;

            DayPart.DropDownHeight = MonthPart.DropDownHeight = YearPart.DropDownHeight = dropH;

            DayPart.ItemHeight = MonthPart.ItemHeight = YearPart.ItemHeight = itemH + borderH + 1;

            DayPart.Size = new Size(TextRenderer.MeasureText("99", Font).Width + PartConstWidth, itemH + borderH);

            MonthPart.Size = new Size(TextRenderer.MeasureText("TEMMUZ", Font).Width + PartConstWidth, itemH + borderH);

            YearPart.Size = new Size(TextRenderer.MeasureText("9999", Font).Width + PartConstWidth, itemH + borderH);

            MonthPart.Left = DayPart.Right + 6;

            YearPart.Left = MonthPart.Right + 6;

            onSizeChanged = false;

            MaximumSize = MinimumSize = Size.Empty;

            MaximumSize = new Size(YearPart.Right * 5 / 4, DayPart.Height);

            MinimumSize = new Size(YearPart.Right, DayPart.Height);

            Size = MinimumSize;

            onSizeChanged = true;

        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (onSizeChanged)
            {

                MonthPart.Width += (Width - YearPart.Right) / 2;

                YearPart.Left = MonthPart.Right + 6;

                YearPart.Width = Width - YearPart.Left;

            }
        }

        #endregion

        #region Helper Functions

        private void SetDayPart()
        {

            var minDay = 1;

            var maxDay = DateTime.DaysInMonth(Year, Month);

            if (Year == minimumDate.Year && Month == minimumDate.Month)
            {
                minDay = minimumDate.Day;
            }

            if (Year == maximumDate.Year && Month == maximumDate.Month)
            {
                maxDay = maximumDate.Day;
            }

            var c = DayPart.Items.Count - 1;

            var change = (int)DayPart.Items[0] != minDay || (int)DayPart.Items[c] != maxDay;

            if (change)
            {

                var day = Day;

                DayPart.Items.Clear();

                for (var d = minDay; d <= maxDay; d++)
                {
                    DayPart.Items.Add(d);
                }

                var index = DayPart.Items.IndexOf(day);

                if (index == -1)
                {
                    DayPart.SelectedIndex = DayPart.Items.Count - 1;
                }

                else
                {

                    DayPart.SelectedIndex = index;

                    change = false;

                }

            }

            if (!change)
            {

                currentDate = Value;

                ValueChanged?.Invoke(this, EventArgs.Empty);

            }

        }

        private void SetYearPart()
        {

            var year = Year;

            YearPart.Items.Clear();

            for (var y = minimumDate.Year; y <= maximumDate.Year; y++)
            {
                YearPart.Items.Add(y);
            }

            var index = YearPart.Items.IndexOf(year);

            if (index == -1)
            {
                YearPart.SelectedIndex = YearPart.Items.Count - 1;
            }

            else
            {
                YearPart.SelectedIndex = index;
            }

        }

        #endregion

    }

    partial class DateComboBox
    {

        private ComboBox DayPart;

        private ComboBox MonthPart;

        private ComboBox YearPart;

        private void InitializeComponent()
        {

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            DayPart = new ComboBox();

            MonthPart = new ComboBox();

            YearPart = new ComboBox();

            void DrawItemEvent(object s, DrawItemEventArgs e)
            {
                if (e.Index > -1)
                {

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(ForeColor), e.Bounds);
                    }

                    else e.DrawBackground();

                    e.Graphics.DrawString((s as ComboBox).Items[e.Index].ToString(), Font, Brushes.Black, e.Bounds, new StringFormat(StringFormatFlags.NoWrap) { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });

                    e.DrawFocusRectangle();

                }
            }

            void MeasureItemEvent(object s, MeasureItemEventArgs e)
            {

                var h = TextRenderer.MeasureText("T", Font).Height;

                e.ItemHeight = h * (int)Math.Log(h * h, Font.Size) - 1;

            }

            SuspendLayout();

            #region DayPart

            for (var i = 1; i <= 31; i++)
            {
                DayPart.Items.Add(i);
            }

            DayPart.DrawMode = DrawMode.OwnerDrawVariable;

            DayPart.DropDownHeight = 177;

            DayPart.DropDownStyle = ComboBoxStyle.DropDownList;

            DayPart.Location = new Point(0, 0);

            DayPart.SelectedIndex = DayPart.Items.IndexOf(DateTime.Now.Day);

            DayPart.Width = TextRenderer.MeasureText("99", Font).Width + PartConstWidth;

            DayPart.DrawItem += DrawItemEvent;

            DayPart.MeasureItem += MeasureItemEvent;

            DayPart.SelectedIndexChanged += DayPart_SelectedIndexChanged;

            #endregion

            #region MonthPart

            for (var i = 1; i <= 12; i++)
            {
                MonthPart.Items.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
            }

            MonthPart.DrawMode = DrawMode.OwnerDrawVariable;

            MonthPart.DropDownHeight = 177;

            MonthPart.DropDownStyle = ComboBoxStyle.DropDownList;

            MonthPart.Location = new Point(DayPart.Right + 6, 0);

            MonthPart.SelectedIndex = MonthPart.Items.IndexOf(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month));

            MonthPart.Width = TextRenderer.MeasureText("TEMMUZ ", Font).Width + PartConstWidth;

            MonthPart.DrawItem += DrawItemEvent;

            MonthPart.MeasureItem += MeasureItemEvent;

            MonthPart.SelectedIndexChanged += MonthPart_SelectedIndexChanged;

            #endregion

            #region YearPart

            for (var i = minimumDate.Year; i <= maximumDate.Year; i++)
            {
                YearPart.Items.Add(i);
            }

            YearPart.DrawMode = DrawMode.OwnerDrawVariable;

            YearPart.DropDownHeight = 177;

            YearPart.DropDownStyle = ComboBoxStyle.DropDownList;

            YearPart.Location = new Point(MonthPart.Right + 6, 0);

            YearPart.SelectedIndex = YearPart.Items.IndexOf(DateTime.Now.Year);

            YearPart.Width = TextRenderer.MeasureText("9999", Font).Width + PartConstWidth;

            YearPart.DrawItem += DrawItemEvent;

            YearPart.MeasureItem += MeasureItemEvent;

            YearPart.SelectedIndexChanged += YearPart_SelectedIndexChanged;

            #endregion

            #region DateComboBox

            BackColor = Color.Transparent;

            Controls.Add(DayPart);

            Controls.Add(MonthPart);

            Controls.Add(YearPart);

            ForeColor = Color.LightSkyBlue;

            MaximumSize = new Size(YearPart.Right * 5 / 4, DayPart.Height);

            MinimumSize = new Size(YearPart.Right, DayPart.Height);

            #endregion

            ResumeLayout(false);

        }

    }
}