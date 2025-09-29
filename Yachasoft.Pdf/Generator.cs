using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;


namespace Yachasoft.Pdf
{
    public class Generator : IGenerator, IDisposable
    {
        private readonly PdfDocument _document;
        private List<GeneratorPage> _pages = new List<GeneratorPage>();
        private GeneratorPage _currentPage;
        private Style _currentStyle = new Style(new XFont("Verdana", 7.0), XBrushes.Black);
        private XPen _currentPen = new XPen(XColors.Black, 1.0);
        private Table _currentTable;
        private bool _expression = true;
        private double _marginTop = 20.0;
        private double _marginBottom = 20.0;

        public Style CurrentStyle { get => _currentStyle; set => _currentStyle = value; }
        public Table CurrentTable { get => _currentTable; set => _currentTable = value; }
        public XGraphics Draw => _currentPage.CurrentGraphics;
        public bool Expression { get => _expression; set => _expression = value; }
        public double PointerY { get => _currentPage.PointY; set => _currentPage.PointY = value; }
        public GeneratorPage PagePointer { get => _currentPage; set => _currentPage = value; }
        public double MarginTop { get => _marginTop; set => _marginTop = value; }
        public double MarginBottom { get => _marginBottom; set => _marginBottom = value; }
        public XPen CurrentPen { get => _currentPen; set => _currentPen = value; }
        public GeneratorPage CurrentPage => _currentPage;
        public List<GeneratorPage> Pages => _pages;

        public Generator(PdfDocument document)
        {
            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        public static Generator Instance(PdfDocument document) => new Generator(document);

        public void AddPage()
        {
            if (_currentPage != null)
            {
                int index = _pages.FindIndex(x => x.Equals(_currentPage));
                if (index < _pages.Count - 1)
                {
                    _currentPage = _pages[index + 1];
                    _currentPage.ResetPointY();
                    return;
                }
            }

            PdfPage page = _document.AddPage();
            _currentPage = new GeneratorPage(page, _marginTop, _marginBottom);
            _pages.Add(_currentPage);
        }

        public void CheckEndOfPage(double heightNewElement)
        {
            if (_currentPage.PointY + heightNewElement > _currentPage.Page.Height - _marginBottom)
            {
                AddPage();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing || _pages == null) return;

            foreach (var page in _pages)
                page?.Dispose();
        }
    }
}

