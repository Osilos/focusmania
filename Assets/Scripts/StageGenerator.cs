using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour {

	[SerializeField]
	private GameObject prefabQuad;
	[SerializeField]
	private Vector2 buildingSize;
	[SerializeField]
	private Vector2 stageSize;
	[SerializeField]
	private Vector2 wallPartSize;

	private Vector2 textureSideSize;

	private void Start ()
	{
		textureSideSize = new Vector2(1f / (buildingSize.x * stageSize.x), 1f / (buildingSize.y * stageSize.y));
		Generate(new int[3] { 0, 1, 2 });
	}

	public void Generate (int[] stages)
	{
		for (int i = 0; i < stages.Length; i++)
		{
			int stage = stages[i];
			Vector3 position = 
				new Vector3(-(stageSize.x * wallPartSize.x / 2), (stageSize.y * wallPartSize.y) * -stage, -5f);
			GenerateStage(stage, position);
		}
	}

	private void GenerateStage (int id, Vector3 position)
	{
		float yStart = position.y;
		GameObject parent = new GameObject();
		parent.name = "Stage : " + id;
		for (int x = 0; x < stageSize.x; x++)
		{
			for (int y = 0; y < stageSize.y; y++)
			{
				GameObject quad = CreateQuad(position);
				quad.transform.SetParent(parent.transform);
				Mesh mesh = quad.GetComponent<MeshFilter>().mesh;
				List<Vector2> uvs = GetUvsFor(x, (buildingSize.y * stageSize.y) - id * stageSize.y - y);

				mesh.uv = uvs.ToArray();
				mesh.RecalculateNormals();

				position.y += wallPartSize.y;
			}

			position.y = yStart;
			position.x += wallPartSize.x;
		}
	}

	private List<Vector2> GetUvsFor (float x, float y)
	{
		List<Vector2> uvs = new List<Vector2>();
		uvs.Add(new Vector2(x * textureSideSize.x, y * textureSideSize.y));
		uvs.Add(new Vector2(x * textureSideSize.x + textureSideSize.x, y * textureSideSize.y - textureSideSize.y));
		uvs.Add(new Vector2(x * textureSideSize.x + textureSideSize.x, y * textureSideSize.y));
		uvs.Add(new Vector2(x * textureSideSize.x, y * textureSideSize.y - textureSideSize.y));
		return uvs;
	}

	private GameObject CreateQuad (Vector3 position)
	{
		return Instantiate(prefabQuad, position, prefabQuad.transform.rotation);
	}
}
