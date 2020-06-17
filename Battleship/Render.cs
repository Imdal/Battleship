using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Battleship
{
    class Render
    {
        private const string white = "..\\..\\..\\Images\\White.png";
        private const string whitex = "..\\..\\..\\Images\\Whitex.png";
        private const string black = "..\\..\\..\\Images\\Black.png";
        private const string blackx = "..\\..\\..\\Images\\Blackx.png";
        public const int GameSize = 30;

        public Render()
        {

        }

        private void DrawPoint(int x, int y, string pic, Canvas canvas)
        {
            var shape = new Rectangle();
            shape.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(pic, UriKind.Relative)) };
            shape.Width = GameSize;
            shape.Height = GameSize;
            Canvas.SetTop(shape, y);
            Canvas.SetLeft(shape, x);
            canvas.Children.Add(shape);
        }

        public void Rendering(Table table, Canvas canvas)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (table.table[i, j] == 0)
                        DrawPoint(i * GameSize, j * GameSize, white, canvas);
                    else if (table.table[i, j] == 1)
                        DrawPoint(i * GameSize, j * GameSize, black, canvas);
                    else if (table.table[i, j] == 2)
                        DrawPoint(i * GameSize, j * GameSize, whitex, canvas);
                    else if (table.table[i, j] == 3)
                        DrawPoint(i * GameSize, j * GameSize, blackx, canvas);

                }
            }
        }

        public void hiddenRendering(Table table, Canvas canvas)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (table.table[i, j] == 0)
                        DrawPoint(i * GameSize, j * GameSize, white, canvas);
                    else if (table.table[i, j] == 1)
                        DrawPoint(i * GameSize, j * GameSize, white, canvas);
                    else if (table.table[i, j] == 2)
                        DrawPoint(i * GameSize, j * GameSize, whitex, canvas);
                    else if (table.table[i, j] == 3)
                        DrawPoint(i * GameSize, j * GameSize, blackx, canvas);

                }
            }
        }

        public void renderShipNumber(TextBlock ship1, TextBlock ship2, TextBlock ship3, TextBlock ship4, TextBlock ship5, Table table)
        {
            if (table.ships[0].exists)
                ship1.Text = "1.";
            if (table.ships[1].exists)
                ship2.Text = "2.";
            if (table.ships[2].exists)
                ship3.Text = "3.";
            if (table.ships[3].exists)
                ship4.Text = "4.";
            if (table.ships[4].exists)
                ship5.Text = "5.";
        }
        
        public void renderShipNumberShunken(TextBlock ship1, TextBlock ship2, TextBlock ship3, TextBlock ship4, TextBlock ship5, Table table)
        {
            if (table.ships[0].shunken)
                ship1.TextDecorations = TextDecorations.Strikethrough;
            if (table.ships[1].shunken)
                ship2.TextDecorations = TextDecorations.Strikethrough;
            if (table.ships[2].shunken)
                ship3.TextDecorations = TextDecorations.Strikethrough;
            if (table.ships[3].shunken)
                ship4.TextDecorations = TextDecorations.Strikethrough;
            if (table.ships[4].shunken)
                ship5.TextDecorations = TextDecorations.Strikethrough;
        }

    }
}
