using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {

    public class GameManager : MonoBehaviour
    {
        List<PlayerScript> badgers = new List<PlayerScript>(); //hålla koll på badgers
        public List<PlayerScript> Badgers { get { return badgers; } set { badgers = value; } } //hålla koll på badgers

        List<PlayerScript> humans = new List<PlayerScript>(); //hålla koll på humans
        public List<PlayerScript> Humans { get { return humans; } set { humans = value; } } //hålla koll på humans

        [SerializeField] MultipleTargetCam multipleTargetCam;
        float cameraValue = 25f;

        public SoundManager soundManager;

        public Transform top;
        public Transform bottom;
        [SerializeField] Transform target;

        float yTop;
        float yBottom;
        float totalDistance;
        public float pctDamage;
        public float pctValue;
        [SerializeField] int camRoundPosition = 0;
        public int _camRoundPosition { get { return camRoundPosition; }}
        [SerializeField] ProgressBar progs;
        WinnerScript winnerScript;

        bool lastRoundBadger = false;
        bool lastRoundHuman = false;

        private void LateUpdate()
        {
            if(multipleTargetCam.targets.Count == 0)
            {
                multipleTargetCam.UpdateCamPos();
            }
            CheckForRoundWinner();
        }

        void Start()
        {           
            winnerScript = FindObjectOfType<WinnerScript>().GetComponent<WinnerScript>();
            yTop = top.position.y;
            yBottom = bottom.position.y;
            totalDistance = yTop - yBottom;
        }
        bool allHumansDead = false;
        bool allBadgersDead = false;

        public void CheckForRoundWinner()
        {
            if(allBadgersDead && allHumansDead)
            {
                NewRound(PlayerScript.PlayerTeam.none);
            }
            else if(allHumansDead && !allBadgersDead)
            {
                NewRound(PlayerScript.PlayerTeam.badger);
            }
            else if (!allHumansDead && allBadgersDead)
            {
                NewRound(PlayerScript.PlayerTeam.human);
            }

            allBadgersDead = false;
            allHumansDead = false;
        }
        public bool TeamIsDead(PlayerScript.PlayerTeam team)
        {
            if(team == PlayerScript.PlayerTeam.human)
            {
                for (int i = 0; i < humans.Count; i++)
                {
                    if (humans[i].dead == false)
                    {
                        allHumansDead = false;
                        return false;
                    }
                }
                allHumansDead = true;
                return true;
            }
            else if (team == PlayerScript.PlayerTeam.badger)
            {
                for (int i = 0; i < badgers.Count; i++)
                {
                    if (badgers[i].dead == false)
                    {
                        allBadgersDead = false;
                        return false;
                    }
                }
                allBadgersDead = true;
                return true;
            }
            else
            {
                return true;
            }
        }


        public void NewRound(PlayerScript.PlayerTeam winner)
        {
            if (winner == PlayerScript.PlayerTeam.badger)
            {
                camRoundPosition--;
                ChangeCameraPos(-cameraValue);
            }
            else if (winner == PlayerScript.PlayerTeam.human)
            {
                camRoundPosition++;
                ChangeCameraPos(cameraValue);
            }

            foreach (var badger in badgers)
            {
                if (badger != null)
                {
                    multipleTargetCam.targets.Clear();
                    Destroy(badger.gameObject);
                }
            }
            foreach (var human in humans)
            {
                if (human != null)
                {
                    multipleTargetCam.targets.Clear();
                    Destroy(human.gameObject);
                }
            }
            StartCoroutine(Wait());
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(2f);

            gameObject.GetComponent<WeaponSpawning>().ClearWeapons();
            gameObject.GetComponent<SpawnPlayers>().Spawn();
        }
        bool BothTeamsDead()
        {
            for (int i = 0; i < humans.Count; i++)
            {
                if (humans[i].dead == false)
                {
                    return false;
                }
            }

            for (int i = 0; i < badgers.Count; i++)
            {
                if (badgers[i].dead == false)
                {
                    return false;
                }
            }
            return true;
        }
        private void CalculatePosition() //Calcutes the position to which the camera should move to. Win condition needs to be here
        {
            if (pctDamage >= 100)
            {
                pctDamage = 100;
                if (lastRoundHuman)
                {
                    winnerScript.Win(PlayerScript.PlayerTeam.human);
                    lastRoundHuman = false;
                }
                lastRoundHuman = true;
            }
            if (pctDamage <= 0)
            {
                pctDamage = 0;
                if (lastRoundBadger)
                {
                    winnerScript.Win(PlayerScript.PlayerTeam.badger);
                    lastRoundBadger = false;
                }
                lastRoundBadger = true;
            }
            pctValue = totalDistance * (pctDamage / 100);
            Debug.Log(pctValue);
            target.position = new Vector3(0, yBottom + pctValue, -20);
            progs.dpctDamage = pctDamage;
            progs.CalculatePosition();
        }
        public void ChangeCameraPos(float value)
        {
            if (value > 0 && lastRoundBadger)
            {
                lastRoundBadger = false;
            }
            if (value < 0 && lastRoundHuman)
            {
                lastRoundHuman = false;
            }
            pctDamage += value;
            CalculatePosition();
        }

        public Vector3 GetTargetPosition()
        {
            return target.position;
        }

        public int GetCamCount()
        {
            return camRoundPosition;
        }
    }
}
