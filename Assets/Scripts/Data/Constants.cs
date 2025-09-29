namespace ComicHero.Data
{
    /// <summary>
    ///     Global constants for Comic Hero.
    /// </summary>
    public static class Constants
    {
        public const float MaxHealth = 100;
        public const int MaxLives = 3;

        /// <summary>
        ///     Stored list of all available player color.
        /// </summary>
        public static readonly PlayerColors[] AvailablePlayerColors = new PlayerColors[8]
        {
            PlayerColors.Blue,
            PlayerColors.Teal,
            PlayerColors.Green,
            PlayerColors.Purple,
            PlayerColors.Red,
            PlayerColors.Yellow,
            PlayerColors.Orange,
            PlayerColors.Black
        };
    }
}