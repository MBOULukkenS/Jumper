namespace Utils
{
    public static class MathUtils
    {
        public static double Percentage(double value, double max)
        {
            return (value / max);
        }

        public static double Percentage(double value, double min, double max)
        {
            return (value - min) / (max - min);
        }

        public static double PercentageLeft(double value, double max)
        {
            return 1d - (value / max);
        }

        public static double GetValueFromPercentage(double percentage, int min, int max)
        {
            return GetValueFromPercentage(percentage, (double)min, (double)max);
        }

        public static double GetValueFromPercentage(double percentage, double min, double max)
        {
            if (percentage > 1 || percentage < 0)
                percentage = percentage > 1 ? 1 : 0;

            return min - (min - max) * percentage;
        }
    }
}