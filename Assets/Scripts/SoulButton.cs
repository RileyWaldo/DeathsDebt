using UnityEngine;

public class SoulButton : MonoBehaviour
{
    [SerializeField] private Button button = Button.North;
    [SerializeField] private float speed = 2f;

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void SetButton(Button button)
    {
        this.button = button;
    }

    public Button GetButton()
    {
        return button;
    }
}
