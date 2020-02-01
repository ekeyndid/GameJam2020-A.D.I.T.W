using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockageS : MonoBehaviour
{

    public GameObject Block;
    public Vector3 StartP;
    public Vector3 End;
    public float SpeedMin;
    public float SpeedMax;
    // Start is called before the first frame update
    void Start()
    {
        StartP = Block.transform.position;
        //StartCoroutine(MoveBlock());
    }

    // Update is called once per frame



    public static float EaseOutQuad(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }

    private void OnCollisionStay(Collision boi)
    {
        if (boi.gameObject.CompareTag("Player"))
        {
            if (boi.gameObject.GetComponent<PlayerController>().Key1)
            {
                StartCoroutine(MoveBlock());
            }
            
        }
    }
        

    


    IEnumerator MoveBlock()
    {
        
            
            float sped = Random.Range(SpeedMin, SpeedMax) / 10;
            for (float i = 0; i < 1; i += sped)
            {
                yield return new WaitForSecondsRealtime(.01f);
                Block.transform.position = new Vector3(EaseOutQuad(StartP.x, End.x, i), EaseOutQuad(StartP.y, End.y, i), EaseOutQuad(StartP.z, End.z, i));
            }
           
        

    }
}
