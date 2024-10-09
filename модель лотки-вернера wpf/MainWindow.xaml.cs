using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static модель_лотки_вернера_wpf.MainWindow;

namespace модель_лотки_вернера_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Graph.ChartAreas.Add(new ChartArea("SeriaArena"));
            series = new Series("Seria");
            series.ChartType = SeriesChartType.Line;
            Graph.Series.Add(series);
            Graph.Series["Seria"].ChartArea = "SeriaArena";

            series2cycle = new Series("series2cycle");
            series2cycle.ChartType = SeriesChartType.Line;
            Graph.Series.Add(series2cycle);
            Graph.Series["series2cycle"].ChartArea = "SeriaArena";

            series3cycle = new Series("series3cycle");
            series3cycle.ChartType = SeriesChartType.Line;
            Graph.Series.Add(series3cycle);
            Graph.Series["series3cycle"].ChartArea = "SeriaArena";
        }

        private Series series;
        private Series series2cycle;
        private Series series3cycle;

        public class Data
        {
            public double сycle { get; set; }
            public double victims { get; set; }
            public double predators { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double z, b, a, o, x0, y0, predators, victims, predatorsCalculate, victimsCalculate;

            z = Convert.ToDouble(tbZ.Text);
            b = Convert.ToDouble(tbB.Text);
            a = Convert.ToDouble(tbA.Text);
            o = Convert.ToDouble(tbO.Text);
            x0 = Convert.ToDouble(tbX0.Text);
            y0 = Convert.ToDouble(tbY0.Text);
            predators = Convert.ToDouble(tbPredators.Text);
            victims = Convert.ToDouble(tbVictims.Text);

            victimsCalculate = (z - a * predators) * victims + victims;
            predatorsCalculate = (o * victimsCalculate - b) * predators + predators;

            var data = new List<Data>();
            data.Add(new Data
            {
                сycle = 2,
                victims = victimsCalculate,
                predators = predatorsCalculate
            });

            int firstGeneration = 3;
            int generation = 150;

            for (int g = 1; g <= 3; g++)
            {
                for (int i = firstGeneration; i <= generation; i++)
                {
                    victimsCalculate = (z - a * predatorsCalculate) * victimsCalculate + victimsCalculate;
                        

                    data.Add(new Data
                    {
                        сycle = i,
                        victims = victimsCalculate,
                        predators = predatorsCalculate
                    });

                    series.Points.AddXY(victimsCalculate, predatorsCalculate);
                }

                data.Add(new Data
                {
                    сycle = g,
                    victims = g,
                    predators = g
                });

                predators -= 50;

                firstGeneration = generation;
                generation += 150;

                victimsCalculate = (z - a * predators) * victims + victims;
                predatorsCalculate = (o * victimsCalculate - b) * predators + predators;
            }

            

            

            dgTable.ItemsSource = data;
        }
    }
}
