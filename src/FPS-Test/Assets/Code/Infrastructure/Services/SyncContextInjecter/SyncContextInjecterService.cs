using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.Services.SyncContextInjecter
{
   public class SyncContextInjecterService : IService
   {
      public SyncContextInjecterService()
      {
         Inject();
      }
      
      [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
      public void Inject()
      {
         SynchronizationContext.SetSynchronizationContext(new UniTaskSynchronizationContext());
      }
   }
}