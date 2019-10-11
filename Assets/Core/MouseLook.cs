using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseLook : MonoBehaviour {
    public GameObject Body;
    public float XSensitivity = 2;
    public float YSensitivity = 2;
    public float BodySmoothTime = 3;
    public bool LockCursor = true;
    
    public Vector2 Rotation {
        get {
            return _rotation;
        }

        set {
            _rotation = value;
            UpdateTransform();
        }
    }
    private bool _cursorIsLocked = true;
    private Vector2 _rotation = new Vector2(0, 0); // Yaw and Pitch
    
    void Update() {
        if (SceneManager.GetActiveScene() != gameObject.scene) {
            return;
        }
        LookRotation(transform);
    }

    void FixedUpdate() {
        if (SceneManager.GetActiveScene() != gameObject.scene) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }
        UpdateCursorLock();
    }
    public void LookRotation(Transform transform) {
        _rotation.y -= YSensitivity * Input.GetAxis("Mouse Y");
        _rotation.x += XSensitivity * Input.GetAxis("Mouse X");

        UpdateTransform();
        UpdateCursorLock();
    }

    private void UpdateTransform() {
        _rotation.y = ClampAngle(_rotation.y, -90, 90);

        Body.transform.localRotation = Quaternion.Slerp(Body.transform.localRotation,
            Quaternion.Euler(0, _rotation.x, 0),
            BodySmoothTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(_rotation.y, _rotation.x, 0);
    }


    public void SetCursorLock(bool value) {
        LockCursor = value;
        if (!LockCursor) {
            //we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock() {
        //if the user set "LockCursor" we check & properly lock the cursos
        if (LockCursor) {
            InternalLockUpdate();
        }
    }

    private void InternalLockUpdate() {
        if(Input.GetKeyUp(KeyCode.Escape)) {
            _cursorIsLocked = false;
        } else if(Input.GetMouseButtonUp(0)) {
            _cursorIsLocked = true;
        }

        if (_cursorIsLocked) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public static float ClampAngle(float angle, float min, float max) {
		if (angle < -360F) {
			angle += 360F;
        }
		if (angle > 360F) {
			angle -= 360F;
        }
		return Mathf.Clamp(angle, min, max);
	}
}