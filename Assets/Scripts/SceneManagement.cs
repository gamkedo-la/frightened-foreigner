﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    static public FMOD.Studio.EventInstance TitleScreenMusic;
    static public FMOD.Studio.PLAYBACK_STATE TitleScreenMusicPlaybackState;

    public GameObject SceneManagerFromHierarchy;

    private void Awake()
    {

        TitleScreenMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/titleScreenMusicV1");
    }

    // Start is called before the first frame update
    void Start()
    {
        //TitleScreenMusic.getPlaybackState(out TitleScreenMusicPlaybackState);
        //Debug.Log(TitleScreenMusicPlaybackState);
        //if (TitleScreenMusicPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        //{
            TitleScreenMusic.start();
        //}   
    }

    // Update is called once per frame
    void Update()
    {
        //TitleScreenMusic.getPlaybackState(out TitleScreenMusicPlaybackState);
       // Debug.Log(TitleScreenMusicPlaybackState);
    }

    public void LoadCemeteryLevelFromIntroCutscene()
    {
        SceneManager.LoadScene("Cemetery Level");
        TitleScreenMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        DontDestroyOnLoad(transform.gameObject);
    }

    public void LoadIntroCutScene()
    {
        SceneManager.LoadScene("Intro CutScene");
        DontDestroyOnLoad(transform.gameObject);
    }
}
