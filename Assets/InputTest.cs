using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

///----------------------------------------$$$$$$$\   $$$$$$\  $$$$$$$\   $$$$$$\  
///----------------------------------------$$  __$$\ $$  __$$\ $$  __$$\ $$$ __$$\ 
///----------------------------------------$$ |  $$ |$$ /  $$ |$$ |  $$ |$$$$\ $$ |
///----------Author------------------------$$$$$$$  |$$$$$$$$ |$$$$$$$  |$$\$$\$$ |
///----------Patryk Podworny---------------$$  ____/ $$  __$$ |$$  ____/ $$ \$$$$ |
///----------------------------------------$$ |      $$ |  $$ |$$ |      $$ |\$$$ |
///----------------------------------------$$ |      $$ |  $$ |$$ |      \$$$$$$  /
///----------------------------------------\__|      \__|  \__|\__|       \______/ 

public class InputTest : MonoBehaviour
{
    public InputActionAsset UniversalInput;
    public void LTrigger()
    {
        Debug.Log(nameof(LTrigger));
    }
    public void RTrigger()
    {
        Debug.Log(nameof(RTrigger));
    }
    public void LBumper()
    {
        Debug.Log(nameof(LBumper));
    }    
    public void RBumper()
    {
        Debug.Log(nameof(RBumper));
    }    
    public void LStickDown()
    {
        Debug.Log(nameof(LStickDown));
    } 
    public void RStickDown()
    {
        Debug.Log(nameof(RStickDown));
    }
    public void Select()
    {
        Debug.Log(nameof(Select));
    }
    public void Pause()
    {
        Debug.Log(nameof(Pause));
    }
    public void A()
    {
        Debug.Log(nameof(A));
    }    
    public void B()
    {
        Debug.Log(nameof(B));
    }
    public void X()
    {
        Debug.Log(nameof(X));
    }
    public void Y()
    {
        Debug.Log(nameof(Y));
    }
    public void DPadLeft()
    {
        Debug.Log(nameof(DPadLeft));
    }
    public void DPadRight()
    {
        Debug.Log(nameof(DPadRight));
    }
    public void DPadUp()
    {
        Debug.Log(nameof(DPadUp));
    }
    public void DPadDown()
    {
        Debug.Log(nameof(DPadDown));
    }
    public void LStick()
    {
        Debug.Log(nameof(LStick));
    }
    public void RStick
        ()
    {
        Debug.Log(nameof(RStick));
    }
}

