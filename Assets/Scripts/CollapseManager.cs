using System;
using System.Collections;
using UnityEngine;

public class CollapseManager : MonoBehaviour
{
    public static CollapseManager Instance;

    public event Action OnCollapsed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Collapse(ActiveItem aItem, ActiveItem bItem)
    {
        ActiveItem fromItem;
        ActiveItem toItem;

        if (Mathf.Abs(aItem.transform.position.y - bItem.transform.position.y) > 0.05f)
        {
            if (aItem.transform.position.y > bItem.transform.position.y)
            {
                fromItem = aItem;
                toItem = bItem;
            }
            else
            {
                fromItem = bItem;
                toItem = aItem;
            }
        }
        else
        {
            if (aItem.Rigidbody.velocity.magnitude > bItem.Rigidbody.velocity.magnitude)
            {
                fromItem = aItem;
                toItem = bItem;
            }
            else
            {
                fromItem = bItem;
                toItem = aItem;
            }
        }

        StartCoroutine(CollapseProcess(fromItem, toItem));
    }

    private IEnumerator CollapseProcess(ActiveItem fromItem, ActiveItem toItem)
    {
        fromItem.Disable();

        if (fromItem.ItemType == ItemType.Ball || toItem.ItemType == ItemType.Ball)
        {
            Vector3 startPosition = fromItem.transform.position;

            for (float i = 0; i < 1f; i += Time.deltaTime / 0.08f)
            {
                fromItem.transform.position = Vector3.Lerp(startPosition, toItem.transform.position, i);
                yield return null;
            }

            fromItem.transform.position = toItem.transform.position;
        }

        if (fromItem.ItemType == ItemType.Ball && toItem.ItemType == ItemType.Ball)
        {
            fromItem.Die();
            toItem.DoEffect();

            ExplodeBall(toItem.transform.position, toItem.Radius + 0.3f);
        }
        else
        {
            if (fromItem.ItemType == ItemType.Ball)
            {
                fromItem.Die();
            }
            else
            {
                fromItem.DoEffect();
            }

            if (toItem.ItemType == ItemType.Ball)
            {
                toItem.Die();
            }
            else
            {
                toItem.DoEffect();
            }
        }
        
        OnCollapsed?.Invoke();
    }

    private void ExplodeBall(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            PassiveItem passiveItem = colliders[i].GetComponentInParent<PassiveItem>();

            if (passiveItem)
            {
                passiveItem.OnAffect();
            }
        }
    }
}