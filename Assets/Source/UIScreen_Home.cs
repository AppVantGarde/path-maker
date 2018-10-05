using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIScreen_Home : MonoBehaviour
{
    public SharedInt currentLevel;
    public Image foregroundImage;
    public SharedPersistentInt savedLevel;

    public void Start( )
    {
        foregroundImage.DOColor( new Color( 0, 0, 0, 0 ), 0.5f ).SetDelay( 0.15f );
    }

    public void TapToPlay( )
    {
        foregroundImage.DOColor( new Color( 0, 0, 0, 1 ), 0.5f ).OnComplete( ( ) =>
        {
            //int idx = SceneManager.GetActiveScene( ).buildIndex;

            //if(++idx >= SceneManager.sceneCountInBuildSettings)
            //    idx = 0;

            

            SceneManager.LoadScene( savedLevel.Value );
        } );
    }

    public void HowToPlay( )
    {
        foregroundImage.DOColor( new Color( 0, 0, 0, 1 ), 0.5f ).OnComplete( ( ) =>
        {
            //int idx = SceneManager.GetActiveScene( ).buildIndex;

            //if(++idx >= SceneManager.sceneCountInBuildSettings)
            //    idx = 0;

            SceneManager.LoadScene( "level_tutorial" );
        } );
    }
}
