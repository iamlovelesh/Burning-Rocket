using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 movement=new Vector3(10f,10f,10f);
    [SerializeField] float  period=2f;
    [Range(0, 1)] [SerializeField] float percentage;
    Vector3 StartPos;
    void Start() 
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycle = Time.time / period;
        const float Tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycle * Tau);
        percentage= rawSinWave / 2f + 0.5f;
        Vector3 offset = movement * percentage;
        transform.position = StartPos + offset;
    }
}
