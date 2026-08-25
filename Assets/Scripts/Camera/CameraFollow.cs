using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    public GameObject player;

    [Header("Follow Settings")]
    [SerializeField]
    private float xOffsetAmount = 1f;

    [SerializeField]
    private float flipXOffsetTime = 0.5f;

    private Coroutine flipCoroutine;
    private PlayerController playerController;
    private bool isFacingRight = true;
    private Vector3 currentOffset;

    void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
        isFacingRight = playerController.isFacingRight;
        currentOffset = GetTargetOffset();
    }

    public void CallFlip()
    {
        if (playerController == null)
            return;

        isFacingRight = playerController.isFacingRight;
        if (flipCoroutine != null)
            StopCoroutine(flipCoroutine);

        flipCoroutine = StartCoroutine(LerpXOffset());
    }

    private Vector3 GetTargetOffset()
    {
        return isFacingRight
            ? new Vector3(xOffsetAmount, 0f, 0f)
            : new Vector3(-xOffsetAmount, 0f, 0f);
    }

    private IEnumerator LerpXOffset()
    {
        Vector3 startOffset = currentOffset;
        Vector3 endOffset = GetTargetOffset();
        float elapsedTime = 0f;

        while (elapsedTime < flipXOffsetTime)
        {
            elapsedTime += Time.deltaTime;
            currentOffset = Vector3.Lerp(startOffset, endOffset, elapsedTime / flipXOffsetTime);
            yield return null;
        }

        currentOffset = endOffset;
    }

    void FixedUpdate()
    {
        if (playerController != null)
            isFacingRight = playerController.isFacingRight;

        transform.position = player.transform.position + currentOffset;
    }
}
