using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    Vector3 defaultScale;

    private void Awake()
    {
        defaultScale = transform.localScale;
    }

    private void Update()
    {
        // slowly set scale back to normal
        transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, Time.deltaTime * 10f);
    }

    public void Beat()
    {
        transform.localScale = defaultScale * 3;
    }
}
