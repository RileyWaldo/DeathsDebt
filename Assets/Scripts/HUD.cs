using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 300f;

    [SerializeField] private TextMeshProUGUI soulsText;
    [SerializeField] private TextMeshProUGUI timeText;

    private int score = 0;
    private bool gameOver = false;

    private void Update()
    {
        if (gameOver)
            return;

        float timeTick = Mathf.Floor(timeRemaining);

        timeRemaining -= Time.deltaTime;

        if(timeRemaining < timeTick)
        {
            var mins = Mathf.Floor((Mathf.Floor(timeRemaining) / 60f)).ToString();
            var secs = Mathf.Floor(timeRemaining % 60f);
            string seconds;

            seconds = secs < 10 ? "0" + secs.ToString() : secs.ToString();

            timeText.text = mins + ":" + seconds;
        }

        if(timeRemaining <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        timeRemaining = 0f;
        gameOver = true;
        //gameover
    }

    public void IncreaseScore()
    {
        score += 1;
        soulsText.text = "Souls Reaped: " + score.ToString();
    }
}
