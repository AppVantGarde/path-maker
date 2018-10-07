using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Perfect : MonoBehaviour
{
    public AudioCue touchedSound;
    public Collider2D trigger;
    public SpriteRenderer centerSpriteRenderer;
    public SpriteRenderer ringSpriteRenderer;
    public Color collectedColor;

    public void OnTriggerEnter2D( Collider2D collision )
    {
        if(collision.gameObject.tag == "Player")
        {
            trigger.enabled = false;
            touchedSound.Play( );
            ringSpriteRenderer.transform.DOScale( 2f, 0.3f ).SetEase( Ease.OutBack, 3 );
            ringSpriteRenderer.DOColor( collectedColor, 0.5f );
            centerSpriteRenderer.DOColor( new Color( collectedColor.r, collectedColor.g, collectedColor.b, 0.5f), 0.5f );
        }
    }
}
