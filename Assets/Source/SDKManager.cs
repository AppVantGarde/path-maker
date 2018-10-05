using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Facebook.Unity;
//using GameAnalyticsSDK;

public class SDKManager : MonoBehaviour
{
    public void Awake( )
    {
        //
        //GameAnalytics.Initialize( );

        ////
        //FB.Init( this.OnInitComplete, this.OnHideUnity );

        //DontDestroyOnLoad( gameObject );
    }

    #region Facebook Callbacks

    //private void OnInitComplete( )
    //{
    //    string logMessage = string.Format( "OnInitCompleteCalled IsLoggedIn='{0}' IsInitialized='{1}'", FB.IsLoggedIn, FB.IsInitialized );
    //    Debug.Log( logMessage );
    //    if(AccessToken.CurrentAccessToken != null)
    //    {
    //        Debug.Log( AccessToken.CurrentAccessToken.ToString( ) );
    //    }
    //}

    //private void OnHideUnity( bool isGameShown )
    //{
    //    Debug.Log( "[Facebook] Is game shown: " + isGameShown );
    //}

    #endregion
}
