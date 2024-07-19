using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    PlayerMovement playerMovement;
    bool isTouching = false;
    Vector3 touchPosition;

    // Variable para ajustar la sensibilidad
    public float sensitivity = 1.0f;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Verificar si estamos en Android o en el editor de Unity
        if (Application.isMobilePlatform)
        {
            HandleTouchInput(); // Manejar entrada táctil en dispositivos móviles
        }
        else
        {
            HandleMouseInput(); // Manejar entrada del mouse en el editor de Unity
        }
    }

    void HandleTouchInput()
    {
        if (SwipeInput.isSwiping) return; // No mover si se está haciendo un swipe

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isTouching = true;
                    touchPosition = GetTouchWorldPosition(touch.position);
                    touchPosition.z = transform.position.z;
                    touchPosition.y = transform.position.y;
                    transform.position = touchPosition; // Mueve el objeto inmediatamente al tocar
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (isTouching)
                    {
                        touchPosition = GetTouchWorldPosition(touch.position);
                        touchPosition.z = transform.position.z;
                        touchPosition.y = transform.position.y;
                        float clampedX = Mathf.Clamp(touchPosition.x, -playerMovement.bounds, playerMovement.bounds);
                        touchPosition.x = clampedX;
                        transform.position = Vector3.Lerp(transform.position, touchPosition, Time.deltaTime * 10f * sensitivity);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isTouching = false;
                    break;
            }
        }
    }

    void HandleMouseInput()
    {
        if (SwipeInput.isSwiping) return; // No mover si se está haciendo un swipe

        if (Input.GetMouseButtonDown(0))
        {
            isTouching = true;
            touchPosition = GetTouchWorldPosition(Input.mousePosition);
            touchPosition.z = transform.position.z;
            touchPosition.y = transform.position.y;
            transform.position = touchPosition; // Mueve el objeto inmediatamente al tocar
        }
        else if (Input.GetMouseButton(0))
        {
            if (isTouching)
            {
                touchPosition = GetTouchWorldPosition(Input.mousePosition);
                touchPosition.z = transform.position.z;
                touchPosition.y = transform.position.y;
                float clampedX = Mathf.Clamp(touchPosition.x, -playerMovement.bounds, playerMovement.bounds);
                touchPosition.x = clampedX;
                transform.position = Vector3.Lerp(transform.position, touchPosition, Time.deltaTime * 10f * sensitivity);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isTouching = false;
        }
    }

    Vector3 GetTouchWorldPosition(Vector2 screenPosition)
    {
        Vector3 touchPosScreen = screenPosition;

        Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(new Vector3(touchPosScreen.x,
            touchPosScreen.y,
            Mathf.Abs(transform.position.z - Camera.main.transform.position.z)));

        return touchPosWorld;
    }
}
