using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


/// <summary>
/// This is an animation that imitates the construction of a reactor module. It inherits from the AnimationBase 
/// class, which has virtual methods for play, pause, skip forward and skip backward.
/// </summary>
public class ConstructionAnimation : AnimationBase
{

    private bool shouldPlay = false;
    private bool skipForward = false;
    private bool skipBackward = false;

    private bool newPart = true;




    public Dictionary<string, List<GameObject>> SortReactorCompsDeactivate(GameObject[] comps)
    {

        Dictionary<string, List<GameObject>> outDict = new Dictionary<string, List<GameObject>>();

        foreach (GameObject comp in comps)
        {
            //comp.GetComponent<Animator>().SetTrigger("StartAnim");

            comp.SetActive(false);

            comp.transform.position = new Vector3(0, 0, -10000);

            string partName = comp.name.Split('_')[1];

            if (outDict.ContainsKey(partName))
            {
                outDict[partName].Add(comp);
            }
            else
            {
                outDict[partName] = new List<GameObject>();
                outDict[partName].Add(comp);
            }

        }
        return outDict;
    }

    public Dictionary<int, List<GameObject>> SortCompsByGroup(GameObject[] comps, string group)
    {

        Dictionary<int, List<GameObject>> outDict = new Dictionary<int, List<GameObject>>();

        // Get the groupings from comp props:
        foreach (GameObject comp in comps)
        {


            comp.SetActive(false);

            comp.transform.position = new Vector3(0, 0, -100);

            int grouping = 0;

            switch (group)
            {
                case "ConsGroup":
                    grouping = comp.GetComponent<CompProps>().consGroup;
                    break;
                
            }

            if (outDict.ContainsKey(grouping))
            {
                outDict[grouping].Add(comp);
            }
            else
            {
                outDict[grouping] = new List<GameObject>();
                outDict[grouping].Add(comp);
            }



        }

        return outDict;
    }


    public override IEnumerator Main()
    {
        yield return base.Main();

        //Dictionary<string, List<GameObject>> sortedComps = SortReactorCompsDeactivate(GameObject.FindGameObjectsWithTag("ReactorComponent"));
        Dictionary<int, List<GameObject>> sortedComps = SortCompsByGroup(GameObject.FindGameObjectsWithTag("ReactorComponent"), "ConsGroup");

        // string[] parts = { "clad", "can", "LowerGridPlate", "UpperGridPlate", "ReactorLiner", "ReflectorBlock", "ShieldLiner", "ShieldRetainer", "Vessel", "VesselCap" };
        int maxI = sortedComps.Keys.Max();

        int i = 0;

        while (true)
        {
            


            //string partName = parts[i];

            // Perform Animation for current i (ONLY ONCE)
            var item = sortedComps[i];

            if (newPart)
            {
                foreach (GameObject comp in item)
                {
                    comp.SetActive(true);
                    comp.GetComponent<Animator>().SetTrigger("FlyIn");
                    //yield return new WaitForSeconds(.05f);

                }
                yield return new WaitForSeconds(1.5f);
                foreach (GameObject comp in item)
                {
                    comp.GetComponent<Animator>().ResetTrigger("FlyIn");

                }

                newPart = false;
            }
            GameObject.Find("CurrentPart").GetComponent<Text>().text = "Current Part: " + i;
            //GameObject.Find("PartThumbnail").GetComponent<Image>().sprite = (Sprite)Resources.Load("Thumbnails/" + partName + ".png");

            if (skipForward)
            {
                if (i != maxI - 1)
                {
                    i += 1;
                    newPart = true;

                }
            }

            if (skipBackward)
            {
                // Undo last animation for i and increment backwards to i-1
                foreach (GameObject comp in item)
                {
                    comp.GetComponent<Animator>().SetTrigger("FlyOut");
                    
                   // yield return new WaitForSeconds(.05f);


                }
                yield return new WaitForSeconds(1.5f);

                foreach (GameObject comp in item)
                {
                    comp.GetComponent<Animator>().ResetTrigger("FlyOut");

                    comp.SetActive(false);

                }


                i -= 1;
                if (i < 0) i = 0;
                GameObject.Find("CurrentPart").GetComponent<Text>().text = "Current Part: " + i;
                newPart = true;
            }


            if (shouldPlay)
            {
                i += 1;
                yield return new WaitForSeconds(2f);
                newPart = true;
            }

            // Don't continue skipping
            skipBackward = false;
            skipForward = false;

            if (i == maxI - 1) Pause();

            yield return null;

        }


    }

    public override void Play()
    {
        base.Play();

        skipBackward = false;
        skipForward = false;
        shouldPlay = true;
    }

    public override void Pause()
    {
        base.Pause();

        skipBackward = false;
        skipForward = false;
        shouldPlay = false;
    }

    public override void SkipForward()
    {
        base.SkipForward();


        shouldPlay = false;
        skipBackward = false;
        skipForward = true;

    }

    public override void SkipBackward()
    {
        base.SkipBackward();

        shouldPlay = false;
        skipForward = false;
        skipBackward = true;
    }

}
