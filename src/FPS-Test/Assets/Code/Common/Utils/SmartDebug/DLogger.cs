using System.Collections.Generic;
using JetBrains.Annotations;

namespace Code.Common.Utils.SmartDebug
{
  [UsedImplicitly]
  public static class DLogger
  {
    private static readonly Dictionary<DSender, string> _cashedSenders = new Dictionary<DSender, string>();

    public static MessageBuilder Message(DSender sender) =>
      new MessageBuilder(GetSenderName(sender), sender);

    private static string GetSenderName(DSender sender)
    {
      if (!_cashedSenders.TryGetValue(sender, out string senderName))
      {
        senderName = sender.Name.Bold();
        _cashedSenders.Add(sender, senderName);
      }

      return senderName;
    }
  }
}