using System.Collections.Generic;
using System.Linq;
using levelsSO;
using scriptableObjects;
using UnityEditor;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public static NodeManager instance;
    public LevelDatabase levelDatabase;


    [SerializeField] private float nodeSize = 1f;
    
    private List<List<BaseNode>> _nodes;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _nodes = new List<List<BaseNode>>();
    }


    public void LoadLevel(BaseLevelSo nextLevelSo, bool firstLoad = false)
    {
        if (!nextLevelSo)
        {
            //brak następnego poziomu zakładam, że to oznacza ostatni poziom i koniec gry
            GameManager.instance.QuitGame();
        }

        SpawnNodeGrid(nextLevelSo);
    }

    public void ClearLevel()
    {
        if (_nodes == null)
        {
            _nodes = new List<List<BaseNode>>();
            return;
        }

        foreach (var node in _nodes.SelectMany(row => row))
        {
            if (node == null) continue;

            node.DestroySelf();
        }

        _nodes.Clear();
    }

    public void ClearLevelInEditor()
    {
#if UNITY_EDITOR
        if (!Application.isEditor || Application.isPlaying) return;

        if (_nodes == null)
        {
            _nodes = new List<List<BaseNode>>();
            return;
        }

        foreach (var node in _nodes.SelectMany(row => row))
        {
            if (node == null)
            {
                continue;
            }

            node.DestroySelfInEditor();
        }

        _nodes.Clear();
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
        EditorUtility.SetDirty(this);
#endif
    }

    private void SpawnNodeGrid(BaseLevelSo spawnedLevelSo)
    {
        var gridHeight = spawnedLevelSo.nodes.Count;

        var startPosition = transform.position;

        for (var x = 0; x < gridHeight; x++)
        {
            var rowList = new List<BaseNode>();
            var gridWidth = spawnedLevelSo.nodes[x].row.Count;

            for (var z = 0; z < gridWidth; z++)
            {
                var nodeSo = spawnedLevelSo.nodes[x].row[z];

                if (!nodeSo)
                {
                    continue;
                }

                var node = SpawnNode(nodeSo, startPosition, x, z, rowList);

                if (!node)
                {
                    continue;
                }

                SpawnElement(node, nodeSo);
            }

            _nodes.Add(rowList);
        }
    }

    private BaseNode SpawnNode(BaseNodeSo nodeSo, Vector3 startPosition, int x, int z, List<BaseNode> row)
    {
        var position = startPosition + new Vector3(x * nodeSize, 0, z * nodeSize);

        var nodeObj = Instantiate(nodeSo.prefab, position, Quaternion.identity, transform);

        var node = nodeObj.GetComponent<BaseNode>();

        if (node != null)
        {
            row.Add(node);
        }
        else
        {
            Debug.LogError("Node prefab is missing BaseNode component!");
        }

        return node;
    }

    private void SpawnElement(BaseNode node, BaseNodeSo nodeSo)
    {
        if (nodeSo.elementPrefab == null)
        {
            return;
        }

        var elementGameObject = Instantiate(nodeSo.elementPrefab, node.transform);
        var element = elementGameObject
            .GetComponent<BaseElement>();
        if (element != null)
        {
            element.SetParentNode(node, nodeSo.withPattern);
        }
        else
        {
            Debug.LogError("No BaseElement found on the prefab!");
        }
    }

    public List<BaseNode> GetNeighbors(BaseNode node)
    {
        if (!node)
        {
            Debug.LogError("Player Parent Node is null!");
            return new List<BaseNode>();
        }

        var neighbors = new List<BaseNode>();

        // Find node's position in grid
        int nodeX = -1, nodeZ = -1;


        for (var x = 0; x < _nodes.Count; x++)
        {
            for (var z = 0; z < _nodes[x].Count; z++)
            {
                if (_nodes[x][z] != node) continue;
                nodeX = x;
                nodeZ = z;
                break;
            }

            if (nodeX != -1) break;
        }

        if (nodeX == -1 || nodeZ == -1)
        {
            return neighbors;
        }

        // Up
        if (nodeX > 0 && nodeZ < _nodes[nodeX - 1].Count)
            neighbors.Add(_nodes[nodeX - 1][nodeZ]);

// Down
        if (nodeX < _nodes.Count - 1 && nodeZ < _nodes[nodeX + 1].Count)
            neighbors.Add(_nodes[nodeX + 1][nodeZ]);

// Left
        if (nodeZ > 0)
            neighbors.Add(_nodes[nodeX][nodeZ - 1]);

// Right
        if (nodeZ < _nodes[nodeX].Count - 1)
            neighbors.Add(_nodes[nodeX][nodeZ + 1]);

        return neighbors;
    }
}