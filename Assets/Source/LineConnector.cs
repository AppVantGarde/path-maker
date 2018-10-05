using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LineConnector : MonoBehaviour
{
    public GameEvent lineCompletedEvent;

    [Space(10)]
    public Image connectorImage;

    public void OnEnable( )
    {
        lineCompletedEvent.Subscribe( GameEvent_LineCompleted );
    }

    public void OnDisable( )
    {
        lineCompletedEvent.UnSubscribe( GameEvent_LineCompleted );
    }

    private void GameEvent_LineCompleted( )
    { 
        Sequence sequence = DOTween.Sequence( );
        sequence.Append( connectorImage.DOColor( new Color( 1, 1, 1, 1 ), 0.06f ) );//DOTween.To( ( ) => coverImage.alpha, x => coverImage.alpha = x, 0.75f, 0.06f ) );
        sequence.Append( connectorImage.DOColor( new Color( 0.4f, 0.4f, 0.4f, 1 ), 0.06f ) );//DOTween.To( ( ) => coverImage.alpha, x => coverImage.alpha = x, 0.4f, 0.06f ) );
        sequence.Append( connectorImage.DOColor( new Color( 1, 1, 1, 1 ), 0.06f ) );//DOTween.To( ( ) => coverImage.alpha, x => coverImage.alpha = x, 0.75f, 0.06f ) );
        sequence.Append( connectorImage.DOColor( new Color( 0, 0, 0, 1 ), 0.75f ) );
        sequence.Play( );
    }
}
