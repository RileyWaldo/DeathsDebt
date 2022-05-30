[System.Serializable]
public struct HumanDifficulty
{
    public int startingScore;
    public int goal;
    public float spawnRate;
    public float speedBoost;
    public float buttonSpeed;

    public HumanDifficulty(int startingScore, int goal, float spawnRate, float speedBoost, float buttonSpeed)
    {
        this.startingScore = startingScore;
        this.goal = goal;
        this.spawnRate = spawnRate;
        this.speedBoost = speedBoost;
        this.buttonSpeed = buttonSpeed;
    }
}
