namespace BrownianMotion.Controls;

public class BrownianMotionDrawable : IDrawable
{
    public List<double[]> Simulations { get; set; } = new();

    private readonly Random _rand = new();

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (Simulations == null || Simulations.Count == 0)
            return;

        // Margens internas
        float padding = 0;
        float width = dirtyRect.Width - padding * 2;
        float height = dirtyRect.Height - padding * 2;

        // Descobre min e max global
        double min = Simulations.Min(sim => sim.Min());
        double max = Simulations.Max(sim => sim.Max());

        // Evita divisão por zero
        double range = max - min;
        if (range == 0) range = 1;

        // Desenha cada simulação
        foreach (var sim in Simulations)
        {
            // Cor aleatória
            canvas.StrokeColor = Color.FromRgb(_rand.Next(256), _rand.Next(256), _rand.Next(256));
            canvas.StrokeSize = 1;

            // Normaliza e desenha linha
            for (int i = 1; i < sim.Length; i++)
            {
                float x1 = padding + (float)((i - 1) / (double)(sim.Length - 1) * width);
                float y1 = padding + (float)((max - sim[i - 1]) / range * height);

                float x2 = padding + (float)(i / (double)(sim.Length - 1) * width);
                float y2 = padding + (float)((max - sim[i]) / range * height);

                canvas.DrawLine(x1, y1, x2, y2);
            }
        }
    }
}
