using UnityEngine;
using UnityEngine.Tilemaps;

public class MaksimController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject tilemapBg;

    [SerializeField]
    public float speed;

    private bool restartPlayer;
    private GameObject inicialPosition;

    // Start is called before the first frame update
    void Start()
    {
        inicialPosition = GameObject.Find("inicialPosition");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Physics2D.IgnoreCollision(tilemapBg.GetComponent<Collider2D>(), GetComponent<Collider2D>());
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

        Restart();    
    }

    void AnimateCharacter(Vector2 dir)
    {
        animator.SetLayerWeight(1, 1f);
        animator.SetFloat("x", dir.x);
        animator.SetFloat("y", dir.y);
    }

     void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Inimigo")){
           restartPlayer = true;
        }
     }

     void Restart(){
         if(restartPlayer == true){
             rb.transform.position = new Vector2(inicialPosition.transform.position.x, inicialPosition.transform.position.y);
             restartPlayer = false;
         }
     }
}
