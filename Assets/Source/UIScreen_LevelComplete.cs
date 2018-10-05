using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIScreen_LevelComplete : MonoBehaviour
{
    public SharedInt playerScore;
    public GameEvent levelCompletedEvent;
    public GameObject screenObject;
    public Image backgroundImage;
    public Image foregroundImage;

    public GameObject levelCompletedObject;
    public GameObject replayLevelObject;

    private SharedPersistentInt _savedLevel;

    public void Awake( )
    {
        _savedLevel = Resources.Load<SharedPersistentInt>( "Shared/SavedLevel" );
    }

    public void OnEnable( )
    {
        foregroundImage.DOColor( new Color( 0, 0, 0, 0 ), 0.25f ).SetDelay( 0.1f ).OnComplete( ( ) => { foregroundImage.color = Color.clear; } );
        levelCompletedEvent.Subscribe( GameEvent_LevelCompleted );
    }

    public void OnDisable( )
    {
        levelCompletedEvent.UnSubscribe( GameEvent_LevelCompleted );
    }

    private void GameEvent_LevelCompleted( )
    {
        backgroundImage.raycastTarget = true;
        backgroundImage.DOColor( new Color( 0, 0, 0, 0.4f ), 0.5f );
        //screenObject.SetActive( true );

        if(playerScore.value == 3)
            levelCompletedObject.SetActive( true );
        else
            replayLevelObject.SetActive( true );
    }

    public void GoToNextLevel( )
    {
        foregroundImage.DOColor( new Color( 0, 0, 0, 1 ), 0.25f ).OnComplete( ( ) =>
        {
            int idx = SceneManager.GetActiveScene( ).buildIndex;

            if(++idx >= SceneManager.sceneCountInBuildSettings)
                idx = Random.Range( 2, SceneManager.sceneCountInBuildSettings );

            _savedLevel.Value = idx;
            SaveGame.Instance.Save( );

            SceneManager.LoadScene( idx );
        } );
    }

    public void ReplayLevel( )
    {
        foregroundImage.DOColor( new Color( 0, 0, 0, 1 ), 0.25f ).OnComplete( ( ) =>
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene( ).buildIndex );
        } );
    }
}
