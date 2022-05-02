using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MaksimController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject tilemapBg;
    
    public GameObject panel;

    private Button okButton;

    [SerializeField]
    public float speed;

    private GameObject inicialPosition;

    // Start is called before the first frame update
    void Start()
    {
        inicialPosition = GameObject.Find("inicialPosition");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Physics2D.IgnoreCollision(tilemapBg.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        
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

    }

    void AnimateCharacter(Vector2 dir)
    {
        animator.SetLayerWeight(1, 1f);
        animator.SetFloat("x", dir.x);
        animator.SetFloat("y", dir.y);
    }

     void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Inimigo")){
            
            LifeManager.Instance.DecrementLife();

            Restart();

            if (LifeManager.Instance.GetLifeNumber() < 1)
            {
                Pause();
                Debug.Log("player colidiu com inimigo");
                panel.SetActive(true);
            }
        }
     }

    void Restart() {
        rb.transform.position = new Vector2(inicialPosition.transform.position.x, inicialPosition.transform.position.y);
    }
    private void Pause()
    {
        Time.timeScale = 0;
    }

    private void Resume()
    {
        Time.timeScale = 1;
    }

    private void onOkButtonClicked()
    {
        Resume();
        Restart();
        panel.SetActive(false);
        LifeManager.Instance.RestartLifeNumber();
    }
}
