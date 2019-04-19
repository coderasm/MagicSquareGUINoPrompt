using System;

namespace MagicSquareGUINoPrompt
{
  class MagicSquare
  {
    private int nSquares = 1;
    private int[,] grid;
    public int[,] Grid {
      get
      {
        return grid;
      }
    }
    private MagicSquare(int nSquares)
    {
      this.nSquares = nSquares;
      generateGrid();
    }

    public static MagicSquare Create(int nSquares)
    {
      return new MagicSquare(nSquares);
    }

    private void generateGrid()
    {
      grid = new int[nSquares, nSquares];
      if (nSquares % 2 != 0)
        populateWithOddMethod();
    }

    private void populateWithOddMethod()
    {
      //place one
      var currentColumn = nSquares / 2;
      var currentRow = 0;
      grid[currentRow, currentColumn] = 1;
      var iterations = nSquares * nSquares - 1;
      for (int i = 0; i < iterations; i++)
      {
        var newRow = currentRow - 1;
        var newColumn = currentColumn + 1;
        //outside upper right corner of square
        if (newRow < 0 && newColumn > nSquares - 1)
        {
          grid[++currentRow, currentColumn] = i + 2;
        }
        //outside above square
        else if (newRow < 0 && newColumn <= nSquares - 1)
        {
          grid[nSquares - 1, newColumn] = i + 2;
          currentRow = nSquares - 1;
          currentColumn = newColumn;
        }
        //outside to the right of square
        else if (newRow <= nSquares - 1 && newColumn > nSquares - 1)
        {
          grid[newRow, 0] = i + 2;
          currentRow = newRow;
          currentColumn = 0;
        }
        else
        {
          //check if already occupied
          if (grid[newRow, newColumn] > 0)
          {
            grid[++currentRow, currentColumn] = i + 2;
          }
          else
          {
            grid[newRow, newColumn] = i + 2;
            currentRow = newRow;
            currentColumn = newColumn;
          }
        }
      }
    }

    public void print()
    {
      int[] columnWidths = new int[nSquares];
      //find column widths
      for (int i = 0; i < grid.GetLength(0); i++)
      {
        for (int p = 0; p < grid.GetLength(1); p++)
        {
          var length = grid[i, p].ToString().Length;
          if (length > columnWidths[p])
            columnWidths[p] = length;
        }
      }
      //print square
      for (int i = 0; i < grid.GetLength(0); i++)
      {
        var row = "";
        for (int p = 0; p < grid.GetLength(1); p++)
        {
          //get item width
          var itemWidth = grid[i, p].ToString().Length;
          var spacesNeeded = 1 + columnWidths[p] - itemWidth;
          //concat needed spaces
          for (int t = 0; t < spacesNeeded; t++)
          {
            row += " ";
          }
          row += $"{grid[i, p]}";
        }
        Console.WriteLine(row);
      }
    }
  }
}
