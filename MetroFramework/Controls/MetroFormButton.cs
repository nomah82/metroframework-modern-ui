using MetroFramework.Components;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MetroFramework.Controls
{
    public class MetroFormButton : Button, IMetroControl
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
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool UseCustomForeColor
        {
            get { return useCustomForeColor; }
            set { useCustomForeColor = value; }
        }

        private bool useStyleColors = false;
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool UseStyleColors
        {
            get { return useStyleColors; }
            set { useStyleColors = value; }
        }

        [Browsable(false)]
        [Category(MetroDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(false)]
        public bool UseSelectable
        {
            get { return GetStyle(ControlStyles.Selectable); }
            set { SetStyle(ControlStyles.Selectable, value); }
        }

        #endregion

        #region Fields

        private bool isHovered = false;
        private bool isPressed = false;

        #endregion
        public MetroFormButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
        }
        #region Paint Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            Color backColor, foreColor;

            MetroThemeStyle _Theme = Theme;
            if (Parent != null)
            {
                if (Parent is IMetroForm)
                {
                    _Theme = ((IMetroForm)Parent).Theme;
                    backColor = MetroPaint.BackColor.Form(_Theme);
                }
                else if (Parent is IMetroControl)
                {
                    //backColor = MetroPaint.GetStyleColor(Style);
                    backColor = MetroPaint.BackColor.Button.Normal(_Theme);
                }
                else
                {
                    backColor = Parent.BackColor;
                }
            }
            else
            {
                backColor = MetroPaint.BackColor.Form(_Theme);
            }

            if (isHovered && !isPressed && Enabled)
            {
                if (Parent is IMetroForm)
                {
                    foreColor = MetroPaint.ForeColor.Button.Normal(_Theme);
                    backColor = MetroPaint.BackColor.Button.Normal(_Theme);
                }
                else
                {
                    foreColor = MetroPaint.ForeColor.Button.Hover(_Theme);
                    backColor = MetroPaint.BackColor.Button.Hover(_Theme);
                }
                
            }
            else if (isHovered && isPressed && Enabled)
            {
                foreColor = MetroPaint.ForeColor.Button.Press(_Theme);
                backColor = MetroPaint.GetStyleColor(Style);
            }
            else if (!Enabled)
            {
                foreColor = MetroPaint.ForeColor.Button.Disabled(_Theme);
                backColor = MetroPaint.BackColor.Button.Disabled(_Theme);
            }
            else
            {
                foreColor = MetroPaint.ForeColor.Button.Normal(_Theme);
            }

            e.Graphics.Clear(backColor);
            Font buttonFont = new Font("Webdings", 9.25f);
            TextRenderer.DrawText(e.Graphics, Text, buttonFont, ClientRectangle, foreColor, backColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        #endregion

        #region Mouse Methods

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPressed = true;
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            Invalidate();

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            Invalidate();

            base.OnMouseLeave(e);
        }

        #endregion
    }
}
