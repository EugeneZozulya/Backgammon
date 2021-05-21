﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            ColumnDefinitionCollection columns = saveGame.ColumnDefinitions;
            RowDefinitionCollection rows = saveGame.RowDefinitions;
            listSave.Width = listLoad.Width = saveFileName.Width = loadFileName.Width = columns[3].Width.Value;
            listSave.Height = listLoad.Height = rows[1].Height.Value + rows[2].Height.Value + rows[3].Height.Value;
            saveFileName.Height = loadFileName.Height = rows[5].Height.Value;
            loadFileName.IsReadOnly = true;
            loadFileName.Focusable = false;
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
            control.Visibility = Visibility.Hidden;
            gameField.Visibility = Visibility.Hidden;
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
                control.Visibility = Visibility.Visible;
                gameField.Visibility = Visibility.Visible;
                DrawCheckers();
            }
            lPlayer2.Content = "Computer";
            mainMenu.Visibility = Visibility.Hidden;
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

        private void controlSurrender_MouseDown(object sender, MouseButtonEventArgs e)
        {
            resultGame.Visibility = Visibility.Visible;
            control.Visibility = Visibility.Hidden;
            gameField.Visibility = Visibility.Hidden;
            game = null;
        }

        private void resultGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            resultGame.Visibility = Visibility.Hidden;
            backToGame.Visibility = Visibility.Hidden;
            mainMenu.Visibility = Visibility.Visible;

        }

        private void controlMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            controlMenu.Foreground = Brushes.White;
        }

        private void controlMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            controlMenu.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void controlSurrender_MouseEnter(object sender, MouseEventArgs e)
        {
            controlSurrender.Foreground = Brushes.White;
        }

        private void controlSurrender_MouseLeave(object sender, MouseEventArgs e)
        {
            controlSurrender.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                control.Visibility = Visibility.Hidden;
                gameField.Visibility = Visibility.Hidden;
                saveGame.Visibility = Visibility.Hidden;
                loadGame.Visibility = Visibility.Hidden;
                resultGame.Visibility = Visibility.Hidden;
                if (game != null) backToGame.Visibility = Visibility.Visible;
                mainMenu.Visibility = Visibility.Visible;
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

        private void loadDialog_MouseEnter(object sender, MouseEventArgs e)
        {
            loadDialog.FontSize = 52;
            loadDialog.Foreground = Brushes.White;
        }

        private void loadDialog_MouseLeave(object sender, MouseEventArgs e)
        {
            loadDialog.FontSize = 50;
            loadDialog.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainMenu.Visibility = Visibility.Hidden;
            saveGame.Visibility = Visibility.Visible;
            saveFileName.Focus();
        }

        private void load_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainMenu.Visibility = Visibility.Hidden;
            loadGame.Visibility = Visibility.Visible;
        }

        private void controlDice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            controlDice.Visibility = Visibility.Hidden;
            resultDice.Visibility = Visibility.Visible;
            game.GenerateDice();
            resultDice.Content = game.Dices[0].ToString() + " : " + game.Dices[1].ToString();
        }

        private void backToGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainMenu.Visibility = Visibility.Hidden;
            control.Visibility = Visibility.Visible;
            gameField.Visibility = Visibility.Visible;
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

        private void yes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newGame.Visibility = Visibility.Hidden;
            game = new GameController(mode);
            control.Visibility = Visibility.Visible;
            gameField.Visibility = Visibility.Visible;
            DrawCheckers();
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
            mainMenu.Visibility = Visibility.Visible;
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

        private void saveBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            saveGame.Visibility = Visibility.Hidden;
            mainMenu.Visibility = Visibility.Visible;
        }

        private void saveBack_MouseEnter(object sender, MouseEventArgs e)
        {
            saveBack.FontSize = 52;
            saveBack.Foreground = Brushes.White;
        }

        private void saveBack_MouseLeave(object sender, MouseEventArgs e)
        {
            saveBack.FontSize = 50;
            saveBack.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void saveDialog_MouseDown(object sender, MouseButtonEventArgs e)
        {
            XML.Save(saveFileName.Text, game);
            saveBack_MouseDown(null, null);
        }

        private void loadBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            loadGame.Visibility = Visibility.Hidden;
            mainMenu.Visibility = Visibility.Visible;
        }

        private void loadBack_MouseEnter(object sender, MouseEventArgs e)
        {
            loadBack.FontSize = 52;
            loadBack.Foreground = Brushes.White;
        }

        private void loadBack_MouseLeave(object sender, MouseEventArgs e)
        {
            loadBack.FontSize = 50;
            loadBack.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }

        private void loadDialog_MouseDown(object sender, MouseButtonEventArgs e)
        {
            game = XML.Download(loadFileName.Text);
            loadBack_MouseDown(null, null);
        }
        private void DrawCheckers()
        {
            CheckerColor player1Color = game.Player1.Checkers.Color, player2Color = game.Player2.Checkers.Color;
            BitmapImage image1 = new BitmapImage(new Uri(player1Color.ToString().ToLower() + ".PNG", UriKind.Relative));
            BitmapImage image2 = new BitmapImage(new Uri(player2Color.ToString().ToLower() + ".PNG", UriKind.Relative));
            if (player1Color != CheckerColor.Black)
            {
                lChecker1.Source = image1;
                lChecker2.Source = image1;
                lChecker3.Source = image1;
                lChecker4.Source = image1;
                lChecker5.Source = image1;
                lChecker6.Source = image1;
                lChecker7.Source = image1;
                lChecker8.Source = image1;
                lChecker9.Source = image1;
                lChecker10.Source = image1;
                lChecker11.Source = image1;
                lChecker12.Source = image1;
                lChecker13.Source = image1;
                lChecker14.Source = image1;
                lChecker15.Source = image1;
                rChecker1.Source = image2;
                rChecker2.Source = image2;
                rChecker3.Source = image2;
                rChecker4.Source = image2;
                rChecker5.Source = image2;
                rChecker6.Source = image2;
                rChecker7.Source = image2;
                rChecker8.Source = image2;
                rChecker9.Source = image2;
                rChecker10.Source = image2;
                rChecker11.Source = image2;
                rChecker12.Source = image2;
                rChecker13.Source = image2;
                rChecker14.Source = image2;
                rChecker15.Source = image2;
            }
        }
    }
}
