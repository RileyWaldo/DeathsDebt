using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoulButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button button = Button.North;
    [SerializeField] private float speed = 128f;

    private Collider2D collider2d;
    private Collider2D tooFarZone;

    private SoulCatchEvent soulCatch;

    private void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        tooFarZone = GameObject.FindWithTag("Finish").GetComponent<Collider2D>();
    }

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(Physics2D.IsTouching(collider2d, tooFarZone))
        {
            soulCatch.Miss();
            Destroy(gameObject);
        }
    }

    public void SetButton(Button button, HumanDifficulty difficulty, SoulCatchEvent catchEvent)
    {
        this.button = button;
        speed = difficulty.buttonSpeed;
        soulCatch = catchEvent;
        var image = GetComponent<Image>();
        switch(button)
        {
            case Button.North:
                image.color = Color.yellow;
                buttonText.text = "Y";
                break;

            case Button.South:
                image.color = Color.green;
                buttonText.text = "A";
                break;

            case Button.East:
                image.color = Color.red;
                buttonText.text = "B";
                break;

            case Button.West:
                image.color = Color.blue;
                buttonText.text = "X";
                break;
        }
    }

    public Button GetButton()
    {
        return button;
    }
}
