using System;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Context;
using Code.Infrastructure.Factories.Game;
using Code.Infrastructure.Services.GameCommander;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.LocalDi;

namespace Code.Infrastructure.Services.CleanUp
{
   public class CleanUpService : ICleanUpService 
   {
      private readonly InfrastructureContext _infrastructureContext;
      private readonly ILocalDiService _localDi;
      private readonly IAssetProvider _assets;
      private readonly CtxGameContext _ctxGameContext;
      private readonly IGameFactory _gameFactory;
      private readonly IGameCommanderService _gameCommander;

      public CleanUpService(InfrastructureContext infrastructureContext,
         ILocalDiService localDi,
         IAssetProvider assets, 
         IGameFactory gameFactory,
         IGameCommanderService gameCommander,
         CtxGameContext ctxGameContext)
      {
         _infrastructureContext = infrastructureContext;
         _localDi = localDi;
         _assets = assets;
         _gameFactory = gameFactory;
         _gameCommander = gameCommander;
         _ctxGameContext = ctxGameContext;
      }

      public void CleanUp()
      {
         DisposeGame();
         
         _infrastructureContext.CleanUp();
         _gameFactory.Cleanup();
         _localDi.CleanUp();
         _assets.Cleanup();
         _gameCommander.CleanUp();
      }

      public void DisposeGame()
      {
         foreach (IDisposable _system in _infrastructureContext.Disposables) 
            _system.Dispose();
      }
   }
}