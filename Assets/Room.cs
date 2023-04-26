using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] public GameObject _tile;
    [SerializeField] private float _tileSize;
    [SerializeField] public int _width;
    [SerializeField] public int _height;

    public List<List<GameObject>> _tileGrid = new List<List<GameObject>>();

    private List<GameObject> _tiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRoom()
    {
        List<GameObject> tiles = new List<GameObject>();
        Vector3 pos = transform.position;
        for (int i = 0; i < _height; i++)
        {
            tiles = new List<GameObject>();
            for (int j = 0; j < _width; j++)
            {
                
                GameObject gameObject = Instantiate(_tile, pos, Quaternion.identity, transform);
                _tiles.Add(gameObject);
                tiles.Add(gameObject);
                pos = new Vector3(pos.x, pos.y, pos.z + _tileSize);
            }
            pos = new Vector3(pos.x + _tileSize, pos.y, pos.z - (_width * _tileSize));
            _tileGrid.Add(tiles);
            
        }

        foreach (GameObject t in _tiles)
        {
            Tile tileScript = t.GetComponent<Tile>();
            if (_tiles.IndexOf(t) == 0)
            {
                tileScript.ChangeWallStates(1, 0, 0, 1);
            }
            else if (_tiles.IndexOf(t) == _tiles.Count - 1)
            {
                tileScript.ChangeWallStates(0, 1, 1, 0);
            }
            else if (_tiles.IndexOf(t) == _width - 1)
            {
                tileScript.ChangeWallStates(1, 1, 0, 0);
            }
            else if (_tiles.IndexOf(t) == _tiles.Count - _width)
            {
                tileScript.ChangeWallStates(0, 0, 1, 1);
            }
            else if (_tiles.IndexOf(t) > 0 && _tiles.IndexOf(t) < _width - 1)
            {
                tileScript.ChangeWallStates(1, 0, 0, 0);
            }
            else if (_tiles.IndexOf(t) > _tiles.Count - _width && _tiles.IndexOf(t) < _tiles.Count - 1)
            {
                tileScript.ChangeWallStates(0, 0, 1, 0);
            }
            else if (_tiles.IndexOf(t) % _width == 0)
            {
                tileScript.ChangeWallStates(0, 0, 0, 1);
            }
            else if (_tiles.IndexOf(t) % _width == _width - 1)
            {
                tileScript.ChangeWallStates(0, 1, 0, 0);
            }
            else
            {
                tileScript.ChangeWallStates(0, 0, 0, 0);
            }
        }
    }

    public void PlaceDoor(int index, int dir)
    {
        GameObject tile = _tiles[index];
        Tile tileScript = tile.GetComponent<Tile>();
        tileScript.PlaceDoor(dir);
    }

    public void PlaceDoor(int x, int y, Direction dir)
    {
        Debug.Log(_tile);
        Debug.Log(_tileGrid.Count);
        Debug.Log(_tileGrid[0].Count);
        Debug.Log(x);
        Debug.Log(y);
        GameObject tile = _tileGrid[x][y];
        Tile tileScript = tile.GetComponent<Tile>();
        tileScript.PlaceDoor(dir);
    }
}
