using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePopUp : MonoBehaviour {
    public Animator Animator;
    
    private Text _scoreText;
  //  private Outline _outline; //ToDo Adjust outline color to part color?

    private void OnEnable() {
        AnimatorClipInfo[] clipInfo = Animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        _scoreText = Animator.GetComponent<Text>();
    //    _outline = Animator.GetComponent<Outline>();
    }

    public void SetScore(int score, Color color) {
        _scoreText.text = "+" + score;
        _scoreText.color = color;
        //  _outline.effectColor = color;
    }
}