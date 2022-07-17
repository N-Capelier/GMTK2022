using UnityEngine;

public class Pickup : MonoBehaviour
{
	public Weapon weapon;

    private void Start()
    {
        gameObject.LeanScale(new Vector2(gameObject.transform.localScale.x * 1.3f,gameObject.transform.localScale.y * 1.3f),4).setLoopPingPong();
        gameObject.LeanMoveLocalY(gameObject.transform.position.y - gameObject.transform.position.y * 0.3f, 2f).setLoopPingPong();
    }
}
