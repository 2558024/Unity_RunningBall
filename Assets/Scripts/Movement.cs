using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]
    public GameController gameController;

    private float moveXWidth = 1.5f;
    private float moveTimeX = 0.1f;
    private bool isXMove;

    private float originY = 0.55f;
    private float gravity = -9.81f;
    private float moveTimeY = 0.3f;
    private bool isJump = false;
    [SerializeField]
    private float jumpForce = 5.0f;

    public float moveSpeed = 20f;

    public float rotateSpeed = 300f;

    private float limitY = -1.0f;

    private new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (gameController.IsGameStart == false) return;

        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);

        if (transform.position.y < limitY)
        {
            Debug.Log("사망하였습니다.");
        }

    }

    public void MoveToY()
    {
        if (isJump == true) return;

        StartCoroutine(OnMoveToY());
    }

    public void MoveToYJump()
    {
        rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, 0, rigidbody.linearVelocity.z);
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

    private IEnumerator OnMoveToX(int direction)
    {
        float current = 0;
        float percent = 0;
        float start = transform.position.x;
        float end = transform.position.x + direction * moveXWidth;

        isXMove = true;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTimeX;

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

            yield return null;
        }

        isJump = false;
        rigidbody.useGravity = true;

    }
}
