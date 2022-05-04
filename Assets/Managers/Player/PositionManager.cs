using UnityEngine;

namespace Assets.Managers.Player
{
    public class PositionManager : MonoBehaviour
    {
        public static PositionManager Instance { get; private set; }

        private Rigidbody2D PlayerRb;

        [SerializeField]
        public GameObject InitialPosition;        

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Start()
        {
            PlayerRb = GetComponent<Rigidbody2D>();   
        }

        public void RestartToInitialPosition()
        {
            PlayerRb.transform.position = new Vector2(InitialPosition.transform.position.x, InitialPosition.transform.position.y);
        }
    }
}
