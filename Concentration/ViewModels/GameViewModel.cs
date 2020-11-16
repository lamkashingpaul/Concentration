using Concentration.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Concentration.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        public BoardViewModel Board { get; private set; }
        public GameStatViewModel GameStat { get; private set; }

        protected ICommand _resetGame;
        protected ICommand _selectCard;

        public ICommand ResetGameCommand
        {
            get { return _resetGame ?? (_resetGame = new RelayCommand(x => { ResetGame(); })); }
        }

        public ICommand SelectCardCommand
        {
            get { return _selectCard ?? (_selectCard = new RelayCommand(x => { SelectCard(x); })); }
        }

        public GameViewModel()
        {
            NewGame();
        }

        private void NewGame()
        {
            Board = new BoardViewModel();
            GameStat = new GameStatViewModel();

            Board.Preparation();

            OnPropertyChanged(nameof(Board));
            OnPropertyChanged(nameof(GameStat));
        }

        private void ResetGame()
        {
            NewGame();
        }

        private void SelectCard(object sender)
        {
            var card = sender as CardViewModel;
            bool? result = Board.MatchCards(card);
            if (result == true)
            {
                GameStat.AddPoints();
            } else if (result == false)
            {
                GameStat.DeducePoints();
                GameStat.Attempts -= 1;
                if (GameStat.Attempts == 0)
                {
                    Board.TimeoutTimer.Stop();
                    Board.AreEnable = false;
                    GameStat.Lose = true;
                }
            }
        }
    }
}
