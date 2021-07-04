using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Pdf
{
    public class GeneratorPage : ICloneable, IDisposable, IEquatable<GeneratorPage>
    {
        public Guid Id { get; set; }

        public PdfPage Page { get; private set; }

        public double MarginTop { get; init; }

        public double MarginBottom { get; init; }

        public XGraphics CurrentGraphics { get; internal set; }

        public double PointY { get; set; } = 10.0;

        internal GeneratorPage(Guid id, double marginTop, double marginBottom)
        {
            this.Id = id;
            this.MarginTop = marginTop;
            this.MarginBottom = marginBottom;
        }

        public GeneratorPage(PdfPage page, double marginTop, double marginBottom)
        {
            this.Id = Guid.NewGuid();
            this.Page = page;
            this.MarginTop = marginTop;
            this.MarginBottom = marginBottom;
            this.CurrentGraphics = XGraphics.FromPdfPage(this.Page);
        }

        public void ResetPointY() => this.PointY = this.MarginTop;

        public GeneratorPage Clone() => new GeneratorPage(this.Id, this.MarginTop, this.MarginBottom)
        {
            Page = this.Page,
            CurrentGraphics = this.CurrentGraphics,
            PointY = this.PointY
        };

        object ICloneable.Clone() => (object)this.Clone();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing)
                return;
            this.CurrentGraphics?.Dispose();
        }

        public bool Equals(GeneratorPage other)
        {
            Guid? id1 = other?.Id;
            Guid id2 = this.Id;
            if (!id1.HasValue)
                return false;
            return !id1.HasValue || id1.GetValueOrDefault() == id2;
        }
    }
}
