using Unity.Burst.CompilerServices;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public ParticleSystem HitParicles;
    Vector2 dir = new Vector2(1f, 1f);
    RaycastHit2D hit;

    private void Update()
    {
        hit = Physics2D.CircleCast(transform.position, 1.1f, dir, 1);
        if (hit.transform.gameObject.CompareTag("Wall"))
        {
            Debug.Log("hit");
            HitParicles.Play();
        }
    }
}
