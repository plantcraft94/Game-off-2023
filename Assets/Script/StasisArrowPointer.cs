using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisArrowPointer : MonoBehaviour
{
    GameObject Arrow;
    private void Update()
    {
        RotateArrow(Stasis.Dir);
    }
    public void spawnArrow()
    {
        Arrow = Instantiate(gameObject, transform.position, transform.rotation);
    }
    public void RotateArrow(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = gameObject.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
