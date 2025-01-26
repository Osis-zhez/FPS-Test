namespace Code.Common.Utils.SmartDebug
{
  public static class DSenders
  {
    public static readonly DSender Application = new DSender(name: "[Application]".Green());
    public static readonly DSender Assets = new DSender(name: "[Assets]");
    public static readonly DSender GameStateMachine = new DSender(name: "[Game State Machine]");
    public static readonly DSender SceneData = new DSender(name: "[Scene Data]");
    public static readonly DSender Postponer = new DSender(name: "[Postponer]");
    public static readonly DSender Localization = new DSender(name: "[Localization]");
  }
}