using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    public Collider2D scanHitbox;
    Vector2 rightAttackOffset;
    public Harvesting tool;

    private void Start()
    {
        rightAttackOffset = transform.localPosition;
    }

    public void AttackRight()
    {
        print("Scan Right");
        scanHitbox.enabled = true;
        scanHitbox.isTrigger = true;
        transform.localPosition = rightAttackOffset;
        StartCoroutine(finishFockingScanning(0.7f));
    }
    public void AttackLeft()
    {
        print("Scan Left");
        scanHitbox.enabled = true;
        scanHitbox.isTrigger = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
        StartCoroutine(finishFockingScanning(0.7f));
    }
    public void AttackDown()
    {
        print("Scan Down");
        scanHitbox.enabled = true;
        scanHitbox.isTrigger = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * 0, rightAttackOffset.y * 2);
        StartCoroutine(finishFockingScanning(0.7f));
    }
    public void AttackUp()
    {
        print("Scan Up");
        scanHitbox.enabled = true;
        scanHitbox.isTrigger = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * 0, rightAttackOffset.y * -2);
        StartCoroutine(finishFockingScanning(0.7f));
    }

    public void StopScan()
    {
        scanHitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (tool != null)
        {
            tool.OnScanHit(other);
        }
    }

    private IEnumerator finishFockingScanning(float delay)
    {
        yield return new WaitForSeconds(delay);

        StopScan();
    }
}
