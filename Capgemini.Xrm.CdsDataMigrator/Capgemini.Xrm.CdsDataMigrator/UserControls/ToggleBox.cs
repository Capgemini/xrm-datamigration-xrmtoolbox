using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class ToggleCheckBox : CheckBox
    {
        public ToggleCheckBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            Padding = new Padding(4);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            OnPaintBackground(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var path = new GraphicsPath())
            {
                var d = Padding.All;
                var r = Height - 2 * d;
                path.AddArc(d, d, r, r, 90, 180);
                path.AddArc(this.Width - r - d, d, r, r, -90, 180);
                path.CloseFigure();
                pevent.Graphics.FillPath(Brushes.LightGray, path);
                r = Height - 1;
                var rect = Checked ? new Rectangle(Width - r - 1, 0, r, r)
                                   : new Rectangle(0, 0, r, r);
                pevent.Graphics.FillEllipse(Checked ? SystemBrushes.ActiveCaption : Brushes.DarkGray, rect);
            }
        }
    }
}