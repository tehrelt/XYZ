using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZ.Models
{
    internal class Plot
    {
        public string Title {get; set;}
        public PlotModel PlotModel { get; private set; }
        public LineSeries LineSeries { get; private set; }
        public List<DataPoint> Points { get; set; }

        public Plot(string title, List<DataPoint> dataPoints)
        {
            PlotModel = new PlotModel { Title = title };
            LineSeries = new LineSeries();
            LineSeries.Points.AddRange(dataPoints);
            PlotModel.Series.Add(LineSeries);
        }
        public PlotModel GetPlot()
        {
            return PlotModel;
        }
    }
}
