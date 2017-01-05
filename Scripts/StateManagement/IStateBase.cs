

namespace Assets.Scripts.StateManagement
{
    public interface IStateBase
    {
        /// <summary>
        /// This is called when the state is created
        /// </summary>
        void BeginState();

        /// <summary>
        /// This is called on every frame
        /// </summary>
        void UpdateState();

        /// <summary>
        /// This is called when a scene has finished loading
        /// </summary>
        void OnSceneLoaded();

        /// <summary>
        /// This is called when the state is no longer active
        /// </summary>
        void DestroyState();


        string GetName();
    }
}
