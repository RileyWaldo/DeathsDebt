using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
    [Header("Difficulty")]
    [Space]
    [SerializeField] private HumanDifficulty difficulty;

    [Header("Set Up")]
    [Space]
    [SerializeField] private float runAwayDistance;
    [SerializeField] private Animator animator;

    private NavMeshAgent agent;
    private Transform player;
    private Vector3 moveTo;
    private delegate void Action();
    private Action action;

    private void Awake()
    {
        action = DetectPlayer;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        moveTo = transform.position;
    }

    private void Update()
    {
        action?.Invoke();
    }

    private void DetectPlayer()
    {
        if (Vector3.SqrMagnitude(player.position - transform.position) > Mathf.Pow(runAwayDistance, 2f))
            return;

        SetState(Fleeing, 2);
    }

    private void Fleeing()
    {
        if(Vector3.SqrMagnitude(moveTo - transform.position) <= Mathf.Pow(0.5f, 2f) || agent.velocity == Vector3.zero)
        {
            Vector3 playerDirection = Camera.main.transform.forward;
            playerDirection.z = 0f;
            Vector3 newPos = Vector3.Cross(playerDirection, Vector3.up);
            playerDirection *= runAwayDistance;
            playerDirection += newPos * Random.Range(-5f, 5f);
            agent.destination = playerDirection;
            moveTo = playerDirection;
            return;
        }

        if(Vector3.SqrMagnitude(player.position - transform.position) > Mathf.Pow(runAwayDistance, 2f))
        {
            agent.destination = transform.position;
            SetState(DetectPlayer, 5);
        }
    }

    private void SetState(Action state, int animation)
    {
        animator.SetInteger("legs", animation);
        action = state;
    }

    public void Pause(bool pause)
    {
        if (pause)
            action = null;
        else
        {
            action = Fleeing;
            agent.speed += difficulty.speedBoost;
        }
    }

    public HumanDifficulty GetDifficulty()
    {
        return difficulty;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(moveTo, 0.5f);
    }
}
