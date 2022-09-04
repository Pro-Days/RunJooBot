using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    private float moveXWidth = 1.5f;
    private float moveTimeX = 0.1f;
    private bool isXMove = false;

    private float originY = 0.55f;
    private float gravity = -9.81f;
    private float moveTimeY = 0.3f;
    private bool isJump = false;

    [SerializeField]
    private float moveSpeed = 10.0f;

    private float limitY = -1.0f;

    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (gameController.IsGameStart == false) return;
        
        moveSpeed += + 0.1f * Time.deltaTime;
        // Debug.Log(moveSpeed);

        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

        gameController.IncreaseScore(transform.position.z);
    }

    public void MoveToX(int x)
    {
        if (isXMove == true) return;

        if (x > 0 && transform.position.x < moveXWidth)
        {
            StartCoroutine(OnMoveToX(x));
        }
        else if (x < 0 && transform.position.x > -moveXWidth)
        {
            StartCoroutine(OnMoveToX(x));
        }
    }

    public void MoveToY()
    {
        if (isJump == true) return;

        StartCoroutine(OnMoveToY());
    }
    
    private IEnumerator OnMoveToX(int direction)
    {
        float currnet = 0;
        float percent = 0;
        float start = transform.position.x;
        float end = transform.position.x + direction * moveXWidth;

        isXMove = true;

        while (percent < 1)
        {
            currnet += Time.deltaTime;
            percent = currnet / moveTimeX;

            float x = Mathf.Lerp(start, end, percent);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);

            yield return null;
        }

        isXMove = false;
    }

    private IEnumerator OnMoveToY()
    {
        float current = 0;
        float percent = 0;
        float v0 = -gravity;

        isJump = true;
        rigidbody.useGravity = false;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTimeY;

            float y = originY + (v0 * percent) + (gravity * percent * percent);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

            yield return null;
        }

        isJump = false;
        rigidbody.useGravity = true;
    }

    public void MoveSpeedReset()
    {
        moveSpeed = 10.0f;
    }
}
