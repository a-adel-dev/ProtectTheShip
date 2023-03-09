using UnityEngine;

public interface IInteractive
{

    public GameObject GameObject { get; }

    void Destroy();
    InteractiveObjectType Type { get; }
}

public enum InteractiveObjectType { ResourceNode, }