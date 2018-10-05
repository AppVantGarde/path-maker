using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreDisplay : MonoBehaviour
{
    public SharedInt playerScore;
    public TMP_Text scoreText;
    public TMP_Text signText;

    private int _prevPlayerScore;

    private Vector3 _originPosition;
    private Vector3 _lerpFromPosition;

    public void Start( )
    {
        _originPosition = transform.localPosition;

        _lerpFromPosition = _originPosition;
        _lerpFromPosition.y -= 30;

        scoreText.SetText( "0" );
    }

    public void Update( )
    {
        if(_prevPlayerScore != playerScore.value)
        {
            scoreText.color = Color.white;

            scoreText.DOKill( );
            scoreText.transform.DOKill( );

            if(playerScore.value < 0)
            {
                signText.SetText( "-" );
                signText.color = Color.red;
            }
            else
            {
                signText.SetText( "+" );
                signText.color = Color.green;
            }

            scoreText.SetText( Mathf.Abs(playerScore.value).ToString( ) );

            //scoreText.color = Color.clear;
            scoreText.transform.localPosition = _lerpFromPosition;

            //scoreText.DOColor( Color.white, 0.25f );
            scoreText.transform.DOLocalMoveY( _originPosition.y, 0.25f );

            _prevPlayerScore = playerScore.value;
        }
    }
}
