using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    //public SharedInt playerScore;
    //public TMP_Text textValue;
    public int negativeValue;

    public bool moving;
    public Vector3 start, end;
    public float moveTime;

    private AudioCue _negativeAudioCue;

    public void Start( )
    {
        //textValue.GetComponent<Renderer>( ).sortingLayerName = "AboveDefault";

        //textValue.SetText( "-" + negativeValue );

        _negativeAudioCue = Resources.Load<AudioCue>( "Audio/Negative" );

        if(!moving)
            return;

        Sequence s = DOTween.Sequence( );
        s.AppendInterval( 0.5f );
        s.Append( transform.DOMove( end, moveTime ).SetEase( Ease.InOutCubic ) );
        s.AppendInterval( 0.5f );
        //s.Append( transform.DOMove( start, moveTime ).SetEase( Ease.InOutCubic ) );
        //s.AppendInterval( 0.5f );
        s.SetLoops( -1, LoopType.Yoyo );

        
    }

    public void OnTriggerEnter2D( Collider2D collision )
    {
        if(collision.gameObject.tag == "Player")
        {
            _negativeAudioCue.Play( );
            //playerScore.value -= negativeValue;
        }
    }

}
