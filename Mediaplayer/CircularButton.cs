using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CircularButton : Button
{
    // Ensure the control remains circular when resized.
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        using (GraphicsPath gp = new GraphicsPath())
        {
            // Create a circle (or ellipse) that fits the control's bounds.
            gp.AddEllipse(0, 0, this.Width, this.Height);
            this.Region = new Region(gp);
        }
    }

    // Optional: Customize the button's appearance by overriding OnPaint.
    protected override void OnPaint(PaintEventArgs pevent)
    {
        // Optionally, you can customize the painting here.
        // For now, we'll call the base method so the text, etc., are drawn.
        base.OnPaint(pevent);
    }
}