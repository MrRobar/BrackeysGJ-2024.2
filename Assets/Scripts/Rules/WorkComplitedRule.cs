using UnityEngine;

[CreateAssetMenu(menuName = "Rules/WorkCompleteRule")]
public class WorkComplitedRule : Rule
{
    public override void Init()
    {
        Work.Completed += OnWorkCompleted;
    }

    private void OnWorkCompleted()
    {
        Work.Completed -= OnWorkCompleted;
        Complete();
    }
}
