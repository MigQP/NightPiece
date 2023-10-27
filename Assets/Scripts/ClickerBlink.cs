using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostProcess;
public class ClickerBlink : MonoBehaviour
{
    public BlinkEffect blinker;

    public int lastPhraseIndex;

    public int phrase;

    public GameObject[] poemPhrases;

    public GameObject[] poemVideos;

    public AudioSource[] audioClips;

    public bool isLocked;

    public float speed = 0.4f;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        blinker.Blink();
        //CheckPhrase();
        StartCoroutine(RR());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isLocked || Input.GetMouseButtonDown(1) && !isLocked)
        {
            isLocked = true;
            StartCoroutine(CheckPhraseCoroutine());
            StartCoroutine(AllowLock());
        }
    }

    IEnumerator AllowLock()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
        isLocked = false;
    }


    IEnumerator CheckPhraseCoroutine()
    {
        blinker.Blink();

        if (phrase >= 0)
        {
            phrase++;
        }


        //phrase++;


        if (phrase < lastPhraseIndex)
        {
            speed = 0.4f;

        }

        if (phrase == lastPhraseIndex)
        {
            speed = 0.2f;
        }

        audioManager.NormalFadeOut();

        yield return new WaitForSeconds(speed);



            if (phrase == lastPhraseIndex)
            {
                phrase = 0;
                
            }
    
        
        CheckPhrase();
        //isLocked = false;
    }


    IEnumerator RR()
    {
        yield return new WaitForSeconds(.4f);
        CheckPhrase();
    }

    void CheckPhrase ()
    {
        //poemPhrases[phrase].SetActive(true);
        for (int i = 0; i < poemPhrases.Length; i++)
        {
            if (i == phrase)
            {
                poemPhrases[phrase].SetActive(true);
                poemVideos[phrase].SetActive(true);
                audioManager.audioLoop = audioClips[phrase];
                audioManager.NormalFadeIn();
            }

            else
            {
                poemPhrases[i].SetActive(false);
                poemVideos[i].SetActive(false);
            }
        }


    }
}
