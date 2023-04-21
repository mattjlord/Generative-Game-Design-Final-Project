using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject _tile;
    [SerializeField] private float _tileSize;
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    private List<GameObject> _tiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateRoom(_width, _height, _tile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRoom(int width, int height, GameObject tile)
    {
        Vector3 pos = transform.position;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                _tiles.Add(Instantiate(tile, pos, Quaternion.identity, transform));
                pos = new Vector3(pos.x, pos.y, pos.z + _tileSize);
            }
            pos = new Vector3(pos.x + _tileSize, pos.y, pos.z - (width * _tileSize));
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
            else if (_tiles.IndexOf(t) == width - 1)
            {
                tileScript.ChangeWallStates(1, 1, 0, 0);
            }
            else if (_tiles.IndexOf(t) == _tiles.Count - width)
            {
                tileScript.ChangeWallStates(0, 0, 1, 1);
            }
            else if (_tiles.IndexOf(t) > 0 && _tiles.IndexOf(t) < width - 1)
            {
                tileScript.ChangeWallStates(1, 0, 0, 0);
            }
            else if (_tiles.IndexOf(t) > _tiles.Count - width && _tiles.IndexOf(t) < _tiles.Count - 1)
            {
                tileScript.ChangeWallStates(0, 0, 1, 0);
            }
            else if (_tiles.IndexOf(t) % _width == 0)
            {
                tileScript.ChangeWallStates(0, 0, 0, 1);
            }
            else if (_tiles.IndexOf(t) % _width == width - 1)
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
}
