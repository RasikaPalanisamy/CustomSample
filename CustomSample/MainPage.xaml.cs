using Microsoft.Maui.Layouts;

namespace CustomSample
{
    public partial class MainPage : ContentPage
    {
        CustomView view;
        public MainPage()
        {
            InitializeComponent();
            view = new CustomView();
            this.Content = view;
        }
    }

    public class CustomView : ControlLayout
    {
        CustomChild customChild;
        CustomContent customContent;
        public CustomView()
        {
            customChild = new CustomChild();
            customContent = new CustomContent();
            this.Children.Add(customChild);
            this.Children.Add(customContent);
            
        }

        protected override ILayoutManager CreateLayoutManager()
        {
            return new ControlLayoutManager(this);
        }

        internal override Size LayoutArrangeChildren(Rect bounds)
        {
            (this.customChild as IView).Arrange(new Rect(0, 0, this.customChild.WidthRequest, this.customChild.HeightRequest));
            (this.customContent as IView).Arrange(new Rect(this.customChild.WidthRequest, 0, this.customContent.WidthRequest, this.customContent.HeightRequest));
            return bounds.Size;
        }

        internal override Size LayoutMeasure(double widthConstraint, double heightConstraint)
        {
            (this.customChild as IView).Measure(this.customChild.WidthRequest, this.customChild.HeightRequest);
            (this.customContent as IView).Measure(this.customContent.WidthRequest, this.customContent.HeightRequest);
            return new Size(widthConstraint, heightConstraint);
        }
    }

    public class CustomChild : ContentView
    {
        Label label;
        public CustomChild()
        {
            this.BackgroundColor = Colors.Pink;
            this.HeightRequest = 100;
            this.WidthRequest = 180;
            label = new Label() { Text = "Custom Child", HeightRequest = 100, WidthRequest = 180 };
            this.Content = label;  
        }
    }

    public class CustomContent : ContentView
    {
        Label label;
        public CustomContent()
        {
            this.BackgroundColor = Colors.Yellow;
            this.HeightRequest = 100;
            this.WidthRequest = 180;
            label = new Label() { Text = "Custom Content", HeightRequest = 100, WidthRequest = 180 };
            this.Content = label;

            PanGestureRecognizer gestureRecognizer = new PanGestureRecognizer();
            gestureRecognizer.PanUpdated += GestureRecognizer_PanUpdated;
            this.GestureRecognizers.Add(gestureRecognizer);
        }

        private void GestureRecognizer_PanUpdated(object? sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    this.OnResizing(e);
                    break;

            }
        }

        double oldData = 0;
        private void OnResizing(PanUpdatedEventArgs e)
        {
            var draggedX = e.TotalX - oldData;
            this.oldData = e.TotalX;
            System.Diagnostics.Debug.WriteLine("Old = " + e.TotalX);
            ((this.Parent as ControlLayout)!.Children[0] as ContentView)!.WidthRequest += draggedX;
            (this.Parent as ControlLayout)!.Children[0].InvalidateMeasure();
        }
    }
}
