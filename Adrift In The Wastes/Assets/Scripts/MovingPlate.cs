using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlate : MonoBehaviour
{
    public GameObject Block;
    public Vector3 StartP;
    public Vector3 End;
    public float SpeedMin;
    public float SpeedMax;
    public float WaitMin;
    public float WaitMax;
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
    void OnEnable()
    {
        
        StartCoroutine(MoveBlock());
        
    }


    IEnumerator MoveBlock()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(WaitMin, WaitMax));
            float sped = Random.Range(SpeedMin, SpeedMax) / 10;
            for (float i = 0; i < 1; i += sped)
            {
                yield return new WaitForSecondsRealtime(.01f);
                Block.transform.position = new Vector3(EaseOutQuad(StartP.x, End.x, i), EaseOutQuad(StartP.y, End.y, i), EaseOutQuad(StartP.z, End.z, i));
            }
            yield return new WaitForSecondsRealtime(Random.Range(WaitMin, WaitMax));
            for (float i = 0; i < 1; i += sped)
            {
                yield return new WaitForSecondsRealtime(.01f);
                Block.transform.position = new Vector3(EaseOutQuad(End.x,StartP.x, i), EaseOutQuad(End.y, StartP.y, i), EaseOutQuad(End.z, StartP.z, i));
            }
        }

}
}
