using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Tiles : MonoBehaviour {

    [System.Serializable]
    public class IntRange
    {
        public IntRange(int mn, int mx)
        {
            min = mn;
            max = mx;
        }
        public bool InRange(int variable)
        {
            return (variable>=min)&&(variable< max);
        }
        public int min;
        public int max;
    }
    public Texture2D tileset;
    public Texture2D[] images;
    public int w;
    public int h;
    [SerializeField]
    [HideInInspector]
    public int[] tilesArray;
    public int tilesResolution = 5;
    public IntRange collidingTiles;
    ArrayList colliderFaces;
    public bool outerCollision;

    class Line {
        public Line(Vector2 nstart, Vector2 nend)
        {
            start = nstart;
            end = nend;
        }
        public Vector2 start;
        public Vector2 end;
    }

    // Use this for initialization
    void Start () {
      if (tilesArray == null || tilesArray.Length != w * h)
      {
          tilesArray = new int[w * h];
      }
      colliderFaces = new ArrayList();
      UpdateTiles();
      UpdateColliders();
        
    }

    public void UpdateTiles()
    {
        UpdateTexture();
    }
	
	// Update is called once per frame
	public void UpdateTexture () {
        colliderFaces = new ArrayList();

        Texture2D newTexture = new Texture2D(w * tilesResolution, h * tilesResolution, TextureFormat.ARGB32, false);
        Vector2 newScale = new Vector2(w, h);
        transform.localScale = newScale;
        //transform.localPosition = newScale / 2;
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Color [] colors = new Color[tilesResolution * tilesResolution];
                int tile = tilesArray[x + y * w];
                    if (tileset != null)
                    {
                        int rowLength = tileset.width / tilesResolution;
                        int row = (tile) / rowLength;
                        colors = tileset.GetPixels(((tile) - rowLength * row) * tilesResolution, tileset.height - tilesResolution * (row + 1), tilesResolution, tilesResolution, 0);
                    }
                    else {
                        colors = images[tilesArray[x + y * w]].GetPixels(0, 0, tilesResolution, tilesResolution, 0);
                    }
                if (tile > 0)
                {
                    TileColliderData(x, y);
                }
                
                newTexture.SetPixels(x * tilesResolution, y * tilesResolution, tilesResolution, tilesResolution, colors, 0);
            }
        }
        newTexture.Apply(false);
        newTexture.filterMode = 0;
        newTexture.mipMapBias = 0;
        Renderer rend = GetComponent<Renderer>();
        rend.sortingOrder = -100;
        rend.sharedMaterial.mainTexture = newTexture;
    }

    bool isSolid(int x, int y)
    {
        return tilesArray[x + y * w] == 0 || !collidingTiles.InRange(tilesArray[x + y  * w]);
    }

    void TileColliderData(int x, int y)
    {
        if (collidingTiles.InRange(tilesArray[x + y * w]))
        {
            if ((y < h - 1 && isSolid(x, y + 1))||y==h-1&&outerCollision)
            {
                colliderFaces.Add(new Line(new Vector2(x, y + 1), new Vector2(x + 1, y + 1)));
            }
            if ((y > 0 && isSolid(x, y - 1))|| y == 0 && outerCollision)
            {
                colliderFaces.Add(new Line(new Vector2(x + 1, y), new Vector2(x, y)));
            }
            if ((x > 0 && isSolid(x - 1, y))||x==0&&outerCollision)
            {
                colliderFaces.Add(new Line(new Vector2(x, y), new Vector2(x, y + 1)));
            }
            if ((x < w - 1 && isSolid(x + 1, y))||x==w-1&&outerCollision)
            {
                colliderFaces.Add(new Line(new Vector2(x + 1, y + 1), new Vector2(x + 1, y)));
            }
        }
    }

    public void UpdateColliders()
    {
        if (colliderFaces == null || colliderFaces.Count == 0)
        {
            UpdateTiles();
        }
        EdgeCollider2D[] oldColliders = GetComponents<EdgeCollider2D>();
        for (int i = 0; i < oldColliders.Length; i++)
        {
           DestroyImmediate(oldColliders[i]);
        }
        while (colliderFaces.Count > 0)
        {
           GenerateCollider();
        }
    }

    void GenerateCollider()
    {
        //Color colColor = new Color(Random.value, Random.value, Random.value);
        ArrayList nColliderFaces = new ArrayList();
        Line firstL = colliderFaces[0] as Line;
        nColliderFaces.Add(firstL);
        colliderFaces.RemoveAt(0);
        nColliderFaces.AddRange(FindNeighbors(firstL));
        if (firstL.start == (nColliderFaces[nColliderFaces.Count-1]as Line).end)
        {
            nColliderFaces.Add(firstL);
        }
        EdgeCollider2D col = gameObject.AddComponent<EdgeCollider2D>();
        Vector2[] nColPoints = new Vector2[nColliderFaces.Count+1];//Every line has two vectors
        for (int i = 0; i < nColliderFaces.Count; i++)//Iterating through collider faces
        {
            nColPoints[i] = new Vector2((nColliderFaces[i] as Line).start.x / w, (nColliderFaces[i] as Line).start.y / h) - Vector2.one / 2;
        }
        nColPoints[nColPoints.Length - 1] = new Vector2((nColliderFaces[nColliderFaces.Count - 1] as Line).end.x / w, (nColliderFaces[nColliderFaces.Count - 1] as Line).end.y / h) - Vector2.one / 2;
        
        col.points = nColPoints;
    }

    ArrayList FindNeighbors(Line line)
    {
        ArrayList neighbors = new ArrayList();
        for (int i = 0; i < colliderFaces.Count; i++)
        {
            if (
                (colliderFaces[i] as Line).start == line.end
                
                )
            {
                Line foundN = colliderFaces[i] as Line;
                neighbors.Add(foundN);
                colliderFaces.RemoveAt(i);
                neighbors.AddRange(FindNeighbors(foundN));
            }
        }
        return neighbors;
    }
}

/*
Line[] sortedColliderFaces = new Line[nColliderFaces.Count];

        sortedColliderFaces[0] = nColliderFaces[0] as Line;
        foreach (Line face in nColliderFaces)
        {
            if (face.start == sortedColliderFaces[0].end)
            {
                sortedColliderFaces[1] = face;
                nColliderFaces.Remove(face);
                break;
            }
        }
        */