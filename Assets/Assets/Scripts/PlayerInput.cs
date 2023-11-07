using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] spawnPoints;


    void Update()
    {
        CheckKeyboardInput();
        CheckGamepadInput();
    }

    void CheckKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) CheckInput(0, "LeftTrigger", "FailTrigger");
        if (Input.GetKeyDown(KeyCode.DownArrow)) CheckInput(1, "DownTrigger", "FailTrigger");
        if (Input.GetKeyDown(KeyCode.UpArrow)) CheckInput(2, "UpTrigger", "FailTrigger");
        if (Input.GetKeyDown(KeyCode.RightArrow)) CheckInput(3, "RightTrigger", "FailTrigger");
    }

    void CheckGamepadInput()
    {

        // L2 (Eje 3 en Unity) - Mover Izquierda
        if (Input.GetAxis("JoystickAxis3") > 0.5f) CheckInput(0, "LeftTrigger", "FailTrigger");

        // L1 (Button 4 en Unity) - Mover Arriba
        if (Input.GetButtonDown("JoystickButton4")) CheckInput(2, "UpTrigger", "FailTrigger");

        // R1 (Button 5 en Unity) - Mover Abajo
        if (Input.GetButtonDown("JoystickButton5")) CheckInput(1, "DownTrigger", "FailTrigger");

        // R2 (Eje 4 en Unity) - Mover Derecha
        if (Input.GetAxis("JoystickAxis4") > 0.5f) CheckInput(3, "RightTrigger", "FailTrigger");
    }

    public void OnLeftButtonPressed() => CheckInput(0, "LeftTrigger", "FailTrigger");
    public void OnDownButtonPressed() => CheckInput(1, "DownTrigger", "FailTrigger");
    public void OnUpButtonPressed() => CheckInput(2, "UpTrigger", "FailTrigger");
    public void OnRightButtonPressed() => CheckInput(3, "RightTrigger", "FailTrigger");

    void CheckInput(int index, string successTrigger, string failTrigger)
    {
        GameObject spawnPoint = spawnPoints[index];
        Transform detecTransform = spawnPoint.transform.GetChild(0);
        SpriteRenderer detecSpriteRenderer = detecTransform.GetComponent<SpriteRenderer>();
        Collider2D detecCollider = detecTransform.GetComponent<Collider2D>();

        Collider2D[] notes = Physics2D.OverlapBoxAll(detecCollider.bounds.center, detecCollider.bounds.size, 0f);
        bool noteHit = false;

        foreach (Collider2D note in notes)
        {
            if (note.CompareTag("Note"))
            {
                Destroy(note.gameObject);
                noteHit = true;
                break;
            }
        }

        if (noteHit)
        {
            gameManager.NoteHit();
            detecSpriteRenderer.color = Color.green;

        }
        else
        {
            gameManager.NoteMissed();
            detecSpriteRenderer.color = Color.red;

        }

        StartCoroutine(ResetColorCoroutine(detecSpriteRenderer)); // Iniciar la corutina para restablecer el color
    }

    IEnumerator ResetColorCoroutine(SpriteRenderer spriteRenderer)
    {
        yield return new WaitForSeconds(0.2f); // Espera medio segundo antes de restablecer el color
        spriteRenderer.color = Color.white;
    }
}
