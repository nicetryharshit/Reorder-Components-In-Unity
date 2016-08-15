using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class ReorderComponents : EditorWindow 
{

	[MenuItem("Window/Reorder Components")]
	static void LaunchWindow()
	{
		ReorderComponents window = (ReorderComponents)EditorWindow.GetWindow(typeof(ReorderComponents));
		GUIContent titleContent = new GUIContent ("Reorder");
		window.titleContent = titleContent;

	}


	//list to cache all the components
	List<Component>componentList =new List<Component>();

	//Callback triggered when selection is changed in Unity Editor
	//Basically, everytime you select some other game object, this method gets called.
	void OnSelectionChange()
	{
		if(Selection.activeGameObject!=null)
		{

			//Get all the components of in the selected Gameobject
			Component[] components = Selection.activeGameObject.GetComponents<Component>() as Component[];

			//clear the component list
			componentList.Clear();

			//null check
			if(components.Length > 0) 
			{
				//set number of components and add components to the list we just cleared
	           	foreach(Component component in components)
	           	{
					componentList.Add(component);
	           	}
	        }	
		}

		Repaint();
    }

	void OnGUI()
	{
		//we start the loop from 1 and not 0 because the first component, i.e, the transform component cannot be removed or moved
		for(int i=1; i<componentList.Count;i++)
		{
			EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();

			//Create 2 buttons for every component, one to move it up and another to move it down.
			if(GUILayout.Button(componentList[i].GetType().Name+ " ^"))
			{
				 UnityEditorInternal.ComponentUtility.MoveComponentUp(componentList[i]);
				
			}

			if(GUILayout.Button(componentList[i].GetType().Name+ " v"))
			{
				 UnityEditorInternal.ComponentUtility.MoveComponentDown(componentList[i]);
				
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();
		}

		//if no Game Object is selected or the selected Game Object less than 1 component (transform)
		if(componentList.Count<2)
		{
			GUIStyle label = new GUIStyle(GUI.skin.box);

			label.wordWrap = true;
			GUI.color = Color.yellow;
			GUILayout.Label("Select a Game Object with at least 2 components.", label);
		}
	}
}
