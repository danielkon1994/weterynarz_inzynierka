using System;
using System.Collections.Generic;
using System.Text;

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
                if (MondayFrom != TimeSpan.MinValue && MondayTo != TimeSpan.MinValue)
                {
                    mondayString.Append(MondayFrom.ToString() + " - " + MondayTo.ToString());
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
                if (TuesdayFrom != TimeSpan.MinValue && TuesdayTo != TimeSpan.MinValue)
                {
                    tuesdayString.Append(TuesdayFrom.ToString() + " - " + TuesdayTo.ToString());
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
                if (WednesdayFrom != TimeSpan.MinValue && WednesdayTo != TimeSpan.MinValue)
                {
                    wednesdayString.Append(WednesdayFrom.ToString() + " - " + WednesdayTo.ToString());
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
                if (ThursdayFrom != TimeSpan.MinValue && ThursdayTo != TimeSpan.MinValue)
                {
                    thursdayString.Append(ThursdayFrom.ToString() + " - " + ThursdayTo.ToString());
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
                if (FridayFrom != TimeSpan.MinValue && FridayTo != TimeSpan.MinValue)
                {
                    fridayString.Append(FridayFrom.ToString() + " - " + FridayTo.ToString());
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
                if (SaturdayFrom != TimeSpan.MinValue && SaturdayTo != TimeSpan.MinValue)
                {
                    saturdayString.Append(SaturdayFrom.ToString() + " - " + SaturdayTo.ToString());
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
                if (SundayFrom != TimeSpan.MinValue && SundayTo != TimeSpan.MinValue)
                {
                    sundayString.Append(SundayFrom.ToString() + " - " + SundayTo.ToString());
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
