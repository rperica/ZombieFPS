using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public enum Direction
    {
        MouseY=1,
        MouseX=0,
    }

    public Direction direction = Direction.MouseX;

    public float sensitivityHorizontal = 2.5f;
    public float sensitivityVertical = 2.5f;

    public float minimumVertical = -45.0f;
    public float minimumHorizontal = 45.0f;

    private float rotationX = 0.0f;

    private void Start()
    {
        GameScreenManager.Instance.onSettingChange += UpdateMouseSensivity;
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameState.Pause)
        {
            if (direction == Direction.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHorizontal, 0);

            }
            else if (direction == Direction.MouseY)
            {
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVertical;
                rotationX = Mathf.Clamp(rotationX, minimumVertical, minimumHorizontal);
                float rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
            }
        }
    }
    public void UpdateMouseSensivity()
    {
        sensitivityHorizontal = GameScreenManager.Instance.mouseSensitivitySlider.value;
        sensitivityVertical = GameScreenManager.Instance.mouseSensitivitySlider.value;

        GameManager.Instance.mouseSensitivity = GameScreenManager.Instance.mouseSensitivitySlider.value;
    }
}
