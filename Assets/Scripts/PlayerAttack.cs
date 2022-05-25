using UnityEngine;
using StarterAssets;

public class PlayerAttack : MonoBehaviour
{
    private StarterAssetsInputs input;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        input = FindObjectOfType<StarterAssetsInputs>();
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
}

