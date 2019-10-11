using UnityEngine;

public class Test : MonoBehaviour {
    public Transform MainCollider;
    public PlayerController PlayerController;

    public float xMod = 0.0001f;
    public float yMod = 0.0001f;
    public float zMod = 0.0001f;

    private int _collisions = 0;

    void FixedUpdate() {
        if (PlayerController != null) {
            if (yMod > 0) {
                PlayerController.OnGround = _collisions > 0;
            }
        }
        if (_collisions > 0) {
            Debug.Log(Mathf.Max(xMod, yMod, zMod));
            var pos = MainCollider.position;
            pos.x += xMod;
            pos.y += yMod;
            pos.z += zMod;
            MainCollider.position = pos;
        }
    }

    void OnTriggerEnter(Collider other) {
        _collisions++;
    }

    void OnTriggerExit(Collider other) {
        _collisions--;
    }
}
