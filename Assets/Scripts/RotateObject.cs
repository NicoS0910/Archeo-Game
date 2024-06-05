using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public KeyCode rotateKey = KeyCode.R; // Taste zum Drehen
    public float rotationSpeed = 100f; // Geschwindigkeit der Drehung

    void Update()
    {
        if (Input.GetKey(rotateKey))
        {
            float rotation = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotation); // Drehung um die Z-Achse
        }
    }
}
