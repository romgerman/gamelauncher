using System;
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

	delegate void FinishedAnimation(bool cancelled);

	abstract class Tween<T>
	{
		public FinishedAnimation OnFinished;

		protected bool isRunning;
		protected TweenType twType;
		
		/// <summary>
		/// Starts an animation
		/// </summary>
		/// <param name="start">Start parameter</param>
		/// <param name="end">End parameter</param>
		/// <param name="ms">Duration</param>
		public abstract void Animate(T start, T end, int ms, TweenType type = TweenType.Linear);

		/// <summary>
		/// Stops an animation
		/// </summary>
		public abstract void Cancel();

		/// <summary>
		/// Updates an animation
		/// </summary>
		/// <param name="value">New value</param>
		/// <param name="dt">Delta time</param>
		public abstract void Update(ref T value, int dt);

		public bool IsRunning() { return isRunning; }

		public abstract T End();
		public abstract T Start();
	}

	class TweenVector2f : Tween<Vector2f>
	{
		private Vector2f startVal;
		private Vector2f endVal;
		private float duration;
		private DateTime startTime;
		private bool settime;

		public override void Animate(Vector2f start, Vector2f end, int ms, TweenType type = TweenType.Linear)
		{
			startVal = start;
			endVal = end;
			duration = ms * 0.001f;
			twType = type;

			isRunning = true;
			settime = false;
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
				var rem = endVal - value;

				if (!settime)
				{
					startTime = DateTime.Now;
					settime = true;
				}
				
				if (rem.Length() > 2f)
				{
					switch (twType)
					{
						case TweenType.Linear:
							value = startVal.Lerp(endVal, (float)(DateTime.Now - startTime).TotalSeconds / duration);
							break;
						case TweenType.EaseQuadIn:
							value = startVal.EaseQuadIn(endVal, (float)(DateTime.Now - startTime).TotalSeconds / duration);
							break;
						case TweenType.EaseCubicIn:
							value = startVal.EaseCubicIn(endVal, (float)(DateTime.Now - startTime).TotalSeconds / duration);
							break;
						case TweenType.EaseSineIn:
							value = startVal.EaseSineIn(endVal, (float)(DateTime.Now - startTime).TotalSeconds / duration);
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
