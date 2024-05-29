using System;
using System.Drawing;
using System.Windows.Forms;

public class TricolorPanel : UserControl
{
    public TricolorPanel()
    {
        this.DoubleBuffered = true; // Включаем двойную буферизацию для снижения мерцания
        this.ResizeRedraw = true;   // Перерисовываем панель при изменении размера
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Получаем графический объект
        Graphics g = e.Graphics;

        // Определяем высоту каждой полосы
        int stripeHeight = this.Height / 3;

        // Рисуем белую полосу
        g.FillRectangle(Brushes.White, 0, 0, this.Width, 240);

        // Рисуем синюю полосу
        g.FillRectangle(Brushes.Blue, 0, 240, this.Width,240);

        // Рисуем красную полосу
        g.FillRectangle(Brushes.Red, 0, 480, this.Width, 250);
    }
} 