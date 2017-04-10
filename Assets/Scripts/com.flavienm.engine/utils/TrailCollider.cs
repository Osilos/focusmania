using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace com.flavienm.superhex
{
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailCollider : MonoBehaviour
    {
        [SerializeField]
        private TrailRenderer trail;
        [SerializeField]
        private List<EdgeCollider2D> edges = new List<EdgeCollider2D>();
        [SerializeField]
        private int maxPointsInEdgeCollider;

        private List<List<Vector2>> pointLists = new List<List<Vector2>>();
        private int lastMaxIndex = 0;

        private void Awake ()
        {
            trail = GetComponent<TrailRenderer>();
            Reset();
        }

        public void Reset ()
        {
            trail.Clear();
            ResetEdge();
        }

        private void ResetEdge ()
        {
            foreach(EdgeCollider2D edge in edges)
            {
                Destroy(edge);
            }
            pointLists.Clear();
            AddNewPointList();
            lastMaxIndex = 0;
        }

        private void UpdateEdgePoints ()
        {
            Vector3[] positions = GetPositionsTrail();
            for (int i = lastMaxIndex; i < positions.Length; i++)
            {
                AddPosition(positions[i]);
                if (pointLists[pointLists.Count - 1].Count >= maxPointsInEdgeCollider)
                    AddNewPointList();
            }
            lastMaxIndex = positions.Length;
        }

        private void AddPosition(Vector3 position)
        {
            List<Vector2> list = pointLists[pointLists.Count - 1];
            if (list.Count < 2)
            {
                list.Add(position);
                return;
            }

            if (HasSameDirection(list[list.Count - 2], list[list.Count - 1], position))
                list[list.Count - 1] = position;
            else
                list.Add(position);
        }

        private bool HasSameDirection (Vector3 start, Vector3 endA, Vector3 endB)
        {
            return GetDirection(start, endA) == GetDirection(start, endB);
        }

        private Vector3 GetDirection (Vector3 start, Vector3 end)
        {
            return (start - end).normalized;
        }

        private void AddNewPointList ()
        {
            pointLists.Add(new List<Vector2>());
        }

        private Vector3[] GetPositionsTrail ()
        {
            Vector3[] positions = new Vector3[trail.positionCount];
            trail.GetPositions(positions);
            return positions;
        }

        private void SetEdgePoints(EdgeCollider2D edge, Vector2[] points)
        {
            edge.points = points;
        }

        private void AddNewEdgeCollider ()
        {
            EdgeCollider2D edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
            edgeCollider.isTrigger = true;
            edges.Add(edgeCollider);
        }

        private Vector2[] GetLocalArray (List<Vector2> list)
        {
            Vector2[] pointsEdge = new Vector2[list.Count];
            list.CopyTo(pointsEdge, 0);
            for (int i = 0; i <pointsEdge.Length; i++)
                pointsEdge[i] = transform.InverseTransformPoint(pointsEdge[i]);
            return pointsEdge;
        }

        private void Update()
        {
            UpdateEdgePoints();
            for (int i = 0; i < pointLists.Count; i++)
            {
                if (i >= edges.Count)
                    AddNewEdgeCollider();
                if (pointLists[i].Count > 1)
                    SetEdgePoints(edges[i], GetLocalArray(pointLists[i]));
            }
        }
    }
}