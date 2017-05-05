using MetroFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MetroFramework.Components;
using MetroFramework.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace MetroFramework.Controls
{    
    /// <summary>
    /// Menustrip for ModernUI-GUIs
    /// </summary>
    public class MetroMenuStrip : System.Windows.Forms.MenuStrip, IMetroControl, IMetroComponent
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MetroMenuStrip()
            : base()
        {
            Renderer = new metroToolStripRenderer(Theme, Style);
            Font = MetroFonts.Label(MetroLabelSize.Medium,MetroLabelWeight.Bold);
            ForeColor = MetroPaint.ForeColor.MenuToolStrip.Normal(Theme);
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
            set
            {
                metroStyle = value;
                Renderer = new metroToolStripRenderer(Theme, Style);
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
            set
            {
                metroTheme = value;
                Renderer = new metroToolStripRenderer(Theme, Style);
            }
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

        public event EventHandler<MetroPaintEventArgs> CustomPaint;
        public event EventHandler<MetroPaintEventArgs> CustomPaintBackground;
        public event EventHandler<MetroPaintEventArgs> CustomPaintForeground;

        /// <summary>
        /// OnItemAdded-Event we adjust the font and forecolor of this item
        /// </summary>
        /// <param name="e"></param>
        protected override void OnItemAdded(System.Windows.Forms.ToolStripItemEventArgs e)
        {
            base.OnItemAdded(e);

            e.Item.Font = MetroFonts.Label(MetroLabelSize.Medium, MetroLabelWeight.Light);
            e.Item.ForeColor = MetroPaint.ForeColor.MenuToolStrip.Normal(Theme);
        }
    }
}
