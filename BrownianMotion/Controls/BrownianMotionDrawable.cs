namespace BrownianMotion.Controls;

public class BrownianMotionDrawable : IDrawable
{
    private readonly Random _rand = new();

    private List<double[]> _simulations = new();
    private List<Color> _colors = new();

    public string LineStyle { get; set; } = "Solid";

    public List<double[]> Simulations
    {
        get => _simulations;
        set
        {
            _simulations = value ?? new List<double[]>();
            _colors = _simulations.Select(_ => Color.FromRgb(_rand.Next(256), _rand.Next(256), _rand.Next(256))).ToList();
        }
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (_simulations == null || _simulations.Count == 0)
            return;

        float padding = 0;
        float width = dirtyRect.Width - padding * 2;
        float height = dirtyRect.Height - padding * 2;

        double min = _simulations.Min(sim => sim.Min());
        double max = _simulations.Max(sim => sim.Max());
        double range = max - min;
        if (range == 0) range = 1;

        canvas.StrokeColor = Colors.Gray.WithAlpha(0.25f);
        canvas.StrokeSize = 1;
        canvas.StrokeDashPattern = null;

        int numVerticalLines = 10;
        int numHorizontalLines = 10;

        for (int i = 0; i <= numVerticalLines; i++)
        {
            float x = padding + (i / (float)numVerticalLines) * width;
            canvas.DrawLine(x, padding, x, padding + height);
        }

        for (int j = 0; j <= numHorizontalLines; j++)
        {
            float y = padding + (j / (float)numHorizontalLines) * height;
            canvas.DrawLine(padding, y, padding + width, y);
        }

        for (int i = 0; i < _simulations.Count; i++)
        {
            var sim = _simulations[i];
            canvas.StrokeColor = _colors[i];
            canvas.StrokeSize = 1;

            canvas.StrokeDashPattern = LineStyle switch
            {
                "Dashed" => [6, 3],
                "Dotted" => [2, 2],
                "DashDot" => [6, 2, 2, 2],
                _ => null
            };

            for (int j = 1; j < sim.Length; j++)
            {
                float x1 = padding + (float)((j - 1) / (double)(sim.Length - 1) * width);
                float y1 = padding + (float)((max - sim[j - 1]) / range * height);

                float x2 = padding + (float)(j / (double)(sim.Length - 1) * width);
                float y2 = padding + (float)((max - sim[j]) / range * height);

                canvas.DrawLine(x1, y1, x2, y2);
            }
        }
    }

}
