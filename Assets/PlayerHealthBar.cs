using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {

	[SerializeField, Range(1,100)]
	private int maxHealth = 100;
	[SerializeField]
	private PlayerEnum thisPlayer;

	private int currentHealth = 100;
	
	[SerializeField, Tooltip("time to fade flash color in seconds")]
	private float blinkTime = 1f;
	[SerializeField]
	private RectTransform healthBarTransform;
	private Image healthBarBG;
	private Coroutine flashRoutine;
	[SerializeField]
	private Color originalColor, damageColor = Color.red, healColor = Color.yellow;

	void Awake(){
		PlayerManager.HealthImpactEvent += HealthImpact;
	}

	void Start(){
		healthBarBG = GetComponent<Image>();
		originalColor = healthBarBG.color;
		if(healthBarTransform == null)
			Debug.LogWarning("healthBarTransform not set for " + gameObject);

		currentHealth = 100;
		SetBarScaleTo(currentHealth);
	}

	public void SetBarScaleTo(int health){
		healthBarTransform.localScale = new Vector2(((float) health / (float) maxHealth), 1);
		//Debug.Log(healthBarTransform.localScale);
	}

	public void HealthImpact(PlayerEnum player, int impactAmount){
		if(player != thisPlayer || impactAmount == 0) return;

		currentHealth = Mathf.Clamp(currentHealth + impactAmount, 0, 100);
		SetBarScaleTo(currentHealth);

		if(currentHealth <= 0 && !NewGameManager.gameOver){
			NewGameManager.instance.ForceGameOver();
		}

		if(flashRoutine != null){
			StopCoroutine(flashRoutine);
		}
		flashRoutine = impactAmount > 0 ? StartCoroutine(FlashHealthBG(healColor, false)) : StartCoroutine(FlashHealthBG(damageColor, true));
	}

	IEnumerator FlashHealthBG(Color flashColor, bool shake){
		healthBarBG.color = flashColor;
		if(shake) iTween.ShakePosition(gameObject, Vector3.one * 15f, blinkTime);
		for(float t = 0;t < blinkTime; t = t + Time.deltaTime){
			if(Time.timeScale == 0) break;
			healthBarBG.color = Color.Lerp(flashColor, originalColor, t / blinkTime);
			yield return null;
		}
		flashRoutine = null;
		yield return null;
	}

	void OnDisable(){
		PlayerManager.HealthImpactEvent -= HealthImpact;
	}

	void OnDestroy(){
		PlayerManager.HealthImpactEvent -= HealthImpact;
	}

}
