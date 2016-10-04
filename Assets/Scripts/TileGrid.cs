using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TileGrid : MonoBehaviour
{
	[SerializeField]
	private float _cellSize = 1f;

    // extents of the grid
    public int minX = -15; 
    public int minY = -15; 
    public int maxX = 15; 
    public int maxY = 15; 

    // choose a colour for the gizmos
    public int gizmoMajorLines = 5; 
    public Color gizmoLineColor = new Color (0.4f, 0.4f, 0.3f, 1f);

	#if UNITY_EDITOR
	void Update()
	{
		if (!Application.isPlaying && _cellSize != 0f)
		{
			int childCount = transform.childCount;

			for (int i = 0; i < childCount; i++)
			{
				Transform child = transform.GetChild(i);
				Vector3 childPosition = child.localPosition;
				float x = Mathf.Round(childPosition.x / _cellSize) * _cellSize;
				float y = Mathf.Round(childPosition.y / _cellSize) * _cellSize;
				float z = childPosition.z;
				child.localPosition = new Vector3(x, y, z);
			}
		}
	}
	#endif

    void OnDrawGizmos ()
    {
        // orient to the gameobject, so you can rotate the grid independently if desired
        Gizmos.matrix = transform.localToWorldMatrix;

        // set colours
        Color dimColor = new Color(gizmoLineColor.r, gizmoLineColor.g, gizmoLineColor.b, 0.25f* gizmoLineColor.a);
        Color brightColor = Color.Lerp (Color.white, gizmoLineColor, 0.75f);

        Vector3 gridOffset = new Vector3(_cellSize / 2f, _cellSize / 2f, 0f);

        // draw the horizontal lines
        for (int x = minX; x < maxX+1; x++)
        {
            // find major lines
            Gizmos.color = (x % gizmoMajorLines == 0 ? gizmoLineColor : dimColor);
            if (x == 0)
                Gizmos.color = brightColor;

            Vector3 pos1 = new Vector3(x, minY, 0) * _cellSize;  
            Vector3 pos2 = new Vector3(x, maxY, 0) * _cellSize;  

            Gizmos.DrawLine ((gridOffset + pos1), (gridOffset + pos2)); 
        }

        // draw the vertical lines
        for (int y = minY; y < maxY+1; y++)
        {
            // find major lines
            Gizmos.color = (y % gizmoMajorLines == 0 ? gizmoLineColor : dimColor);
            if (y == 0)
                Gizmos.color = brightColor;

            Vector3 pos1 = new Vector3(minX, y, 0) * _cellSize;
            Vector3 pos2 = new Vector3(maxX, y, 0) * _cellSize;

            Gizmos.DrawLine ((gridOffset + pos1), (gridOffset + pos2));
        }
    }

}
