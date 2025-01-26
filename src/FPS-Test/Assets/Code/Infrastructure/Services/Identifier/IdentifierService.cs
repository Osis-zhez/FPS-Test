namespace Code.Infrastructure.Services.Identifier
{
  public class IdentifierService : IIdentifierService
  {
    private int _lastId = 1;

    public int Next()
    {
      return ++_lastId;
    }
  }
}