using UnityEngine;

public class PriorityYieldInstruction : CustomYieldInstruction {
    private static int _highRegistred = 0;

    private readonly Priority _priority;

    public PriorityYieldInstruction(Priority priority) {
        _priority = priority;
        if (priority == Priority.High)
        {
            _highRegistred++;
        }
    }

    public override bool keepWaiting {
        get {
            var result = _priority != Priority.High && _highRegistred > 0;
            if (!result && _priority == Priority.High)
            {
                _highRegistred--;
            }
            return result;
        }
    }

    public enum Priority {
        High, Low
    }
}