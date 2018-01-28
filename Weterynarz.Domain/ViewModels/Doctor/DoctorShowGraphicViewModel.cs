using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Basic.Const;

namespace Weterynarz.Domain.ViewModels.Doctor
{
    public class DoctorShowGraphicViewModel
    {
        public int Id { get; set; }

        public TimeSpan MondayFrom { get; set; }
        public TimeSpan MondayTo { get; set; }
        public TimeSpan TuesdayFrom { get; set; }
        public TimeSpan TuesdayTo { get; set; }
        public TimeSpan WednesdayFrom { get; set; }
        public TimeSpan WednesdayTo { get; set; }
        public TimeSpan ThursdayFrom { get; set; }
        public TimeSpan ThursdayTo { get; set; }
        public TimeSpan FridayFrom { get; set; }
        public TimeSpan FridayTo { get; set; }
        public TimeSpan SaturdayFrom { get; set; }
        public TimeSpan SaturdayTo { get; set; }
        public TimeSpan SundayFrom { get; set; }
        public TimeSpan SundayTo { get; set; }

        public string Monday {
            get
            {
                StringBuilder mondayString = new StringBuilder();
                if (MondayFrom != TimeSpan.Zero && MondayTo != TimeSpan.Zero)
                {
                    mondayString.Append(MondayFrom.ToString(@"hh\:mm") + " - " + MondayTo.ToString(@"hh\:mm"));
                }
                if (mondayString.Length == 0)
                {
                    mondayString.Append("NIEOBECNY");
                }
                return mondayString.ToString();
            }
        }
        public string Tuesday {
            get
            {
                StringBuilder tuesdayString = new StringBuilder();
                if (TuesdayFrom != TimeSpan.Zero && TuesdayTo != TimeSpan.Zero)
                {
                    tuesdayString.Append(TuesdayFrom.ToString(@"hh\:mm") + " - " + TuesdayTo.ToString(@"hh\:mm"));
                }
                if(tuesdayString.Length == 0)
                {
                    tuesdayString.Append("NIEOBECNY");
                }
                return tuesdayString.ToString();
            }
        }
        public string Wednesday {
            get
            {
                StringBuilder wednesdayString = new StringBuilder();
                if (WednesdayFrom != TimeSpan.Zero && WednesdayTo != TimeSpan.Zero)
                {
                    wednesdayString.Append(WednesdayFrom.ToString(@"hh\:mm") + " - " + WednesdayTo.ToString(@"hh\:mm"));
                }
                if (wednesdayString.Length == 0)
                {
                    wednesdayString.Append("NIEOBECNY");
                }
                return wednesdayString.ToString();
            }
        }
        public string Thursday {
            get
            {
                StringBuilder thursdayString = new StringBuilder();
                if (ThursdayFrom != TimeSpan.Zero && ThursdayTo != TimeSpan.Zero)
                {
                    thursdayString.Append(ThursdayFrom.ToString(@"hh\:mm") + " - " + ThursdayTo.ToString(@"hh\:mm"));
                }
                if (thursdayString.Length == 0)
                {
                    thursdayString.Append("NIEOBECNY");
                }
                return thursdayString.ToString();
            }
        }
        public string Friday {
            get
            {
                StringBuilder fridayString = new StringBuilder();
                if (FridayFrom != TimeSpan.Zero && FridayTo != TimeSpan.Zero)
                {
                    fridayString.Append(FridayFrom.ToString(@"hh\:mm") + " - " + FridayTo.ToString(@"hh\:mm"));
                }
                if (fridayString.Length == 0)
                {
                    fridayString.Append("NIEOBECNY");
                }
                return fridayString.ToString();
            }
        }
        public string Saturday {
            get
            {
                StringBuilder saturdayString = new StringBuilder();
                if (SaturdayFrom != TimeSpan.Zero && SaturdayTo != TimeSpan.Zero)
                {
                    saturdayString.Append(SaturdayFrom.ToString(@"hh\:mm") + " - " + SaturdayTo.ToString(@"hh\:mm"));
                }
                if (saturdayString.Length == 0)
                {
                    saturdayString.Append("NIEOBECNY");
                }
                return saturdayString.ToString();
            }
        }
        public string Sunday {
            get
            {
                StringBuilder sundayString = new StringBuilder();
                if (SundayFrom != TimeSpan.Zero && SundayTo != TimeSpan.Zero)
                {
                    sundayString.Append(SundayFrom.ToString(@"hh\:mm") + " - " + SundayTo.ToString(@"hh\:mm"));
                }
                if (sundayString.Length == 0)
                {
                    sundayString.Append("NIEOBECNY");
                }
                return sundayString.ToString();
            }
        }
    }
}
