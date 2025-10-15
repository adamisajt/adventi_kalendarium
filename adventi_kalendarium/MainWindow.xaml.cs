using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace adventi_kalendarium
{
    public partial class MainWindow : Window
    {
        private List<int> napok;
        private Random rnd = new Random();
        private Dictionary<int, bool> openedDays = new Dictionary<int, bool>();

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

        private List<SolidColorBrush> buttonColors = new List<SolidColorBrush>()
        {
            Brushes.LightPink, Brushes.LightBlue, Brushes.LightGreen, Brushes.LightCoral,
            Brushes.LightCyan, Brushes.LightGoldenrodYellow, Brushes.LightSalmon, Brushes.LightSeaGreen,
            Brushes.LightSkyBlue, Brushes.LightSteelBlue, Brushes.LightYellow, Brushes.MistyRose,
            Brushes.PeachPuff, Brushes.PaleGreen, Brushes.PaleTurquoise, Brushes.PaleVioletRed,
            Brushes.Thistle, Brushes.PowderBlue, Brushes.Lavender, Brushes.Honeydew,
            Brushes.Khaki, Brushes.Cornsilk, Brushes.LemonChiffon, Brushes.LightGray
        };

        public MainWindow()
        {
            InitializeComponent();

            openedDays = Enumerable.Range(1, 24).ToDictionary(d => d, d => false);

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
                int day = napok[index];
                Button btn = new Button();
                btn.Content = $"{day} {GetDayEmoji(day)}";
                btn.FontSize = 18;
                btn.Margin = new Thickness(5);
                btn.Background = buttonColors[day - 1];
                btn.Click += Button_Click;
                btn.Tag = day;

                btn.IsEnabled = IsButtonEnabled(day);

                mainGrid.Children.Add(btn);
                Grid.SetRow(btn, index / 4);
                Grid.SetColumn(btn, index % 4);
            }
        }

        private string GetDayEmoji(int day)
        {
            return ajandekok[day - 1].Split(' ').Last();
        }

        private bool IsButtonEnabled(int day)
        {
  
            if (day == 1) return true;

            for (int d = 1; d < day; d++)
            {
                if (!openedDays[d])
                    return false;
            }
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int napSzam = (int)btn.Tag;

            if (!IsButtonEnabled(napSzam))
            {
                MessageBox.Show("Csak sorban lehet megnyitni az ablakokat.");
                return;
            }

            string ajandek = ajandekok[rnd.Next(ajandekok.Count)];

            DateTime christmas = new DateTime(DateTime.Today.Year, 12, 24);

    
            int daysUntilChristmas = 24 - napSzam;

            textBlock.Text = $"A(z) {napSzam}. ajtó mögött {ajandek} rejtőzik. Karácsonyig még {daysUntilChristmas} nap van.";

            PlayClickSound();
            AnimateButton(btn);

            btn.IsEnabled = false;
            openedDays[napSzam] = true;

            RefreshButtonStates();
        }

        private void PlayClickSound()
        {
            try
            {
                SystemSounds.Beep.Play();
            }
            catch { }
        }

        private void AnimateButton(Button btn)
        {
            DoubleAnimation anim = new DoubleAnimation(1.0, 0.5, new Duration(TimeSpan.FromSeconds(0.3)))
            {
                AutoReverse = true
            };
            btn.BeginAnimation(Button.OpacityProperty, anim);
        }

        private void RefreshButtonStates()
        {
            foreach (var child in mainGrid.Children)
            {
                if (child is Button btn)
                {
                    int day = (int)btn.Tag;
                    if (!openedDays[day])
                    {
                        btn.IsEnabled = IsButtonEnabled(day);
                    }
                }
            }
        }
    }
}
