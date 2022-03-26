using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dreamteck.Splines;

public class GameManager : MonoBehaviour
{
    public static int score;
    protected bool gameover;
    [SerializeField]
    protected DrawLineScript drawLine;
    [SerializeField]
    protected GroupBuilderScript group;
    [SerializeField]
    protected SimpleUI ui;
    public ParticleSystem victoryParticle;
    public AudioSource victorySource;
    protected float timeToRestart = 3.0f;
    protected float nextRestart;
    public void Win()
    {
        drawLine.enabled = false;
        group.enabled = false;
        group.Square();
        ui.WinTabletVisability(true);
        victoryParticle.Play(true);
        victorySource.Play();
        nextRestart = timeToRestart + Time.time;
        gameover = true;
    }
    public void GameOver()
    {
        drawLine.enabled = false;
        group.enabled = false;
        ui.OverTabletVisability(true);
        nextRestart = timeToRestart + Time.time;
        gameover = true;
    }
    public void StartGame()
    {
        ui.DrawTabletVisability(false);
        group.Follow = true;
    }
    public void Update()
    {
        if (gameover && Time.time >= nextRestart)
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
