using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleUI : MonoBehaviour
{
    [SerializeField]
    protected Image drawTablet,winTablet, overTablet,drawZone;
    [SerializeField]
    protected Text score;
    public void DrawTabletVisability(bool visible)
    {
        drawTablet.enabled = visible;
        drawZone.enabled = !visible;
    }
    public void WinTabletVisability(bool visible)
    {
        winTablet.enabled = visible;
        drawZone.enabled = !visible;
    }
    public void OverTabletVisability(bool visible)
    {
        overTablet.enabled = visible;
        drawZone.enabled = !visible;
    }

    protected void LateUpdate()
    {
        score.text = GameManager.score.ToString();
    }
}
