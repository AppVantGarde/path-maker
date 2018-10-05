using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIButton_Back : MonoBehaviour
{
    public Image foregroundImage;

    public void GoToMainMenu( )
    {
        foregroundImage.DOColor( new Color( 0, 0, 0, 1 ), 0.35f ).OnComplete( ( ) =>
        {
            //int idx = SceneManager.GetActiveScene( ).buildIndex;

            //if(++idx >= SceneManager.sceneCountInBuildSettings)
            //    idx = 0;

            SceneManager.LoadScene( "main" );
        } );
    }
}
