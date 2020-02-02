using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour
{
    public AudioClip musicPresent;
    public AudioClip musicPast;
    public AudioClip chamberPresent;
    public AudioClip chamberPast;
    public AudioClip walkPresent;
    public AudioClip walkPast;
    public AudioClip landing;
    public AudioClip wind;
    public AudioClip pastAmbience;
    public AudioClip itemCollect;
    public AudioClip puzzleComplete;
    public AudioClip repairedLand;
    public AudioSource PresentS;
    public AudioSource PastS;
    public AudioSource SFXS;
    public AudioSource Ambiance;
    public GameObject player;
    public Rigidbody rb;
    public float tState;
    // Start is called before the first frame update
    void Start()
    {
        tState = player.gameObject.GetComponent<PlayerController>().CurrentTimePhase;
        StartCoroutine(music());
        PresentS.Play();
        PastS.Play();
        Ambiance.Play();

    }

    // Update is called once per frame
    void Update()
    {
        tState = player.gameObject.GetComponent<PlayerController>().CurrentTimePhase;
    }
   
    IEnumerator music()
    {
        while (true)
        {
            yield return new WaitForSeconds(0);
            switch (tState)
            {
                case 0:
                    PresentS.UnPause();
                    Ambiance.UnPause();
                    PastS.Pause();
                    break;
                case 1:
                    PastS.UnPause();
                    PresentS.Pause();
                    Ambiance.Pause();
                    break;
                case 2:
                    PresentS.Pause();
                    Ambiance.Pause();
                    PastS.Pause();
                    PresentS.clip = repairedLand;
                    PresentS.Play();
                    break;

            }
        }
        yield return null;
    }

}
