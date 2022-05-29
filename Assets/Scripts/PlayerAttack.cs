using UnityEngine;
using StarterAssets;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AudioClip attackSound;

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
        if(!audioSource.isPlaying)
            audioSource.Play();
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            animator.SetTrigger("Attack");
    }
}

