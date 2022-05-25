using UnityEngine;
using StarterAssets;

public class SoulCatchEvent : MonoBehaviour
{
    private StarterAssetsInputs input;

    [SerializeField] private RectTransform[] spawnPoints;
    [SerializeField] private GameObject buttonPrefab;

    private int score = 0;
    private int goal = 0;
    private float spawnRate = 1.0f;
    private float spawnTime = 0f;
    private float buttonSpeed = 1f;

    private void Awake()
    {
        input = FindObjectOfType<StarterAssetsInputs>();
    }

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
        Vector3 pos = spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position;

        Instantiate(buttonPrefab, pos, Quaternion.identity);
    }

    private void YouWin()
    {

    }

    private void GameOver()
    {
        Destroy(gameObject);
    }

    public void Miss()
    {
        score -= 1;
        if (score < 0)
            GameOver();
    }
}
