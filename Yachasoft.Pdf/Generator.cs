using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Pdf
{
    public class Generator : IGenerator, IDisposable
    {
        private readonly PdfDocument _document;
        private List<GeneratorPage> _pages = new List<GeneratorPage>();
        private GeneratorPage _currentPage;
        private Style _currentStyle = new Style(new XFont("Verdana", 7.0, (XFontStyle)0), XBrushes.Black);
        private XPen _currentPen = new XPen(XColors.Black, 1.0);
        private Table _currentTable;
        private bool _expression = true;
        private double _marginTop = 20.0;
        private double _marginBottom = 20.0;

        public Style CurrentStyle
        {
            get => this._currentStyle;
            set => this._currentStyle = value;
        }

        public Table CurrentTable
        {
            get => this._currentTable;
            set => this._currentTable = value;
        }

        public XGraphics Draw => this._currentPage.CurrentGraphics;

        public bool Expression
        {
            get => this._expression;
            set => this._expression = value;
        }

        public double PointerY
        {
            get => this._currentPage.PointY;
            set => this._currentPage.PointY = value;
        }

        public GeneratorPage PagePointer
        {
            get => this._currentPage;
            set => this._currentPage = value;
        }

        public double MarginTop
        {
            get => this._marginTop;
            set => this._marginTop = value;
        }

        public double MarginBottom
        {
            get => this._marginBottom;
            set => this._marginBottom = value;
        }

        public XPen CurrentPen
        {
            get => this._currentPen;
            set => this._currentPen = value;
        }

        public GeneratorPage CurrentPage => this._currentPage;

        public List<GeneratorPage> Pages => this._pages;

        public Generator(PdfDocument document) => this._document = document;

        public static Generator Instance(PdfDocument document) => document != null ? new Generator(document) : throw new ArgumentNullException(nameof(document));

        public void AddPage()
        {
            bool flag = true;
            if (this._currentPage != null)
            {
                int index = this._pages.FindIndex((Predicate<GeneratorPage>)(x => x.Equals(this._currentPage)));
                if (index < this._pages.Count - 1)
                {
                    flag = false;
                    this._currentPage = this._pages[index + 1];
                    this._currentPage.ResetPointY();
                }
            }
            if (!flag)
                return;
            this._currentPage = new GeneratorPage(this._document.AddPage(), this._marginTop, this._marginBottom);
            this._pages.Add(this._currentPage);
        }

        public void CheckEndOfPage(double heightNewElement)
        {
            if (this._currentPage.PointY + heightNewElement <= this._currentPage.Page.Height - this._marginBottom)
                return;
            this.AddPage();
            this._currentPage.PointY += heightNewElement;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing || this._pages == null)
                return;
            foreach (GeneratorPage page in this._pages)
                page?.Dispose();
        }
    }
}
