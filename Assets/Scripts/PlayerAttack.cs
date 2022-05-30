using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private GameObject soulCatchGamePrefab;

    private StarterAssetsInputs input;
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        input = FindObjectOfType<StarterAssetsInputs>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = attackSound;
    }

    private void OnEnable()
    {
        input.onAttack += Attack;
    }

    private void OnDisable()
    {
        input.onAttack -= Attack;
    }

    private void Attack()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            animator.SetTrigger("Attack");
    }

    //animation events

    private void AttackSFXTrigger()
    {
        audioSource.Play();
    }

    private void AttackHitTrigger()
    {
        var hits = Physics.OverlapSphere(transform.position + transform.forward, 1f);

        foreach(Collider hit in hits)
        {
            var human = hit.GetComponent<Human>();
            if (human == null)
                continue;

            human.Pause(true);
            input.GetComponent<PlayerInput>().enabled = false;
            GameObject.FindWithTag("Music").GetComponent<Music>().PlaySecondMusic();
            var soulCatch = Instantiate(soulCatchGamePrefab);
            soulCatch.GetComponent<SoulCatchEvent>().SetUp(human.GetDifficulty());
            soulCatch.GetComponent<SoulCatchEvent>().SetHuman(human);
            break;
        }
    }
}

