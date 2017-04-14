using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageGenerator : com.flavienm.engine.EngineObject {

	[SerializeField]
	private GameObject prefabQuad;
	[SerializeField]
	private Vector2 buildingSize;
	[SerializeField]
	private Vector2 stageSize;
	[SerializeField]
	private Vector2 wallPartSize;

	[SerializeField]
	private float offSetToCreate;

	private Vector2 textureSideSize;

	private int lastStageGenerate;

	private List<GameObject> stages = new List<GameObject>();

	private List<GameObject> quads = new List<GameObject>();

	private float lastRowGenerate;
	[SerializeField]
	private float aupif;
	[SerializeField]
	private float offSetToDestroy;
    private bool shoudlReload;

    private void StartGenerate ()
	{
		textureSideSize = new Vector2(1f / (buildingSize.x * stageSize.x), 1f / (buildingSize.y * stageSize.y));
		for (int i = 0; i < buildingSize.y; i++)
		{
			GameObject stage = new GameObject();
			stage.name = "Stage : " + i;
			stage.transform.position = new Vector3(-(stageSize.x * wallPartSize.x / 2), (stageSize.y * wallPartSize.y) * -i, -5f);
			stages.Add(stage);
		}

		for (int i = 0; i < stageSize.x * stageSize.y * 4; i++)
		{
			InstantiateQuad(Vector3.zero, false);
		}

		Generate(new int[3] { 0, 1, 2 });
	}

	protected override void OnNewGame()
	{
        if (shoudlReload)
        {
            quads.ForEach(x => x.SetActive(false));
            StartGenerate();
        }
	}

	protected override void OnMenu()
	{
		base.OnMenu();
        
		StartGenerate();
        shoudlReload = false;
    }

	protected override void OnGameOver()
	{
        shoudlReload = true;
	}

	public void Generate (int[] stages)
	{
		for (int i = 0; i < stages.Length; i++)
		{
			int stage = stages[i];
			Vector3 position = GetPositionRow(i * stageSize.y);
				
			GenerateStage(stage);
			lastStageGenerate = stage;
		}
	}

	private Vector3 GetPositionRow (float row)
	{
		return new Vector3(-(stageSize.x * wallPartSize.x / 2), -row * wallPartSize.y + aupif, -5f);
	}

	private void Update ()
	{
		float currentRow = GetCurrentRow() + offSetToDestroy;
		if (lastRowGenerate < currentRow)
		{
			Vector3 position = GetPositionRow(currentRow);
			GenerateRow(position, currentRow);
		}
	}

	private float GetCurrentRow ()
	{
		return Mathf.Abs(-aupif*2 + Mathf.Ceil(transform.position.y / wallPartSize.y));
	}

	private void GenerateRow (Vector3 position, float row)
	{
		for (int x = 0; x < stageSize.x; x++)
		{
			GameObject quad = CreateQuad(position);

			Mesh mesh = quad.GetComponent<MeshFilter>().mesh;
			List<Vector2> uvs = GetUvsFor(x, row);

			mesh.uv = uvs.ToArray();
			mesh.RecalculateNormals();

			position.x += wallPartSize.x;
		}
		lastRowGenerate = row;
	}

	private void GenerateStage (int id)
	{
		for (int y = 0; y < stageSize.y; y++)
		{
			GenerateRow(GetPositionRow(id * stageSize.y + y), id * stageSize.y + y);
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
		GameObject quad = quads.Find(x => x.activeSelf == false);
		if (quad)
		{
			quad.SetActive(true);
			quad.transform.position = position;
			return quad;
		}
		return InstantiateQuad(position, true);
	}

	private GameObject InstantiateQuad (Vector3 position, bool active)
	{
		GameObject quad = Instantiate(prefabQuad, position, prefabQuad.transform.rotation);
		quads.Add(quad);
		quad.SetActive(active);
		return quad;
	}
}
