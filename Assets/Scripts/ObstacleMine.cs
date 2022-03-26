using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMine : MonoBehaviour
{
    [SerializeField]
    protected DummyDeath effect;
    public float radius = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) == (1 << 6)) && other.tag.Equals("Player"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius,(1<<6),QueryTriggerInteraction.Collide);
            for (int i = 0; i < colliders.Length; i++)
            {
                //Debug.Log(colliders[i].name);
                if(colliders[i].tag.Equals("Player"))
                colliders[i].GetComponent<DummyCharacter>().Kill();
            }
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
