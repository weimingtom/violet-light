using System.Collections;

public class EffectCommand : Commands
{
    ScreenShake screenShake;
    string effect;
    public void SetEffect(string _effect)
    {
        effect = _effect;
    }
    public override void PrintData()
    {

    }
    public override bool ExecuteCommand()
    {
        FXManager.Instance.Spawn( effect );
        return true;
    }
    public override void Reset()
    {
    }
    public override bool Destroy()
    {
        return true;
    }
}
