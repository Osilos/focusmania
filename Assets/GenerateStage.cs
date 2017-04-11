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

	// Use this for initialization
	void Start () {
		Vector3 position = new Vector3(-(size.x/2), -(size.y/2), -5f);
		float sideSizeX = textureSize.x / size.x;
		float sideSizeY = textureSize.y / size.y;
		
		for (int x = 0; x < size.x; x++)
		{
			for (int y = 0; y < size.y; y++)
			{
				GameObject quad = CreateQuad(position);
				Mesh mesh = quad.GetComponent<MeshFilter>().mesh;
				List<Vector2> uvs = new List<Vector2>();

				

				uvs.Add(new Vector2(x * sideSizeX, y * sideSizeY));
				uvs.Add(new Vector2(x * sideSizeX + sideSizeX, y * sideSizeY));
				uvs.Add(new Vector2(x * sideSizeX + sideSizeX, y * sideSizeY + sideSizeY));
				uvs.Add(new Vector2(x * sideSizeX, y * sideSizeY + sideSizeY));
				mesh.uv = uvs.ToArray();
                mesh.RecalculateNormals();
                position.y += 1f;
			}
				
			position.y = -(size.y / 2);
			position.x += 1f;
		}
	}

	private GameObject CreateQuad (Vector3 position)
	{
		return Instantiate(prefabQuad, position, prefabQuad.transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
