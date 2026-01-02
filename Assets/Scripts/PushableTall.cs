using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PushableTall : MonoBehaviour
{
    public Tilemap collisionTilemap;
    public LayerMask pushableLayer;
    public float slideTime = 0.15f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool _isMoving;
    [SerializeField] private Vector3Int finalCell = new(-1, 1, 0);
    private Vector3Int _targetCell;
    public bool isFinished;
    

    public void Push(Vector2 direction)
    {
        if (_isMoving || isFinished) return;

        Vector3Int currentCell = collisionTilemap.WorldToCell(transform.position + new Vector3(0, 0.1f, 0));
        _targetCell = currentCell + new Vector3Int((int)direction.x, (int)direction.y, 0);

        Debug.Log("old loc = " + currentCell);
        Debug.Log("new loc = " + _targetCell);

        if (IsBlockedTall(_targetCell)) return;

        Vector3 targetPos = collisionTilemap.GetCellCenterWorld(_targetCell);
        targetPos.y -= collisionTilemap.cellSize.y / 2f;

        StartCoroutine(SlideTo(targetPos));
    }

    private bool IsBlockedTall(Vector3Int baseCell)
    {
        Vector3Int topCell = baseCell + new Vector3Int(0, 1, 0);

        if (collisionTilemap.HasTile(baseCell) || collisionTilemap.HasTile(topCell))
            return true;

        return CheckPhysicsAtCell(baseCell) || CheckPhysicsAtCell(topCell);
    }

    private bool CheckPhysicsAtCell(Vector3Int cell)
    {
        Vector3 checkPos = collisionTilemap.GetCellCenterWorld(cell);

        Collider2D[] hits = Physics2D.OverlapPointAll(checkPos, pushableLayer);

        foreach (var hit in hits)
        {
            if (hit.gameObject != gameObject)
                return true;
        }

        return false;
    }

    private IEnumerator SlideTo(Vector3 target)
    {
        _isMoving = true;
        Vector3 start = transform.position;
        float t = 0f;

        while (t < slideTime)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, t / slideTime);
            yield return null;
        }

        transform.position = target;
        _isMoving = false;

        if (_targetCell == finalCell)
        {
            Debug.Log("reached goal!" + finalCell + " " + _targetCell);

            isFinished = true;
            spriteRenderer.color = Color.red;
        }
    }
}