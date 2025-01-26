using System;

namespace Code.Infrastructure.Services.GameCommander
{
   public interface IGameCommand : IDisposable
   {
     
      void Execute();
      // void SetCommand(); это все для создания листа или очереди команд должен лежать в конкретном интерфейсе команды
      // void StartCommand(); set+execute
      // void Execute(); можно при создании команды вызывать setcommand и внем же execute. Должен лежать в общем интерфейсе
      // Чтобы вызываться по общему интерфейсу после сета
      // Также можно execute выполнить отдельно в листе или очереди
   }
}