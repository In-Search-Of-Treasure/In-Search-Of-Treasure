using Assets.Managers;
using Assets.Managers.Player;
using System.Collections;
using UnityEngine;

public class MaksimController : Subject
{
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject tilemapBg;
    public GameObject inventoryIsFullMessage;
    public GameObject inventory;

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
        var keyController = FindObjectOfType<KeyController>();
        var pauseController = FindObjectOfType<PauseMenuController>();

        AddObserver(hudController);
        AddObserver(keyController);
        AddObserver(pauseController);

        LifeManager.Instance.RestartLifeNumber();               
    }

    // Update is called once per frame
    void Update()
    {
        speed = GetPlayerSpeed();

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
        PlayerOpenCloseInventory();        
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
        PlayerCollidesWithFruit(collision);
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

    private void PlayerCollidesWithFruit(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.Fruits.Fruit1)){
            InventoryIsFull();
            InventoryManager.Instance.AddFruit(Fruits.Fruit1, collision.gameObject);
        }

        if (collision.gameObject.CompareTag(TagsConstants.Fruits.Fruit2))
        {
            InventoryIsFull();
            InventoryManager.Instance.AddFruit(Fruits.Fruit2, collision.gameObject);
        }

        if (collision.gameObject.CompareTag(TagsConstants.Fruits.Fruit3))
        {
            InventoryIsFull();
            InventoryManager.Instance.AddFruit(Fruits.Fruit3, collision.gameObject);
        }
    }

    private void PlayerPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Notify(NotificationType.PlayerPressedEsc);
        }
    }

    private void PlayerOpenCloseInventory()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            Notify(NotificationType.PlayerOpenOrCloseInventory, inventory);
        }
    }    

    private void DecrementLifeNumber()
    {
        Debug.Log("The player lost 1 life");
    }

    private void InventoryIsFull()
    {
        if (InventoryManager.Instance.IsFull())
        {            
            StartCoroutine(OpenCloseIsFullMsg());
            return;
        }
    }

    private IEnumerator OpenCloseIsFullMsg()
    {
        inventoryIsFullMessage.SetActive(true);        
        yield return new WaitForSeconds(3);
        inventoryIsFullMessage.SetActive(false);
    }

    private float GetPlayerSpeed()
    {
        var speed = PlayerPrefs.GetFloat(PlayerPrefConstants.SkillsRelated.PlayerSpeed, 700);
        return speed;
    }
}
