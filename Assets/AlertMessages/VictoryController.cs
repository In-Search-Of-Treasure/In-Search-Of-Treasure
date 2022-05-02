using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryController : MonoBehaviour
{
    public GameObject panel;
    public GameObject player;
    
    private Button okButton;
    private Rigidbody2D rbPlayer;
    
    private GameObject inicialPosition;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);

        okButton = panel.GetComponentInChildren<Button>();
        okButton.onClick.AddListener(onOkButtonClicked);

        rbPlayer = player.GetComponent<Rigidbody2D>();

        inicialPosition = GameObject.Find("inicialPosition");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Pause();
            Debug.Log("player colidiu com baú");
            panel.SetActive(true);
        }
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

    private void Restart()
    {
        rbPlayer.transform.position = new Vector2(inicialPosition.transform.position.x, inicialPosition.transform.position.y);
    }
}
