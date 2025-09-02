namespace BrownianMotion.Controls;

public class BrownianMotionDrawable : IDrawable
{
    private readonly Random _rand = new();

    private List<double[]> _simulations = new();
    private List<Color> _colors = new();

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

        // Margens internas
        float padding = 0;
        float width = dirtyRect.Width - padding * 2;
        float height = dirtyRect.Height - padding * 2;

        // Descobre min e max global
        double min = _simulations.Min(sim => sim.Min());
        double max = _simulations.Max(sim => sim.Max());

        // Evita divisão por zero
        double range = max - min;
        if (range == 0) range = 1;

        // Desenha cada simulação
        for (int i = 0; i < _simulations.Count; i++)
        {
            var sim = _simulations[i];
            canvas.StrokeColor = _colors[i];
            canvas.StrokeSize = 1;

            // Normaliza e desenha linha
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
