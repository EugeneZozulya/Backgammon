using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Backgammon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameController game;
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// MouseDown event of the label "Выход".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// MouseDown event of the label "Игрок vs Игрок".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void playerVsPlayer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            game = new GameController(GameMode.playerVsPlayer);
            //if (firstPlayer != null)
            //{
            //    newGame.Visibility = Visibility.Visible;
            //    no.Visibility = Visibility.Visible;
            //    yes.Visibility = Visibility.Visible;
            //    double offset = canvas.Width / 4;
            //    Canvas.SetLeft(yes, canvas.Width / 2 - offset + offset/4);
            //    Canvas.SetLeft(no, canvas.Width / 2 + offset / 8);
            //}
            //else
            //{
            //    firstPlayer = new Player();
            //    secondPlayer = new Player();  
            //    visibleField();
            //    offsetting();
            //}
            //hideMenu();
            //player2.Content = "Player2";  
        }
        /// <summary>
        /// MouseDown event of the label "Сдаться".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void menu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (firstPlayer != null) backToGame.Visibility = Visibility.Visible;
            //hideField();
            //visibleMenu();
        }
        /// <summary>
        /// MouseDown event of the label "Игрок vs Компьютер".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void playerVsComp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            game = new GameController(GameMode.playerVsComp);
            //if (firstPlayer != null)
            //{
            //    newGame.Visibility = Visibility.Visible;
            //    no.Visibility = Visibility.Visible;
            //    yes.Visibility = Visibility.Visible;
            //    double offset = canvas.Width / 4;
            //    Canvas.SetLeft(yes, canvas.Width / 2 - offset + offset / 4);
            //    Canvas.SetLeft(no, canvas.Width / 2 + offset / 8);
            //}
            //else
            //{
            //    firstPlayer = new Player();
            //    secondPlayer = new Player();
            //    visibleField();
            //    offsetting();
            //}
            //player2.Content = "Computer";
            //hideMenu();
        }

        private void playerVsPlayer_MouseEnter(object sender, MouseEventArgs e)
        {
            playerVsPlayer.FontSize = 52;
            playerVsPlayer.Foreground = Brushes.White;
        }

        private void playerVsPlayer_MouseLeave(object sender, MouseEventArgs e)
        {
            playerVsPlayer.FontSize = 50;
            playerVsPlayer.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void playerVsComp_MouseEnter(object sender, MouseEventArgs e)
        {
            playerVsComp.FontSize = 52;
            playerVsComp.Foreground = Brushes.White;
        }

        private void playerVsComp_MouseLeave(object sender, MouseEventArgs e)
        {
            playerVsComp.FontSize = 50;
            playerVsComp.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void save_MouseEnter(object sender, MouseEventArgs e)
        {
            save.FontSize = 52;
            save.Foreground = Brushes.White;
        }

        private void save_MouseLeave(object sender, MouseEventArgs e)
        {
            save.FontSize = 50;
            save.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void load_MouseEnter(object sender, MouseEventArgs e)
        {
            load.FontSize = 52;
            load.Foreground = Brushes.White;
        }

        private void load_MouseLeave(object sender, MouseEventArgs e)
        {
            load.FontSize = 50;
            load.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void exit_MouseEnter(object sender, MouseEventArgs e)
        {
            exit.FontSize = 52;
            exit.Foreground = Brushes.White;
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            exit.FontSize = 50;
            exit.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void surrender_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //result.Visibility = Visibility.Visible;
            //hideField();
            //firstPlayer = null;
            //secondPlayer = null;
            //Canvas.SetTop(playerVsPlayer, Canvas.GetTop(playerVsPlayer) - 40);
            //Canvas.SetTop(playerVsComp, Canvas.GetTop(playerVsComp) - 40);
            //Canvas.SetTop(save, Canvas.GetTop(save) - 40);
            //Canvas.SetTop(load, Canvas.GetTop(load) - 40);
            //Canvas.SetTop(exit, Canvas.GetTop(exit) - 40);
        }

        private void result_MouseDown(object sender, MouseButtonEventArgs e)
        {
            result.Visibility = Visibility.Hidden;
            hideField();
            visibleMenu();

        }

        private void menu_MouseEnter(object sender, MouseEventArgs e)
        {
            menu.Foreground = Brushes.White;
        }

        private void menu_MouseLeave(object sender, MouseEventArgs e)
        {
            menu.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void surrender_MouseEnter(object sender, MouseEventArgs e)
        {
            surrender.Foreground = Brushes.White;
        }

        private void surrender_MouseLeave(object sender, MouseEventArgs e)
        {
            surrender.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                visibleMenu();
                hideField();
                hideSaveOrDownload();
                //if (firstPlayer != null) backToGame.Visibility = Visibility.Visible;
                result.Visibility = Visibility.Hidden;
                newGame.Visibility = Visibility.Hidden;
                no.Visibility = Visibility.Hidden;
                yes.Visibility = Visibility.Hidden;
            }
        }

        private void saveDialog_MouseEnter(object sender, MouseEventArgs e)
        {
            saveDialog.FontSize = 52;
            saveDialog.Foreground = Brushes.White;
        }

        private void saveDialog_MouseLeave(object sender, MouseEventArgs e)
        {
            saveDialog.FontSize = 50;
            saveDialog.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void downloadDialog_MouseEnter(object sender, MouseEventArgs e)
        {
            downloadDialog.FontSize = 52;
            downloadDialog.Foreground = Brushes.White;
        }

        private void downloadDialog_MouseLeave(object sender, MouseEventArgs e)
        {
            downloadDialog.FontSize = 50;
            downloadDialog.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hideSaveOrDownload();
            visibleMenu();
        }

        private void back_MouseEnter(object sender, MouseEventArgs e)
        {
            back.FontSize = 52;
            back.Foreground = Brushes.White;
        }

        private void back_MouseLeave(object sender, MouseEventArgs e)
        {
            back.FontSize = 50;
            back.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hideField();
            hideMenu();
            visibleSaveOrDownload();
            saveDialog.Visibility = Visibility.Visible;
            FocusManager.SetFocusedElement(this, fileName);
        }

        private void load_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hideField();
            hideMenu();
            visibleSaveOrDownload();
            downloadDialog.Visibility = Visibility.Visible;
        }

        private void dice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Random rnd = new Random();
            int dice1 = rnd.Next(1, 6);
            int dice2 = rnd.Next(1, 6);
            dice.Visibility = Visibility.Hidden;
            resultDice.Visibility = Visibility.Visible;
            resultDice.Content = dice1.ToString() + " : " + dice2.ToString();
        }

        private void backToGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hideMenu();
            visibleField();
        }

        private void backToGame_MouseEnter(object sender, MouseEventArgs e)
        {
            backToGame.FontSize = 52;
            backToGame.Foreground = Brushes.White;
        }

        private void backToGame_MouseLeave(object sender, MouseEventArgs e)
        {
            backToGame.FontSize = 50;
            backToGame.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }
        private void hideMenu()
        {
            rectangle.Visibility = Visibility.Hidden;
            info.Visibility = Visibility.Hidden;
            playerVsComp.Visibility = Visibility.Hidden;
            playerVsPlayer.Visibility = Visibility.Hidden;
            save.Visibility = Visibility.Hidden;
            load.Visibility = Visibility.Hidden;
            exit.Visibility = Visibility.Hidden;
            backToGame.Visibility = Visibility.Hidden;
        }
        private void hideField()
        {
            menu.Visibility = Visibility.Hidden;
            dice.Visibility = Visibility.Hidden;
            surrender.Visibility = Visibility.Hidden;
            player1.Visibility = Visibility.Hidden;
            player2.Visibility = Visibility.Hidden;
            resultDice.Visibility = Visibility.Hidden;
        }
        private void visibleMenu()
        {
            rectangle.Visibility = Visibility.Visible;
            info.Visibility = Visibility.Visible;
            playerVsComp.Visibility = Visibility.Visible;
            playerVsPlayer.Visibility = Visibility.Visible;
            save.Visibility = Visibility.Visible;
            load.Visibility = Visibility.Visible;
            exit.Visibility = Visibility.Visible;
        }
        private void visibleField()
        {
            menu.Visibility = Visibility.Visible;
            dice.Visibility = Visibility.Visible;
            surrender.Visibility = Visibility.Visible;
            player1.Visibility = Visibility.Visible;
            player2.Visibility = Visibility.Visible;
        }
        private void visibleSaveOrDownload()
        {
            listDownload.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Visible;
            fileName.Visibility = Visibility.Visible;
            saveOrDownload.Visibility = Visibility.Visible;

        }
        private void hideSaveOrDownload()
        {
            saveOrDownload.Visibility = Visibility.Hidden;
            listDownload.Visibility = Visibility.Hidden;
            back.Visibility = Visibility.Hidden;
            saveDialog.Visibility = Visibility.Hidden;
            fileName.Visibility = Visibility.Hidden;
            downloadDialog.Visibility = Visibility.Hidden;
        }

        private void yes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newGame.Visibility = Visibility.Hidden;
            no.Visibility = Visibility.Hidden;
            yes.Visibility = Visibility.Hidden;
            //firstPlayer = new Player();
            //secondPlayer = new Player();
            visibleField();
        }

        private void yes_MouseEnter(object sender, MouseEventArgs e)
        {
            yes.FontSize = 92;
            yes.Foreground = Brushes.White;
        }

        private void yes_MouseLeave(object sender, MouseEventArgs e)
        {
            yes.FontSize = 90;
            yes.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void no_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newGame.Visibility = Visibility.Hidden;
            no.Visibility = Visibility.Hidden;
            yes.Visibility = Visibility.Hidden;
            visibleMenu();
            backToGame.Visibility = Visibility.Visible;
        }

        private void no_MouseEnter(object sender, MouseEventArgs e)
        {
            no.FontSize = 92;
            no.Foreground = Brushes.White;
        }

        private void no_MouseLeave(object sender, MouseEventArgs e)
        {
            no.FontSize = 90;
            no.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void offsetting()
        {
            Canvas.SetTop(backToGame, Canvas.GetTop(playerVsPlayer) - 40);
            Canvas.SetTop(playerVsPlayer, Canvas.GetTop(playerVsPlayer) + 40);
            Canvas.SetTop(playerVsComp, Canvas.GetTop(playerVsComp) + 40);
            Canvas.SetTop(save, Canvas.GetTop(save) + 40);
            Canvas.SetTop(load, Canvas.GetTop(load) + 40);
            Canvas.SetTop(exit, Canvas.GetTop(exit) + 40);

        }
    }
}
