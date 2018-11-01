using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class PhaseControl : NetworkBehaviour
{

    private enum PhaseState
    {
        Start,
        Exploration,
        Wave,
        GameOver
    }

    private PhaseState _currentPhase = PhaseState.Start;
    public int _waveNumber = 0;
    private List<PhaseData> waves;

    // Use this for initialization
    void Start()
    {
        waves = new List<PhaseData>(){
            new PhaseData(),
            new PhaseData(),
            new PhaseData(),
            new PhaseData(),
            new PhaseData(),
            new PhaseData()
        };
        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(PhaseCoroutine());
    }

    private IEnumerator PhaseCoroutine()
    {
        for (var i = 0; i < waves.Count; i++)
        {
            var phaseData = waves[i];
            yield return StartCoroutine(WaveCoroutine(phaseData));
            yield return StartCoroutine(ExplorationCoroutine(phaseData));
        }
    }

    private class PhaseData
    {
        public int enemies = 5;
        public int waveDuration = 30;
        public int explorationDuration = 60;
    }

    private interface IPhase : IDisposable
    {
        void Execute();
    }

    private class Wave : IPhase
    {

        private List<Enemy> enemies;
        private Coroutine corutine;
        private readonly MonoBehaviour monoBehaviour;
        private readonly PhaseData phaseData;

		private bool abortPhase = false;

        public Wave(MonoBehaviour mono, PhaseData phaseData)
        {
            this.phaseData = phaseData;
            this.monoBehaviour = mono;

        }

        private IEnumerator WaveCoroutine(float duration)
        {
            Debug.Log("Wave!");
            for (float t = 0; t < phaseData.waveDuration || abortPhase; t += Time.deltaTime)
            {
				if(enemies.Count <= 0)
					continue;
                yield return null;
            }
        }

        public void Execute()
        {
            corutine = monoBehaviour.StartCoroutine(WaveCoroutine(phaseData.waveDuration));
        }

        public void Dispose()
        {
            monoBehaviour.StopCoroutine(corutine);
        }


    }

    private class Exploration : IPhase
    {
		private List<Enemy> enemies;
        private Coroutine corutine;
        private readonly MonoBehaviour monoBehaviour;
        private readonly PhaseData phaseData;

        public Exploration(MonoBehaviour mono, PhaseData phaseData)
        {
            this.phaseData = phaseData;
            this.monoBehaviour = mono;
        }

        private IEnumerator ExplorationCoroutine(float duration)
        {
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                yield return null;
            }
        }

        public void Execute()
        {
            corutine = monoBehaviour.StartCoroutine(ExplorationCoroutine(phaseData.explorationDuration));
        }

        public void Dispose()
        {
            monoBehaviour.StopCoroutine(corutine);
        }
    }
}
