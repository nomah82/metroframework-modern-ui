using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;

namespace MetroFramework.Controls
{
    public class MetroTrackBarDouble : Control, IMetroControl
    {
        #region Interface

        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
            {
                CustomPaintBackground(this, e);
            }
        }

        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
            {
                CustomPaint(this, e);
            }
        }

        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
            {
                CustomPaintForeground(this, e);
            }
        }

        private MetroColorStyle metroStyle = MetroColorStyle.Default;
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        [DefaultValue(MetroColorStyle.Default)]
        public MetroColorStyle Style
        {
            get
            {
                if (DesignMode || metroStyle != MetroColorStyle.Default)
                {
                    return metroStyle;
                }

                if (StyleManager != null && metroStyle == MetroColorStyle.Default)
                {
                    return StyleManager.Style;
                }
                if (StyleManager == null && metroStyle == MetroColorStyle.Default)
                {
                    return MetroDefaults.Style;
                }

                return metroStyle;
            }
            set { metroStyle = value; }
        }

        private MetroThemeStyle metroTheme = MetroThemeStyle.Default;
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        [DefaultValue(MetroThemeStyle.Default)]
        public MetroThemeStyle Theme
        {
            get
            {
                if (DesignMode || metroTheme != MetroThemeStyle.Default)
                {
                    return metroTheme;
                }

                if (StyleManager != null && metroTheme == MetroThemeStyle.Default)
                {
                    return StyleManager.Theme;
                }
                if (StyleManager == null && metroTheme == MetroThemeStyle.Default)
                {
                    return MetroDefaults.Theme;
                }

                return metroTheme;
            }
            set { metroTheme = value; }
        }

        private MetroStyleManager metroStyleManager = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MetroStyleManager StyleManager
        {
            get { return metroStyleManager; }
            set { metroStyleManager = value; }
        }

        private bool useCustomBackColor = false;
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool UseCustomBackColor
        {
            get { return useCustomBackColor; }
            set { useCustomBackColor = value; }
        }

        private bool useCustomForeColor = false;
        [Browsable(false)]
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseCustomForeColor
        {
            get { return useCustomForeColor; }
            set { useCustomForeColor = value; }
        }

        private bool useStyleColors = false;
        [Browsable(false)]
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseStyleColors
        {
            get { return useStyleColors; }
            set { useStyleColors = value; }
        }

        [Browsable(false)]
        [Category(MetroDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(true)]
        public bool UseSelectable
        {
            get { return GetStyle(ControlStyles.Selectable); }
            set { SetStyle(ControlStyles.Selectable, value); }
        }

        #endregion

        #region Events

        public event EventHandler ValueMinChanged;
        private void OnValueMinChanged()
        {
            if (ValueMinChanged != null)
                ValueMinChanged(this, EventArgs.Empty);
        }

        public event EventHandler ValueMaxChanged;
        private void OnValueMaxChanged()
        {
            if (ValueMaxChanged != null)
                ValueMaxChanged(this, EventArgs.Empty);
        }

        /*public event ScrollEventHandler Scroll;
        private void OnScroll(ScrollEventType scrollType, int newValue)
        {
            if (Scroll != null)
                Scroll(this, new ScrollEventArgs(scrollType, newValue));
        }*/


        #endregion



        private Rectangle track1;
        private Rectangle track2;

        public MetroTrackBarDouble()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                                 ControlStyles.OptimizedDoubleBuffer |
                                 ControlStyles.ResizeRedraw |
                                 ControlStyles.Selectable |
                                 ControlStyles.SupportsTransparentBackColor |
                                 ControlStyles.UserMouse |
                                 ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;

            Minimum = 0;
            Maximum = 100;
            ValueMin=10;
            ValueMax=90;            
        }        

        private int barMinimum = 0;
        [DefaultValue(0)]
        public int Minimum
        {
            get { return barMinimum; }
            set
            {
                if (value < barMaximum)
                {
                    barMinimum = value;
                    /*if (trackerValue < barMinimum)
                    {
                        trackerValue = barMinimum;
                        if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    }*/
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Minimal value is greather than maximal one");
            }
        }


        private int barMaximum = 100;
        [DefaultValue(100)]
        public int Maximum
        {
            get { return barMaximum; }
            set
            {
                if (value > barMinimum)
                {
                    barMaximum = value;
                    /*if (trackerValue > barMaximum)
                    {
                        trackerValue = barMaximum;
                        if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    }*/
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Maximal value is lower than minimal one");
            }
        }


        private int trackerValueMin = 10;
        [DefaultValue(10)]
        public int ValueMin
        {
            get { return trackerValueMin; }
            set
            {
                if (value >= barMinimum & value <= barMaximum)
                {
                    trackerValueMin = value;
                    OnValueMinChanged();
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Value is outside appropriate range (min, max)");
            }
        }

        private int trackerValueMax = 90;
        [DefaultValue(10)]
        public int ValueMax
        {
            get { return trackerValueMax; }
            set
            {
                if (value >= barMinimum & value <= barMaximum)
                {
                    trackerValueMax = value;
                    OnValueMaxChanged();
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Value is outside appropriate range (min, max)");
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {              
            DrawTrackBar(e.Graphics, Color.DimGray, Color.DarkGray);
        }

        private void DrawTrackBar(Graphics g, Color thumbColor, Color barColor)
        {
            int TrackX1 = (((ValueMin - barMinimum) * (Width - 6)) / (barMaximum - barMinimum));
            int TrackX2 = (((ValueMax - barMinimum) * (Width - 6)) / (barMaximum - barMinimum));

            //la barra
            using (SolidBrush b = new SolidBrush(barColor))
            {
                Rectangle barRect = new Rectangle(0, Height / 2 - 2, Width , 4);
                g.FillRectangle(b, barRect);
            }

            //los indicadores
            Color cmin = thumbColor;
            Color cmax = thumbColor;
            if (Focused)
            {
                Color selected = MetroPaint.GetStyleColor(Style);
                if (!seleccionadoMax)
                    cmin = selected;
                else cmax = selected;
            }
            else if (isHovered)
            {
                Color selected = MetroPaint.ForeColor.Label.Normal(Theme);
                if (!seleccionadoMax)
                    cmin = selected;
                else cmax = selected;
            }

            using (SolidBrush b = new SolidBrush(cmin))
            {
                track1 = new Rectangle(TrackX1, Height/2 - 8, 6, 16);
                g.FillRectangle(b, track1);
            }
            using (SolidBrush b = new SolidBrush(cmax))
            {
                track2 = new Rectangle(TrackX2, Height/2 - 8, 6, 16);
                g.FillRectangle(b, track2);
            }
        }

        private bool seleccionadoMax = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (track1.Contains(e.Location))
                {
                    seleccionadoMax = false;
                    Invalidate();
                }
                else if (track2.Contains(e.Location))
                {
                    seleccionadoMax = true;
                    Invalidate();
                }
                else 
                    OnMouseMove(e);
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int v = e.Delta / 120 * (barMaximum - barMinimum) / 50;
            SetProperValue(GetProperValue() + v);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (/*Capture &*/ e.Button == MouseButtons.Left)
            {
                Point pt = e.Location;
                int p = pt.X;

                float coef = (float)(barMaximum - barMinimum) / (float)(ClientSize.Width - 3);
                int value = barMinimum;
                value = (int)(p * coef + barMinimum);

                if (value <= barMinimum)
                {
                    value = barMinimum;                    
                }
                else if (value >= barMaximum)
                {
                    value = barMaximum;                    
                }
                SetProperValue(value);
                Invalidate();                
            }
        }

        private void SetProperValue(int value)
        {
            int minSpace = 1;
            if (seleccionadoMax)
            {
                if (value < ValueMin + minSpace)
                    value = ValueMin + minSpace;
                else if (value > Maximum)
                    value = Maximum;
                ValueMax = value;
                OnValueMaxChanged();
            }
            else
            {
                if (value > ValueMax - minSpace)
                    value = ValueMax - minSpace;
                else if (value < Minimum)
                    value = Minimum;
                ValueMin = value;
                OnValueMinChanged();
            }
        }

        private int GetProperValue()
        {
            if (seleccionadoMax)
                return ValueMax;
            else return ValueMin;
        }

        private bool isFocused;
        protected override void OnGotFocus(EventArgs e)
        {
            isFocused = true;
            Invalidate();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            isFocused = true;
            Invalidate();
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            Invalidate();
            base.OnLeave(e);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                if (seleccionadoMax)
                    return base.ProcessDialogKey(keyData);
                else
                {
                        seleccionadoMax = true;
                        Invalidate();
                        return true;
                }
            }
            else if (keyData == (Keys.Shift | Keys.Tab))
            {
                if (!seleccionadoMax)
                    return base.ProcessDialogKey(keyData);
                else
                {
                        seleccionadoMax = false;
                        Invalidate();
                        return true;
                }
            }
            else
            {
                OnKeyDown(new KeyEventArgs(keyData));
                return true;
            }            
        }

        private bool isHovered = false;
        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            Invalidate();

            base.OnMouseLeave(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //isHovered = true;
            base.OnKeyDown(e);
            int Value = GetProperValue();
            int smallChange = 1;
            int largeChange = 10;

            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                    seleccionadoMax = !seleccionadoMax;
                    break;
                case Keys.Left:
                    SetProperValue(Value - (int)smallChange);
                    
                    break;
                case Keys.Right:
                    SetProperValue(Value + (int)smallChange);
                    
                    break;
                case Keys.Home:
                    SetProperValue(barMinimum);
                    break;
                case Keys.End:
                    SetProperValue(barMaximum);
                    break;
                case Keys.PageUp:
                    SetProperValue(Value - (int)largeChange);
                    
                    break;
                case Keys.PageDown:
                    SetProperValue(Value + (int)largeChange);                    
                    break;
            }

            Invalidate();
        }
    }
}
