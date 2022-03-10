using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DependencyProperties
{
    enum Precipitation
    {
        Sunny,
        Cloudy,
        Rain,
        Snow
    }

    class WeatherControl : DependencyObject
    {
        public string WindDirection { get; set; }
        public int WindSpeed { get; set; }
        public Precipitation precipitation;

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
                    0, //Значение по умолчанию
                    FrameworkPropertyMetadataOptions.Journal | //Флаги
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    null, //Действие при изменении
                    new CoerceValueCallback(CoerceTemp)), //Коррекция значений
                new ValidateValueCallback(ValidateTemp)); //Валидация    
        }

        public WeatherControl(int temperature, string windDirection, int windSpeed, Precipitation precipitation)
        {
            Temp = temperature;
            WindDirection = windDirection;
            WindSpeed = windSpeed;
            this.precipitation = precipitation;
        }
        
        private static bool ValidateTemp(object value)
        {
            int temp = (int)value;
            if (temp >= -50 && temp <= 50)
                return true;
            else return false;
        }

        private static object CoerceTemp(DependencyObject d, object baseValue) //Метод корректирует значение температуры, если вне диапазона то 0
        {
            int temp = (int)baseValue;
            if (temp >= -50 && temp <= 50)
                return temp;
            else return 0;
        }
    }
}
