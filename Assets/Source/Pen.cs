using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pen : MonoBehaviour
{
    public GameEvent drawingCompletedEvent;
    public GameEvent drawingOutOfBoundsEvent;

    public LineRenderer line;
    public LineRenderer path;
    public GameObject agent;

    public BoxCollider2D lineStartCollider;
    public BoxCollider2D lineEndCollider;

    public SpriteRenderer fingerSprite;

    private List<Vector3> _points = new List<Vector3>( );
    private List<Vector3> _path = new List<Vector3>( );

    private bool _isDrawing;
    private bool _lineComplete;

    private int _fingerID;

    private float _timeStartDrawing;
    private float _totalTimeToDrawLine;

    public void Start( )
    {
        line.enabled = true;
        line.gameObject.transform.position = new Vector3( 0, 0, 0 );

        Material[ ] materials = new Material[ ] { Resources.Load<Material>( "Materials/line" ) };

        line.materials = materials;//new Material( Shader.Find( "Mobile/Particles/Alpha Blended" ) );
        line.numCornerVertices = 2;
        line.numCapVertices = 8;
        line.positionCount = 0;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.startColor = Color.black;
        line.endColor = Color.black;
        line.sortingOrder = 2;
        line.positionCount = 0;
        line.useWorldSpace = true;

        line.SetPositions( new Vector3[ ] { new Vector3( 100, 0, 0 ), new Vector3( 200, 0, 0 ) } );
       // DOVirtual.DelayedCall( 0.1f, ( ) => { line.positionCount = 0; } );
    }

    public void FixedUpdate( )
    {
#if UNITY_EDITOR
        if(Input.GetMouseButton( 0 ))
        {
            if(!_lineComplete)
            {
                if(!_isDrawing)
                {

                    
                    Vector3 screenToWorld = Camera.main.ScreenToWorldPoint( Input.mousePosition );
                    screenToWorld.z = 0;

                    if(lineStartCollider.OverlapPoint( screenToWorld ))
                        TryToStartLine( );
                    else
                        return;
                }

                UpdateLine( Input.mousePosition );
            }
        }
        else
        {
            if(_isDrawing && !_lineComplete)
            {
                DOVirtual.DelayedCall( 0.1f, ( ) =>
                {
                    fingerSprite.DOColor( new Color( 1, 1, 1, 0 ), 0.25f );
                    line.positionCount = 0;
                    _points.Clear( );
                    drawingOutOfBoundsEvent.Trigger( );
                } );
            }

            if(_isDrawing && _lineComplete)
            {
                agent.transform.DOPath( _path.ToArray( ), 2f, PathType.Linear ).SetEase( Ease.Linear );
            }

            _isDrawing = false;
        }
#else

        //if(_lineComplete)
        //    return;

        Touch? touch = null;
        if(_fingerID > int.MinValue)
        {
            foreach(Touch value in Input.touches)
            {
                if(value.fingerId == _fingerID )
                {
                    touch = new Touch?( value );
                    break;
                }
            }

            if(!_lineComplete)
            {
                if(!_isDrawing)
                {
                    Vector3 screenToWorld = Camera.main.ScreenToWorldPoint( touch.Value.position );
                    screenToWorld.z = 0;

                    if(lineStartCollider.OverlapPoint( screenToWorld ))
                        TryToStartLine( );
                    else
                        return;
                }

                UpdateLine( touch.Value.position );
            }

            if(touch.Value.phase == TouchPhase.Ended || touch.Value.phase == TouchPhase.Canceled)
            {
                _fingerID = int.MinValue;
            }
        }
        else
        {
            if(_isDrawing && !_lineComplete)
            {
                DOVirtual.DelayedCall( 0.1f, ( ) =>
                {
                    line.positionCount = 0;
                    _points.Clear( );
                    drawingOutOfBoundsEvent.Trigger( );
                } );
            }

            if(_isDrawing && _lineComplete)
            {
                agent.transform.DOPath( _path.ToArray( ), 2f, PathType.Linear ).SetEase( Ease.Linear );
            }

            _isDrawing = false;
        }

        if(Input.touchCount > 0 && !_isDrawing )
        {
            foreach(Touch newTouch in Input.touches)
            {
                if(newTouch.phase != TouchPhase.Ended && newTouch.phase != TouchPhase.Canceled)
                {
                    _fingerID = newTouch.fingerId;
                    touch = new Touch?( newTouch );
                    break;
                }
            }
        }

        //if( !_isDrawing )
        //{
        //    foreach(Touch touch2 in Input.touches)
        //    {
        //        if(!(touch2.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Canceled))
        //        {
        //            if(!_lineComplete )
        //            {
        //                Vector3 screenToWorld = Camera.main.ScreenToWorldPoint( touch2.position );
        //                screenToWorld.z = 0;

        //                if(lineStartCollider.OverlapPoint( screenToWorld ))
        //                {
        //                    TryToStartLine( );
        //                }             
        //            }

        //            _fingerID = touch2.fingerId;
        //            touch = new Touch?( touch2 );
        //            break;
        //        }
        //    }
        //}

        //if(touch.HasValue)
        //{
        //    if(!_lineComplete)
        //    {
        //        if(!_isDrawing)
        //        {
        //            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint( touch.Value.position );
        //            screenToWorld.z = 0;

        //            if(lineStartCollider.OverlapPoint( screenToWorld ))
        //                TryToStartLine( );
        //            else
        //                return;
        //        }

        //        UpdateLine( Input.mousePosition );
        //    }
        //}
        //else
        //{
        //    if(_isDrawing && !_lineComplete)
        //    {
        //        DOVirtual.DelayedCall( 0.1f, ( ) =>
        //        {
        //            line.positionCount = 0;
        //            _points.Clear( );
        //            drawingOutOfBoundsEvent.Trigger( );
        //        } );
        //    }

        //    if(_isDrawing && _lineComplete)
        //    {
        //        agent.transform.DOPath( _path.ToArray( ), _totalTimeToDrawLine * 1.6f, PathType.Linear ).SetEase( Ease.Linear );
        //    }

        //    _fingerID = int.MinValue;
        //    _isDrawing = false;
        //}

        //else if(!_isDrawing)
        //{
        //    foreach(Touch touch2 in Input.touches)
        //    {
        //        if(!(touch2.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Canceled))
        //        {
        //            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint( touch2.position );
        //            screenToWorld.z = 0;

        //            if(lineStartCollider.OverlapPoint( screenToWorld ))
        //                TryToStartLine( );
        //            else
        //                return;

        //            _fingerID = touch2.fingerId;
        //            touch = new Touch?( touch2 );
        //            break;
        //        }
        //    }
        //}

        //if(touch == null)
        //{
        //    if(_isDrawing && _lineComplete)
        //    {
        //        agent.transform.DOPath( _path.ToArray( ), 2, PathType.Linear ).SetEase( Ease.Linear );
        //    }

        //    if(_isDrawing)
        //    {
        //        if(!_lineComplete)
        //        {
        //            DOVirtual.DelayedCall( 0.1f, ( ) =>
        //            {
        //                line.positionCount = 0;
        //                _points.Clear( );
        //                drawingOutOfBoundsEvent.Trigger( );
        //            } );
        //        }

        //        _fingerID = int.MinValue;
        //        _isDrawing = false;
        //    }
            
        //}
        //else if(touch.Value.phase == TouchPhase.Ended || touch.Value.phase == TouchPhase.Canceled)
        //{
        //    if(!_lineComplete)
        //    {
        //        DOVirtual.DelayedCall( 0.1f, ( ) =>
        //        {
        //            line.positionCount = 0;
        //            _points.Clear( );
        //            drawingOutOfBoundsEvent.Trigger( );
        //        } );
        //    }

        //    if(_isDrawing && _lineComplete)
        //    {
        //        agent.transform.DOPath( _path.ToArray( ), 2, PathType.Linear ).SetEase( Ease.Linear );
        //    }

        //    _fingerID = int.MinValue;
        //    _isDrawing = false;
        //}
        //else
        //{
        //    if(!_lineComplete)
        //        UpdateLine( touch.Value.position );
        //}
#endif
    }

    private void UpdateLine( Vector3 point )
    {
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint( point );
        screenToWorld.z = 0;

        Vector3 screenToView = Camera.main.ScreenToViewportPoint( point );
        float viewY = screenToView.y;
        screenToView.z = 0;

        //screenToView.x += 0.5f;
        screenToView.y += 0.4625f;

        if(viewY >= 0.5f)
        {
            _isDrawing = false;
            line.positionCount = 0;
            _points.Clear( );
            drawingOutOfBoundsEvent.Trigger( );
            return;
        }

        fingerSprite.transform.position = screenToWorld;

        if(!_points.Contains( screenToWorld ))
        {
            _points.Add( screenToWorld );

            Vector3 pathPoint = Camera.main.ViewportToWorldPoint( screenToView );
            pathPoint.z = 0;
            _path.Add( pathPoint );

            line.positionCount = _points.Count;
            line.SetPosition( _points.Count - 1, _points[_points.Count - 1] );

            if(lineEndCollider.OverlapPoint( screenToWorld ))
            {
                fingerSprite.DOColor( new Color( 1, 1, 1, 0 ), 0.25f );

                Vector3 finalPoint = lineEndCollider.transform.position;
                finalPoint.z = 0;
                _points.Add( finalPoint );
                line.positionCount = _points.Count;
                line.SetPosition( _points.Count - 1, _points[_points.Count - 1] );

                //_isDrawing = false;
                _lineComplete = true;

                _totalTimeToDrawLine = Time.time - _timeStartDrawing;
                drawingCompletedEvent.Trigger( );

                //path.positionCount = _path.Count;
                //path.SetPositions( _path.ToArray( ) );

                //agent.transform.DOPath( _path.ToArray( ), 2, PathType.Linear ).SetEase( Ease.Linear );
            }
        }
    }

    private void TryToStartLine( )
    {
        if(_isDrawing)
            return;

        fingerSprite.DOColor( new Color( 1, 1, 1, 1 ), 0.5f );

        _isDrawing = true;
        _points.Clear( );
        _path.Clear( );

        _timeStartDrawing = Time.time;

        //line = new GameObject( "line" ).AddComponent<LineRenderer>( );
        line.enabled = true;
        line.gameObject.transform.position = new Vector3( 0, 0, 0 );

        Material[ ] materials = new Material[ ] { Resources.Load<Material>( "Materials/line" ) };

        line.materials = materials;//new Material( Shader.Find( "Mobile/Particles/Alpha Blended" ) );
        line.numCornerVertices = 2;
        line.numCapVertices = 8;
        line.positionCount = 0;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.startColor = Color.black;
        line.endColor = Color.black;
        line.sortingOrder = 2;
        line.positionCount = 2;
        line.useWorldSpace = true;

        //Vector3[ ] points = new Vector3[ ] { Vector3.zero, Vector3.zero };
        //line.SetPositions( points );
        //line.positionCount = 0;
        //line.gameObject.SetActive( false );
        //line.gameObject.SetActive( true );



        //path.gameObject.transform.position = new Vector3( 0, 0, 0 );
        ////line.material = new Material( Shader.Find( "Mobile/Particles/Alpha Blended" ) );
        //path.numCornerVertices = 2;
        //path.numCapVertices = 8;
        //path.positionCount = 0;
        //path.startWidth = 0.05f;
        //path.endWidth = 0.05f;
        //path.startColor = Color.black;
        //path.endColor = Color.black;
        //path.sortingOrder = 4;
        //path.useWorldSpace = false;
    }
}
