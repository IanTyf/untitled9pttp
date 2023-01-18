using UnityEngine;
using UnityEditor;
using System.Collections;

public class MultiAnimPreview: EditorWindow {

	class Styles
	{
		public Styles()
		{
		}
	}
	static Styles s_Styles;

	protected GameObject go;
	protected GameObject go2;
	protected AnimationClip animationClip;
	protected AnimationClip animationClip2;
	protected float time = 0.0f;
	protected float time2 = 0.0f;
	protected bool animationMode = false;
	protected bool isPlayingAll = false;

	[MenuItem("Benbees/Multi-Anim Preview", false, 2000)]
	public static void DoWindow()
	{
		GetWindow<MultiAnimPreview>();
	}

	public void OnEnable()
	{		
	}

	public void OnGUI()
	{
		if (s_Styles == null)
			s_Styles = new Styles();

		GUILayout.BeginHorizontal(EditorStyles.toolbar);

		EditorGUI.BeginChangeCheck();
		GUILayout.Toggle(AnimationMode.InAnimationMode(), "Animate", EditorStyles.toolbarButton);
		if (EditorGUI.EndChangeCheck())
		{
			ToggleAnimationMode();
			Debug.Log(AnimationMode.InAnimationMode());
		}

		GUILayout.FlexibleSpace();

		if (GUILayout.Button("Reset", EditorStyles.toolbarButton))
		{
			Reset();
		}


		EditorGUI.BeginChangeCheck();
		isPlayingAll = GUILayout.Toggle(isPlayingAll, "Play", EditorStyles.toolbarButton);
		if (EditorGUI.EndChangeCheck())
		{
			if (isPlayingAll)
			{
				PlayAll();
				Debug.Log("playing all");
			}
			else
			{
				StopPlayAll();
				Debug.Log("stopped playing all");
			}
		}

		GUILayout.EndHorizontal();
		
		EditorGUILayout.BeginVertical();

		go = EditorGUILayout.ObjectField(go, typeof(GameObject), true) as GameObject;
		if (go != null)
		{
			animationClip = EditorGUILayout.ObjectField(animationClip, typeof(AnimationClip), false) as AnimationClip;
			if (animationClip != null)
			{
				float startTime = 0.0f;
				float stopTime = animationClip.length;
				time = EditorGUILayout.Slider(time, startTime, stopTime);
			}
			else if (AnimationMode.InAnimationMode())
				AnimationMode.StopAnimationMode();
		}

		go2 = EditorGUILayout.ObjectField(go2, typeof(GameObject), true) as GameObject;
		if (go2 != null)
		{
			animationClip2 = EditorGUILayout.ObjectField(animationClip2, typeof(AnimationClip), false) as AnimationClip;
			if (animationClip2 != null)
			{
				float startTime = 0.0f;
				float stopTime = animationClip2.length;
				time2 = EditorGUILayout.Slider(time2, startTime, stopTime);
			}
			else if (AnimationMode.InAnimationMode())
				AnimationMode.StopAnimationMode();
		}


		EditorGUILayout.EndVertical();
	}

	void Update()
	{
		if ((go == null) || (animationClip == null) || (go2 == null) || (animationClip2 == null))
			return;

		if (isPlayingAll)
		{
			
			// do this bc the first few frames are always super slow, and the animation would snap to 0.33 or something immediately, which is not good
			if (Time.deltaTime < 0.1f)
			{
				time += Time.deltaTime;
				time2 += Time.deltaTime;
			}
			else
			{
				// do this bc for some reason the frame rate will stay at lowest if time remains zero.. no clue why
				time += 0.005f;
				time2 += 0.005f;
			}

			Debug.Log(Time.deltaTime);
		}
 		

		// there is a bug in AnimationMode.SampleAnimationClip which crash unity if there is no valid controller attached
		Animator animator = go.GetComponent<Animator>();
		if (animator != null && animator.runtimeAnimatorController == null)
			return;

		Animator animator2 = go2.GetComponent<Animator>();
		if (animator2 != null && animator2.runtimeAnimatorController == null)
			return;

		if (!EditorApplication.isPlaying && AnimationMode.InAnimationMode())
		{
			AnimationMode.BeginSampling();
			AnimationMode.SampleAnimationClip(go, animationClip, time);
			AnimationMode.SampleAnimationClip(go2, animationClip2, time2);
			AnimationMode.EndSampling();

			SceneView.RepaintAll();
			Repaint();
		}
	}

	void ToggleAnimationMode()
	{
		Debug.Log("animation mode toggled");
		if(AnimationMode.InAnimationMode())
			AnimationMode.StopAnimationMode();
		else
			AnimationMode.StartAnimationMode();
	}

	void PlayAll()
	{
		//time = 0f;
		//time2 = 0f;
	}

	void StopPlayAll()
	{
		//time = 0f;
		//time2 = 0f;
	}

	void Reset()
	{
		time = 0f;
		time2 = 0f;
	}
}
