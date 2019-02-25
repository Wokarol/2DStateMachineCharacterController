namespace Wokarol.StateSystem
{
    public interface IHasExitState
    {
        StateSystem.State ExitState { get; set; }
    }
}