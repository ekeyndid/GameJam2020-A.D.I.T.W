using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateboi : MonoBehaviour
{
    public GameObject key;
    public float State;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case 0:
                key.gameObject.transform.Rotate(new Vector3(0.0f, 20.0f, 0.0f) * Time.deltaTime);
                
                break;
            case 1:
                key.gameObject.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f) * Time.deltaTime);
                key.gameObject.transform.position = Player.gameObject.transform.position + new Vector3(0.0f, 1.2f, 0.0f);
                break;

            case 2:
                key.SetActive(false);
                break;
        }
        
    }
}
