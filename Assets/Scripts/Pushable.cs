using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pushable : MonoBehaviour {
    public Tilemap collisionTilemap; // walls / obstacles
    public LayerMask pushableLayer; // other blocks
    public float slideTime = 0.15f; // sliding speed

    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool _isMoving;
    
    [SerializeField] private Vector3Int finalCell = new(-1, 1, 0);
    private Vector3Int _targetCell;
    private bool _isFinished;

    public bool Push(Vector2 direction)
    {
        if (_isMoving || _isFinished) return false;

        Vector3Int currentCell = collisionTilemap.WorldToCell(transform.position);
        _targetCell = currentCell + new Vector3Int((int)direction.x, (int)direction.y, 0);
        Debug.Log("old loc = " + currentCell);
        Debug.Log("new loc = " + _targetCell);


        // Check if blocked
        if (IsBlocked(_targetCell))
            return false;


        // Start slide coroutine
        StartCoroutine(SlideTo(collisionTilemap.GetCellCenterWorld(_targetCell)));
        return true;
    }

    private bool IsBlocked(Vector3Int cell) {
        // Tilemap collision
        if (collisionTilemap.HasTile(cell))
            return true;

        // Another pushable object?
        var hit = Physics2D.OverlapPoint(
            collisionTilemap.GetCellCenterWorld(cell),
            pushableLayer
        );

        return hit != null;
    }

    private IEnumerator SlideTo(Vector3 target) {
        _isMoving = true;

        Vector3 start = transform.position;
        float t = 0f;

        while (t < slideTime) {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, t / slideTime);
            yield return null;
        }

        transform.position = target; // perfect snap
        _isMoving = false;

        if (_targetCell == finalCell) {
            Debug.Log("reached goal!" + finalCell + " " + _targetCell);
           
            _isFinished = true;
            spriteRenderer.color = Color.red;
        }
    }
}