using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LottoStatistik
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEinlesen_Click(object sender, RoutedEventArgs e)
        {
            int[] zahlen = new int[] { };
            Dictionary<int, int> lottozahlen = new Dictionary<int, int>();

            string data = System.IO.File.ReadAllText(@"lottozahlen_archiv.csv");

            foreach (string line in data.Split('\n'))
                try {
                    foreach (string i in line.Split(',')[1].Split('-'))
                        zahlen = zahlen.Append(int.Parse(i)).ToArray();
                } catch { }
            

            for (int i = 0; i < 49; i++)
                lottozahlen.Add(i, zahlen.Count(n => n == i + 1));

            var dict = lottozahlen.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (KeyValuePair<int, int> pair in dict)
                txbAusgabe.Text += $"Lottozahl: {pair.Key} -> Häufigkeit: {pair.Value}\n";
            
        }
    }
}
