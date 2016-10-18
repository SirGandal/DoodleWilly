using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
	
//	public EndLevel e;
//	public ScoreManager sm;
//	public SoundManager soundm;
	GameObject go;
	private int delayStartTime = 1;
	public int s1 = 0;
	public int s2 = 0;
	public int m = 0;
	private string sec1;
	private string sec2;
	private string minute;
	
	public void StartTimer(){
				s1=0;
				s2=0;
				m=0;
		setTime(m,s1,s2);
		InvokeRepeating("timeUpdate",0, 1);
	}
	
		public void setTime(int minutes, int second1, int second2){
		m = minutes;
		s1 = second1;
		s2 = second2;
	}
	
	private void updateTextMesh() {
		sec1 = s1.ToString();
		sec2 = s2.ToString();
		minute = m.ToString();
				this.GetComponent<Text>().text = minute+":"+sec1+sec2;
	}
	
	public void StopTimer(){
				CancelInvoke("timeUpdate");
	}
	
	void timeUpdate(){
			if(s1==5 && s2==9){
				s1=0;
				s2=0;
				m++;
			}else{
				if(s2==9){
					s1++;
					s2=0;
				}else{
					s2++;
				}		
			}
		
			if((int)Time.timeSinceLevelLoad>=delayStartTime){
				updateTextMesh();
			}
		}
}