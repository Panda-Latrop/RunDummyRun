using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class GroupBuilderScript : MonoBehaviour
{
    [SerializeField]
    protected new Transform transform;
    protected List<Vector3> positions = new List<Vector3>();
    [SerializeField]
    protected Vector3 scale;
    [SerializeField]
    protected DummyCharacter prefab;
    [SerializeField]
    protected int startParty = 10;
    [SerializeField]
    protected GameManager gameManager;
    
    protected List<DummyCharacter> characters = new List<DummyCharacter>();

    [SerializeField]
    protected SplineFollower splineFollower;

    public int Count => positions.Count;

    public bool Follow { get => splineFollower.follow; set => splineFollower.follow = value; }

    protected void Start()
    {
        for (int i = 0; i < startParty; i++)
        {
            MemberSpawn();
        }
        Square(true);
    }
    public void MemberSpawn()
    {
        DummyCharacter ch = Instantiate(prefab);
        MemberAdd(ch);
    }
    public void MemberAdd(DummyCharacter ch)
    {
        ch.transform.SetParent(transform, true);
        ch.SetGroup(this);
        ch.gameObject.tag = "Player";
        characters.Add(ch);
    }
    public void MemberRemove(DummyCharacter character)
    {
        characters.Remove(character);
        if(characters.Count <= 0)
        {
            gameManager.GameOver();
            splineFollower.follow = false;
        }
    }
    public void AddPosition(Vector3 point)
    {
        point = Vector3.Scale(point, scale);
        //point = point + transform.position;     
        positions.Add(point);
    }
    public void ClearPoisitions()
    {
        positions.Clear();
    }
    protected void CheckGroup()
    {
        for (int k = characters.Count - 1; k >= 0; k--)
        {
            if (characters[k] == null)
                characters.RemoveAt(k);
        }       
    }

    [ContextMenu("Regroup")]
    public void Regroup()
    {
        splineFollower.follow = true;
        int j = 0;
        int i = 0;

        //CheckGroup();


        if (characters.Count > 0)
        {

           

            if (characters.Count < positions.Count)
            {
                Debug.Log("pc: " + positions.Count + " cc: " + characters.Count + " mod: " + (positions.Count / characters.Count));

                int mod = Mathf.CeilToInt(positions.Count / characters.Count + 0.5f);
                if (mod <= 0)
                    mod = 1;

                for (; i < positions.Count && j < characters.Count; i++)
                {
                    //Debug.Log("i: " + i + " mod " + ((i + 1) % mod));
                    if ((i) % mod == 0)
                    {
                        characters[j].MoveTo(positions[i]);
                        characters[j].SetIdle(false);
                        j++;
                    }
                }
                i = 0;
            }

            while (j < characters.Count)
            {

                characters[j].MoveTo(positions[i]);
                characters[j].SetIdle(false);
                j++; i++;
                if (i >= positions.Count)
                    i = 0;
            }
        }
    }
    [ContextMenu("Square")]
    public void Square(bool force = false)
    {
       /// CheckGroup();

        int line = Mathf.CeilToInt(Mathf.Sqrt(characters.Count) + 0.5f);
        int j = 0, k = 0;
        float start, step;
        start = scale.x / 2.0f;
        step = scale.x / line;
        float offset = 0;
        if (line % 2 == 0)
            offset = start / 4.0f;
        for (int i = 0; i < characters.Count; i++)
        {
            Vector3 pos = new Vector3(-start + step * j + offset, 0, start - step * k - offset);
            if (force)
                characters[i].transform.localPosition = pos;
            else
                           characters[i].MoveTo(pos);

            characters[i].SetIdle(true);
            j++;
            if (j >= line)
            {
                k++;
                j = 0;
            }
            
            
        }
    }

    private void OnDrawGizmos()
    {
        if (positions.Count > 0)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(positions[0], Vector3.one * 0.25f);
            for (int i = 1; i < positions.Count; i++)
            {
                Gizmos.DrawLine(positions[i - 1], positions[i]);
                Gizmos.DrawWireCube(positions[i], Vector3.one * 0.25f);
            }
        }
    }
}
