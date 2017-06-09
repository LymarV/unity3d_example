using System;
using UnityEngine;
using System.Collections;

namespace Mechanics
{
    public class Game: MonoBehaviour
    {
		private const float DeathPauseTime = 0.3f;
        private const float PowerDecreaseValue = 0.001f;

        public PlayerController playerController;
        
        private readonly Session session = new Session();
        
        private GameContext gameContext;
        
        private DisposablePack disposablePack;
        
        private bool IsPaused
        {
            get { return gameContext != null ? gameContext.IsPaused : false; }
        }
        
        void Awake()
        {
            Locator.Register(this);
            
            disposablePack = new DisposablePack();
        }
        
        void Start()
        {
            gameContext = Locator.Find<GameContext>();
            
            var subscription = gameContext.WhenPropertyChanged(c => c.IsPaused, OnPauseChanged);
            disposablePack.Add (subscription);
            
            gameContext.ResurectRequested += ResurectSession;
            gameContext.RestartRequested += RestartSession;
            
            playerController.OnDied += PlayerDied;
            playerController.Stay();
        }
        
        public void RestartSession (bool run)
        {
            session.Reset();
            
            playerController.Reset();
            if (run)
            {
                playerController.Run();
            }
            else
            {
                playerController.Stay();
            }
            
            Camera.main.GetComponent<SmoothFollow>().InstantMove();
        }
        
        public void ResurectSession()
        {
            playerController.transform.Translate(Vector3.up * 4);
            playerController.Run();
            playerController.Jump();
        }
        
        private void OnPauseChanged(bool paused)
        {
            playerController.Pause(paused);
        }
        
        void Update()
        {
            if (IsPaused || !playerController.IsAlive) {
                return;
            }
            
			if (playerController.transform.position.x > 1000)
			{
				RepositionWorld ();
			}

            session.Update();
            gameContext.Scores = (int)session.Scores;
            gameContext.Powerbar = Mathf.Clamp01(gameContext.Powerbar - PowerDecreaseValue);
        }
        
        private void PlayerDied()
        {
            gameContext.Die();
			StartCoroutine(DeathAnimation());
        }

		private void RepositionWorld()
		{
			var dx = playerController.transform.position.x - 50; // don't reset to zero X-axis because there is StartupLocation there

            playerController.transform.Translate(-dx, 0, 0);
            Camera.main.transform.Translate(-dx, 0, 0);

		}

        public void AddPowerup(string powerupType)
        {
            var powerupsConfig = Locator.Find<RootConfig>().PowerupsConfig;

            BasePowerup powerup = null;

            playerController.AddPowerup(powerup);
        }

        private IEnumerator DeathAnimation()
        {
			Time.timeScale = 0;

			yield return new WaitForSecondsRealtime(DeathPauseTime);

			Time.timeScale = 1;
        }
    }
}
