using System.Collections.ObjectModel;

namespace BrownianMotion.Controls;

public partial class BrownianMotionChart : ContentView
{
    private readonly BrownianMotionDrawable _drawable;

    public BrownianMotionChart()
    {
        InitializeComponent();

        _drawable = new BrownianMotionDrawable();
        Canvas.Drawable = _drawable;
    }

    public static readonly BindableProperty SimulationsProperty =
        BindableProperty.Create(
            nameof(Simulations),
            typeof(ObservableCollection<double[]>),
            typeof(BrownianMotionChart),
            propertyChanged: OnSimulationsChanged);

    public ObservableCollection<double[]> Simulations
    {
        get => (ObservableCollection<double[]>)GetValue(SimulationsProperty);
        set => SetValue(SimulationsProperty, value);
    }


    private static void OnSimulationsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (BrownianMotionChart)bindable;

        if (oldValue is ObservableCollection<double[]> oldCollection)
        {
            oldCollection.CollectionChanged -= control.OnSimulationsCollectionChanged;
        }

        if (newValue is ObservableCollection<double[]> newCollection)
        {
            newCollection.CollectionChanged += control.OnSimulationsCollectionChanged;
            control._drawable.Simulations = newCollection.ToList();
            control.Canvas.Invalidate();
        }
    }

    private void OnSimulationsCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (sender is ObservableCollection<double[]> collection)
        {
            _drawable.Simulations = collection.ToList();
            Canvas.Invalidate();
        }
    }

    public static readonly BindableProperty LineStyleProperty =
        BindableProperty.Create(
            nameof(LineStyle),
            typeof(string),
            typeof(BrownianMotionChart),
            "Solid",
            propertyChanged: OnLineStyleChanged);

    public string LineStyle
    {
        get => (string)GetValue(LineStyleProperty);
        set => SetValue(LineStyleProperty, value);
    }

    private static void OnLineStyleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (BrownianMotionChart)bindable;
        if (newValue is string style)
        {
            control._drawable.LineStyle = style;
            control.Canvas.Invalidate();
        }
    }
}
