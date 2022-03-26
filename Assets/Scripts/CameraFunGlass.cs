using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunGlass : MonoBehaviour
{
    [SerializeField]
    protected AudioSource source;
    private void OnCollisionEnter(Collision collision)
    {
        source.Play();
    }
}
