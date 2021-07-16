using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSequence : MonoBehaviour
{
    public Camera myCam;
    public GameObject warrior, enemy;

    public LTBezierPath path = new LTBezierPath();
    public List<Transform> warriorPath = new List<Transform>();

    public Transform battleCamPos;
    public GameObject spartGO;
    public GameObject darkPanel;
    public GameObject combatBG;

    public GameObject swordHitEnemy, swordHitPlayer;

    public Transform plaayerStandingPos, attackJumpPosPlayer, attackPosPlayer, enemyStandingPos, attackJumpPosEnemy, attackPosEnemy;

    IEnumerator Start()
    {
        Vector3[] pathArr = new Vector3[warriorPath.Count];
        for (int i = 0; i < warriorPath.Count; i++)
        {
            pathArr[i] = warriorPath[i].position;
        }

        //path = new LTBezierPath(pathArr);
        warrior.GetComponent<Animator>().Play("Idle");
        yield return new WaitForSeconds(2f);

        warrior.GetComponent<Animator>().Play("Run");

        LeanTween.moveSpline(warrior, pathArr, 10f).setOnComplete(PlayerReachedToCombatPos);

        LeanTween.value(5f, 30f, 2f).setDelay(1f).setOnUpdate((float val)=> {
            myCam.orthographicSize = val;
        });
    }

    private void PlayerReachedToCombatPos()
    {
        StartCoroutine(EnemyActionRoutine());
        
    }

    IEnumerator EnemyActionRoutine()
    {
        warrior.GetComponent<Animator>().Play("Idle");
        myCam.GetComponent<CameraFollow>().UpdateTarget(battleCamPos);

        LeanTween.value(30f, 5f, 1f).setDelay(1f).setOnUpdate((float val) => {
            myCam.orthographicSize = val;
        });

        yield return new WaitForSeconds(3f);
        enemy.GetComponent<Animator>().Play("GetReady");
        yield return new WaitForSeconds(0.5f);
        warrior.GetComponent<Animator>().Play("GetReady");

        yield return new WaitForSeconds(1f);
        //spartGO.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        LeanTween.scale(darkPanel, Vector3.one, 0.15f).setOnComplete(()=> {
            combatBG.SetActive(true);
            LeanTween.scale(darkPanel, Vector3.zero, 0.15f).setDelay(1f);
        });

        yield return new WaitForSeconds(0.5f);

        enemy.GetComponent<Animator>().Play("Idle");
        warrior.GetComponent<Animator>().Play("Idle");

        yield return new WaitForSeconds(1f);

        warrior.GetComponent<Animator>().Play("Jump");

        LeanTween.move(warrior, attackJumpPosPlayer.position, 0.3f).setOnComplete(() => {
            LeanTween.move(warrior, attackPosPlayer.position, 0.3f);
        });

        yield return new WaitForSeconds(0.6f);
        warrior.GetComponent<Animator>().Play("Idle");
        yield return new WaitForSeconds(0.2f);
        warrior.GetComponent<Animator>().Play("Attack2");
        yield return new WaitForSeconds(0.2f);
        //swordHitEnemy.SetActive(true);
        enemy.GetComponent<Animator>().Play("Hurt");

        yield return new WaitForSeconds(0.3f);
        warrior.GetComponent<Animator>().Play("Idle");
        yield return new WaitForSeconds(0.2f);
        enemy.GetComponent<Animator>().Play("Idle");

        warrior.GetComponent<Animator>().Play("Jump");
        LeanTween.move(warrior, attackJumpPosPlayer.position, 0.3f).setOnComplete(() => {
            LeanTween.move(warrior, plaayerStandingPos.position, 0.3f);
        });

        yield return new WaitForSeconds(0.6f);

        warrior.GetComponent<Animator>().Play("Idle");

        yield return new WaitForSeconds(1f);
        enemy.GetComponent<Animator>().Play("Jump");

        LeanTween.move(enemy, attackJumpPosEnemy.position, 0.3f).setOnComplete(() => {
            LeanTween.move(enemy, attackPosEnemy.position, 0.3f);
        });

        yield return new WaitForSeconds(0.6f);
        enemy.GetComponent<Animator>().Play("Idle");
        yield return new WaitForSeconds(0.2f);
        enemy.GetComponent<Animator>().Play("Attack2");
        yield return new WaitForSeconds(0.2f);
        //swordHitPlayer.SetActive(true);
        warrior.GetComponent<Animator>().Play("Hurt");

        yield return new WaitForSeconds(0.3f);
        enemy.GetComponent<Animator>().Play("Idle");
        yield return new WaitForSeconds(0.2f);
        warrior.GetComponent<Animator>().Play("Idle");

        enemy.GetComponent<Animator>().Play("Jump");
        LeanTween.move(enemy, attackJumpPosEnemy.position, 0.3f).setOnComplete(() => {
            LeanTween.move(enemy, enemyStandingPos.position, 0.3f);
        });

        yield return new WaitForSeconds(0.6f);

        enemy.GetComponent<Animator>().Play("Idle");
    }
}
