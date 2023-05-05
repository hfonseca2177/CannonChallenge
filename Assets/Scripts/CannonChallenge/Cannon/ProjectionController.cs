using System.Collections.Generic;
using UnityEngine;

namespace CannonChallenge.Cannon
{
    /// <summary>
    /// Simulate cannon ball trajectory
    /// </summary>
    public class ProjectionController: MonoBehaviour
    {
        [Tooltip("Cannon data reference")]
        [SerializeField] private CannonDataReference _cannonDataReference;
        [Tooltip("Number of points in the line renderer")]
        [SerializeField] private int _numPoints = 50;
        [Tooltip("Time interval between points")]
        [SerializeField] private float _pointsIntervalTime = 0.1f;
        [Tooltip("Layer mask to check target position ")] 
        [SerializeField] private LayerMask _targetMask;
        [Tooltip("Shot position")]
        public Transform _shotSpot;
        
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            DrawnLine();
        }

        /// <summary>
        /// Applying the equation of trajectory derivation formula: y = y(0) + V(0)t - 1/2 * g*t^2
        /// where:
        ///    y = Y (up) Direction
        ///    y(0) = initial position
        ///    V(0) = initial velocity
        ///    t = time
        ///    g = gravity
        ///    
        /// </summary>
        private void DrawnLine()
        {
            _lineRenderer.positionCount = _numPoints;
            List<Vector3> segments = new List<Vector3>();
            Vector3 startingPosition = _shotSpot.position;
            Vector3 startingVelocity = _shotSpot.up * _cannonDataReference.ShotSpeed.CurrentValue;
            for (float t = 0; t < _numPoints; t += _pointsIntervalTime)
            {
                Vector3 newPoint = startingPosition + t * startingVelocity;
                newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
                segments.Add(newPoint);
                Collider[] results = new Collider[1];
                int size = Physics.OverlapSphereNonAlloc(newPoint,2, results, _targetMask);
                if (size > 0)
                {
                    _lineRenderer.positionCount = segments.Count;
                    break;
                }
            }
            _lineRenderer.SetPositions(segments.ToArray());
        }
    }
}