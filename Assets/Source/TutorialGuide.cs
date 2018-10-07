using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialGuide : MonoBehaviour
{
    public GameEvent lineCompletedEvent;

    public Image handImage;
    public Sprite fingerUpSprite;
    public Sprite fingerDownSprite;

    public RectTransform startConnector;
    public RectTransform endConnector;

    public void OnEnable( )
    {
        lineCompletedEvent.Subscribe( GameEvent_LineCompleted );
    }

    public void OnDisable( )
    {
        lineCompletedEvent.UnSubscribe( GameEvent_LineCompleted );
    }

    public void GameEvent_LineCompleted( )
    {
        gameObject.SetActive( false );
        transform.DOKill( );
    }

    public void Start( )
    {
        DOVirtual.DelayedCall( 0.5f, ( ) =>
        {
            Sequence s = DOTween.Sequence( );
            s.AppendCallback( ( ) =>
            {
                handImage.color = Color.clear;
                handImage.sprite = fingerUpSprite;
                handImage.transform.position = startConnector.position;
            } );
            s.AppendInterval( 0.25f );
            s.Append( handImage.DOColor( Color.white, 0.25f ) );
            s.AppendInterval( 0.25f );
            s.AppendCallback( ( ) =>
            {
                handImage.sprite = fingerDownSprite;
                
            } );
            s.Append( handImage.transform.DOMove( endConnector.position, 1.5f ).SetEase( Ease.InOutCubic ) );
            //s.AppendInterval( 0.75f );
            s.AppendCallback( ( ) =>
            {
                handImage.sprite = fingerUpSprite;
            } );
            s.AppendInterval( 0.25f );
            s.Append( handImage.DOColor( Color.clear, 0.25f ) );
            s.SetLoops( -1, LoopType.Restart );
            s.Play( );
        } );
    }
}
