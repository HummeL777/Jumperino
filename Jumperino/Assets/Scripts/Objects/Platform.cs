using System.Collections;
using UnityEngine;
using static Spawner;

public class Platform : MonoBehaviour
{
    public bool first;
    public bool activated;

    public bool withCoin;
    public coinType coinStructure;

    public bool moving;
    public float speed;
    public float waitTime;
    public Vector3 initialPosition;
    public Vector3 targetPosition;

    public static PlatformSkin skin;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = skin.sprite;
        GetComponent<BoxCollider2D>().size = skin.colliderSize;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (withCoin)
        {
            spawnCoins();
        }

        if(moving)
        {
           StartCoroutine(CycleMove(transform, initialPosition, targetPosition, speed, waitTime));
        }
    }

    public void spawnCoins()
    {
        switch (coinStructure)
        {
            case coinType.low:
                {
                    Instantiate(_coinPrefab, transform.position + Vector3.up * 1f, Quaternion.identity, transform);
                    break;
                }
            case coinType.medium:
                {
                    Instantiate(_coinPrefab, transform.position + Vector3.up * 2f, Quaternion.identity, transform);
                    break;
                }
            case coinType.high:
                {
                    Instantiate(_coinPrefab, transform.position + Vector3.up * 3f, Quaternion.identity, transform);
                    break;
                }
        }
    }

    public static IEnumerator CycleMove(Transform targetObject, Vector3 initialPosition, Vector3 targetPosition, float motionSpeed, float waitTime)
    {
        for (; ; )
        {
            for (; ; )
            {
                if(targetObject.localPosition != targetPosition)
                {
                    targetObject.localPosition = Vector3.MoveTowards(targetObject.localPosition, targetPosition, motionSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    yield return new WaitForSeconds(waitTime);
                    break;
                }

                yield return new WaitForFixedUpdate();
            }
            for (; ; )
            {
                if (targetObject.localPosition != initialPosition)
                {
                    targetObject.localPosition = Vector3.MoveTowards(targetObject.localPosition, initialPosition, motionSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    yield return new WaitForSeconds(waitTime);
                    break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
