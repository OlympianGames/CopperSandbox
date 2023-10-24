using CopperEngine.Core;
using ImPlotNET;

namespace CopperSandbox.Testing;

public class ImPlotTesting : CopperWindow
{
    protected override string WindowName { get; set; } = "ImPlot Testing";

    protected override void WindowUpdate()
    {
        if (ImPlot.BeginPlot("My Plot"))
        {
            // Plot the bar data
            float barData = 0;
            ImPlot.PlotBars("My Bar Plot", ref barData, 11);

            // Plot the line data
            float xData = 0;
            float yData = 0;
            ImPlot.PlotLine("My Line Plot", ref xData, ref yData, 1000);

            // Additional plotting and customization can be done here

            ImPlot.EndPlot();
        }
    }
}