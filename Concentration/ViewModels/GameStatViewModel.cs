using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Concentration.ViewModels
{
    public class GameStatViewModel : ViewModelBase
    {
        private const int _col = 4;
        private const int _row = 5;
        private const int _numberOfCards = _col * _row;

        private const int _pointGet = 50;
        private const int _pointLose = 25;

        private int _matchedCards;
        private int _score;

        private bool _win;

        public int MatchedCards
        {
            get { return _matchedCards; }
            set
            {
                _matchedCards = value;
                OnPropertyChanged(nameof(MatchedCards));
            }
        }

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        public bool Win
        {
            get { return _win; }
            set
            {
                _win = value;
                OnPropertyChanged("WinMessage");
            }
        }

        public GameStatViewModel()
        {
            NewGameStat();
        }

        public void NewGameStat()
        {
            _matchedCards = 0;
            _score = 0;
            _win = false;
        }

        public Visibility WinMessage
        {
            get { return Win ? Visibility.Visible : Visibility.Collapsed; }
        }

        public void AddPoints()
        {
            Score += _pointGet;
            MatchedCards += 2;
            if (MatchedCards == _numberOfCards)
            {
                Win = true;
            }
        }

        public void DeducePoints()
        {
            Score -= _pointLose;
        }
    }
}
