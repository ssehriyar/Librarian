using UnityEngine;
using System;
using MyUtils.Singleton;

namespace MyUtils.TimeManager
{
    public class TimeManager : Singleton<TimeManager>
    {
        public static Action OnTick;        // 200ms
        public static Action OnSecond;      // tick * 5
        public static Action OnFiveSecond;  // tick * 5 * 5
        public static Action OnTenSecond;   // tick * 10 * 5
        public static Action On20Second;

        private const float TICK_TIMER_MAX = 0.2f;  // 5 ticks per second, 200ms
        private int tick;
        private float tickTimer;

        private void Awake()
        {
			//OnTick = null;
			//OnSecond = null;
			//OnFiveSecond = null;
			//OnTenSecond = null;
			//On20Second = null;
		}

        private void Update()
        {
            TimeCalculator();
        }

        private void TimeCalculator()
        {
            tickTimer += Time.deltaTime;
            if (tickTimer >= TICK_TIMER_MAX)
            {
                tickTimer = 0;
                tick++;

                OnTick?.Invoke();
                if (tick % 5 == 0)
                    OnSecond?.Invoke();

                if (tick % 25 == 0)
                    OnFiveSecond?.Invoke();

                if (tick % 50 == 0)
                    OnTenSecond?.Invoke();

                if (tick % 100 == 0)
                    On20Second?.Invoke();
            }
        }

        private void OnDestroy()
        {
            Destroy(this.gameObject);
        }
    }
}