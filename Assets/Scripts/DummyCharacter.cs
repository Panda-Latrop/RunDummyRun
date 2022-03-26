using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCharacter : MonoBehaviour
{
    protected bool isFree = true;
    protected GroupBuilderScript group;
    protected bool hasTarget ;
    [SerializeField]
    protected DummyDeath death;
    protected Vector3 target;
    [SerializeField]
    protected float scale = 10.0f;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected AudioCueScriptableObject sound;
    [SerializeField]
    protected AudioSource source;
    public void SetIdle(bool idle)
    {
        animator.SetBool("Idle", idle);
    }
    public void SetGroup(GroupBuilderScript group)
    {
        this.group = group;
        isFree = false;
    }

    protected void LateUpdate()
    {
        if (!isFree && hasTarget)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * scale);
            if ((transform.localPosition - target).sqrMagnitude < 0.1f)
                enabled = false;
        }
    }
    public void MoveTo(Vector3 target)
    {
        this.target = target;
        hasTarget = true;
        enabled = true;
    }
    public void Kill()
    {
        group.MemberRemove(this);
        Instantiate(death, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) == (1 << 6)) && other.tag.Equals("Pawn") && !isFree)
        {
            var ch = other.GetComponent<DummyCharacter>();
            ch.SetIdle(false);
            group.MemberAdd(ch);
            source.clip = sound;
            source.Play();
        }
    }
}
