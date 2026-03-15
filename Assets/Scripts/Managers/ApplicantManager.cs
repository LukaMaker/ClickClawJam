using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI;

namespace Assets.Scripts.Managers
{
    // acts as data controller for GameState.Hiring, prepares resume data for the UI and manages resume batch flow for the UI to render to the screen.
    // manages total resumes processed and signals to the UI once the round is complete to let the player click the buttons to advance game state.
    public class ApplicantManager : MonoBehaviour
    {
        public static ApplicantManager Instance { get; private set; }

        public List<Resume> currentRoundPool = new List<Resume>();
        public int processedResumesCount = 0;

        private void Awake()
        {
            Instance = this;
        }

        public void HandleHiringRound()
        {
            currentRoundPool.Clear();
            processedResumesCount = 0;

            List<Resume> tempGlobalPool = new List<Resume>(Globals.GlobalWorkerPool.Values);

            for (int i = 0; i < GameConfig.ResumesPerRound && tempGlobalPool.Count > 0; i++)
            {
                int randomIndex = Random.Range(0, tempGlobalPool.Count);
                currentRoundPool.Add(tempGlobalPool[randomIndex]);
                tempGlobalPool.RemoveAt(randomIndex);
            }

            HiringCanvasUI.Instance.StartNewRoundUI();
        }

        public List<Resume> GetNextBatch()
        {
            List<Resume> batch = new List<Resume>();
            int resumesToSpawn = Mathf.Min(GameConfig.ResumesPerBatch, currentRoundPool.Count - processedResumesCount);

            for (int i = 0; i < resumesToSpawn; i++)
            {
                batch.Add(currentRoundPool[processedResumesCount + i]);
            }

            return batch;
        }

        public void ConfirmBatch()
        {
            int resumesSpawnedLastBatch = Mathf.Min(GameConfig.ResumesPerBatch, currentRoundPool.Count - processedResumesCount);
            processedResumesCount += resumesSpawnedLastBatch;
        }

        public bool IsRoundComplete()
        {
            return processedResumesCount >= currentRoundPool.Count;
        }
    }
}