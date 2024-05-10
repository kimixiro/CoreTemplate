using Core.Services.ServiceLocator;
using UnityEngine;

namespace Core.Services.Input
{
    public interface IInputService: IService
    {
        Vector2 GetMovementAxis(out bool isPressing);
        bool InteractButtonPressed();
    }
}