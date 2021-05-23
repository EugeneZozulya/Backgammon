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
        GameMode mode;
        Image selectedImage;
        List<Image> player1Checkers = new List<Image>();
        List<Image> player2Checkers = new List<Image>();
        bool isFocus = false;
        Border border = new Border();
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
            mode = GameMode.playerVsPlayer;
            if (game != null) newGame.Visibility = Visibility.Visible;
            else
            {
                game = new GameController(mode);
                control.Visibility = Visibility.Visible;
                gameField.Visibility = Visibility.Visible;
                DrawCheckers();
            }
            mainMenu.Visibility = Visibility.Hidden;
            lPlayer2.Content = "Player2";
        }
        /// <summary>
        /// MouseDown event of the label "Сдаться".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void controlMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (game != null) backToGame.Visibility = Visibility.Visible;
            gameField.Visibility = Visibility.Hidden;
            control.Visibility = Visibility.Hidden;
            mainMenu.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// MouseDown event of the label "Игрок vs Компьютер".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void playerVsComp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mode = GameMode.playerVsPlayer;
            if (game != null) newGame.Visibility = Visibility.Visible;
            else
            {
                game = new GameController(mode);
                gameField.Visibility = Visibility.Visible;
                control.Visibility = Visibility.Visible;
                DrawCheckers();
            }
            lPlayer2.Content = "Computer";
            mainMenu.Visibility = Visibility.Hidden;
        }
        private void controlSurrender_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ImageSource winPlayer1 = new BitmapImage(new Uri("pack://application:,,,/Backgammon;component/Image/WinPlayer1.png", UriKind.RelativeOrAbsolute));
            ImageSource winPlayer2 = new BitmapImage(new Uri("pack://application:,,,/Backgammon;component/Image/WinPlayer2.png", UriKind.RelativeOrAbsolute));
            if (game.Player1.State && game.Mode == GameMode.playerVsPlayer) resultGame.Background = new ImageBrush(winPlayer2);
            else if (game.Player2.State) resultGame.Background = new ImageBrush(winPlayer1);
            resultGame.Visibility = Visibility.Visible;
            gameField.Visibility = Visibility.Hidden;
            control.Visibility = Visibility.Hidden;
            game = null;
        }
        private void resultGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            resultGame.Visibility = Visibility.Hidden;
            backToGame.Visibility = Visibility.Hidden;
            mainMenu.Visibility = Visibility.Visible;

        }
        private void controlText_MouseEnter(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            label.Foreground = Brushes.White;
        }
        private void controlText_MouseLeave(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            label.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                gameField.Visibility = Visibility.Hidden;
                control.Visibility = Visibility.Hidden;
                saveOrDownload.Visibility = Visibility.Hidden;
                dialog.Visibility = Visibility.Hidden;
                resultGame.Visibility = Visibility.Hidden;
                if (game != null) backToGame.Visibility = Visibility.Visible;
                mainMenu.Visibility = Visibility.Visible;
            }
        }
        private void save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainMenu.Visibility = Visibility.Hidden;
            saveOrDownload.Visibility = Visibility.Visible;
            dialog.Visibility = Visibility.Visible;
            loadingOrSaving.Content = "Сохранить игру";
            fileName.Focus();
        }
        private void load_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainMenu.Visibility = Visibility.Hidden;
            saveOrDownload.Visibility = Visibility.Visible;
            dialog.Visibility = Visibility.Visible;
            loadingOrSaving.Content = "Загрузить игру";
        }
        private void controlDice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            game.GenerateDice();
            if (game.Player1.State)
            {
                player1Dice.Content = game.Dices[0].ToString() + " : " + game.Dices[1].ToString();
                player1Dice.Visibility = Visibility.Visible;
                player2Dice.Visibility = Visibility.Hidden;
            }
            else if (game.Player2.State)
            {
                player2Dice.Content = game.Dices[0].ToString() + " : " + game.Dices[1].ToString();
                player2Dice.Visibility = Visibility.Visible;
                player1Dice.Visibility = Visibility.Hidden;
            }
        }
        private void backToGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainMenu.Visibility = Visibility.Hidden;
            gameField.Visibility = Visibility.Visible;
            control.Visibility = Visibility.Visible;
        }
        private void yes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newGame.Visibility = Visibility.Hidden;
            game = new GameController(mode);
            gameField.Visibility = Visibility.Visible;
            control.Visibility = Visibility.Visible;
            DrawCheckers();
        }
        private void no_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newGame.Visibility = Visibility.Hidden;
            mainMenu.Visibility = Visibility.Visible;
        }
        private void back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            saveOrDownload.Visibility = Visibility.Hidden;
            dialog.Visibility = Visibility.Hidden;
            mainMenu.Visibility = Visibility.Visible;
        }
        private void loadOrSave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(loadingOrSaving.Content.ToString() == "Сохранить игру")
                XML.Save(fileName.Text, game);
            else game = XML.Download(fileName.Text);
            back_MouseDown(null, null);
        }
        private void DrawCheckers()
        {
            player1Checkers.Add(lChecker15);
            player1Checkers.Add(lChecker14);
            player1Checkers.Add(lChecker13);
            player1Checkers.Add(lChecker12);
            player1Checkers.Add(lChecker11);
            player1Checkers.Add(lChecker10);
            player1Checkers.Add(lChecker9);
            player1Checkers.Add(lChecker8);
            player1Checkers.Add(lChecker7);
            player1Checkers.Add(lChecker6);
            player1Checkers.Add(lChecker5);
            player1Checkers.Add(lChecker4);
            player1Checkers.Add(lChecker3);
            player1Checkers.Add(lChecker2);
            player1Checkers.Add(lChecker1);
            player2Checkers.Add(rChecker15);
            player2Checkers.Add(rChecker14);
            player2Checkers.Add(rChecker13);
            player2Checkers.Add(rChecker12);
            player2Checkers.Add(rChecker11);
            player2Checkers.Add(rChecker10);
            player2Checkers.Add(rChecker9);
            player2Checkers.Add(rChecker8);
            player2Checkers.Add(rChecker7);
            player2Checkers.Add(rChecker6);
            player2Checkers.Add(rChecker5);
            player2Checkers.Add(rChecker4);
            player2Checkers.Add(rChecker3);
            player2Checkers.Add(rChecker2);
            player2Checkers.Add(rChecker1);
            CheckerColor player1Color = game.Player1.Checkers.Color, player2Color = game.Player2.Checkers.Color;
            BitmapImage image1 = new BitmapImage(new Uri("Image/" + player1Color.ToString().ToLower() + ".PNG", UriKind.RelativeOrAbsolute));
            BitmapImage image2 = new BitmapImage(new Uri("Image/" + player2Color.ToString().ToLower() + ".PNG", UriKind.RelativeOrAbsolute));
            if (player1Color != CheckerColor.Black)
            {
                for (int i = 0; i < player2Checkers.Count; i++)
                {
                    player1Checkers[i].Source = image1;
                    player2Checkers[i].Source = image2;
                }
            }
        }
        private void Animation(Label label, int size, Color color)
        {
            label.FontSize = size;
            label.Foreground = new SolidColorBrush(color);
        }
        private void bigText_MouseEnter(object sender, MouseEventArgs e) => Animation((Label)sender, 52, Color.FromRgb(255, 255, 255));
        private void smallText_MouseLeave(object sender, MouseEventArgs e) => Animation((Label)sender, 50, Color.FromRgb(255, 254, 204));
        private void noOrYesText_MouseEnter(object sender, MouseEventArgs e) => Animation((Label)sender, 92, Color.FromRgb(255, 255, 255));
        private void noOrYesText_MouseLeave(object sender, MouseEventArgs e) => Animation((Label)sender, 90, Color.FromRgb(255, 254, 204));
        private void selectChecker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectedImage = (Image)sender;
            border.Width = selectedImage.Width;
            border.Height = selectedImage.Height;
            border.BorderBrush = Brushes.White;
            UIElementCollection collection = gameField.Children;
            if (!collection.Contains(border))
                gameField.Children.Add(border);
            Grid.SetColumn(border, Grid.GetColumn(selectedImage));
            Grid.SetRow(border, Grid.GetRow(selectedImage));
            border.Margin = selectedImage.Margin;
            border.BorderThickness = new Thickness(4);
        }
        private void gameField_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedImage != null && isFocus)
            {
                Point position = e.GetPosition(gameField);
                ColumnDefinitionCollection columns = gameField.ColumnDefinitions;
                int oldindex, newIndex, numRow = 1, numColumn;
                if (position.Y < gameField.Height)
                    numRow = 2;
                numColumn = (int)Math.Round(position.X / (columns[2].ActualWidth + columns[3].ActualWidth));
                if (position.X > gameField.Width / 2) numColumn--;
                numColumn *= 2;
                border.BorderThickness = new Thickness(0);
                //перемещение фишки с помощью GameController
                isFocus = false;
            }
            isFocus = true;
        }
    }
}
