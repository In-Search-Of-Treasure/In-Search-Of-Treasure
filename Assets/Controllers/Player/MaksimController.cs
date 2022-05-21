using Assets.Managers;
using Assets.Managers.Player;
using UnityEngine;

public class MaksimController : Subject
{
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject tilemapBg;

    [SerializeField]
    public float speed;

    private const string TRAP_DAMAGE_ANIMATION = "Spike_Trap_Middle";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Avoid player collision with camera follow area.
        Physics2D.IgnoreCollision(tilemapBg.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        var hudController = FindObjectOfType<HudController>();
        AddObserver(hudController);

        LifeManager.Instance.RestartLifeNumber();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed * Time.deltaTime;

        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            AnimateCharacter(rb.velocity);
        }
        else
        {
            animator.SetLayerWeight(1, 0f);
        }

        PlayerPause();
    }

    void AnimateCharacter(Vector2 dir)
    {
        animator.SetLayerWeight(1, 1f);
        animator.SetFloat("x", dir.x);
        animator.SetFloat("y", dir.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerCollidesWithEnemy(collision);
        PlayerCollidesWithChest(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.Player.Trap))
        {
            if (collision.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(TRAP_DAMAGE_ANIMATION))
            {
                PlayerDecrementLife();
            }
        }
    }

    private void PlayerDecrementLife()
    {
        Notify(NotificationType.PlayerLost1Life);
        DecrementLifeNumber();
        PositionManager.Instance.RestartToInitialPosition();

        if (LifeManager.Instance.GetLifeNumber() < 1)
        {
            GameManager.Instance.PauseGame();
            SceneGameManager.Instance.LoadGameOverMenu();
        }
    }

    private void PlayerCollidesWithEnemy(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.Player.Enemy))
        {
            PlayerDecrementLife();
        }
    }

    private void PlayerCollidesWithChest(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.Player.Chest))
        {
            GameManager.Instance.PauseGame();
            SceneGameManager.Instance.LoadGameWon();
        }
    }

    private void PlayerPause()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.PauseGame();
            GameManager.Instance.GameWonDeactive();
            SceneGameManager.Instance.LoadMainMenu();
        }
    }

    private void DecrementLifeNumber()
    {
        Debug.Log("The player lost 1 life");
    }
}
