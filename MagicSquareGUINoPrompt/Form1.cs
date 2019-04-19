using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicSquareGUINoPrompt
{
  public partial class magicSquares : Form
  {
    private List<int[,]> grids = new List<int[,]>();
    private int clientWidth = 860;
    private int clientHeight = 390;
    public magicSquares()
    {
      InitializeComponent();
      ClientSize = new Size(clientWidth, clientHeight);
      MinimumSize = ClientSize;
      grids.Add(MagicSquare.Create(3).Grid);
      grids.Add(MagicSquare.Create(5).Grid);
      grids.Add(MagicSquare.Create(7).Grid);
      Invalidate();
    }

    private void onPaint(object sender, PaintEventArgs e)
    {
      drawGrids(e);
    }

    private void drawGrids(PaintEventArgs e)
    {
      var spacing = 10;
      var smallSquareDimension = 40;
      var bottom = clientHeight - 10;
      var spacingBetweenGrids = 20;
      int[] widths = new int[3];
      //compute widths
      for (int i = 0; i < grids.Count; i++)
      {
        var nSquares = grids.ElementAt(i).GetLength(0);
        widths[i] = nSquares * smallSquareDimension + (nSquares + 1) * spacing;
      }

      for (int i = 0; i < grids.Count; i++)
      {
        var x = 0;
        var y = 0;
        var xOffset = spacingBetweenGrids;
        //size all elements
        var largeSquareDimension = widths[i];
        //compute offset
        for (int p = 0; p < widths.Length; p++)
        {
          if (p < i)
            xOffset += widths[p] + spacingBetweenGrids;
        }
        x += xOffset;
        y = bottom - largeSquareDimension;
        var pen = new Pen(Color.Black);
        var brush = new SolidBrush(Color.Black);

        //make outer square
        var largeRect = new Rectangle(x, y, largeSquareDimension, largeSquareDimension);
        e.Graphics.DrawRectangle(pen, largeRect);
        e.Graphics.FillRectangle(brush, largeRect);

        pen.Color = Color.LimeGreen;
        brush.Color = Color.LimeGreen;
        //move to first square/cell
        var xInitial = x + spacing;
        x = xInitial;
        y += spacing;
        //draw grid
        for (int t = 0; t < grids.ElementAt(i).GetLength(0); t++)
        {
          for (int p = 0; p < grids.ElementAt(i).GetLength(1); p++)
          {
            //draw small square/cell
            var smallRect = new Rectangle(x, y, smallSquareDimension, smallSquareDimension);
            e.Graphics.DrawRectangle(pen, smallRect);
            e.Graphics.FillRectangle(brush, smallRect);
            //draw number
            brush.Color = Color.Black;
            var font = new Font("Arial", smallSquareDimension * .50F);
            var textX = x + smallSquareDimension / (grids.ElementAt(i)[t, p] > 9 ? 10 : 4);
            var textY = y + smallSquareDimension / 5;
            var format = new StringFormat();
            e.Graphics.DrawString(grids.ElementAt(i)[t, p].ToString(), font, brush, textX, textY, format);
            brush.Color = Color.LimeGreen;
            //move to next cell
            x = x + smallSquareDimension + spacing;
          }
          //move to next row
          x = xInitial;
          y = y + smallSquareDimension + spacing;
        }
      };
    }
  }
}
