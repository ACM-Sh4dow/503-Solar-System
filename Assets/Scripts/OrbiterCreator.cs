using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrbiterCreator : MonoBehaviour
{
    public GameObject planet;
    public GameObject sun;
    private Vector3 mousePos = new Vector3(0, 0, 0);
    public static OrbiterCreator Instance;

    public Action objectCreated;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CreatePlanet(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Instantiate(planet, mousePos, Quaternion.identity);
        objectCreated?.Invoke();
    }

    public void CreateSun(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Instantiate(sun, mousePos, Quaternion.identity);
        objectCreated?.Invoke();
    }
}
