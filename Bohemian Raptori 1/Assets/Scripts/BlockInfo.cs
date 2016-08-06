using UnityEngine;
using System.Collections;

public class BlockInfo : MonoBehaviour {
	public Vector2 size;

	public void moveBlock(float move)
	{
		transform.position = new Vector3 (
			transform.position.x + move,
			transform.position.y,
			transform.position.z);
	}

	public bool isPast(float point)
	{
		return point > getBack();
	}

	public float getFront()
	{
		return transform.position.x - (size.x * 0.5f);
	}

	public float getBack()
	{
		return transform.position.x + (size.x * 0.5f);
	}

	public float getHalf()
	{
		return size.x * 0.5f;
	}
}
