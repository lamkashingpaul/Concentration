using Concentration.Utilities;
using Concentration.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Concentration.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // All views available
        private object _currentView;
        private object _startView;
        private object _gameView;

        // All viewmodel available
        private object _currentViewModel;
        private object _startViewModel;
        private object _gameViewModel;

        // ICommand to switch view
        private ICommand _gotoStartViewModelCommand;
        private ICommand _gotoGameViewModelCommand;
        private ICommand _closeWindowCommand;

        private bool _wrongpassword;
        private Brush _borderBrush;

        public ICommand GotoStartViewModelCommand
        {
            get { return _gotoStartViewModelCommand ?? (_gotoStartViewModelCommand = new RelayCommand(x => { GotoStartViewModel(); })); }
        }
        public ICommand GotoGameViewModelCommand
        {
            get { return _gotoGameViewModelCommand ?? (_gotoGameViewModelCommand = new RelayCommand(x => { GotoGameViewModel(); })); }
        }

        public ICommand CloseWindowCommand
        {
            get { return _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand(x => { CloseWindow(x); })); }
        }

        // Default Constructor
        public MainWindowViewModel()
        {
            _startView = new StartView();
            _gameView = new GameView();

            _startViewModel = new StartViewModel();
            _gameViewModel = new GameViewModel();

            _wrongpassword = false;
            _borderBrush = Brushes.Black;

            CurrentView = _startView;
            CurrentViewModel = _startViewModel;
        }


        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public bool WrongPassword
        {
            get { return _wrongpassword; }
            set
            {
                _wrongpassword = value;
                OnPropertyChanged(nameof(WrongPassword));
            }
        }

        public Brush BorderBrush
        {
            get { return _borderBrush; }
            set
            {
                if (WrongPassword)
                {
                    _borderBrush = Brushes.Red;
                }else
                {
                    _borderBrush = Brushes.Black;
                }
                OnPropertyChanged(nameof(BorderBrush));
            }
        }

        // Methods to switch view
        private void GotoStartViewModel()
        {
            CurrentViewModel = _startViewModel;
            CurrentView = _startView;
        }
        private void GotoGameViewModel()
        {

            CurrentView = _gameView;
            CurrentViewModel = _gameViewModel;

        }
        private void CloseWindow(object Windows)
        {
            (Windows as System.Windows.Window)?.Close();
        }
    }
}
