using UnityEngine;
using System;
using MyUtils.Singleton;

namespace MyUtils.TimeManager
{
	public class Timer : Singleton<Timer>
	{
		private const float TICK_TIMER_MAX = 0.2f;

		private int tick = 0;
		private float tickTimer = 0;

		public event Action OnTick = delegate { };
		public event Action On1Second = delegate { };
		public event Action On2Second = delegate { };
		public event Action On5Second = delegate { };

		private void Update()
		{
			TimerInvoke();
		}

		private void TimerInvoke()
		{
			tickTimer += Time.deltaTime;
			if (tickTimer >= TICK_TIMER_MAX)
			{
				tickTimer = 0;
				tick++;

				OnTick?.Invoke();
				if (tick % 5 == 0)
					On1Second?.Invoke();

				if (tick % 10 == 0)
					On2Second?.Invoke();

				if (tick % 25 == 0)
					On5Second?.Invoke();
			}
		}

		private void OnDestroy()
		{
			Destroy(this.gameObject);
		}
	}
}