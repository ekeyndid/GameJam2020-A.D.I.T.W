using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiemcounterpart : MonoBehaviour
{
    public GameObject me;
    public GameObject otherme;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        me.gameObject.transform.position = otherme.gameObject.transform.position;
            
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
