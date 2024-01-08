using UnityEngine;
using WerewolfHunt.Player;

public class SpiderWeb : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isTrapped = false;
    void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.Instance.gameObject)
        {
            isTrapped = true;
            PlayerController.Instance.Trap();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.Instance.gameObject)
        {
            isTrapped = false;
            PlayerController.Instance.Untrap();
        }
    }

    private void OnDestroy()
    {
        if (isTrapped)
        {
            PlayerController.Instance.Untrap();
        }
    }
}