using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

public class TextEffect : Subject
{
    public Text text;
    public bool playOnAwake = true;
    public float delayToStart;
    public float delayBetweenChars = 0.125f;
    public float delayAfterPunctuation = 0.5f;
    private string story;
    private float originDelayBetweenChars;
    private bool lastCharPunctuation = false;
    private char charComma;
    private char charPeriod;
    private char charEmpty;
    public GameObject AudioTypping;
    private AudioSource TyppingFX;

    void Awake()
    {
        TyppingFX = AudioTypping.GetComponent<AudioSource>();
        TyppingFX.clip = AudioTypping.GetComponent<AudioSource>().clip;
        
        var cutsceneController = FindObjectOfType<CutsceneController>();
        var keyController = FindObjectOfType<KeyController>();
        AddObserver(cutsceneController);
        AddObserver(keyController);

        text = GetComponent<Text>();
        originDelayBetweenChars = delayBetweenChars;
        charComma = Convert.ToChar(44);
        charPeriod = Convert.ToChar(46);
        charEmpty = Convert.ToChar(" ");//Convert.ToChar(255);

        if (playOnAwake)
        {
            ChangeText(text.text, delayToStart);
        }
    }

    private void Update()
    {
        SkipCutscene();
    }

    //Update text and start typewriter effect
    private void ChangeText(string textContent, float delayBetweenChars = 0f)
    {
        StopCoroutine(PlayText()); //stop Coroutime if exist
        story = textContent;
        text.text = ""; //clean text
        Invoke("Start_PlayText", delayBetweenChars); //Invoke effect
    }

    private void Start_PlayText()
    {
        StartCoroutine(PlayText(ReleaseTextWritter));
    }

    private IEnumerator PlayText(Action callBack = null)
    {
        foreach (char c in story)
        {
            delayBetweenChars = originDelayBetweenChars;

            if (lastCharPunctuation)  //If previous character was a comma/period, pause typing
            {
                TyppingFX.Pause();
                yield return new WaitForSeconds(delayBetweenChars = delayAfterPunctuation);
                lastCharPunctuation = false;
            }

            if ( c == charEmpty /*|| c == charComma || c == charPeriod */ )
            {
              TyppingFX.Pause();
              lastCharPunctuation = true;
            }

            TyppingFX.PlayOneShot(TyppingFX.clip,0.002f);
            text.text += c;
            yield return new WaitForSeconds(delayBetweenChars);                                    
        }

        if (callBack != null)
        {
            callBack();
        }
    }

    private void ReleaseTextWritter()
    {
        TyppingFX.Stop();
        Notify(NotificationType.CutsceneStopped);
    }

    private void SkipCutscene() 
    {
        if (Input.anyKey)
        {
            Notify(NotificationType.CutsceneSkipped);
        }
    }
}
