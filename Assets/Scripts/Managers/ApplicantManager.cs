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

        public List<Employee> currentRoundPool = new List<Employee>();
        public int processedResumesCount = 0;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            EventBus.OnHireRoundEnded += HandleHireRoundEnded;
        }

        private void OnDisable()
        {
            EventBus.OnHireRoundEnded -= HandleHireRoundEnded;
        }

        private void HandleHireRoundEnded(Dictionary<BaseDepartment, List<Employee>> hiredEmployees)
        {
            foreach (var workerData in hiredEmployees)
            {
                BaseDepartment department = workerData.Key;
                List<Employee> employees = workerData.Value;
                
                if (department != null && employees != null)
                {
                    department.AssignNewEmployees(employees);
                    Debug.Log($"Assigned {employees.Count} employees to {department.name}");
                }
            }
        }

        public void HandleHiringRound()
        {
            currentRoundPool.Clear();
            processedResumesCount = 0;

            currentRoundPool = GetRandomEmployees();

            

            HiringCanvasUI.Instance.StartNewRoundUI();
        }
        private List<Employee> GetRandomEmployees()
        {
            //gets stack of 10 employees from global
            List<Employee> employees = new List<Employee>();

            return employees;
        }

        public List<Resume> GetNextBatch()
        {
            List<Resume> batch = new List<Resume>();
            int resumesToSpawn = Mathf.Min(GameConfig.ResumesPerBatch, currentRoundPool.Count - processedResumesCount);

            for (int i = 0; i < resumesToSpawn; i++)
            {
                //batch.Add(currentRoundPool[processedResumesCount + i]);
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