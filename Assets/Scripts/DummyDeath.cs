using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDeath : MonoBehaviour
{
    [SerializeField]
    protected new Rigidbody rigidbody;
    [SerializeField]
    protected float power;
    [SerializeField]
    protected float spread;
    [SerializeField]
    protected Vector3 direction;
    [SerializeField]
    protected AudioCueScriptableObject sound;
    [SerializeField]
    protected AudioSource source;
    [SerializeField]
    protected float timeToDestroy;
    protected float nextDestroy;
    public bool usePhysics = true;

    public void Start()
    {
        if (usePhysics)
        {
            float power = this.power * Random.Range(0.75f, 1.25f);
            float angle = Mathf.Tan(Mathf.Deg2Rad * spread * 0.5f);
            direction = Random.insideUnitSphere * angle + direction;
            direction.Normalize();
            rigidbody.AddForce(direction * power, ForceMode.Impulse);
            rigidbody.AddTorque(Random.insideUnitSphere * power);
        }
        source.clip = sound;
        source.Play();
        nextDestroy = Time.time + timeToDestroy;
    }

    protected void LateUpdate()
    {
        if(!source.isPlaying && Time.time >= nextDestroy)
        {
            Destroy(gameObject);
        }
    }
}
