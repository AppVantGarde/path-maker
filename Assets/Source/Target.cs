using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Target : MonoBehaviour
{
    public SharedInt playerScore;
    public GameEvent levelCompletedEvent;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer shadowSprite;
    public RectTransform lineEndRect;

    public ParticleSystem confetti;

    public void Start( )
    {
        Vector3 worldToViewPort = Camera.main.WorldToViewportPoint( transform.position );
        worldToViewPort.y -= 0.4625f;

        lineEndRect.anchorMin = new Vector2( worldToViewPort.x, worldToViewPort.y );
        lineEndRect.anchorMax = new Vector2( worldToViewPort.x, worldToViewPort.y );
        lineEndRect.anchoredPosition = Vector2.zero;


        lineEndRect.DOAnchorPos( Vector2.zero, 0.25f );
    }

    public void OnTriggerEnter2D( Collider2D collision )
    {
        if(collision.gameObject.tag == "Player")
        {
            //if(playerScore.value >= 0)
            //    confetti.Play( );
            //transform.DOScale( transform.localScale * 1 * 2f, 0.25f );
            //Color color = spriteRenderer.color;
            //color.a = 0;
            //spriteRenderer.DOColor( color, 0.2f );

            //levelCompletedEvent.Trigger( );

            //DOVirtual.DelayedCall( 1.0f, ( ) =>
            //{
            //    int idx = SceneManager.GetActiveScene( ).buildIndex;

            //    if(++idx >= SceneManager.sceneCountInBuildSettings )
            //        idx = 0;

            //    SceneManager.LoadScene( idx );
            //} );
        }
    }
}
