# FPSDisplayer
Tool to display fps at GUI in Unity Engine.

</br>

## Images
<img src="Images/sample.png" width=954 height=584></img></br>


</br></br>


## How to use

</br>

### Create
> Get or Create(instantiate) FpsDisplayer.
```C#
FpsDisplayer displayer = FpsDisplayer.GetOrCreate();
FpsDisplayer displayer = FpsDisplayer.GetOrCreate(30);
FpsDisplayer displayer = FpsDisplayer.GetOrCreate(75, DisplayPosition.UpperRight);
```

</br>

> Create() method is implemented like this.</br>
> Their default value is (fontSize : 30) and (displayPosition : DisplayerPosition.UpperLeft).
```C#
public void GetOrCreate() {}
public void GetOrCreate(int fontSize) {}
public void GetOrCreate(int fontSize, DisplayPosition displayPosition) {}
```

</br>

### Modify
> Modify instantiated or created FpsDisplayer's text value.
```C#
public void ModifyFpsDisplayer()
{
    FpsDisplayer fpsDisplayer = FpsDisplayer.GetOrCreate();
    fpsDisplayer.SetFontSize(50);
    fpsDisplayer.SetTextColor(Color.green);
    fpsDisplayer.SetDisplayPosition(DisplayPosition.MiddleCenter);
}
```

</br>

### Destroy
> Destroy FpsDisplayer gameObject.
```C#
public void DestroyFpsDisplayer()
{
    FpsDisplayer.Destroy();
}
```
