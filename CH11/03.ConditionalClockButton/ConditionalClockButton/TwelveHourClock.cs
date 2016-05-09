using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClockButton;

namespace ConditionalClockButton
{
    public class TwelveHourClock : ClockButton.Clock
    {
        private int hour12 = 12;
        private bool isAm = true;
        private bool isPm = false;

        public int Hour12
        {
            set { SetProperty<int>(ref hour12, value); }
            get { return hour12; }
        }

        public bool IsAm
        {
            set { SetProperty<bool>(ref isAm, value); }
            get { return isAm; }
        }

        public bool IsPm
        {
            set { SetProperty<bool>(ref isPm, value); }
            get { return isPm; }
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            if (propertyName == "Hour")
            {
                this.Hour12 = (this.Hour - 1) % 12 + 1;
                this.IsAm = this.Hour < 12;
                this.isPm = !this.IsAm;
            }

            base.OnPropertyChanged(propertyName);
        }
    }
}
