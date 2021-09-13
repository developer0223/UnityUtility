# FPSDisplayer
Tool to display fps at GUI in Unity Engine.

</br>

## Images
<img src="/Images/sample.png" width=954 height=584></img></br>


</br></br>


## How to use

</br>

### Create
```C#
public FpsDisplayer CreateDisplayer() 
{
    // FpsDisplayer displayer = FpsDisplayer.GetOrCreate();
    // FpsDisplayer displayer = FpsDisplayer.GetOrCreate(30);
    FpsDisplayer displayer = FpsDisplayer.GetOrCreate(75, DisplayPosition.UpperRight);
}
```
> Get or Create(instantiate) FpsDisplayer.

</br>

```C#
public void GetOrCreate() {}
public void GetOrCreate(int fontSize) {}
public void GetOrCreate(int fontSize, DisplayPosition displayPosition) {}
```
> Create() method is implemented like this.</br>
> Their default value is (fontSize : 30) and (displayPosition : DisplayerPosition.UpperLeft).

</br>

### Modify
```C#
public void ModifyFpsDisplayer()
{
    FpsDisplayer fpsDisplayer = FpsDisplayer.GetOrCreate();
    fpsDisplayer.SetFontSize(50);
    fpsDisplayer.SetTextColor(Color.green);
    fpsDisplayer.SetDisplayPosition(DisplayPosition.MiddleCenter);
}
```
> Modify instantiated or created FpsDisplayer's text value.

</br>

### Destroy
```C#
public void DestroyFpsDisplayer()
{
    FpsDisplayer.Destroy();
}
```
> Destroy FpsDisplayer gameObject.
