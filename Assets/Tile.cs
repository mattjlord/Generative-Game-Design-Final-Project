using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Material _groundMaterial;
    [SerializeField] private Material _wallMaterial;
    [SerializeField] private Material _doorframeMaterial;

    [SerializeField] private Mesh _corner;
    [SerializeField] private Mesh _wallSegment;
    [SerializeField] private Mesh _floor;
    [SerializeField] private Mesh _doorway;
    [SerializeField] private Mesh _ceiling;
    [SerializeField] private Mesh _doorframe;
    [SerializeField] private Mesh _wallJoinL;
    [SerializeField] private Mesh _wallJoinR;

    [SerializeField] private MeshFilter _topWallFilter;
    [SerializeField] private MeshFilter _rightWallFilter;
    [SerializeField] private MeshFilter _bottomWallFilter;
    [SerializeField] private MeshFilter _leftWallFilter;
    [SerializeField] private MeshFilter _topDoorframeFilter;
    [SerializeField] private MeshFilter _rightDoorframeFilter;
    [SerializeField] private MeshFilter _bottomDoorframeFilter;
    [SerializeField] private MeshFilter _leftDoorframeFilter;
    [SerializeField] private MeshFilter _floorFilter;
    [SerializeField] private MeshFilter _ceilingFilter;
    [SerializeField] private MeshFilter _cornerTLFilter;
    [SerializeField] private MeshFilter _cornerTRFilter;
    [SerializeField] private MeshFilter _cornerBLFilter;
    [SerializeField] private MeshFilter _cornerBRFilter;

    [SerializeField] private List<MeshRenderer> _wallRenderers;
    [SerializeField] private List<MeshRenderer> _groundRenderers;
    [SerializeField] private List<MeshRenderer> _doorframeRenderers;

    [SerializeField] private Light _light;

    [SerializeField] private int _topWallState;
    [SerializeField] private int _rightWallState;
    [SerializeField] private int _bottomWallState;
    [SerializeField] private int _leftWallState;

    [SerializeField] private Color _lightColor;

    // Start is called before the first frame update
    public void Start()
    {
        UpdateTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AssignWall(MeshFilter wallFilter, MeshFilter doorFilter, int state)
    {
        switch(state)
        {
            case 0:
                wallFilter.mesh = null;
                doorFilter.mesh = null;
                break;
            case 1:
                wallFilter.mesh = _wallSegment;
                doorFilter.mesh = null;
                break;
            case 2:
                wallFilter.mesh = _doorway;
                doorFilter.mesh = _doorframe;
                break;
        }
    }

    void AssignCorner(int prevWallState, int nextWallState, MeshFilter cornerFilter)
    {
        if (prevWallState == 0 && nextWallState == 0)
        {
            cornerFilter.mesh = null;
        }
        else if (prevWallState > 0 && nextWallState == 0)
        {
            cornerFilter.mesh = _wallJoinL;
        }
        else if (prevWallState == 0 && nextWallState > 0)
        {
            cornerFilter.mesh = _wallJoinR;
        }
        else
        {
            cornerFilter.mesh = _corner;
        }
    }

    public void ChangeWallStates(int top, int right, int bottom, int left)
    {
        _topWallState = top;
        _rightWallState = right;
        _bottomWallState = bottom;
        _leftWallState = left;
        UpdateTile();
    }

    public void PlaceDoor(int dir)
    {
        switch(dir)
        {
            case 0:
                _topWallState = 0;
                break;
            case 1:
                _rightWallState = 0;
                break;
            case 2:
                _leftWallState = 0;
                break;
            case 3:
                _bottomWallState = 0;
                break;
        }
    }

    void UpdateTile()
    {
        AssignWall(_bottomWallFilter, _bottomDoorframeFilter, _bottomWallState);
        AssignWall(_leftWallFilter, _leftDoorframeFilter, _leftWallState);
        AssignWall(_topWallFilter, _topDoorframeFilter, _topWallState);
        AssignWall(_rightWallFilter, _rightDoorframeFilter, _rightWallState);
        AssignCorner(_leftWallState, _topWallState, _cornerTLFilter);
        AssignCorner(_topWallState, _rightWallState, _cornerTRFilter);
        AssignCorner(_rightWallState, _bottomWallState, _cornerBRFilter);
        AssignCorner(_bottomWallState, _leftWallState, _cornerBLFilter);
        _floorFilter.mesh = _floor;
        _ceilingFilter.mesh = _ceiling;

        foreach (MeshRenderer m in _wallRenderers)
        {
            m.material = _wallMaterial;
        }

        foreach (MeshRenderer m in _groundRenderers)
        {
            m.material = _groundMaterial;
        }

        foreach (MeshRenderer m in _doorframeRenderers)
        {
            m.material = _doorframeMaterial;
        }

        _light.color = _lightColor;
    }
}
