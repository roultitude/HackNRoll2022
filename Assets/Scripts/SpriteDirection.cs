using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhatRobit
{
	public class SpriteDirection : MonoBehaviour
	{
		[SerializeField]
		protected Transform parentTransform;
		public Facing _facing = Facing.Down;

		public virtual Facing Facing { get { return _facing; } }
		//public virtual Vector2 Angle { get { return _camera.CurrentRotation; } }

		public virtual void Awake()
		{

		}

		public virtual void LateUpdate()
		{
			float rY = parentTransform.eulerAngles.y;
			if(rY > 180)
            {
				rY = rY - 360;
            }
			float x = Mathf.Abs(rY);
			if (x < 22.5f)
			{
				_facing = Facing.Up;
			}
			else if (x < 67.5f)
			{
				_facing = rY < 0 ? Facing.UpLeft : Facing.UpRight;
			}
			else if (x < 112.5f)
			{
				_facing = rY < 0 ? Facing.Left : Facing.Right;
			}
			else if (x < 157.5f)
			{
				_facing = rY < 0 ? Facing.DownLeft : Facing.DownRight;
			}
			else
			{
				_facing = Facing.Down;
			}
		}
	}
}
