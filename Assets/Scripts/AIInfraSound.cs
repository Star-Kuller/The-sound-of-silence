using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInfraSound : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private bool circularRoute;
    [SerializeField] private bool stopAtEnd;
    [SerializeField] private ParticleSystem playerDetectedEmitter;
    [SerializeField] private int playerDetectedEmitCount = 150;
    private Collider2D _playerCollider;
    private Transform _playerTransform;
    private List<Transform> _playerWay = new List<Transform>();
    private State _state = State.Idle;
    
    private bool isChasing = false;

    private enum State
    {
        Idle,
        PlayerDetected,
        Aggressive,
        Wait,
        Return,
    }
    
    private Rigidbody2D _rb;
    private int _currentWaypointIndex = 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        for (var i = 0; i < waypoints.Count; i++)
        {
            var waypoint = waypoints[i];
            
            var nextWaypoint = waypoints[(i + 1) % waypoints.Count];

            if((stopAtEnd || !circularRoute) && (i + 1) % waypoints.Count == 0)
                return;
            if (waypoint != null && nextWaypoint != null)
            {
                Gizmos.DrawLine(waypoint.position, nextWaypoint.position);
            }
        }
    }
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        var player = GameObject.FindWithTag("Player");
        _playerCollider = player.GetComponent<Collider2D>();
        _playerTransform = player.GetComponent<Transform>();
    }
    
    private void FixedUpdate()
    {
        switch (_state)
        {
            case State.Idle:
                Idle();
                break;
            case State.PlayerDetected:
                StartCoroutine(Wait5Sec());
                break;
            case State.Aggressive:
                _rb.MovePosition(Vector3.MoveTowards(
                    _rb.position,
                    _playerTransform.position,
                    speed));
                break;
            case State.Wait:
                break;
            case State.Return:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private IEnumerator Wait5Sec()
    {
        playerDetectedEmitter.Emit(playerDetectedEmitCount);
        yield return new WaitForSeconds(5);
        _state = State.Aggressive;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    private void Idle()
    {
        if (isChasing) _state = State.PlayerDetected;
        
        if (waypoints.Count <= 0) return;
        _rb.MovePosition(Vector3.MoveTowards(
            _rb.position,
            waypoints[_currentWaypointIndex].position,
            speed));
        
        if (_currentWaypointIndex+1 == waypoints.Count && stopAtEnd) return;
        
        if (!(Vector3.Distance(transform.position, waypoints[_currentWaypointIndex].position) < 0.1f)) return;
        _currentWaypointIndex++;
        if (!circularRoute && _currentWaypointIndex == waypoints.Count)
            waypoints.Reverse();
        
        if (_currentWaypointIndex == waypoints.Count)
            _currentWaypointIndex = 0;
    }
}
