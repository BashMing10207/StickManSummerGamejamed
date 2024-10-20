using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class eixt : MonoBehaviour
{
    public void esc()
    {
        Utils.ForceCrash(ForcedCrashCategory.FatalError);

    }
}

