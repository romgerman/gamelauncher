using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SFML.System;

namespace GamepadListener
{
	enum TweenType
	{
		Linear,
		EaseQuadIn,
		EaseCubicIn,
		EaseSineIn
	}

	public delegate void FinishedAnimation(bool cancelled);

	abstract class Tween<T>
	{
		public FinishedAnimation OnFinished;
		
		/// <summary>
		/// Starts an animation
		/// </summary>
		/// <param name="start">Start parameter</param>
		/// <param name="end">End parameter</param>
		/// <param name="ms">Duration</param>
		public abstract void Animate(T start, T end, int ms, TweenType type = TweenType.Linear);

		/// <summary>
		/// Cancels an animation
		/// </summary>
		public abstract void Cancel();

		/// <summary>
		/// Updates an animation
		/// </summary>
		/// <param name="value">New value</param>
		/// <param name="dt">Delta time</param>
		public abstract void Update(ref T value, int dt);

		public abstract bool IsRunning();

		public abstract T End();
		public abstract T Start();
	}

	class TweenVector2f : Tween<Vector2f>
	{
		private bool isRunning;

		private Vector2f startVal;
		private Vector2f endVal;
		private Vector2f newVal;
		private float duration;
		private TweenType ttype;
		private DateTime startTime;

		public override void Animate(Vector2f start, Vector2f end, int ms, TweenType type = TweenType.Linear)
		{
			startVal = start;
			endVal = end;
			newVal = startVal;
			duration = ms * 0.001f;
			ttype = type;

			isRunning = true;
			startTime = DateTime.Now;
		}

		public override void Cancel()
		{
			isRunning = false;
			OnFinished?.Invoke(true);
		}

		public override void Update(ref Vector2f value, int dt)
		{
			if (isRunning)
			{
				var rem = endVal - newVal;

				if (Math.Abs(rem.X) > 10f || Math.Abs(rem.Y) > 10f)
				{
					switch (ttype)
					{
						case TweenType.Linear:
							newVal = value = startVal.Lerp(endVal, (float)(DateTime.Now - startTime).TotalSeconds / duration);
							break;
						case TweenType.EaseQuadIn:
							newVal = value = startVal.EaseQuadIn(endVal, (float)(DateTime.Now - startTime).TotalSeconds / duration);
							break;
						case TweenType.EaseCubicIn:
							newVal = value = startVal.EaseCubicIn(endVal, (float)(DateTime.Now - startTime).TotalSeconds / duration);
							break;
						case TweenType.EaseSineIn:
							newVal = value = startVal.EaseSineIn(endVal, (float)(DateTime.Now - startTime).TotalSeconds / duration);
							break;
					}
				}
				else
				{
					isRunning = false;
					OnFinished?.Invoke(false);
				}
			}
		}

		public override bool IsRunning()
		{
			return isRunning;
		}

		public override Vector2f End()
		{
			return endVal;
		}

		public override Vector2f Start()
		{
			return startVal;
		}
	}
}
