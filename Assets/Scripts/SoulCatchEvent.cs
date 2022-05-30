using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public enum Button
{
    North,
    East,
    South,
    West
}

public class SoulCatchEvent : MonoBehaviour
{
    [Header("Set Up")]
    [SerializeField] private GameObject buttonPrefab;

    [Header("UI Elements")]
    [SerializeField] private RectTransform[] spawnPoints;
    [SerializeField] private GameObject inputBar;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI goalText;

    private int score = 0;
    private int goal = 0;
    private float spawnRate = 1.0f;
    private float spawnTime = 0f;
    private HumanDifficulty difficulty;
    private Human human;

    private void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime < spawnRate)
            return;

        spawnTime -= spawnRate;
        SpawnButton();
    }

    public void SetUp(HumanDifficulty difficulty)
    {
        this.difficulty = difficulty;
        score = difficulty.startingScore;
        goal = difficulty.goal;
        spawnRate = difficulty.spawnRate;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString();
        goalText.text = "Goal: " + goal.ToString();
    }

    private void SpawnButton()
    {
        int index = Random.Range(0, spawnPoints.Length);
        RectTransform pos = spawnPoints[index];

        var button = Instantiate(buttonPrefab, pos.position, Quaternion.identity, pos.transform);
        Button buttonType;

        if (index == 0)
            buttonType = Button.West;
        else if (index == 1)
            buttonType = Button.North;
        else if (index == 2)
            buttonType = Button.South;
        else
            buttonType = Button.East;

        button.GetComponent<SoulButton>().SetButton(buttonType, difficulty, this);
    }

    private void YouWin()
    {

    }

    private void GameOver()
    {
        Destroy(gameObject);
        human.Pause(false);
        FindObjectOfType<StarterAssetsInputs>().GetComponent<PlayerInput>().enabled = true;
    }

    private void HitButton()
    {
        score += 1;
        if (score >= goal)
            YouWin();
        UpdateUI();
    }

    public void Miss()
    {
        score -= 1;
        if (score < 0)
            GameOver();
        UpdateUI();
    }

    public void SetHuman(Human human)
    {
        this.human = human;
    }

    private void CheckButtonPress(Button buttonToCheck)
    {
        Collider2D[] colliders = new Collider2D[4];
        int numberOfColliders = inputBar.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);

        if (numberOfColliders <= 0)
        {
            Miss();
            return;
        }

        for (int i = 0; i < numberOfColliders; i++)
        {
            var button = colliders[i].GetComponent<SoulButton>();
            if (button == null)
                continue;

            if (button.GetButton() == buttonToCheck)
                HitButton();
        }
    }

    public void OnNorth(InputValue value)
    {
        if (!value.isPressed)
            return;

        CheckButtonPress(Button.North);
    }

    public void OnSouth(InputValue value)
    {
        if (!value.isPressed)
            return;

        CheckButtonPress(Button.South);
    }

    public void OnEast(InputValue value)
    {
        if (!value.isPressed)
            return;

        CheckButtonPress(Button.East);
    }

    public void OnWest(InputValue value)
    {
        if (!value.isPressed)
            return;

        CheckButtonPress(Button.West);
    }
}
