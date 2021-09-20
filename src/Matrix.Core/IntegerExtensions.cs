namespace Matrix
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Determines whether an integer value is in the given range between min and max values.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The allowed minimum.</param>
        /// <param name="max">The allowed maximum.</param>
        /// <returns></returns>
        public static bool IsInRange(this int value, int min, int max)
        {
            return (value >= min && value <= max);
        }
    }
}
