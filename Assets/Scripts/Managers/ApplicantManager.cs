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

        public Stack<Resume> currentRoundPool = new Stack<Resume>();
        public List<Resume> currentHand = new List<Resume>();
        public int processedResumesCount = 0;

        [SerializeField] private GameObject resume, resumeStack;
        [SerializeField] private List<Transform> resumeHolders = new List<Transform>();

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
            DrawPool(Globals.GlobalWorkerPool, GameConfig.ResumesPerRound);

            for (int i = 0; i < GameConfig.ResumesPerBatch; i++)
            {
                DrawFromPool();
            }
            PositionResumes();
            HiringCanvasUI.Instance.StartNewRoundUI();
        }
        public void PositionResumes()
        {
            for (int i = 0; i < currentHand.Count; i ++)
            {
                currentHand[i].transform.parent = resumeHolders[i];
                currentHand[i].transform.localPosition = Vector3.zero;
            }
        }

        public void DrawPool(List<Employee> employees, int pileSize)
        {
            //draws initial pool of 10 resumes
            currentRoundPool = new Stack<Resume>();
            while (employees.Count > 0 && currentRoundPool.Count < pileSize)
            {
                Employee randomEmployee;

                int index = Random.Range(0, employees.Count - 1);
                randomEmployee = employees[index];

                Resume nextResume = Instantiate(resume, Vector3.zero, Quaternion.identity, resumeStack.transform).GetComponent<Resume>();
                nextResume.Initialize(randomEmployee);
                currentRoundPool.Push(nextResume);

                employees.RemoveAt(index);
            }
        }
        public bool DrawFromPool()
        {
            if (currentRoundPool.Count == 0)
            {
                return false;
            }
            //add to empty hand slot first
            for (int i = 0; i < currentHand.Count; i++)
            {
                if (currentHand[i] == null)
                {
                    currentHand[i] = currentRoundPool.Pop();
                    return true;
                }
            }
            currentHand.Add(currentRoundPool.Pop());
            return true;
        }
        /*
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
        }*/
    }
}