namespace ComicHero.Data
{
    /// <summary>
    ///     Container of stats for the players.
    /// </summary>
    [System.Serializable]
    public class CharacterStats
    {
        //Character Data
        public string name;
        public Species species;
        public Heroes heroType;
        public Villains villainType;

        //Stats
        public float balance = 1.0f;
        public float maxHealth = Constants.MaxHealth;
        public float punchDamage = Constants.MaxPunchDamage;
        public float speed = Constants.MaxSpeed;
        public float jumpForce = Constants.MaxJumpForce;
    }
}