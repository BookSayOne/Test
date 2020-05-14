using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPath : MonoBehaviour
{
    public List<Transform> paths = new List<Transform>();

    [HideInInspector]
    public bool IsCurved;

    [HideInInspector]
    public Color GizmosColor = Color.white;

    [HideInInspector]
    public float GizmosRadius = 10.0f;

    public void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        for (var i = 0; i < paths.Count; i++)
        {
            Gizmos.DrawSphere(paths[i].transform.position, GizmosRadius);
            if (i < paths.Count - 1)
                DrawPart(i);
        }
    }

    private void DrawPart(int ind)
    {
        if (IsCurved)
        {
            var devidedPoints = GetDivededPoints(ind);
            for (var i = 0; i < devidedPoints.Length - 1; i++)
                Gizmos.DrawLine(devidedPoints[i], devidedPoints[i + 1]);
        }
        else
        {
            Gizmos.DrawLine(paths[ind].position, paths[(ind + 1) % paths.Count].position);
        }
    }

    private Vector2[] GetDivededPoints(int ind)
    {
        var points = new Vector2[11];
        var pointInd = 0;
        var indexes = GetSplinePointIndexes(ind, true);
        Vector2 a = paths[indexes[0]].transform.position;
        Vector2 b = paths[indexes[1]].transform.position;
        Vector2 c = paths[indexes[2]].transform.position;
        Vector2 d = paths[indexes[3]].transform.position;
        for (float t = 0; t <= 1.001f; t += 0.1f)
            points[pointInd++] = SplineCurve.GetPoint(a, b, c, d, t);
        return points;
    }

    public int[] GetSplinePointIndexes(int baseInd, bool isForwardDirection)
    {
        var dInd = isForwardDirection ? 1 : -1;
        return new[]
        {
                Mathf.Clamp(baseInd - dInd, 0, paths.Count - 1),
                baseInd,
                Mathf.Clamp(baseInd + dInd, 0, paths.Count - 1),
                Mathf.Clamp(baseInd + 2*dInd, 0, paths.Count - 1)
            };
    }
}
