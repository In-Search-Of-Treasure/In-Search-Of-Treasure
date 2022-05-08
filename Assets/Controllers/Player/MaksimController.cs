using Assets.Managers;
using Assets.Managers.Player;
using UnityEngine;
using UnityEngine.UI;

public class MaksimController : Subject
{
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject tilemapBg;
    public GameObject panel;
    private Button okButton;

    [SerializeField]
    public float speed;   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Avoid player collision with camera follow area.
        Physics2D.IgnoreCollision(tilemapBg.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        var hudController = FindObjectOfType<HudController>();

        AddObserver(hudController);

        panel.SetActive(false);

        okButton = panel.GetComponentInChildren<Button>();
        okButton.onClick.AddListener(onOkButtonClicked);

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
    }

    private void onOkButtonClicked()
    {
        panel.SetActive(false);
        GameManager.Instance.ResumeGame();
        PositionManager.Instance.RestartToInitialPosition();        
        LifeManager.Instance.RestartLifeNumber();
    }

    private void PlayerCollidesWithEnemy(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.Player.Enemy))
        {
            //LifeManager.Instance.DecrementLife();
            //LifeSubject.LostLifeEvent += DecrementLifeNumber;
            Notify(null, NotificationType.PlayerLost1Life);

            DecrementLifeNumber();
            PositionManager.Instance.RestartToInitialPosition();

            if (LifeManager.Instance.GetLifeNumber() < 1)
            {
                GameManager.Instance.PauseGame();                
                SceneGameManager.Instance.LoadGameOverMenu();                
            }
        }
    }

    private void PlayerPause()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.PauseGame();
            SceneGameManager.Instance.LoadMainMenu();
        }
    }

    private void DecrementLifeNumber()
    {
        Debug.Log("The player lost 1 life");
    }   
}
