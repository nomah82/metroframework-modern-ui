using MetroFramework.Interfaces;
using System;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Drawing;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MetroFramework.Controls
{
    
    public class MetroIconButton : Control, IMetroControl
    {
        public MetroIconButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.UserPaint, true);
        }

        private Image _iconoBoton;
        public Image IconoBoton
        {
            get { return _iconoBoton; }
            set
            {
                _iconoBoton = value;
                createimages();
            }
        }

        private Image _styleColorImg = null;

        private Image _lightimg=null;
        private Image _darkimg=null;

        private Image _disableLight = null;
        private Image _disableDark = null;


        private Image _darklightimg=null;
        private Image _lightlightimg=null;

        private void createimages()
        {
            if (_iconoBoton != null)
            {
                Color style = MetroPaint.GetStyleColor(Style);
                _styleColorImg = MetroImage.WhiteBlackToTransparentFore(new Bitmap(_iconoBoton), style);
                if (!applyGrayToIcon)
                {
                    
                    Color fore = MetroPaint.ForeColor.IconButton.Normal(MetroThemeStyle.Light);
                    _lightimg = MetroImage.WhiteBlackToTransparentForeStyle(new Bitmap(_iconoBoton), fore, style);
                    fore = MetroPaint.ForeColor.IconButton.Normal(MetroThemeStyle.Dark);
                    _darkimg = MetroImage.WhiteBlackToTransparentForeStyle(new Bitmap(_iconoBoton), fore, style);
                    fore = MetroPaint.ForeColor.IconButton.Hover(MetroThemeStyle.Light);
                    _lightlightimg = MetroImage.WhiteBlackToTransparentForeStyle(new Bitmap(_iconoBoton), fore, style);
                    fore = MetroPaint.ForeColor.IconButton.Hover(MetroThemeStyle.Dark);
                    _darklightimg = MetroImage.WhiteBlackToTransparentForeStyle(new Bitmap(_iconoBoton), fore, style);

                    fore = MetroPaint.ForeColor.IconButton.Disable(MetroThemeStyle.Light);
                    _disableLight = MetroImage.WhiteBlackToTransparentForeStyle(new Bitmap(_iconoBoton), fore, fore);
                    _disableLight = MetroImage.ConvertGrayScale(_disableLight);

                    fore = MetroPaint.ForeColor.IconButton.Disable(MetroThemeStyle.Dark);
                    _disableDark = MetroImage.WhiteBlackToTransparentForeStyle(new Bitmap(_iconoBoton), fore, fore);
                    _disableDark = MetroImage.ConvertGrayScale(_disableDark);

                }
                else
                {                                       
                    if (useCustomBackColor)
                    {
                        Color fore = MetroPaint.ForeColor.IconButton.Normal(MetroThemeStyle.Light);
                        Color back = BackColor;
                        
                        _lightimg = MetroImage.WhiteBlackToBackFore(new Bitmap(_iconoBoton), back, fore);
                        fore = MetroPaint.ForeColor.IconButton.Normal(MetroThemeStyle.Dark);
                        _darkimg = MetroImage.WhiteBlackToBackFore(new Bitmap(_iconoBoton), back, fore);
                        fore = MetroPaint.ForeColor.IconButton.Hover(MetroThemeStyle.Dark);
                        _lightlightimg = MetroImage.WhiteBlackToBackFore(new Bitmap(_iconoBoton), back, fore);
                        _darklightimg = _lightlightimg;
                    }
                    else
                    {
                        Color fore = MetroPaint.ForeColor.IconButton.Normal(MetroThemeStyle.Light);
                        _lightimg = MetroImage.WhiteBlackToTransparentFore(new Bitmap(_iconoBoton), fore);
                        fore = MetroPaint.ForeColor.IconButton.Normal(MetroThemeStyle.Dark);
                        _darkimg = MetroImage.WhiteBlackToTransparentFore(new Bitmap(_iconoBoton), fore);
                        fore = MetroPaint.ForeColor.IconButton.Hover(MetroThemeStyle.Dark);
                        _lightlightimg = MetroImage.WhiteBlackToTransparentFore(new Bitmap(_iconoBoton), fore);
                        _darklightimg = _lightlightimg;

                    }

                }
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
            set { metroStyle = value;
                
            }
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
            set { metroStyleManager = value;
            createimages();
            }
        }

        private bool useCustomBackColor = false;
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool UseCustomBackColor
        {
            get { return useCustomBackColor; }
            set {
                useCustomBackColor = value;
                createimages();
            }
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

        private int notifyValue = 0;
        [DefaultValue(0)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public int NotifyValue
        {
            get { return notifyValue; }
            set { notifyValue = value; }

        }

        private int notifySize = 17;
        [DefaultValue(17)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public int NotifySize
        {
            get { return notifySize; }
            set { notifySize = value; }
        }
		
		private bool applyGrayToIcon = true;
        [DefaultValue(true)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool ApplyGrayToIcon
        {
            get { return applyGrayToIcon; }
            set {
                applyGrayToIcon = value;
                createimages();
            }
        }

        private bool styleColorMode = false;
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool StyleColorMode
        {
            get { return styleColorMode; }
            set
            {
                styleColorMode = value;
                Invalidate();
            }
        }	


        [Browsable(false)]
        [Category(MetroDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(false)]
        public bool UseSelectable
        {
            get { return GetStyle(ControlStyles.Selectable); }
            set { SetStyle(ControlStyles.Selectable, value); }
        }

        public event EventHandler<MetroPaintEventArgs> CustomPaint;
        public event EventHandler<MetroPaintEventArgs> CustomPaintBackground;
        public event EventHandler<MetroPaintEventArgs> CustomPaintForeground;


/*        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                base.OnPaintBackground(e);
            }
            catch
            {
                Invalidate();
            }
        } */

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }

                OnPaintForeground(e);
            }
            catch
            {
                Invalidate();
            }
        }

        Color foreColor;
        private bool isHovered = false;
        private bool isPressed = false;
        private bool isFocused = false;


        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            //e.Graphics.Clear(Color.Transparent);
            if (_lightimg != null && _darkimg != null)
            {
                Bitmap bmp;
                if (!Enabled)
                {
                    if (Theme == MetroThemeStyle.Light)
                        bmp = new Bitmap(_disableLight);
                    else bmp = new Bitmap(_disableDark);
                }
                else if (Theme == MetroThemeStyle.Light)
                {
                    if (isHovered && !isPressed && Enabled)
                        bmp = new Bitmap(_lightlightimg);
                    else if (styleColorMode)
                        bmp = new Bitmap(_styleColorImg);
                    else bmp = new Bitmap(_lightimg);
                }
                else
                {
                    if (isHovered && !isPressed && Enabled)
                        bmp = new Bitmap(_darklightimg);
                    else if (styleColorMode)
                        bmp = new Bitmap(_styleColorImg);
                    else
                        bmp = new Bitmap(_darkimg);

                }
                if (bmp != null)
                {
                    int paddingx = 0;
                    if (notifyValue > 0)
                        paddingx = notifySize / 2;
                    int paddingy = bmp.Height * paddingx / bmp.Width;
                    e.Graphics.DrawImage(bmp, new Rectangle(0, paddingy, Width - paddingx, Height - paddingy));
                }
            }

            if (notifyValue > 0)
            {
                var size = notifySize;
                var g = e.Graphics;
                var x = Width - size - 2;
                float y = 1;

                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;

                var f = new Font("Arial", 8f, FontStyle.Regular);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawEllipse(new Pen(Color.DarkRed), new RectangleF(x, y, size, size));
                g.FillEllipse(new SolidBrush(Color.DarkRed), new RectangleF(x, y, size, size));
                g.DrawString(notifyValue.ToString(), f, new SolidBrush(Color.White), new RectangleF(x + 0.5f, y + 0.0f, size, size), format);
            }
        }

        #region Focus Methods

        protected override void OnGotFocus(EventArgs e)
        {
            isFocused = true;
            isPressed = false;
            Invalidate();

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            Invalidate();

            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            isFocused = true;
            isPressed = true;
            Invalidate();

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            Invalidate();

            base.OnLeave(e);
        }

        #endregion

        #region Keyboard Methods

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                isHovered = true;
                isPressed = true;
                Invalidate();
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (!isFocused)
            {
                isHovered = false;
                isPressed = false;
            }
            Invalidate();

            base.OnKeyUp(e);
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

                //if (Name == "lnkClear" && Parent.GetType().Name == "MetroTextBox") this.PerformClick();
                //if (Name == "lnkClear" && Parent.GetType().Name == "SearchControl") this.PerformClick();
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
            isPressed = false;

            Invalidate();

            base.OnMouseLeave(e);
        }

        #endregion

        #region Overridden Methods

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        #endregion



    }
}
