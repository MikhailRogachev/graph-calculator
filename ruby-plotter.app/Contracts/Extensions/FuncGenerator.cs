namespace ruby_plotter.app.Contracts.Extensions;

public static class FuncGenerator
{
    public static (double[] Ys, double[] Ts) Sin(
        double frequency = 0.05,
        double duration = 1,
        double amplitude = 1,
        int phase = 0,
        double framerate = 24600)
    {
        var phaseRad = phase * Math.PI / 180;
        var freqKhz = frequency * 1000; // Convert frequency to Hz
        int count = Convert.ToInt32(duration * framerate);

        double[] ts = new double[count];
        double[] ys = new double[count];

        for (int i = 0; i < count; i++)
        {
            ts[i] = i / framerate;
            ys[i] = amplitude * Math.Sin(0.5 * Math.PI * freqKhz * ts[i] + phaseRad);
        }

        return (ys, ts);
    }

    public static (double[] Ys, double[] Ts) SinCardinal(
        double frequency = 0.05,
        double xStart = 0,
        double xEnd = 1,
        double framerate = 24600)
    {
        var freqKhz = frequency * 1000; // Convert frequency to Hz
        var duration = xEnd + (xStart < 0 ? xStart * -1 : xStart);
        int count = Convert.ToInt32(duration * framerate);

        double[] ts = new double[count];
        double[] ys = new double[count];


        for (int i = 0; i < count; i++)
        {
            ts[i] = xStart + i / framerate;

            if (ts[i] == 0)
            {
                ys[i] = 1;
            }
            else
            {
                ys[i] = Math.Sin(Math.PI * freqKhz * ts[i]) / (Math.PI * freqKhz * ts[i]);
            }
        }

        return (ys, ts);
    }
}