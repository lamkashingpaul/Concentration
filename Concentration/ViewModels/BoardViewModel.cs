using Concentration.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Concentration.ViewModels
{
    public class BoardViewModel : ViewModelBase
    {
        private const int _col = 4;
        private const int _row = 5;
        private const int _numberOfCards = _col * _row;
        public ObservableCollection<CardViewModel> Cards { get; private set; }

        private int _numberOfCardsSelected;

        private CardViewModel SelectedCard1;
        private CardViewModel SelectedCard2;

        private DispatcherTimer _readyTimer;
        private DispatcherTimer _timeoutTimer;

        private const int _readytime = 5;
        private const int _timeouttime = 3;

        private bool _areEnable;

        public BoardViewModel()
        {
            AreEnable = false;
            Cards = new ObservableCollection<CardViewModel>();

            var UnshuffledCards = new ObservableCollection<CardViewModel>();

            Random rng = new Random();
            var idsSet = new HashSet<int>();
            for (int i = 0; i < _numberOfCards; i += 2)
            {
                int num;
                do
                {
                    num = rng.Next(1, 151 + 1);
                } while (idsSet.Contains(num));
                idsSet.Add(num);

                UnshuffledCards.Add(new CardViewModel(i, num));
                UnshuffledCards.Add(new CardViewModel(i + 1, num));
            }

            for (int i = 0; i < _numberOfCards; i++)
            {
                int rngNumber = rng.Next(_numberOfCards - i);
                Cards.Add(UnshuffledCards[rngNumber]);
                UnshuffledCards.RemoveAt(rngNumber);
                Cards[i].ColCoordinate = i % _col;
                Cards[i].RowCoordinate = i / _col;
            }

            SelectedCard1 = null;
            SelectedCard2 = null;
            NumberOfCardsSelected = 0;

            _timeoutTimer = new DispatcherTimer();
            _readyTimer = new DispatcherTimer();
        }

        public int NumberOfCardsSelected
        {
            get { return _numberOfCardsSelected; }
            private set
            {
                _numberOfCardsSelected = value;
                OnPropertyChanged(nameof(NumberOfCardsSelected));
                OnPropertyChanged(nameof(AreEnable));
            }
        }

        public bool AreEnable
        {
            get
            {
                if (NumberOfCardsSelected < 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                _areEnable = value;
                OnPropertyChanged(nameof(AreEnable));
            }
        }

        public bool? MatchCards(object sender)
        {
            if (NumberOfCardsSelected == 2)
            {
                return null;
            }

            var card = sender as CardViewModel;
            Console.WriteLine(card.ColCoordinate);
            Console.WriteLine(card.RowCoordinate);
            Console.WriteLine(card.IsEnable);
            Console.WriteLine(card.IsSelected);

            if (NumberOfCardsSelected == 0)
            {
                SelectedCard1 = card;
                SelectedCard1.IsEnable = false;
                NumberOfCardsSelected += 1;
                return null;
            } else if (NumberOfCardsSelected == 1)
            {
                if (card != SelectedCard1)
                {
                    SelectedCard2 = card;
                    SelectedCard2.IsEnable = false;
                    NumberOfCardsSelected += 1;

                    if (SelectedCard1.MatchId == SelectedCard2.MatchId)
                    {
                        SelectedCard1.IsMatched = true;
                        SelectedCard2.IsMatched = true;
                        SelectedCard1 = SelectedCard2 = null;
                        NumberOfCardsSelected = 0;
                        return true;
                    }
                    else
                    {
                        _timeoutTimer.Interval = new TimeSpan(0, 0, _timeouttime);
                        _timeoutTimer.Tick += TimeoutTick;
                        _timeoutTimer.Start();
                        return false;
                    }
                }
            }
            return null;
        }

        public DispatcherTimer ReadyTimer
        {
            get { return _readyTimer; }
        }
        public DispatcherTimer TimeoutTimer
        {
            get { return _timeoutTimer; }
        }

        public void Preparation()
        {
            _readyTimer.Interval = new TimeSpan(0, 0, _readytime);
            _readyTimer.Tick += ReadyTick;
            _readyTimer.Start();

            AreEnable = false;
            foreach (var card in Cards)
            {
                card.IsEnable = false;
            }
        }

        private void TimeoutTick(object sender, EventArgs e)
        {
            if (SelectedCard1 != null)
            {
                SelectedCard1.IsEnable = true;
            }
            if (SelectedCard2 != null)
            {
                SelectedCard2.IsEnable = true;
            }
            SelectedCard1 = SelectedCard2 = null;
            NumberOfCardsSelected = 0;
            _timeoutTimer.Stop();
        }

        private void ReadyTick(object sender, EventArgs e)
        {
            AreEnable = true;
            foreach (var card in Cards)
            {
                card.IsEnable = true;
            }
            _readyTimer.Stop();
        }
    }
}
