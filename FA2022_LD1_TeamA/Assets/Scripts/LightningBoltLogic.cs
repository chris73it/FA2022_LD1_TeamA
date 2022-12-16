using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public float Timer = 0f;
    public float Duration;

    // Update is called once per frame
    void Update()
    {
        if (Timer < Duration)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
