using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSample
{
    public abstract class ControlLayout : Layout
    {
        internal abstract Size LayoutArrangeChildren(Rect bounds);



        internal abstract Size LayoutMeasure(double widthConstraint, double heightConstraint);
    }



    internal class ControlLayoutManager : LayoutManager
    {
        ControlLayout layout;
        internal ControlLayoutManager(ControlLayout layout) : base(layout)
        {
            this.layout = layout;
        }



        public override Size ArrangeChildren(Rect bounds) => this.layout.LayoutArrangeChildren(bounds);



        public override Size Measure(double widthConstraint, double heightConstraint) => this.layout.LayoutMeasure(widthConstraint, heightConstraint);
    }
}
