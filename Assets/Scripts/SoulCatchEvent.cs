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
    [SerializeField] private RectTransform[] spawnPoints;
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private GameObject inputBar;

    private int score = 0;
    private int goal = 0;
    private float spawnRate = 1.0f;
    private float spawnTime = 0f;
    private float buttonSpeed = 1f;

    private void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime < spawnRate)
            return;

        spawnTime -= spawnRate;
        SpawnButton();
    }

    private void SpawnButton()
    {
        RectTransform pos = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];

        Instantiate(buttonPrefab, pos.position, Quaternion.identity, pos.transform);
    }

    private void YouWin()
    {

    }

    private void GameOver()
    {
        Destroy(gameObject);
    }

    private void HitButton()
    {
        score += 1;
        if (score >= goal)
            YouWin();
    }

    public void Miss()
    {
        score -= 1;
        if (score < 0)
            GameOver();
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
