using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeInput : MonoBehaviour
{
    public UnityEvent OnJump;
    public UnityEvent OnTransform;

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool stopTouch = false;

    [SerializeField]
    private float swipeRange = 50f;
    [SerializeField]
    private float tapRange = 10f;

    public static bool isSwiping = false;

    void Update()
    {
        // Verificar si estamos en una plataforma móvil o en el editor
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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    stopTouch = false;
                    isSwiping = false;
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    currentTouchPosition = touch.position;
                    if (!stopTouch)
                    {
                        Vector2 distance = currentTouchPosition - startTouchPosition;

                        if (distance.magnitude > swipeRange)
                        {
                            if (Mathf.Abs(distance.x) < tapRange)
                            {
                                if (distance.y > 0)
                                {
                                    OnJump.Invoke();
                                }
                                else if (distance.y < 0)
                                {
                                    OnTransform.Invoke();
                                }
                                isSwiping = true;
                                stopTouch = true;
                            }
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    stopTouch = false;
                    isSwiping = false;
                    break;
            }
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stopTouch = false;
            isSwiping = false;
            startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            currentTouchPosition = (Vector2)Input.mousePosition;
            if (!stopTouch)
            {
                Vector2 distance = currentTouchPosition - startTouchPosition;

                if (distance.magnitude > swipeRange)
                {
                    if (Mathf.Abs(distance.x) < tapRange)
                    {
                        if (distance.y > 0)
                        {
                            OnJump.Invoke();
                        }
                        else if (distance.y < 0)
                        {
                            OnTransform.Invoke();
                        }
                        isSwiping = true;
                        stopTouch = true;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            stopTouch = false;
            isSwiping = false;
        }
    }
}
