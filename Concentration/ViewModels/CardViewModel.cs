using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Concentration.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        private int _id;
        private int _matchId;
        private int _imageId;
        private string _imageSource;

        private int _imageHeight;
        private int _imageWidth;

        private int _colCoordinate;
        private int _rowCoordinate;

        private bool _isEnable;
        private bool _isMatched;
        private bool _isSelected;


        public CardViewModel(int id, int num)
        {
            Id = id;
            MatchId = id / 2;
            ImageId = num;

            ImageHeight = 128;
            ImageWidth = 128;

            IsEnable = true;
            IsMatched = false;
            IsSelected = false;         
        }

        public int Id
        {
            get { return _id; }
            private set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int MatchId
        {
            get { return _matchId; }
            private set
            {
                _matchId = value;
                OnPropertyChanged(nameof(MatchId));
            }
        }

        public int ImageId
        {
            get { return _imageId; }
            private set
            {
                _imageId = value;
                OnPropertyChanged(nameof(ImageId));
            }
        }

        public string ImageSource
        {
            get { return _imageSource; }
            private set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public int ImageHeight
        {
            get { return _imageHeight; }
            private set
            {
                _imageHeight = value;
                OnPropertyChanged(nameof(ImageHeight));
            }
        }

        public int ImageWidth
        {
            get { return _imageWidth; }
            private set
            {
                _imageWidth = value;
                OnPropertyChanged(nameof(ImageWidth));
            }
        }

        public int ColCoordinate
        {
            get { return _colCoordinate; }
            set
            {
                _colCoordinate = value;
                OnPropertyChanged(nameof(ColCoordinate));
            }
        }
        public int RowCoordinate
        {
            get { return _rowCoordinate; }
            set
            {
                _rowCoordinate = value;
                OnPropertyChanged(nameof(RowCoordinate));
            }
        }

        public bool IsEnable
        {
            get { return _isEnable; }
            set
            {
                _isEnable = value;
                if (_isEnable)
                {
                    ImageSource = "";
                } else if (!_isEnable)
                {
                    ImageSource = $"/Concentration;component/Images/{ImageId:D3}.png";
                }
                OnPropertyChanged(nameof(IsEnable));
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public bool IsMatched
        {
            get { return _isMatched; }
            set
            {
                _isMatched = value;
                OnPropertyChanged(nameof(IsMatched));
            }
        }
    }
}
