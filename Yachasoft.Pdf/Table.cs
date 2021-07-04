using System;
using System.Collections.Generic;

namespace Yachasoft.Pdf
{
    public class Table
    {
        private IGenerator _generator;
        private readonly double _x;
        private int _rowIndex = -1;

        public bool DrawBorders { get; init; } = true;

        public double DefaultRowHeight { get; init; } = 10.0;

        public List<double> ColumnsWidth { get; internal set; }

        public Row CurrentRow { get; set; }

        public Table(IGenerator generator, double x, double y, List<double> columnsWidth)
        {
            this._generator = generator;
            this._x = x;
            generator.PointerY = y;
            this.ColumnsWidth = columnsWidth;
        }

        public IGenerator AddRow(double? height)
        {
            double num = height ?? this.DefaultRowHeight;
            this._generator.CheckEndOfPage(num);
            ++this._rowIndex;
            this.CurrentRow = new Row(this._generator, this, this._rowIndex, this._x, this._generator.PointerY, num);
            this._generator.PointerY += num;
            return this._generator;
        }
    }
}
