using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Novah.core
{
    class FlatButton : Control
    {
        private SolidBrush borderBrush, textBrush;

        private Rectangle borderRectangle;

        private bool active = false;

        private StringFormat stringFormat = new StringFormat();

        public override Cursor Cursor { get; set; } = Cursors.Hand;

        public float BorderThickness { get; set; } = 2;

        public FlatButton()
        {
            borderBrush = new SolidBrush(ColorTranslator.FromHtml("#D499B9"));
            textBrush = new SolidBrush(ColorTranslator.FromHtml("#011638"));

            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            this.Paint += Button_Paint;
        }

        private void Button_Paint(object sender, PaintEventArgs e)
        {
            borderRectangle = new Rectangle(0, 0, Width, Height);
            e.Graphics.DrawRectangle(new Pen(borderBrush, BorderThickness), borderRectangle);
            e.Graphics.DrawString(this.Text, this.Font, (active) ? textBrush : borderBrush, borderRectangle, stringFormat);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            base.BackColor = ColorTranslator.FromHtml("#9055A2");

            active = true;
            
            base.BackColor = ColorTranslator.FromHtml("#ffffff");
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            base.BackColor = ColorTranslator.FromHtml("#2E294E");

            active = false;

            base.BackColor = ColorTranslator.FromHtml("#ffffff");
        }
    }
}
