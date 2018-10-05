using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DrawingPanelFeedback : MonoBehaviour
{
    public GameEvent drawingCompletedEvent;
    public GameEvent drawingOutOfBoundsEvent;

    public Image panelImage;

    public void OnEnable( )
    {
        drawingCompletedEvent.Subscribe( GameEvent_DrawingCompleted );
        drawingOutOfBoundsEvent.Subscribe( GameEvent_DrawingOutOfBounds );
    }

    public void OnDisable( )
    {
        drawingCompletedEvent.UnSubscribe( GameEvent_DrawingCompleted );
        drawingOutOfBoundsEvent.UnSubscribe( GameEvent_DrawingOutOfBounds );
    }

    private void GameEvent_DrawingCompleted( )
    {
        //panelImage.DOColor( new Color( 0, 1, 0, 0.5f ), 0.1f ).SetEase( Ease.InOutCubic );
        //panelImage.DOColor( new Color( 1, 1, 1, 0.6f ), 0.15f ).SetEase( Ease.InOutCubic ).SetDelay( 0.11f );
    }

    private void GameEvent_DrawingOutOfBounds( )
    {
        panelImage.DOColor( new Color( 1, 0, 0, 0.5f ), 0.1f ).SetEase( Ease.InOutCubic );
        panelImage.DOColor( new Color( 1, 1, 1, 0.6f ), 0.15f ).SetEase( Ease.InOutCubic ).SetDelay( 0.11f );
    }
}
