using System;
using System.Text.RegularExpressions;
using Ui.UiInterface;
using Zenject;

namespace Ui
{
    public class Model : ITime, ICoupon
    {
        private View _view;
        
        private int _countCoupon;
        private string _time;
        
        private const string TimeFormatPattern = @"^([0-1]?[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$";

        [Inject]
        public void Construct(View view)
        {
            _view = view;
        }
        
        public string TimeStartAfterGame
        {
            set
            {
                if (value == null) throw new ArgumentNullException();
                if (!CheckTimeFormat(value)) throw new FormatException();

                _time = value;
                RecordViewTime(_time);
            }
        }

        public int CountCoupon
        {
            get => _countCoupon;
            set
            {
                if (value < 0) throw new ArgumentException();
                _countCoupon = value;
                RecordViewCountCoupon(_countCoupon.ToString());
            }
        }

        private void RecordViewTime(string time) => _view.TimeAfterStartGame.text = time;

        private void RecordViewCountCoupon(string count) => _view.CountCoupons.text = count;

        private bool CheckTimeFormat(string time) => Regex.IsMatch(time, TimeFormatPattern);

    }
}