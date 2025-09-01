namespace BrownianMotion.Models;

public class BrownianMotion(double sigma, double mean, double initialPrice, int numDays)
{
    public double Sigma { get; } = sigma;
    public double Mean { get; } = mean;
    public double InitialPrice { get; } = initialPrice;
    public int NumDays { get; } = numDays;
    public static double[] Generate(double sigma, double mean, double initialPrice, int numDays)
    {
        Random rand = new Random();
        double[] prices = new double[numDays];
        prices[0] = initialPrice;

        for (int i = 1; i < numDays; i++)
        {
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

            double retornoDiario = mean + sigma * z;

            prices[i] = prices[i - 1] * Math.Exp(retornoDiario);
        }

        return prices;
    }
}
