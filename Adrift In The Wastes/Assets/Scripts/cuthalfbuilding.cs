using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuthalfbuilding : MonoBehaviour
{
    public GameObject trigger;
    public GameObject b;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider E)
    {
        if(E.gameObject.CompareTag("Player"))
        {
            b.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider E)
    {
        if (E.gameObject.CompareTag("Player"))
        {
            b.gameObject.SetActive(true);
        }
    }
}
