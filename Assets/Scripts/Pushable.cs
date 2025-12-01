using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pushable : MonoBehaviour {
    public Tilemap collisionTilemap; // walls / obstacles
    public LayerMask pushableLayer; // other blocks
    public float slideTime = 0.15f; // sliding speed

    private bool isMoving = false;

    [SerializeField] private Vector3Int _finalCell = new Vector3Int(-1, 1, 0);
    private Vector3Int targetCell;
    [SerializeField] private Rigidbody2D _rb;
    

    public bool Push(Vector2 direction) {
        if (isMoving) return false;

        Vector3Int currentCell = collisionTilemap.WorldToCell(transform.position);
        targetCell = currentCell + new Vector3Int((int)direction.x, (int)direction.y, 0);
        Debug.Log("old loc = " + currentCell);
        Debug.Log("new loc = " + targetCell);


        // Check if blocked
        if (IsBlocked(targetCell))
            return false;


        // Start slide coroutine
        StartCoroutine(SlideTo(collisionTilemap.GetCellCenterWorld(targetCell)));
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
        isMoving = true;

        Vector3 start = transform.position;
        float t = 0f;

        while (t < slideTime) {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, t / slideTime);
            yield return null;
        }

        transform.position = target; // perfect snap
        isMoving = false;

        if (targetCell == _finalCell) {
            Debug.Log("reached goal!" + _finalCell + " " + targetCell);
            _rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}