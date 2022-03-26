using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    protected bool close = false;
    [SerializeField]
    protected DummyDeath death;
    private void OnTriggerEnter(Collider other)
    {

        if (!close && ((1 << other.gameObject.layer) == (1 << 6)) && other.tag.Equals("Player"))
        {
            Instantiate(death, transform.position, Quaternion.identity);
            GameManager.score++;
            Destroy(gameObject);
            close = true;
        }
    }
}
