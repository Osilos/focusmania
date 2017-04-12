using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStage : MonoBehaviour {

	[SerializeField]
	private GameObject prefabQuad;
	[SerializeField]
	private Vector2 textureSize;
	[SerializeField]
	private Vector2 size;
	[SerializeField]
	private Texture texture;

	[SerializeField]
	private Vector2 wallPartSize;
	
	private void Start () {
		Vector3 position = new Vector3(-(size.x*wallPartSize.x/2), -(size.y*wallPartSize.y/2), -5f);
		float sideSizeX = 1f / size.x;
		float sideSizeY = 1f / size.y;
		
		for (int x = 0; x < size.x; x++)
		{
			for (int y = 0; y < size.y; y++)
			{
				GameObject quad = CreateQuad(position);
				
				Mesh mesh = quad.GetComponent<MeshFilter>().mesh;
				List<Vector2> uvs = new List<Vector2>();

				uvs.Add(new Vector2(x * sideSizeX, y * sideSizeY));
				uvs.Add(new Vector2(x * sideSizeX + sideSizeX, y * sideSizeY + sideSizeY));
				uvs.Add(new Vector2(x * sideSizeX + sideSizeX, y * sideSizeY));
				uvs.Add(new Vector2(x * sideSizeX, y * sideSizeY + sideSizeY));

				mesh.uv = uvs.ToArray();
				mesh.RecalculateNormals();

				position.y += wallPartSize.y;
			}
				
			position.y = -(size.y * wallPartSize.y / 2);
			position.x += wallPartSize.x;
		}
	}

	private GameObject CreateQuad (Vector3 position)
	{
		return Instantiate(prefabQuad, position, prefabQuad.transform.rotation);
	}
}
