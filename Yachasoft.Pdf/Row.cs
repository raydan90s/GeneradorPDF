using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Pdf.Helpers;

namespace Yachasoft.Pdf
{
    public class Row
    {
        private IGenerator _generator;
        private Table _table;
        private double _x;
        private double _y;
        private double _height;
        private int _rowIndex;
        private int _cellIndex = -1;

        public Row(
          IGenerator generator,
          Table table,
          int rowIndex,
          double X,
          double Y,
          double height)
        {
            this._generator = generator;
            this._table = table;
            this._x = X;
            this._y = Y;
            this._height = height;
            this._rowIndex = rowIndex;
        }

        public void AddCell(string text, XParagraphAlignment alignment = XParagraphAlignment.Center)
        {
            ++this._cellIndex;
            double num = this._table.ColumnsWidth[this._cellIndex];
            if (this._table.DrawBorders)
                this._generator.Rectangle(new XRect(this._x, this._y, num, this._height));
            this._generator.WriteInBox(text, alignment, this._x + 1.0, this._y + 1.0, num - 2.0, this._height - 2.0);
            this._x += num;
        }

        public void AddCell(Decimal? valor)
        {
            ++this._cellIndex;
            double num = this._table.ColumnsWidth[this._cellIndex];
            if (this._table.DrawBorders)
                this._generator.Rectangle(new XRect(this._x, this._y, num, this._height));
            this._generator.WriteInBox(valor.GetValueOrDefault(), this._x + 1.0, this._y + 1.0, num - 2.0, this._height - 2.0);
            this._x += num;
        }

        public void AddCell(int? valor)
        {
            ++this._cellIndex;
            double num = this._table.ColumnsWidth[this._cellIndex];
            if (this._table.DrawBorders)
                this._generator.Rectangle(new XRect(this._x, this._y, num, this._height));
            this._generator.WriteInBox((long)valor.GetValueOrDefault(), this._x + 1.0, this._y + 1.0, num - 2.0, this._height - 2.0);
            this._x += num;
        }
    }
}
