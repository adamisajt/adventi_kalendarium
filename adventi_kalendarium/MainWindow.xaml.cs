using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace adventi_kalendarium
{
    public partial class MainWindow : Window
    {
        private List<int> napok;
        private Random rnd = new Random();

        private List<string> ajandekok = new List<string>()
        {
            "egy csoki 🍫", "egy forró tea ☕", "egy mosoly 😊", "egy hópihe ❄️",
            "egy ölelés 🤗", "egy karácsonyi zene 🎶", "egy meleg pulcsi 🧶",
            "egy gyertyafény 🕯️", "egy finom süti 🍪", "egy séta a hóban 🚶‍♀️",
            "egy csillagfény ✨", "egy bögre forralt bor 🍷", "egy boldog emlék 💭",
            "egy karácsonyi mese 📖", "egy szaloncukor 🍬", "egy kis pihenés 😌",
            "egy új remény 💫", "egy meglepetés 🎁", "egy baráti üzenet 💌",
            "egy tábla csoki 🍫", "egy nevetés 😂", "egy dal 🎵", "egy ölelés 💖",
            "egy karácsonyi csoda 🌟"
        };

        public MainWindow()
        {
            InitializeComponent();

           
            for (int i = 0; i < 6; i++)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int j = 0; j < 4; j++)
            {
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            
            napok = Enumerable.Range(1, 24).ToList();
            napok = napok.OrderBy(x => rnd.Next()).ToList();

            
            for (int index = 0; index < napok.Count; index++)
            {
                Button btn = new Button();
                btn.Content = napok[index].ToString();
                btn.FontSize = 18;
                btn.Margin = new Thickness(5);
                btn.Background = Brushes.LightGoldenrodYellow;
                btn.Click += Button_Click;

                int row = index / 4;
                int column = index % 4;
                Grid.SetRow(btn, row);
                Grid.SetColumn(btn, column);
                mainGrid.Children.Add(btn);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int napSzam = int.Parse(btn.Content.ToString());

            string ajandek = ajandekok[rnd.Next(ajandekok.Count)];

            DateTime today = DateTime.Today; 
            DateTime christmas = new DateTime(today.Year, 12, 24);
            int remainingDays = (christmas - today).Days;
            if (remainingDays < 0)
            {
                remainingDays = 0; 
            }

            textBlock.Text = $"A(z) {napSzam}. ajtó mögött {ajandek} rejtőzik. Karácsonyig még {remainingDays} nap van.";

            btn.IsEnabled = false;
        }
    }
}
