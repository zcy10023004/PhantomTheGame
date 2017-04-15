﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phantom.Utility.MessageBus;
using System;
using UnityEngine.SceneManagement;

namespace Phantom.Enviroment
{
    public class EnviromentSettings : MonoBehaviour, ISubscriber<SpottedEvent>, ISubscriber<EnemyDieEvent>
    {
        private bool spotted = false;

        public int enemyCount;


        public void Start() {
            MainBus.Instance.Subscribe(this);
        }

        public void OnGUI()
        {
            if (spotted)
            {
                MainBus.Instance.PublishEvent(new HUDSetText("YOU LOSE"));
                StartCoroutine("ExecuteAfter", 5f);
            }
        }

        private IEnumerator ExecuteAfter(float time)
        {
            yield return new WaitForSeconds(time);

            SceneManager.LoadScene("Demo");

        }

        public void OnEvent(SpottedEvent evt)
        {
            if (!spotted)
            {
                spotted = true;
            }
        }

        public void OnEvent(EnemyDieEvent evt)
        {
            enemyCount--;
            if (enemyCount <= 0) {
                MainBus.Instance.PublishEvent(new HUDSetText("YOU WIN"));
            }
        }
    }
    
}
