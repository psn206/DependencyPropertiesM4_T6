using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DependencyProperties
{
    enum PresenceOfPrecipitation
    {
        sunny,
        cloudy,
        rain,
        snow
    }

    internal class WeatherControl : DependencyObject
    {
        int windSpeed;
        string windDirection;
        PresenceOfPrecipitation pOfP;

        public WeatherControl(int windSpeed, string windDirection, PresenceOfPrecipitation pOfP)
        {
            this.windSpeed = windSpeed;
            this.windDirection = windDirection;
            this.pOfP = pOfP;
        }

        public int Temperature { get => windSpeed; set => windSpeed = value; }
        public string WindDirection { get => windDirection; set => windDirection = value; }
        internal PresenceOfPrecipitation POfP { get => pOfP; set => pOfP = value; }

        public static readonly DependencyProperty TempProperty;

        public int Temp
        {
            get => (int)GetValue(TempProperty);
            set => SetValue(TempProperty, value);
        }

        static WeatherControl()
        {
            TempProperty = DependencyProperty.Register(
            nameof(Temp),
            typeof(int),
            typeof(WeatherControl),
            new FrameworkPropertyMetadata(
                0,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender,
                null,
                new CoerceValueCallback(CoerceTemp)),
                new ValidateValueCallback(ValidateTemp));
        }

        private static object CoerceTemp(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if (v <= 50 && v >= -50)
            {
                return v;
            }
            else { return null; }
        }

        private static bool ValidateTemp(object value)
        {
            int v = (int)value;
            if (v <= 50 && v >= -50)
            {
                return true;
            }
            else { return false; }
        }
    }
}
