using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpike : MonoBehaviour
{
    protected bool active;
    public Transform spike;
    public Vector3 lowPoint, hightPoint;
    public float timeToActive = 2.0f;
    protected float nextActive;
    public float speed = 10;
    protected void Update()
    {
        if(Time.time >= nextActive)
        {
            active = !active;
            nextActive = Time.time + timeToActive;
        }
        if (active)
        {
            spike.localPosition = Vector3.Lerp(spike.localPosition, hightPoint, Time.deltaTime* speed);
        }
        else
        {
            spike.localPosition = Vector3.Lerp(spike.localPosition, lowPoint, Time.deltaTime * speed);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (active && ((1 << other.gameObject.layer) == (1 << 6)) && other.tag.Equals("Player"))
        {
            var ch = other.GetComponent<DummyCharacter>();
            ch.Kill();
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + lowPoint, Vector3.one * 0.25f);
        Gizmos.DrawWireCube(transform.position + hightPoint, Vector3.one * 0.25f);
    }
}
