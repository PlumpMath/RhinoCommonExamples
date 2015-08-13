﻿using System.Drawing;
using Rhino;
using Rhino.Commands;
using Rhino.Display;

public class DrawBitmapConduit : Rhino.Display.DisplayConduit
{
  private readonly DisplayBitmap m_display_bitmap;

  public DrawBitmapConduit()
  {
    var flag = new System.Drawing.Bitmap(100, 100);
    for( int x = 0; x <  flag.Height; ++x )
        for( int y = 0; y < flag.Width; ++y )
            flag.SetPixel(x, y, Color.White);

    var g = Graphics.FromImage(flag);
    g.FillEllipse(Brushes.Blue, 25, 25, 50, 50);
    m_display_bitmap = new DisplayBitmap(flag);
  }

  protected override void DrawForeground(Rhino.Display.DrawEventArgs e)
  {
    e.Display.DrawBitmap(m_display_bitmap, 50, 50, Color.White);
  }
}

partial class Examples
{
  static readonly DrawBitmapConduit m_conduit = new DrawBitmapConduit();

  public static Result ConduitBitmap(RhinoDoc doc)
  {
    // toggle conduit on/off
    m_conduit.Enabled = !m_conduit.Enabled;
    
    RhinoApp.WriteLine("Custom conduit enabled = {0}", m_conduit.Enabled);
    doc.Views.Redraw();
    return Result.Success;
  }
}
