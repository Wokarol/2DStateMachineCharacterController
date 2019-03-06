using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DirectionalTeleporter : MonoBehaviour
{
    [SerializeField] Vector2 direction = new Vector2();
    new Collider2D collider;

    private void Start() {
        collider = GetComponent<Collider2D>();
        if (!collider.isTrigger) {
            Debug.LogWarning("Collider should be set to trigger", this);
        }
    }

    private void OnDrawGizmosSelected() {
        if (collider == null) collider = GetComponent<Collider2D>();
        var bounds = collider.bounds;
        Gizmos.DrawWireCube(bounds.center + (Vector3)direction, bounds.size);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        collision.transform.Translate(direction);
    }

}
