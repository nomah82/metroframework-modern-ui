using MetroFramework.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace MetroFramework.Controls
{
    /// <summary>
    /// Renderer for the ModernUI-Toolstrip
    /// </summary>
    class metroToolStripRenderer : ToolStripProfessionalRenderer
    {
        MetroThemeStyle _theme;
        public metroToolStripRenderer(MetroThemeStyle theme, MetroColorStyle style)
            : base(new metrocolorscheme(theme, style))
        {
            _theme = theme;
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextFont= MetroFonts.Label(MetroLabelSize.Medium, MetroLabelWeight.Regular);
            if (e.Item.Selected && e.Item.Pressed == false)
                e.TextColor = MetroPaint.ForeColor.MenuToolStrip.Hover(_theme);
            else
                e.TextColor = MetroPaint.ForeColor.MenuToolStrip.Normal(_theme);

            base.OnRenderItemText(e);
        }

        public static Image AdjustRGBGamma(Image img, float red, float green, float blue, float gamma)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            ImageAttributes imgAttributes = new ImageAttributes();

            ColorMatrix matrix = new ColorMatrix(
                new float[][] {
                new float[] { red, 0, 0, 0, 0 },
                new float[] { 0, green, 0, 0, 0 },
                new float[] { 0, 0, blue, 0, 0 },
                new float[] { 0, 0, 0, 1.0f, 0 },
                new float[] { 0, 0, 0, 0, 1 },
                });

            imgAttributes.ClearColorMatrix();
            imgAttributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            imgAttributes.SetGamma(gamma, ColorAdjustType.Bitmap);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height),
                    0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
            }

            return (Image)bmp;
        }

        /// <summary>
        /// Render an item
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            // automatic adjust the color of attached images...
            if (e.Item.Selected && e.Item.Pressed == false)
                e.Graphics.DrawImageUnscaled(AdjustRGBGamma(e.Item.Image, 1f, 1f, 1f, 0.01f), e.ImageRectangle);
            else
            {
                if (_theme==MetroThemeStyle.Dark)
                    e.Graphics.DrawImageUnscaled(AdjustRGBGamma(e.Item.Image, 1f, 1f, 1f, 0.01f), e.ImageRectangle);
                else
                    base.OnRenderItemImage(e);
            }
        }

        /// <summary>
        /// ColorTable for the Renderer
        /// </summary>
        class metrocolorscheme : ProfessionalColorTable
        {
            MetroThemeStyle _theme;
            MetroColorStyle _style;
            public metrocolorscheme(MetroThemeStyle theme, MetroColorStyle style)
            {
                _theme = theme;
                _style = style;
            }

            private Color MetroBackColor()
            {
                return MetroPaint.BackColor.MenuToolStrip(_theme);
            }

            private Color MetroAccentColor()
            {
                return MetroPaint.GetStyleColor(_style);
            }

            public override Color ButtonSelectedHighlight
            {
                get { return ButtonSelectedGradientMiddle; }
            }
            public override Color ButtonSelectedHighlightBorder
            {
                get { return ButtonSelectedBorder; }
            }
            public override Color ButtonPressedHighlight
            {
                get { return ButtonPressedGradientMiddle; }
            }
            public override Color ButtonPressedHighlightBorder
            {
                get { return ButtonPressedBorder; }
            }
            public override Color ButtonCheckedHighlight
            {
                get { return ButtonCheckedGradientMiddle; }
            }
            public override Color ButtonCheckedHighlightBorder
            {
                get { return ButtonSelectedBorder; }
            }
            public override Color ButtonPressedBorder
            {
                get { return ButtonSelectedBorder; }
            }
            public override Color ButtonSelectedBorder
            {
                get { return MetroAccentColor(); }
            }
            public override Color ButtonCheckedGradientBegin
            {
                get { return MetroBackColor(); }
            }
            public override Color ButtonCheckedGradientMiddle
            {
                get { return MetroBackColor(); }
            }
            public override Color ButtonCheckedGradientEnd
            {
                get { return MetroBackColor(); }
            }
            public override Color ButtonSelectedGradientBegin
            {
                get { return MetroAccentColor(); }
            }
            public override Color ButtonSelectedGradientMiddle
            {
                get { return MetroAccentColor(); }
            }
            public override Color ButtonSelectedGradientEnd
            {
                get { return MetroAccentColor(); }
            }
            public override Color ButtonPressedGradientBegin
            {
                get { return MetroAccentColor(); }
            }
            public override Color ButtonPressedGradientMiddle
            {
                get { return MetroAccentColor(); }
            }
            public override Color ButtonPressedGradientEnd
            {
                get { return MetroAccentColor(); }
            }
            public override Color CheckBackground
            {
                get { return MetroBackColor(); }
            }
            public override Color CheckSelectedBackground
            {
                get { return MetroAccentColor(); }
            }
            public override Color CheckPressedBackground
            {
                get { return MetroAccentColor(); }
            }
            public override Color GripDark
            {
                get { return MetroPaint.ForeColor.MenuToolStrip.Normal(_theme); }
            }
            public override Color GripLight
            {
                get { return MetroBackColor(); }
            }
            public override Color ImageMarginGradientBegin
            {
                get { return MetroBackColor(); }
            }
            public override Color ImageMarginGradientMiddle
            {
                get { return MetroBackColor(); }
            }
            public override Color ImageMarginGradientEnd
            {
                get { return MetroBackColor(); }
            }
            public override Color ImageMarginRevealedGradientBegin
            {
                get { return MetroBackColor(); }
            }
            public override Color ImageMarginRevealedGradientMiddle
            {
                get { return MetroBackColor(); }
            }
            public override Color ImageMarginRevealedGradientEnd
            {
                get { return MetroBackColor(); }
            }
            public override Color MenuStripGradientBegin
            {
                get { return MetroBackColor(); }
            }
            public override Color MenuStripGradientEnd
            {
                get { return MetroBackColor(); }
            }
            public override Color MenuItemSelected
            {
                get { return MetroAccentColor(); }
            }
            public override Color MenuItemBorder
            {
                get { return MetroAccentColor(); }
            }
            public override Color MenuBorder
            {
                get { return MetroAccentColor(); }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return MetroAccentColor(); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return MetroAccentColor(); }
            }
            public override Color MenuItemPressedGradientBegin
            {
                get { return MetroBackColor(); }
            }
            public override Color MenuItemPressedGradientMiddle
            {
                get { return MetroBackColor(); }
            }
            public override Color MenuItemPressedGradientEnd
            {
                get { return MetroBackColor(); }
            }
            public override Color RaftingContainerGradientBegin
            {
                get { return MetroBackColor(); }
            }
            public override Color RaftingContainerGradientEnd
            {
                get { return MetroBackColor(); }
            }
            public override Color SeparatorDark
            {
                get { return MetroBackColor(); }
            }
            public override Color SeparatorLight
            {
                get { return MetroBackColor(); }
            }
            public override Color StatusStripGradientBegin
            {
                get { return MetroAccentColor(); }
            }
            public override Color StatusStripGradientEnd
            {
                get { return MetroAccentColor(); }
            }
            public override Color ToolStripBorder
            {
                get { return MetroBackColor(); }
            }
            public override Color ToolStripDropDownBackground
            {
                get { return MetroBackColor(); }
            }
            public override Color ToolStripGradientBegin
            {
                get { return MetroBackColor(); }
            }
            public override Color ToolStripGradientMiddle
            {
                get { return MetroBackColor(); }
            }
            public override Color ToolStripGradientEnd
            {
                get { return MetroBackColor(); }
            }
            public override Color ToolStripContentPanelGradientBegin
            {
                get { return MetroBackColor(); }
            }
            public override Color ToolStripContentPanelGradientEnd
            {
                get { return MetroBackColor(); }
            }
            public override Color ToolStripPanelGradientBegin
            {
                get { return MetroBackColor(); }
            }
            public override Color ToolStripPanelGradientEnd
            {
                get { return MetroBackColor(); }
            }
            public override Color OverflowButtonGradientBegin
            {
                get { return MetroBackColor(); }
            }
            public override Color OverflowButtonGradientMiddle
            {
                get { return MetroBackColor(); }
            }
            public override Color OverflowButtonGradientEnd
            {
                get { return MetroBackColor(); }
            }
        }
    }
}
