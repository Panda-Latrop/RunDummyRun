using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSaw : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) == (1 << 6)) && other.tag.Equals("Player"))
        {
            var ch = other.GetComponent<DummyCharacter>();
            ch.Kill();
        }
    }

}
