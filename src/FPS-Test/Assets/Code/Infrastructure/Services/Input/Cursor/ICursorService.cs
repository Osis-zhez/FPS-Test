namespace Code.Infrastructure.Services.Input.Cursor
{
   public interface ICursorService
   {
      void LockCursor();
      void UnlockCursor();
   }
}