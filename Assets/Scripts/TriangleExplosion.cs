using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.flavienm.engine;

public class TriangleExplosion : MonoBehaviour
{
	Collider lCollider;
	MeshFilter lMeshFilter;
	MeshRenderer lMeshRenderer;
	Transform lTrianglesPool;
	FollowEye Player;

	private void Start()
	{
		lCollider = GetComponent<Collider>();
		lMeshFilter = GetComponent<MeshFilter>();
		lMeshRenderer = GetComponent<MeshRenderer>();
		lTrianglesPool = GameObject.Find("TrianglesPool").transform;
		Player = GameObject.Find("Player").GetComponent<FollowEye>();
	}

	public IEnumerator SplitMesh(bool destroy)
	{

		//if(GetComponent<MeshFilter>() == null || GetComponent<SkinnedMeshRenderer>() == null)
		//{
		//	yield return null;
		//}

		if(lCollider)
		{
			lCollider.enabled = false;
		}

		Mesh M = new Mesh();
		if(lMeshFilter)
		{
			M = lMeshFilter.mesh;
		}
		else if(GetComponent<SkinnedMeshRenderer>())
		{
			M = GetComponent<SkinnedMeshRenderer>().sharedMesh;
		}

		Material[] materials = new Material[0];
		if(lMeshRenderer)
		{
			materials = lMeshRenderer.materials;
		}
		else if(GetComponent<SkinnedMeshRenderer>())
		{
			materials = GetComponent<SkinnedMeshRenderer>().materials;
		}

		Vector3[] verts = M.vertices;
		Vector3[] normals = M.normals;
		Vector2[] uvs = M.uv;
		for(int submesh = 0; submesh < M.subMeshCount; submesh++)
		{

			int[] indices = M.GetTriangles(submesh);

			for(int i = 0; i < indices.Length; i += 3)
			{
				Vector3[] newVerts = new Vector3[3];
				Vector3[] newNormals = new Vector3[3];
				Vector2[] newUvs = new Vector2[3];
				for(int n = 0; n < 3; n++)
				{
					int index = indices[i + n];
					newVerts[n] = verts[index];
					newUvs[n] = uvs[index];
					newNormals[n] = normals[index];
				}

				Mesh mesh = new Mesh();
				mesh.vertices = newVerts;
				mesh.normals = newNormals;
				mesh.uv = newUvs;

				mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

				GameObject GO;
				if(lTrianglesPool.childCount != 0)
				{
					GO = lTrianglesPool.GetChild(0).gameObject;
					GO.SetActive(true);
					GO.transform.parent = null;
					GO.transform.position = transform.position;
					GO.transform.rotation = transform.rotation;
					GO.transform.localScale = transform.localScale;
					GO.GetComponent<MeshRenderer>().material = materials[submesh];
					GO.GetComponent<MeshFilter>().mesh = mesh;
					GO.GetComponent<Rigidbody>().velocity = Vector3.zero;
					Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(0f, 0.5f), transform.position.z + Random.Range(-0.5f, 0.5f));
					GO.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(300, 500), explosionPos, 5);
				}
				else
				{
					GO = new GameObject("Triangle " + (i / 3));
					GO.layer = LayerMask.NameToLayer("Particle");
					GO.transform.position = transform.position;
					GO.transform.rotation = transform.rotation;
					GO.transform.localScale = transform.localScale;
					GO.AddComponent<MeshRenderer>().material = materials[submesh];
					GO.AddComponent<MeshFilter>().mesh = mesh;
					GO.AddComponent<BoxCollider>();
					Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(0f, 0.5f), transform.position.z + Random.Range(-0.5f, 0.5f));
					GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(300, 500), explosionPos, 5);
					GO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;

				}
				IEnumerator lcoroutine = GoToPool(GO);
				Player.StartCoroutine(lcoroutine);
				//Destroy(GO, 5 + Random.Range(0.0f, 5.0f));
			}
		}

		GetComponent<Renderer>().enabled = false;

		yield return new WaitForSeconds(1.0f);
		if (destroy)
		{
			Destroy(gameObject);
		}
		else
		{
			GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
            gameObject.SetActive(false);
		}

	}

	public IEnumerator GoToPool(GameObject pGO)
	{
		yield return new WaitForSeconds(5 + Random.Range(0.0f, 5.0f));
		pGO.transform.SetParent(lTrianglesPool);
		pGO.SetActive(false);
	}
}